using Core.Models;
using Core.Rules.PhoneNumber.Rule;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Core.Rules.PhoneNumber
{
    public class PhoneNumberValue : ValueObject, IComparable<PhoneNumberValue>
    {
        public string Value { get; private set; }

        public PhoneNumberValue(string value)
        {
            CheckRule(new NotNullRule<string>(value));
            CheckRule(new PhoneNumberRule(value));
            Value = value;
        }

        #region Conversion

        public static implicit operator string(PhoneNumberValue value) => value.Value;

        public static implicit operator PhoneNumberValue(string value) => new PhoneNumberValue(value);

        #endregion

        public int CompareTo([AllowNull] PhoneNumberValue other)
        {
            return Value.CompareTo(other.Value);
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
