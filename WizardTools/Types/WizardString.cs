using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FAA.WizardEncoding;
using WizardTools.Utils;

namespace WizardTools.Types
{
    class WizardString : IStringable
    {
        private int indentLevel;

        private string encodedValue;
        private string decodedValue;

        public string EncodedValue
        {
            get
            {
                return encodedValue;
            }
            set
            {
                encodedValue = value;
                Decode();
            }
        }

        public string DecodedValue
        {
            get
            {
                return decodedValue;
            }
            set
            {
                decodedValue = value;
                Encode();
            }
        }

        public WizardString(string encStr)
        {
            EncodedValue = encStr;
        }
        public WizardString()
        {
            encodedValue = "";
            decodedValue = "";
        }

        private void Decode()
        {
            decodedValue = WizardUTFEncoder.DecodeText(encodedValue);
        }

        private void Encode()
        {
            Encode(indentLevel);
        }
        private void Encode(int indentLevel)
        {
            string indents = new string(' ', (indentLevel + 1) * 2); // Т.к. передается уровень вложения элемента, многострочный элемент отрисуется еще с одним отступом
            encodedValue = WizardUTFEncoder.EncodeText(decodedValue, indents);
        }

        public string ToStructuredString(int indentLevel)
        {
            Encode(indentLevel);
            if(EncodedValue.IndexOf(Const.CR) > -1) return Const.CR + EncodedValue;
            else return EncodedValue;
        }

        public void LoadFromStringList(List<String> data)
        {
            EncodedValue = string.Join(Const.CR, data);
        }
    }
}
