
using Core.BussinesRule;

namespace Core.Models
{
    public class DomainObject
    {
        public void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }

    }
}
