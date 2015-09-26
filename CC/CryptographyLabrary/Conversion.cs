using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyLabrary
{
    public class Conversion
    {

        public static String FromIntegerToBinary(int IntegerNumber)
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

        public static String FromTextToHex(String text)
        {
            String HexString = "";

            foreach (char word in text)
            {
                HexString += String.Format("{0:X}", Convert.ToInt32(word));
            }

            return HexString;
        }

        public static String FromHexToBinary(String HexString)
        {
            String BinaryString = "";

            try
            {
                for (Int32 i = 0; i < HexString.Length; i++)
                {
                    Int32 Hex = Convert.ToInt32(HexString[i].ToString(), 16);

                    Int32 Factorial = 8;

                    for (Int32 j = 0; j < 4; j++)
                    {
                        if (Hex >= Factorial)
                        {
                            Hex -= Factorial;
                            BinaryString += "1";
                        }
                        else
                        {
                            BinaryString += "0";
                        }
                        Factorial /= 2;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + " - wrong hexa integer format.");
            }

            return BinaryString;
        }
        public static String FromTextToBinary(String text)
        {
            return FromHexToBinary(FromTextToHex(text));
        }
        public static String FromBinaryToText(String BinaryText)
        {
            StringBuilder text = new StringBuilder(BinaryText.Length / 8);

            for (int i = 0; i < (BinaryText.Length / 8); i++)
            {
                String word = BinaryText.Substring(i * 8, 8);
                text.Append((char)Convert.ToInt32(word, 2));
                //text += (char)Convert.ToInt32(word, 16);
            }

            return text.ToString();
        }
    }
}