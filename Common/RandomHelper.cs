using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class RandomHelper
    {
         /// <summary>
        ///生成制定位数的随机码（数字）
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateRandomCode(int length)
        {
            var result = new StringBuilder();
            result.Append("EPS_");
            for (var i = 0; i<length; i++)
            {
                var r = new Random(Guid.NewGuid().GetHashCode());
                result.Append(r.Next(0, 10));
            }
            return result.ToString();
        }

    }
}
