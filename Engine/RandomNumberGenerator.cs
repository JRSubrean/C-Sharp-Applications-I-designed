using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Engine
{
    public static class RandomNumberGenerator
    {
        private static readonly RNGCryptoServiceProvider nGenerator = new RNGCryptoServiceProvider();

        public static int NumberBetween(int minValue, int maxValue)
        {
            byte[] randomNumber = new byte[1];

            nGenerator.GetBytes(randomNumber);

            double asciiValueOfRandomCharacter = Convert.ToDouble(randomNumber[0]);

            double multiplier = Math.Max(0, (asciiValueOfRandomCharacter / 255d) - 0.00000000001d);
            /*Math.Max is used here, and we're also subtracting 0.00000000001
             to ensure the "multiplier" will always be between 0.0 and
             .9999999999. Otherwise, it's possible for it to be "1", which
             causes problems in our rounding.*/

            int range = maxValue - minValue + 1;
            /*We need to add 1 to the range, to allow for the rounding done
             with Math.Floor*/

            double randomValueInRange = Math.Floor(multiplier * range);

            return (int)(minValue + randomValueInRange);
        }

    }
}
