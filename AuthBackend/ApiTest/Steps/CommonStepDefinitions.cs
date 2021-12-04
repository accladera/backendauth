using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using System;
using Microsoft.Extensions.DependencyInjection;
using Data.Database;
using Microsoft.EntityFrameworkCore;
using AuthBackend;

namespace SystemCore.Spec.StepDefinitions
{
    [Binding]
    class CommonStepDefinitions
    {
        private readonly ScenarioContext _context;
        private readonly WebApplicationFactory<Startup> _factory;

        public CommonStepDefinitions(
            ScenarioContext scenarioContext,
            WebApplicationFactory<Startup> webApplicationFactory)
        {
            _context = scenarioContext;
            _factory = webApplicationFactory.WithWebHostBuilder(builder =>
            {
                var projectDir = Directory.GetCurrentDirectory();
                var configPath = Path.Combine(projectDir, "appsettings.json");
                builder.ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile(configPath);
                });
                builder.ConfigureServices(services =>
                {
                    services.AddDbContext<DataBaseContext>(options =>
                    {
                        options.UseInMemoryDatabase(_context.Get<string>("DB_Key"));
                    });
                });
            });
        }


        [Given(@"la siguiente solicitud")]
        public void GivenIHaveTheFollowingRequestBody(string multilineText)
        {
            _context.Add("body", multilineText);
        }

        [When(@"se solicita ""(.*)"" credenciales que se procese a la url ""(.*)"", usando el metodo ""(.*)""")]
        public async Task WhenIPostTheRequestToTheService(string withCredentials, string url, string method)
        {
            HttpMethod httpMethod;
            var useCredentials = withCredentials == "con";

            switch (method) {
                case "get":
                    httpMethod = HttpMethod.Get;
                    break;
                case "post":
                    httpMethod = HttpMethod.Post;
                    break;
                case "put":
                    httpMethod = HttpMethod.Put;
                    break;
                case "delete":
                    httpMethod = HttpMethod.Delete;
                    break;
                default:
                    httpMethod = HttpMethod.Get;
                    break;
            }

            var regex = new Regex(@"\{\w+\}");
            var tmp = url;
            var replaced = new char[] {'{', '}'};
            foreach (Match match in regex.Matches(tmp))
            {
                var key = match.Value.Trim(replaced);
                url = url.Replace(match.Value, _context.Get<string>(key));
            }

            var requestBody = _context.Get<string>("body");
            var request = new HttpRequestMessage(httpMethod, url)
            {
                Content = new StringContent(requestBody)
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/json")
                    }
                }
            };
            request.Headers.Authorization = useCredentials ? new AuthenticationHeaderValue(_context.Get<string>("Token")) : null;

            var client = _factory.CreateClient();

            var response = await client.SendAsync(request).ConfigureAwait(false);
            try
            {
                _context.Set(response.StatusCode, "ResponseStatusCode");
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                Console.Error.WriteLine(responseBody);
                _context.Set(responseBody, "ResponseBody");
            }
            finally
            {
            }
        }

        [Then(@"la respuesta debe tener el codigo de estado (.+)")]
        public void ThenTheResultShouldBeResponse(int statusCode)
        {
            var currentStatus = (int)_context.Get<HttpStatusCode>("ResponseStatusCode");
            if (currentStatus == 400)
            {
                Console.WriteLine(_context.Get<string>("ResponseBody"));
            }
            Assert.Equal(statusCode, currentStatus);
        }

        [Given(@"inicio de sesion del usuario con las siguientes credenciales usuario ""(.*)"", password ""(.*)""")]
        public async Task GivenInicioDeSesionDelUsuarioConLasSiguientesCredencialesUsuarioPassword(string username, string password)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/")
            {
                Content = new StringContent("{\"username\": \"" + username + "\", \"password\": \"" + password + "\"}")
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/json")
                    }
                }
            };

            var client = _factory.CreateClient();

            var response = await client.SendAsync(request).ConfigureAwait(false);
            try
            {
                _context.Set(response.StatusCode, "ResponseStatusCode");
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var responseParsed = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(responseBody);
                _context.Set(responseParsed["token"], "Token");
            }
            finally
            {
            }
        }

        [Then(@"el resultado debe contener la informacion")]
        public void ThenElResultadoDebeContenerLaInformacion(string multilineText)
        {
            var json = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(multilineText);
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(_context.Get<string>("ResponseBody"));
            foreach (KeyValuePair<string, object> entry in json)
            {
                Assert.Equal(response[entry.Key], entry.Value);
            }
        }

        [Then(@"la respuesta ""(.*)"" contener un listado vacio")]
        public void ThenTheResultShouldBeListEmpty(string shouldBe)
        {
            if (shouldBe.StartsWith("no"))
            {
                Assert.NotEqual("[]", _context.Get<string>("ResponseBody"));
            }
            else 
            {
                Assert.Equal("[]", _context.Get<string>("ResponseBody"));
            }
        }

        [Then(@"la respuesta debe contener un objeto con estos campos definidos")]
        public void ThenLaRespuestaDebeContenerUnObjetoConEstosCamposDefinidos(string data)
        {
            var json = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(data);
            var respuesta = _context.Get<string>("ResponseBody");

            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(_context.Get<string>("ResponseBody"));
            foreach (JProperty record in json.Properties())
            {
                if (record.Value is JObject )
                {
                    var child = response[record.Name];
                    foreach (JProperty record1 in record.Value)
                    {
                        if (!(record1.Value is JObject))
                        {
                            Assert.True(record1.Value == child[record1.Name].Value<dynamic>(), record1.Name);
                        }
                    }
                }
                else 
                {
                    Assert.True(record.Value.Value<dynamic>() == response[record.Name].Value<dynamic>(), record.Name);
                }
            }
        }

        [Then(@"la respuesta debe contener un error validacion con estos campos definidos")]
        public void ThenLaRespuestaDebeContenerUnErrrorValidacionConEstosCamposDefinidos(string data)
        {
            var json = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(data);
            var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(_context.Get<string>("ResponseBody"));

           
            Assert.True(json == respuesta, respuesta);
                      
            
        }

        [Then(@"imprimir la respuesta")]
        public void AndPrintTheResponse()
        {
            var respuesta = _context.Get<string>("ResponseBody");
            Console.WriteLine($"Respuesta => {_context.Get<string>("ResponseBody")}");
        }


    }
}
