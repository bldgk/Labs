﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyLabrary
{
    public class Tritemius : Cipher
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
        public int EncryptStep(int CharPostition) => 3 * CharPostition + 2;

        public int EncodingCharIndex(int CharIndex, int CharPosition) => (CharIndex + EncryptStep(CharPosition)) % Alphabet.Count();


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
        public int DecryptStep(int CharPostition) => 3 * CharPostition + 2;

        public int DecodingCharIndex(int CharIndex, int CharPosition) => ((CharIndex - DecryptStep(CharPosition)) < 0) ? (Alphabet.Count() - Math.Abs((CharIndex - DecryptStep(CharPosition)))) % Alphabet.Count() : ((CharIndex - DecryptStep(CharPosition))) % Alphabet.Count();

    }
}
