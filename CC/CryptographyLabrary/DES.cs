using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyLabrary
{
    public class DES : Cipher
    {
        public char[] Alphabet { get; set; }
        public Int32 BinaryTextLength { get; set; }
        public DES()
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

            List<String> TextBinaryBy64bits = new List<String>();
            StringBuilder EncryptedText = new StringBuilder();
            String BinaryText = Conversion.FromTextToBinary(text);
            BinaryTextLength = BinaryText.Length;
            if ((BinaryText.Length % 64) != 0)
            {
                Int32 maxLength = 0;
                maxLength = ((BinaryText.Length / 64) + 1) * 64;
                BinaryText = BinaryText.PadRight(maxLength, '0');
            }
            TextBinaryBy64bits = BinaryText.SplitInParts().ToList();
            foreach (String Part64 in TextBinaryBy64bits)
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

            String C0 = "", D0 = "";

            C0 = this.SetLeftHalvesKey(KeyPermutation);
            D0 = this.SetRightHalvesKey(KeyPermutation);

            Keys keys = this.SetAllKeys(C0, D0);

            StringBuilder DecryptedText = new StringBuilder();
            String PermutatedText = String.Empty;
            String DecryptedBinaryText = String.Empty;
            for (Int32 i = 0; i < (BinaryText.Length / 64); i++)
            {
                PermutatedText = this.DoPermutation(BinaryText.Substring(i * 64, 64), DESData.InitialPermutation);
                String L0 = "", R0 = "";
                L0 = this.SetLeftHalvesKey(PermutatedText);
                R0 = this.SetRightHalvesKey(PermutatedText);
                DecryptedBinaryText += this.DoCycles(L0, R0, keys, true);
            }
            DecryptedBinaryText = DecryptedBinaryText.Remove(DecryptedBinaryText.Length - (DecryptedBinaryText.Length - BinaryTextLength));
            DecryptedText.Append(Conversion.FromBinaryToText(DecryptedBinaryText));
            return DecryptedText.ToString();
        }
        public String DoCycles(String L0, String R0, Keys keys, Boolean IsReverse)
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
        public String P(String text) => this.DoPermutation(text, DESData.PermutationP_blocks);

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
        public String E_Selection(String Rn_1) => this.DoPermutation(Rn_1, DESData.PermutationKeyExtension);
      
        public String XOR(String text1, String text2)
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
        public Boolean IsEnough(Int32 i, Boolean IsReverse) => (IsReverse == false) ? i < 16 : i >= 0;

        public String DoPermutation(String text, Int32[,] order)
        {
            string PermutatedText = "";

            int rowIndex = Convert.ToInt32(text[0].ToString() + text[text.Length - 1].ToString(), 2);
            int colIndex = Convert.ToInt32(text.Substring(1, 4), 2);

            PermutatedText = Conversion.FromIntegerToBinary(order[rowIndex, colIndex]);

            return PermutatedText;
        }
        public String DoPermutation(String text, Int32[] order)
        {
            StringBuilder PermutatedText = new StringBuilder(order.Length);

            for (int i = 0; i < order.Length; i++)
            {
                PermutatedText.Append(text[order[i] - 1]);
            }

            return PermutatedText.ToString();
        }
        public String SetLeftHalvesKey(String text) => this.SetHalvesKey(true, text);
        public String SetRightHalvesKey(String text) => this.SetHalvesKey(false, text);
        
        public String SetHalvesKey(Boolean IsLeft, String text)
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
        public String LeftShift(String text) => this.LeftShift(text, 1);

        public String LeftShift(String text, Int32 count)
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
        
    public class DESData
    {
        public static readonly int[] InitialPermutation = { 58, 50, 42, 34, 26, 18, 10, 2, 60, 52,
                                           44, 36, 28, 20, 12, 4, 62, 54, 46, 38,
                                           30, 22, 14, 6, 64, 56, 48, 40, 32, 24,
                                           16, 8, 57, 49, 41, 33, 25, 17, 9, 1, 59,
                                           51, 43, 35, 27, 19, 11, 3, 61, 53, 45,
                                           37, 29, 21, 13, 5, 63, 55, 47, 39, 31,
                                           23, 15, 7 };
        public static readonly int[] FinalPermutation = { 40, 8, 48, 16, 56, 24, 64, 32, 39, 7,
                                            47, 15, 55, 23, 63, 31, 38, 6, 46, 14,
                                            54, 22, 62, 30, 37, 5, 45, 13, 53, 21,
                                            61, 29, 36, 4, 44, 12, 52, 20, 60, 28,
                                            35, 3, 43, 11, 51, 19, 59, 27, 34, 2,
                                            42, 10, 50, 18, 58, 26, 33, 1, 41, 9,
                                            49, 17, 57, 25 };
        public static readonly int[] PermutationKey = {57, 49, 41, 33, 25, 17, 9, 1, 58, 50,
                                          42, 34, 26, 18, 10, 2, 59, 51, 43, 35,
                                          27, 19, 11, 3, 60, 52, 44, 36, 63, 55,
                                          47, 39, 31, 23, 15, 7, 62, 54, 46, 38,
                                          30, 22, 14, 6, 61, 53, 45, 37, 29, 21,
                                          13, 5, 28, 20, 12, 4 };
        public static readonly int[] PermutationKeyCompression = { 14, 17, 11, 24, 1, 5, 3, 28, 15, 6, 21,
                                          10, 23, 19, 12, 4, 26, 8, 16, 7, 27,
                                          20, 13, 2, 41, 52, 31, 37, 47, 55, 30,
                                          40, 51, 45, 33, 48, 44, 49, 39, 56, 34,
                                          53, 46, 42, 50, 36, 29, 32 };
        public static readonly int[] PermutationKeyExtension = { 32, 1, 2, 3, 4, 5, 4, 5, 6, 7, 8, 9, 8,
                                          9, 10, 11, 12, 13, 12, 13, 14, 15, 16,
                                          17, 16, 17, 18, 19, 20, 21, 20, 21, 22,
                                          23, 24, 25, 24, 25, 26, 27, 28, 29, 28,
                                          29, 30, 31, 32, 1 };
        public static readonly int[] PermutationP_blocks = { 16, 7, 20, 21, 29, 12, 28, 17, 1, 15, 23,
                                          26, 5, 18, 31, 10, 2, 8, 24, 14, 32,
                                          27, 3, 9, 19, 13, 30, 6, 22, 11, 4, 25 };
        public static readonly int[] Shifts = { 0, 1, 1, 2, 2, 2, 2, 2, 2, 1,
                                                2, 2, 2, 2, 2, 2, 1 };
        public static List<int[,]> S_Blocks = new List<int[,]>();

        public static readonly int[,] S_block1 ={ { 14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7 },
                                         { 0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8 },
                                         { 4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0 },
                                         { 15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13 } };
        public static readonly int[,] S_block2 ={ { 15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10 },
                                         { 3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5 },
                                         { 0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15 },
                                         { 13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9 } };
        public static readonly int[,] S_block3 ={ { 10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8 },
                                         { 13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1 },
                                         { 13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7 },
                                         { 1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12 } };
        public static readonly int[,] S_block4 ={ { 7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15 },
                                         { 13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9 },
                                         { 10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4 },
                                         { 3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14 } };
        public static readonly int[,] S_block5 ={ { 2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9 },
                                         { 14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6 },
                                         { 4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14 },
                                         { 11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3 } };
        public static readonly int[,] S_block6 ={ { 12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11 },
                                         { 10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8 },
                                         { 9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6 },
                                         { 4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13 } };
        public static readonly int[,] S_block7 ={ { 4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1 },
                                         { 13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6 },
                                         { 1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2 },
                                         { 6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12 } };
        public static readonly int[,] S_block8 ={ { 13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7 },
                                         { 1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2 },
                                         { 7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8 },
                                         { 2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11 } };

        static DESData()
        {
            S_Blocks.Add(S_block1);
            S_Blocks.Add(S_block2);
            S_Blocks.Add(S_block3);
            S_Blocks.Add(S_block4);
            S_Blocks.Add(S_block5);
            S_Blocks.Add(S_block6);
            S_Blocks.Add(S_block7);
            S_Blocks.Add(S_block8);
        }
    }
    public class Keys
    {
        public string[] Cn = new string[17];
        public string[] Dn = new string[17];

        public string[] Kn = new string[16];
    }
    static class StringExtensions
    {
        public static IEnumerable<String> SplitInParts(this String s)
        {

            for (var i = 0; i < s.Length; i += 64)
                yield return s.Substring(i, Math.Min(64, s.Length - i));
        }
    }
}