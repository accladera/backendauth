using Core.Rules.WebSite.Rule;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Rules.WebSite
{
    public class WebSiteValue : ValueObject, IComparable<WebSiteValue>
    {
        public string Value { get; private set; }

        public WebSiteValue(string value)
        {
            CheckRule(new NotNullRule<string>(value));
            CheckRule(new WebSiteUrlRule(value));
            Value = value;
        }

        #region Conversion

        public static implicit operator string(WebSiteValue value) => value.Value;

        public static implicit operator WebSiteValue(string value) => new WebSiteValue(value);

        #endregion

        public int CompareTo([AllowNull] WebSiteValue other)
        {
            return Value.CompareTo(other.Value);
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
