using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyLabrary
{
    public class Tritemius:Cipher
    {
        public int Step { get; set; }
        public char[] Alphabet { get; set; }
        public Tritemius()
        {
            Step = 0;
            Alphabet = new char[] { };
        }
        public string Encryption(String text)
        {
            string EncryptedText = "";
            int CharPosition = 0;
            foreach (char symbol in text)
            {

                EncryptedText += Alphabet[EncodingCharIndex(Array.IndexOf(Alphabet, symbol), CharPosition + 1)];
                CharPosition++;
            }
            return EncryptedText;
        }
        public int EncryptStep(int CharPostition)
        {
            Step = 3 * CharPostition + 2;
            return Step;

        }
        public int EncodingCharIndex(int CharIndex, int CharPosition)
        {
            int NewCharIndex = 0;
            NewCharIndex = (CharIndex + EncryptStep(CharPosition)) % Alphabet.Count();

            return NewCharIndex;
        }
        public string Decryption(String text)
        {
            string DecryptedText = "";
            int CharPosition = 1;
            foreach (char symbol in text)
            {

                DecryptedText += Alphabet[DecodingCharIndex(Array.IndexOf(Alphabet, symbol), CharPosition)];
                CharPosition++;
            }
            return DecryptedText;
        }
        public int DecryptStep(int CharPostition)
        {
            Step = 3 * CharPostition + 2;
            return Step;

        }
        public int DecodingCharIndex(int CharIndex, int CharPosition)
        {
            int NewCharIndex = 0;
            if ((CharIndex - DecryptStep(CharPosition)) < 0)
            {
                NewCharIndex = (Alphabet.Count() - Math.Abs((CharIndex - DecryptStep(CharPosition)))) % Alphabet.Count();
            }
            else
            {
                NewCharIndex = ((CharIndex - DecryptStep(CharPosition))) % Alphabet.Count();
            }
            return NewCharIndex;
        }
    }
}
