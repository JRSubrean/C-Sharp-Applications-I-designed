using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public static class NumberAssigner
    {
        static int theNextNumber = 0;

        public static int getNextNumber()
        {
            theNextNumber = (theNextNumber + 1);
            return theNextNumber;
        }
    }
}