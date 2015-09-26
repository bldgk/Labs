using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyLabrary
{
    public class XOR : Cipher
    {
        public char[] Alphabet { get; set; }
        public int[] RNS { get; set; }
        public string Key { get; set; }
        public XOR()
        {

            Alphabet = new char[] { };
            RNS = new int[] { };
            Key = "";
        }
        public string Encryption(string text)
        {
            string EncryptedText = "";


            Key = GenerateRNS("qwerty", text.Length);
            int CharPosition = 0;
            foreach (char symbol in text)
            {


                EncryptedText += Alphabet[EncodingCharIndex(Array.IndexOf(Alphabet, symbol), Array.IndexOf(Alphabet, Key[CharPosition]))];
                CharPosition++;
            }


            return EncryptedText;
        }

        public int EncodingCharIndex(int CharIndex, int RNSIndex)
        {
            int NewCharIndex = 0;
            NewCharIndex = (CharIndex + RNSIndex) % Alphabet.Length;

            return NewCharIndex;
        }

        public string GenerateRNS(string gamma, int length)
        {
            string temp = "";
            int i = 0;
            int j = 0;
            while (i != length)
            {
                temp += gamma[j];
                j++;
                if (j == gamma.Length)
                    j = 0;

                i++;
            }
            return temp;
        }
        public string Decryption(string text)
        {
            string DecryptedText = "";


            Key = GenerateRNS("qwerty", text.Length);
            int CharPosition = 0;
            foreach (char symbol in text)
            {


                DecryptedText += Alphabet[DecodingCharIndex(Array.IndexOf(Alphabet, symbol), Array.IndexOf(Alphabet, Key[CharPosition]))];
                CharPosition++;
            }


            return DecryptedText;
        }
        public int DecodingCharIndex(int CharIndex, int RNSIndex)
        {
            int NewCharIndex = 0;
            NewCharIndex = (CharIndex - RNSIndex) % Alphabet.Length;
            if ((CharIndex - RNSIndex) < 0)
            {
                NewCharIndex = (Alphabet.Count() - Math.Abs((CharIndex - RNSIndex))) % Alphabet.Count();
            }
            else
            {
                NewCharIndex = ((CharIndex - RNSIndex)) % Alphabet.Count();
            }
            return NewCharIndex;
        }

    }
}
