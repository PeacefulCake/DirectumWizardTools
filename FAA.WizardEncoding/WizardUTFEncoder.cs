using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAA.WizardEncoding
{
   public static class WizardUTFEncoder
    {
        private static int allowedLineLength = 64;
        private static string lineSeparator = " +\r\n";

        private static char quoteSeparator = '\'';
        private static char diez = '#';
        private static char[] decodedSeparators;

        static WizardUTFEncoder()
        {
            decodedSeparators = new char[] { '#', ' ', '+', '\r', '\n' };
        }

        public static string DecodeText(string str)
        {
            str = str.Trim();

            char fc = str[0];
            if (fc != quoteSeparator && fc != diez)
            {
                throw new ArgumentException("Передаваемая строка должна начинаться на решетку или кавычку");
            }

            StringBuilder resultSB = new StringBuilder();

            bool shouldConvert = true;
            string newItem;
            foreach (string item in str.Split(quoteSeparator)) // , StringSplitOptions.RemoveEmptyEntries
            {
                if (!string.IsNullOrEmpty(item))
                {
                    if (shouldConvert)
                        newItem = DecodeString(item);
                    else newItem = item;

                    resultSB.Append(newItem);
                }
                
                shouldConvert = !shouldConvert;
            }
            return resultSB.ToString();
        }

        private static string DecodeString(string str)
        {
            StringBuilder resultSB = new StringBuilder();
            char c;
            foreach (string item in str.Split(decodedSeparators, StringSplitOptions.RemoveEmptyEntries))
            {
                c = Convert.ToChar(Convert.ToInt32(item));
                resultSB.Append(c);
            }
            return resultSB.ToString();
        }

        public static string EncodeText(string str, string indent = "")
        {
            StringBuilder resultSB = new StringBuilder();
            bool lined = str.Length > allowedLineLength;
            int charCounter = 0;
            bool qouteStarted = false;
            foreach (char c in str.ToCharArray())
            {
                if (lined && charCounter == 0)
                {
                    resultSB.Append(indent);
                }

                if (isCharAllowed(c))
                {
                    if (!qouteStarted)
                    {
                        resultSB.Append(quoteSeparator);
                        qouteStarted = true;
                    }
                    resultSB.Append(c);
                }
                else
                {
                    if (qouteStarted)
                    {
                        resultSB.Append(quoteSeparator);
                        qouteStarted = false;
                    }
                    resultSB.Append(EncodeChar(c));
                }

                if(lined) charCounter++;
                if (lined && charCounter == allowedLineLength)
                {
                    if (qouteStarted)
                    {
                        resultSB.Append(quoteSeparator);
                        qouteStarted = false;
                    }
                    resultSB.Append(lineSeparator);
                    charCounter = 0;
                }
            }

            if(qouteStarted) resultSB.Append(quoteSeparator);
            return resultSB.ToString();
        }

        private static string EncodeChar(char c)
        {
            return string.Format("#{0}", Convert.ToInt32(c));
        }

        private static bool isCharAllowed(char c)
        {
            int charCode = Convert.ToInt32(c);
            return !(charCode < 32 || charCode == 39 || charCode > 126);
        }
/*
        public static IEnumerable<string> SplitForLines(string str)
        {
            var sList = new List<string>();
            int index = 0;

            string tStr;

            while (allowedLineLength * (index + 1) < str.Length)
            {
                tStr = str.Substring(index * allowedLineLength, allowedLineLength);
                sList.Add(tStr + lineSeparator);
                index++;
            }
            sList.Add(str.Substring(index * allowedLineLength, str.Length - index * allowedLineLength));

            return sList;
        }
*/
    }
}
