using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyLabrary
{
    public class MyDES:Cipher
    {
        public char[] Alphabet { get; set; }
        public Int32 BinaryTextLength { get; set; }
        public MyDES()
        {
            Alphabet = new char[] { };
            BinaryTextLength = 0;
        }

        public String Encryption(String text)
        {

            String HexKey = Conversion.FromTextToHex("0123456789ABCDEF");
            String BinaryKey = Conversion.FromHexToBinary(HexKey);
            String KeyPermutation = this.DoPermutation(BinaryKey, DESData.PermutationKey);

            String C0 = "", D0 = "";

            C0 = this.SetLeftHalvesKey(KeyPermutation);
            D0 = this.SetRightHalvesKey(KeyPermutation);

            Keys keys = this.SetAllKeys(C0, D0);

            List<String> BinaryTextBy64Bit = new List<String>();
            StringBuilder EncryptedText = new StringBuilder();

            String CharIndexesInText = String.Empty;
            foreach (Char Char in text)
            {
                CharIndexesInText += Array.IndexOf(Alphabet, Char);
            }
            String CharIndexesInTextInBinaryFormat = Conversion.FromTextToBinary(CharIndexesInText);

            BinaryTextLength = CharIndexesInTextInBinaryFormat.Length;
            if ((CharIndexesInTextInBinaryFormat.Length % 64) != 0)
            {
                Int32 maxLength = 0;
                maxLength = ((CharIndexesInTextInBinaryFormat.Length / 64) + 1) * 64;
                CharIndexesInTextInBinaryFormat = CharIndexesInTextInBinaryFormat.PadRight(maxLength, '0');
            }
            BinaryTextBy64Bit = CharIndexesInTextInBinaryFormat.SplitInParts().ToList();
            foreach (String Part64 in BinaryTextBy64Bit)
            {
                for (Int32 i = 0; i < (Part64.Length / 64); i++)
                {
                    String PermutatedText = this.DoPermutation(Part64.Substring(i * 64, 64), DESData.InitialPermutation);

                    String L0 = "", R0 = "";

                    L0 = this.SetLeftHalvesKey(PermutatedText);
                    R0 = this.SetRightHalvesKey(PermutatedText);
                    EncryptedText.Append(DoCycles(L0, R0, keys, false));
                }
            }

            return EncryptedText.ToString();
        }
        public String Decryption(String BinaryText)
        {
            String HexKey = Conversion.FromTextToHex("0123456789ABCDEF");
            String BinaryKey = Conversion.FromHexToBinary(HexKey);
            String KeyPermutation = this.DoPermutation(BinaryKey, DESData.PermutationKey);

            string C0 = "", D0 = "";

            C0 = this.SetLeftHalvesKey(KeyPermutation);
            D0 = this.SetRightHalvesKey(KeyPermutation);

            Keys keys = this.SetAllKeys(C0, D0);


            StringBuilder DecryptedText = new StringBuilder();
            String PermutatedText = String.Empty;
            String DecryptedBinaryText = String.Empty;
            for (int i = 0; i < (BinaryText.Length / 64); i++)
            {
                PermutatedText = this.DoPermutation(BinaryText.Substring(i * 64, 64), DESData.InitialPermutation);

                String L0 = "", R0 = "";

                L0 = this.SetLeftHalvesKey(PermutatedText);
                R0 = this.SetRightHalvesKey(PermutatedText);

                DecryptedBinaryText += this.DoCycles(L0, R0, keys, true);

            }
            DecryptedBinaryText = DecryptedBinaryText.Remove(DecryptedBinaryText.Length - (DecryptedBinaryText.Length - BinaryTextLength));

            //    #region It's for correct subtracted '0' that have added for set text multiple of 64bit
            //if ((i * 64 + 64) == binaryText.Length)
            //{
            //    StringBuilder last_text = new StringBuilder(FinalText.TrimEnd('0'));

            //    int count = FinalText.Length - last_text.Length;

            //    if ((count % 8) != 0)
            //    {
            //        count = 8 - (count % 8);
            //    }

            //    string append_text = "";

            //    for (int k = 0; k < count; k++)
            //    {
            //        append_text += "0";
            //    }

            //    DecryptedTextBuilder.Append(last_text.ToString() + append_text);
            //}
            //#endregion
            //else
            //{
            //    //      DecryptedTextBuilder.Append(FinalText);
            //}



            String DecryptedCharIndexes = Conversion.FromBinaryToText(DecryptedBinaryText);
            return DecryptedText.Append(Conversion.FromBinaryToText(DecryptedBinaryText)).ToString();

        }
        public String DoCycles(String L0, String R0, Keys keys, bool IsReverse)
        {
            String Ln = "", Rn = "", Ln_1 = L0, Rn_1 = R0;

            int i = 0;

            if (IsReverse == true)
            {
                i = 15;
            }

            while (this.IsEnough(i, IsReverse))
            {
                Ln = Rn_1;
                Rn = this.XOR(Ln_1, this.F(Rn_1, keys.Kn[i]));

                //Next Step of L1, R1 is L2 = R1, R2 = L1 + f(R1, K2), hence, value of Step1's Ln, Rn is Rn_1, Ln_1 in Step2.
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

            String R16L16 = Rn + Ln;

            String EncryptedBinaryText = this.DoPermutation(R16L16, DESData.FinalPermutation);
            return EncryptedBinaryText;
        }
        public String F(String Rn_1, String Kn)
        {
            String E_Rn_1 = this.E_Selection(Rn_1);

            String XOR_Rn_1_Kn = this.XOR(E_Rn_1, Kn);

            String sBoxedText = this.S_BlockPermutation(XOR_Rn_1_Kn);

            String P_sBoxedText = this.P(sBoxedText);

            return P_sBoxedText;
        }
        public String P(String text)
        {
            String PermutatedText = "";

            PermutatedText = this.DoPermutation(text, DESData.PermutationP_blocks);

            return PermutatedText;
        }
        public String S_BlockPermutation(String text)
        {
            StringBuilder TransformedText = new StringBuilder(32);

            for (int i = 0; i < 8; i++)
            {
                String temp = text.Substring(i * 6, 6);
                TransformedText.Append(this.DoPermutation(temp, DESData.S_Blocks[i]));
            }

            return TransformedText.ToString();
        }
        public string E_Selection(string Rn_1)
        {
            string ExpandedText = this.DoPermutation(Rn_1, DESData.PermutationKeyExtension);

            return ExpandedText;
        }
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
        public bool IsEnough(int i, bool IsReverse)
        {
            return (IsReverse == false) ? i < 16 : i >= 0;
        }
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
        public string SetLeftHalvesKey(string text)
        {
            return this.SetHalvesKey(true, text);
        }
        public string SetRightHalvesKey(string text)
        {
            return this.SetHalvesKey(false, text);
        }
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
        public Keys SetAllKeys(String C0, String D0)
        {
            Keys keys = new Keys();
            keys.Cn[0] = C0;
            keys.Dn[0] = D0;

            for (int i = 1; i < keys.Cn.Length; i++)
            {
                keys.Cn[i] = this.LeftShift(keys.Cn[i - 1], DESData.Shifts[i]);
                keys.Dn[i] = this.LeftShift(keys.Dn[i - 1], DESData.Shifts[i]);
                keys.Kn[i - 1] = this.DoPermutation(keys.Cn[i] + keys.Dn[i], DESData.PermutationKeyCompression);
            }

            return keys;
        }
        public String LeftShift(String text)
        {
            return this.LeftShift(text, 1);
        }
        public String LeftShift(String text, int count)
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
