using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.BussinesRule
{
    [Serializable]
    public class BusinessRuleValidationException : Exception
    {
        public IBusinessRule BrokenRule { get; set; }

        public string Details { get; set; }

        public BusinessRuleValidationException(IBusinessRule brokenRule) : base(brokenRule.Message)
        {
            BrokenRule = brokenRule;
            Details = brokenRule.Message;
        }

        protected BusinessRuleValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        public override string ToString()
        {
            return $"{ BrokenRule.GetType().FullName }: { Details }";
        }
    }
}
