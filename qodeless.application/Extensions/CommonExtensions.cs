using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace qodeless.application.Extensions
{
    public static class CommonExtensions
    {
        public static string FormatCode(this int code)
        {
            switch (code.ToString().Length)
            {
                case 1:
                    return $"00000{code}";
                case 2:
                    return $"0000{code}";
                case 3:
                    return $"000{code}";
                case 4:
                    return $"00{code}";
                case 5:
                    return $"0{code}";
                case 6:
                    return $"{code}";
                default:
                    return string.Empty;
            }
        }
    }
}
