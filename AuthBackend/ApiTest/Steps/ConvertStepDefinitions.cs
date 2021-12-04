using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using System;
using Data.Database;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SystemCore.Spec.StepDefinitions
{
    [Binding]
    class ConvertStepDefinitions
    {
        private readonly ScenarioContext _context;

        public ConvertStepDefinitions(ScenarioContext scenarioContext)
        {
            _context = scenarioContext;
        }

        [Given(@"la siguiente entidad ""(.*)"" registrada")]

        public void GivenLaSiguienteEntidadRegistrada(string entityName, string data) 
        {       
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(_context.Get<string>("DB_Key"))
                .Options;

            using  (var context = new DataBaseContext(options))
            {
                switch (entityName)
                {
                    case "authclient":
                        StoreAuthClient(context, data);
                        break;
                }
            }   
        }

        private void StoreAuthClient(DataBaseContext db, string data)
        {
            var parse = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(data);
            db.Users.Add(parse);
            db.SaveChanges();
            _context.Set(parse.Id.ToString(), "Id");
        }

    }
}
