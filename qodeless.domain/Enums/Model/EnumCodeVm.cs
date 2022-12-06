using System;

namespace qodeless.domain.Enums.Model
{    public class EnumCodeVm
    {
        public EnumCodeVm(string text, string value)
        {
            Value = value;
            Text = text;
        }

        public EnumCodeVm(string text, Guid valueId)
        {
            Value = valueId.ToString();
            Text = text;
        }

        public string Value { get; private set; }
        public string Text { get; private set; }
    }
}
