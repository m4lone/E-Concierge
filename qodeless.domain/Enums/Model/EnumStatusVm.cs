using System;

namespace qodeless.domain.Enums.Model
{    public class EnumStatusVm
    {
        public EnumStatusVm(string text, string value)
        {
            Value = value;
            Text = text;
        }

        public EnumStatusVm(string text, Guid valueId)
        {
            Value = valueId.ToString();
            Text = text;
        }

        public string Value { get; private set; }
        public string Text { get; private set; }
    }
}
