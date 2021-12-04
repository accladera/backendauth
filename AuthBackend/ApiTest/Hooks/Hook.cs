using System;
using TechTalk.SpecFlow;

namespace System.Spec.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext _context;

        public Hooks(ScenarioContext context)
        {
            _context = context;
        }

        [BeforeScenario]
        public void SetupMemoryContext()
        {
            var key = Guid.NewGuid().ToString();
            _context.Set(key, "DB_Key");
        }       
    }
}
