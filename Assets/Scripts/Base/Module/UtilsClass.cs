using System;
using UnityEngine;
using System.Linq;

namespace Base.Module
{
    public static class UtilsClass
    {
        public static string FormatMoney(int money, int decPlace = 2)
        {
            string result = String.Empty;
            float place = Mathf.Pow(10f, decPlace);

            string[] abbrev = {"K", "M", "B", "T"};
            string str = (money < 0) ? "-" : "";
            float size;

            money = Mathf.Abs(money);

            for (int i = abbrev.Length - 1; i >= 0; --i)
            {
                size = Mathf.Pow(10, (i + 1) * 3);
                if (size <= money)
                {
                    money = (int) (Mathf.Floor(money * place / size) / place);
                    if ((money == 1000) && (i < abbrev.Length - 1))
                    {
                        money = 1;
                        i += 1;
                    }

                    result = money + abbrev[i];
                    break;
                }
            }

            return str + result;
        }
        /// <summary>
        /// Convert money string with format 1.xxx.xxx or 1,xxx,xxx to Float
        /// </summary>
        /// <param name="moneyStr"></param>
        /// <returns>money in float</returns>
        public static float ConvertMoneyToNumber(string moneyStr)
        {
            var resultStr = moneyStr.Split('.');
            if (resultStr.Length <= 1)
            {
                resultStr = moneyStr.Split(',');
            }
            string joinString = string.Join("", resultStr);
            return float.Parse(joinString);
        }
        
        
    }
}