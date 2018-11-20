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

        public static List<Int32> loadDefaultScores() {
            List<Int32> scores = new List<Int32>();

            for (Int32 i = 10; i >= 1; i--) {
                scores.Add(i * 1000);
            }

            return scores;
        }


    }
}
