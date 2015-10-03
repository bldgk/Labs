using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CryptographyLabrary
{
    public class Shtirliz : ICipher
    {
        public char[] Alphabet { get; set; }
        public char[,] AlphabetShtirliz { get; set; }
        public Shtirliz()
        {
            MakeAlphabetFile();
            Alphabet = new char[] { };
            AlphabetShtirliz = MakeAlphabet();
        }
        public string Encryption(string text)
        {
            bool isBreak = false;
            string EncryptedText = String.Empty;
            int m = (int)Math.Sqrt(AlphabetShtirliz.Length);
            foreach (char c in text)
            {
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        if (c == AlphabetShtirliz[i, j])
                        {
                            string k1 = String.Empty;
                            string k2 = String.Empty;
                            if ((i + 1).ToString().Length == 1)
                                k1 = "0";
                            if ((j + 1).ToString().Length == 1)
                                k2 = "0";
                            EncryptedText += k1 + (i + 1).ToString() + k2 + (j + 1).ToString();
                            isBreak = !isBreak;
                            break;
                        }
                    }
                    if (isBreak == true)
                    {
                        isBreak = false;
                        break;
                    }
                }
                EncryptedText += " ";
            }
            return EncryptedText;
        }
        public string Decryption(string text)
        {
            string DecryptedText = String.Empty;
            int m = (int)Math.Sqrt(AlphabetShtirliz.Length);
            List<string> chars = text.Split(' ').ToList();
            chars.Remove("");
            foreach (string c in chars)
            {
                DecryptedText += AlphabetShtirliz[Convert.ToUInt32(c.Remove(2, 2)) - 1, Convert.ToUInt32(c.Remove(0, 2)) - 1];
            }
            return DecryptedText;
        }
        public char[,] MakeAlphabet()
        {
            string text = String.Empty;
            char[,] TempAlphabet = new char[,] { };
            text = CryptographyLabrary.Properties.Resources.alphabet;
            int n = text.Length;
            int m = (int)Math.Sqrt(n);
            TempAlphabet = new char[m, m];
            int k = 0;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    try
                    {
                        TempAlphabet[i, j] = text[k];
                    }
                    catch
                    {
                        TempAlphabet[i, j] = ' ';
                    }
                    k++;
                }
            }
            return TempAlphabet;
        }
        public void MakeAlphabetFile()
        {
            string text = String.Empty;
            foreach (string word in CryptographyLabrary.Properties.Resources.alphabet.Split(',', '(', ')', '!', '@', '#', '$', '%', '^', '&', '*', '?', '-', '.'))
            {
                text += word;
            }
            using (StreamWriter file = new System.IO.StreamWriter(@"../../../CryptographyLabrary/alphabet.txt"))
            {
                file.WriteLine(text.ToLower());
            }
        }
    }
}