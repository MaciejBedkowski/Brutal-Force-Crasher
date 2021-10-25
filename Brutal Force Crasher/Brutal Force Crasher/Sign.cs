using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brutal_Force_Crasher
{
    public class Sign
    {
        private string passwordSigns = "qwertyuiopasdfghjklzxcvbnm0123456789*";
        public string sign = "q";
        public int positionSign = 1;

        public void nextSign()
        {
            sign = passwordSigns[positionSign].ToString();
            positionSign++;
        }

        public void resetSign()
        {
            sign = "q";
            positionSign = 1;
        }

        public bool ifIsTheLastSign()
        {
            if(sign == "9")
                return true;
            return false;
        }
    }
}
