using Core.BussinesRule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.Rules.PersonName.Rules
{
    public class OnlyLettersRule : IBusinessRule
    {
        private readonly string _value;

        public OnlyLettersRule(string value)
        {
            _value = value;
        }

        public string Message => "El nombre no puede contener puede contener números";

        public bool IsBroken()
        {
            return Regex.IsMatch(_value, "\\d");
        }
    }
}