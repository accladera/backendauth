
using Core.BussinesRule;
using Core.Models;
using Core.Rules;
using Core.Rules.PersonName.Rules;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Core.Rules.PersonName
{
    public class PersonNameValue : ValueObject, IComparable<PersonNameValue>
    {
        public string Value { get; private set; }

        public PersonNameValue(string value)
        {
            CheckRule(new NotNullRule<string>(value));
            CheckRule(new OnlyLettersRule(value));
            CheckRule(new NameLengtRule(value));

            Value = value;
        }


        #region Conversion

        public static implicit operator string(PersonNameValue value) => value.Value;

        public static implicit operator PersonNameValue(string value) => new PersonNameValue(value);

        #endregion

        public int CompareTo([AllowNull] PersonNameValue other)
        {
            return Value.CompareTo(other.Value);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        
    }
}
