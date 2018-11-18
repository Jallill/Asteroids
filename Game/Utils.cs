using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEngine {
    static class Utils {
            public static string Truncate(this string value, int maxLength) {
                if (string.IsNullOrEmpty(value)) return value;
                return value.Length <= maxLength ? value : value.Substring(value.Length - maxLength);
        }
    }
}
