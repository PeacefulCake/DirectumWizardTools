using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FAA.WizardEncoding;
using FAA.Utils;

namespace FAA.WizardTools.Types
{
    class WizardString
    {
        private const int innerIndents = 2;

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
            string indents = new string(' ', innerIndents);
            encodedValue = WizardUTFEncoder.EncodeText(decodedValue, indents);
        }

        public List<string> RawData
        {
            get { 
                List<string> result = new List<string>(StringUtils.GetListFromText(EncodedValue));
                if (result.Count > 1)
                {
                    result.Insert(0, "");
                }
                return result;
            }
        }

        public void LoadFromDataList(List<String> data)
        {
            EncodedValue = string.Join(WSConstants.CR, data);
        }

        public void PowerLoad(string value, List<string> data, int index)
        {
            if (value == "") this.LoadFromDataList(StringUtils.PickWizardString(data, index + 1));
            else this.EncodedValue = value;
        }
    }
}
