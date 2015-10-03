using System;
using System.Text;

namespace CryptographyLabrary
{
    public class Conversion
    {

        public static string FromIntegerToBinary(int IntegerNumber)
        {
            StringBuilder BinaryNumberStr = new StringBuilder();
            int BinaryNumber = IntegerNumber;
            int Factorial = 128;
            for (int i = 0; i < 8; i++)
            {
                if (BinaryNumber >= Factorial)
                {
                    BinaryNumber -= Factorial;
                    BinaryNumberStr.Append("1");
                }
                else
                {
                    BinaryNumberStr.Append("0");
                }
                Factorial /= 2;
            }
            return BinaryNumberStr.ToString();
        }
        public static string FromTextToHex(string text)
        {
            string HexString = "";
            foreach (char word in text)
            {
                HexString += String.Format("{0:X}", Convert.ToInt32(word));
            }
            return HexString;
        }
        public static string FromHexToBinary(string HexString)
        {
            string BinaryString = "";
            try
            {
                for (int i = 0; i < HexString.Length; i++)
                {
                    int Hex = Convert.ToInt32(HexString[i].ToString(), 16);
                    int Factor = 8;
                    for (int j = 0; j < 4; j++)
                    {
                        if (Hex >= Factor)
                        {
                            Hex -= Factor;
                            BinaryString += "1";
                        }
                        else
                        {
                            BinaryString += "0";
                        }
                        Factor /= 2;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + " - wrong hexa integer format.");
            }
            return BinaryString;
        }
        public static string FromTextToBinary(string text) => FromHexToBinary(FromTextToHex(text));
        public static string FromBinaryToText(string BinaryText)
        {
            StringBuilder text = new StringBuilder(BinaryText.Length / 8);
            for (int i = 0; i < (BinaryText.Length / 8); i++)
            {
                string word = BinaryText.Substring(i * 8, 8);
                text.Append((char)Convert.ToInt32(word, 2));
            }
            return text.ToString();
        }
    }
}