using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFPL.Chapter3
{
    internal class BasicClassSintaxClass
    {
        public string publicField;

        private string privateField;

        public const string CONSTANT_FIELD = "I am constantField.";

        public string GetPrivateField() => privateField;

        public void SetPrivateField(string NewPrivateFieldValue)
        {
            privateField = NewPrivateFieldValue;
        }

        public BasicClassSintaxClass()
        {
            publicField = "I am publicField.";
            privateField = "I am privateField.";
        }

        public BasicClassSintaxClass(string publicField = "I am publicField.", string privateField = "I am privateField.")
        {
            this.publicField = publicField;
            this.privateField = privateField;
        }

        public void Print()
        {
            Console.WriteLine($"{publicField}\n{privateField}\n{CONSTANT_FIELD}");
        }
    }
}
