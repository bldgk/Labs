using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryptographyLabrary
{
    public class MyDES : ICipher
    {
        public char[] Alphabet { get; set; }
        public int BinaryTextLength { get; set; }
        public MyDES()
        {
            Alphabet = new char[] { };
            BinaryTextLength = 0;
        }
        public string Encryption(string text)
        {
            string HexKey = Conversion.FromTextToHex("0123456789ABCDEF");
            string BinaryKey = Conversion.FromHexToBinary(HexKey);
            string KeyPermutation = DoPermutation(BinaryKey, DESData.PermutationKey);
            string C0 = "", D0 = "";
            C0 = SetLeftHalvesKey(KeyPermutation);
            D0 = SetRightHalvesKey(KeyPermutation);
            Keys keys = SetAllKeys(C0, D0);
            List<string> BinaryTextBy64Bit = new List<string>();
            StringBuilder EncryptedText = new StringBuilder();
            string CharIndexesInText = String.Empty;
            foreach (char Char in text)
            {
                CharIndexesInText += Array.IndexOf(Alphabet, Char);
            }
            string CharIndexesInTextInBinaryFormat = Conversion.FromTextToBinary(CharIndexesInText);
            BinaryTextLength = CharIndexesInTextInBinaryFormat.Length;
            if ((CharIndexesInTextInBinaryFormat.Length % 64) != 0)
            {
                int maxLength = 0;
                maxLength = ((CharIndexesInTextInBinaryFormat.Length / 64) + 1) * 64;
                CharIndexesInTextInBinaryFormat = CharIndexesInTextInBinaryFormat.PadRight(maxLength, '0');
            }
            BinaryTextBy64Bit = CharIndexesInTextInBinaryFormat.SplitInParts(64).ToList();
            foreach (string Part64 in BinaryTextBy64Bit)
            {
                for (int i = 0; i < (Part64.Length / 64); i++)
                {
                    string PermutatedText = DoPermutation(Part64.Substring(i * 64, 64), DESData.InitialPermutation);
                    string L0 = "", R0 = "";
                    L0 = SetLeftHalvesKey(PermutatedText);
                    R0 = SetRightHalvesKey(PermutatedText);
                    EncryptedText.Append(DoCycles(L0, R0, keys, false));
                }
            }
            return EncryptedText.ToString();
        }
        public string Decryption(string BinaryText)
        {
            string HexKey = Conversion.FromTextToHex("0123456789ABCDEF");
            string BinaryKey = Conversion.FromHexToBinary(HexKey);
            string KeyPermutation = DoPermutation(BinaryKey, DESData.PermutationKey);
            string C0 = "", D0 = "";
            C0 = SetLeftHalvesKey(KeyPermutation);
            D0 = SetRightHalvesKey(KeyPermutation);
            Keys keys = SetAllKeys(C0, D0);
            StringBuilder DecryptedText = new StringBuilder();
            string PermutatedText = String.Empty;
            string DecryptedBinaryText = String.Empty;
            for (int i = 0; i < (BinaryText.Length / 64); i++)
            {
                PermutatedText = DoPermutation(BinaryText.Substring(i * 64, 64), DESData.InitialPermutation);
                string L0 = "", R0 = "";
                L0 = SetLeftHalvesKey(PermutatedText);
                R0 = SetRightHalvesKey(PermutatedText);
                DecryptedBinaryText += DoCycles(L0, R0, keys, true);
            }
            DecryptedBinaryText = DecryptedBinaryText.Remove(DecryptedBinaryText.Length - (DecryptedBinaryText.Length - BinaryTextLength));
            string DecryptedCharIndexes = Conversion.FromBinaryToText(DecryptedBinaryText);
            return DecryptedText.Append(Conversion.FromBinaryToText(DecryptedBinaryText)).ToString();

        }
        public string DoCycles(string L0, string R0, Keys keys, bool IsReverse)
        {
            string Ln = "", Rn = "", Ln_1 = L0, Rn_1 = R0;
            int i = 0;
            if (IsReverse == true)
            {
                i = 15;
            }
            while (IsEnough(i, IsReverse))
            {
                Ln = Rn_1;
                Rn = XOR(Ln_1, F(Rn_1, keys.Kn[i]));
                Ln_1 = Ln;
                Rn_1 = Rn;
                if (IsReverse == false)
                {
                    i += 1;
                }
                else
                {
                    i -= 1;
                }
            }
            string R16L16 = Rn + Ln;
            string EncryptedBinaryText = DoPermutation(R16L16, DESData.FinalPermutation);
            return EncryptedBinaryText;
        }
        public string F(string Rn_1, string Kn)
        {
            string E_Rn_1 = E_Selection(Rn_1);
            string XOR_Rn_1_Kn = XOR(E_Rn_1, Kn);
            string sBoxedText = S_BlockPermutation(XOR_Rn_1_Kn);
            string P_sBoxedText = P(sBoxedText);
            return P_sBoxedText;
        }
        public string P(string text) => DoPermutation(text, DESData.PermutationP_blocks);
        public string S_BlockPermutation(string text)
        {
            StringBuilder TransformedText = new StringBuilder(32);
            for (int i = 0; i < 8; i++)
            {
                string temp = text.Substring(i * 6, 6);
                TransformedText.Append(DoPermutation(temp, DESData.S_Blocks[i]));
            }

            return TransformedText.ToString();
        }
        public string E_Selection(string Rn_1) => DoPermutation(Rn_1, DESData.PermutationKeyExtension);
        public string XOR(string text1, string text2)
        {
            if (text1.Length != text2.Length)
            {
                Console.WriteLine("Two data blocks for XOR are must get same size.");
                return null;
            }
            StringBuilder XORed_Text = new StringBuilder(text1.Length);
            for (int i = 0; i < text1.Length; i++)
            {
                if (text1[i] != text2[i])
                {
                    XORed_Text.Append("1");
                }
                else
                {
                    XORed_Text.Append("0");
                }
            }

            return XORed_Text.ToString();
        }
        public bool IsEnough(int i, bool IsReverse) => (IsReverse == false) ? i < 16 : i >= 0;
        public string DoPermutation(string text, int[,] order)
        {
            string PermutatedText = "";
            int rowIndex = Convert.ToInt32(text[0].ToString() + text[text.Length - 1].ToString(), 2);
            int colIndex = Convert.ToInt32(text.Substring(1, 4), 2);
            PermutatedText = Conversion.FromIntegerToBinary(order[rowIndex, colIndex]);
            return PermutatedText;
        }
        public string DoPermutation(string text, int[] order)
        {
            StringBuilder PermutatedText = new StringBuilder(order.Length);
            for (int i = 0; i < order.Length; i++)
            {
                PermutatedText.Append(text[order[i] - 1]);
            }
            return PermutatedText.ToString();
        }
        public string SetLeftHalvesKey(string text) => SetHalvesKey(true, text);
        public string SetRightHalvesKey(string text) => SetHalvesKey(false, text);
        public string SetHalvesKey(bool IsLeft, string text)
        {
            if ((text.Length % 8) != 0)
            {
                Console.WriteLine("The key is not multiple of 8bit.");
                return null;
            }
            int midindex = (text.Length / 2) - 1;
            string result = "";
            if (IsLeft)
            {
                result = text.Substring(0, midindex + 1);
            }
            else
            {
                result = text.Substring(midindex + 1);
            }
            return result;
        }
        public Keys SetAllKeys(string C0, string D0)
        {
            Keys keys = new Keys();
            keys.Cn[0] = C0;
            keys.Dn[0] = D0;
            for (int i = 1; i < keys.Cn.Length; i++)
            {
                keys.Cn[i] = LeftShift(keys.Cn[i - 1], DESData.Shifts[i]);
                keys.Dn[i] = LeftShift(keys.Dn[i - 1], DESData.Shifts[i]);
                keys.Kn[i - 1] = DoPermutation(keys.Cn[i] + keys.Dn[i], DESData.PermutationKeyCompression);
            }
            return keys;
        }
        public string LeftShift(string text) => LeftShift(text, 1);
        public string LeftShift(string text, int count)
        {
            if (count < 1)
            {
                Console.WriteLine("The count of leftshift is must more than 1 time.");
                return null;
            }
            string temp = text.Substring(0, count);
            StringBuilder shifted = new StringBuilder(text.Length);
            shifted.Append(text.Substring(count) + temp);
            return shifted.ToString();
        }
    }
}