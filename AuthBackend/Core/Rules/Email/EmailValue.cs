using Core.Models;
using Core.Rules.Email.Rules;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Core.Rules.Email
{
    public class EmailValue : ValueObject, IComparable<EmailValue>
    {
        public string Value { get; }

        public EmailValue(string value)
        {
            CheckRule(new NotNullRule<string>(value));
            CheckRule(new FormatEmailRule(value));
            Value = value;
        }

        #region Conversion

        public static implicit operator string(EmailValue value) => value.Value;

        public static implicit operator EmailValue(string value) => new EmailValue(value);

        #endregion

        public int CompareTo([AllowNull] EmailValue other)
        {
            return Value.CompareTo(other.Value);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
