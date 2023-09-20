using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vyvojovy_pomocnik.Classes
{
    class Convertor
    {
        public static DateTime ConvertToDate(string vstup)
        {
            string year = "";
            string day = "";
            string month = "";
            int index = 0;

            foreach (var c in vstup)
            {
                if (c != '.' && index == 0)
                {
                    day += c;
                }

                if (c != '.' && index == 1)
                {
                    month += c;
                }

                if (c != '.' && index == 2)
                {
                    year += c;
                }

                if (c == '.')
                {
                    index++;
                }
            }

            int year1 = Convert.ToInt32(year);
            int month1 = Convert.ToInt32(month);
            int day1 = Convert.ToInt32(day);

            DateTime date = new DateTime(year1, month1, day1);

            return date;
        }

        public static int ConvertToInt(string vstup)
        {
            string vyst = "";
            bool konec = true;
            foreach (var item in vstup)
            {
                if (item != '.' && konec)
                {
                    vyst += item;
                }
                else
                {
                    konec = false;
                }
            }

            int id_pro = 0;

            if (vyst.Length > 0)
            {
                id_pro = Int32.Parse(vyst);
            }

            return id_pro;
        }
    }
}
