using System;
using System.Linq;
using CryptographyLabrary;
using System.Collections;
using static System.Math;
using System.Numerics;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using System.Windows.Forms;
namespace ConsoleApplication1
{


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose mode: \n\t1 - DES\n\t2 - Mod Calc\n\t3 - RSA\n\t4 - El-Gammal\n\t5 - Diffie-Hellman\n\t6 - MD5\n\t7 - SHA1\n\t8 - Blowfish\n\t9 - AES\n\t10 - DSA\n\t11 - Elliptic\n\t12 - SHA2");
            Console.Write("> ");
            switch (Console.ReadLine())
            {
                
                case "1": DESTest(); break;
                case "2": ModCalc(); break;
                case "3": RSATest(); break;
                case "4": El_GammalTest(); break;
                case "5": Diffie_Hellman(); break;
                case "6": MD5Test(); break;
                case "7": SHA1Test(); break;
                case "8": BlowfishTest(); break;
                case "9": AESTest(); break;
                case "10": DSATest(); break;
                case "11": EllipticTest(); break;
                case "12":  SHA2Test();break;
            }

        }



        #region RSATest
        public static void RSATest()
        {
            CryptographyLabrary.RSA RSA = new CryptographyLabrary.RSA();
            foreach (var n in RSA.KeysPQ)
                Console.WriteLine("Key " + n.ToString());
            Console.WriteLine("n = " + RSA.N(RSA.KeysPQ[0], RSA.KeysPQ[1]));
            Console.WriteLine("phi = " + RSA.Phi(RSA.N(RSA.KeysPQ[0], RSA.KeysPQ[1])));
            Console.WriteLine("Enter text ");
            String Ttext = Console.ReadLine();
            Console.WriteLine("Text " + Ttext);
            String encs = RSA.Encryption(Ttext);
            Console.WriteLine("Encrypted Text = " + encs.ToString());
            String decs = RSA.Decryption(encs);
            Console.WriteLine("Decrypted Text = " + decs.ToString());

            Console.WriteLine("=================RSABigInteger===============");
            RSABigInteger RSABI = new RSABigInteger();
            Console.WriteLine("Text " + Ttext);
            encs = RSABI.Encryption(Ttext);
            Console.WriteLine("Encrypted Text = " + encs.ToString());
            decs = RSABI.Decryption(encs);
            Console.WriteLine("Decrypted Text = " + decs.ToString());
            Console.ReadKey();
        }
        #endregion
        #region DES_test
        public static void DESTest()
        {

            DES Des = new DES();
            Des.Alphabet = "123456780".ToArray();
            Console.WriteLine("Enter text: ");
            String Text = Console.ReadLine();
            Console.WriteLine("Text: " + Text + "/n Length: " + Text.Length);
            String encrtext = Des.Encryption(Text);
            String decrtext = Des.Decryption(encrtext);
            Console.WriteLine("Encrypted by DES: \n" + encrtext + "\n Length = " + encrtext.Length);
            Console.WriteLine("Decrypted by DES: \n" + decrtext + "\n Length = " + decrtext.Length);
            Console.ReadKey();
            //Cipher = new MyDES();
            //Cipher.Alphabet = "123456780".ToArray();
            //encrtext = Cipher.Encryption(Text);
            //decrtext = Cipher.Decryption(encrtext);
            //Console.WriteLine("Encrypted by myDES: \n" + encrtext + "\n Length = " + encrtext.Length);
            //Console.WriteLine("Decrypted by myDES: \n" + decrtext + "\n Length = " + decrtext.Length);

        }
        #endregion
        #region ModCalcTest
        public static void ModCalc()
        {
            Console.Write("Enter expression. \n Base: ");
            Int32 Base = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n Degre: ");
            Int32 Degree = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n Divider: ");
            Int32 Divider = Convert.ToInt32(Console.ReadLine());
            ModCalculator Exp = new ModCalculator(Base, Degree, Divider);
            Console.WriteLine("\n Remainder: " + Exp.GetRemainder());
            Console.ReadKey();
        }
        #endregion
        #region Diffie-HellmanTest
        public static void Diffie_Hellman()
        {
            CDH Alice = new CDH();
            CDH Bob = new CDH();
            Alice.CreateLink(Bob);
            Console.ReadKey();
        }
        #endregion
        #region El-GammalTest
        public static void El_GammalTest()
        {
            El_Gammal Alice = new El_Gammal();
            El_Gammal Bob = new El_Gammal();
            Alice.CreateLink(Bob);
            Console.WriteLine("Enter text ");
            string text = Console.ReadLine();
            string bobtext = Bob.Encryption(text);
            Console.WriteLine(bobtext);
            string alicetext = Alice.Decryption(bobtext);
            Console.WriteLine(alicetext);
            Console.ReadKey();
        }
        #endregion
        #region MD5
        public static void MD5Test()
        {
            MD3 md3 = new MD3();
            Console.WriteLine("Enter text: ");
            string source = Console.ReadLine();
            Console.WriteLine("The MD5 hash of " + source + " is: ");
            foreach (var b in md3.Hash(source))
            {
                Console.Write(b.ToHex().ToString().ToLower());
            }
            Console.WriteLine(".");
            Console.ReadKey();
        }

        #endregion
        #region SHA1Test
        public static void SHA1Test()
        {
            SHA1 sh = new SHA1();
            Console.WriteLine("Enter text:");
            string text = Console.ReadLine();
            Console.WriteLine("Hash is " + sh.Hash(text).ToLower());
            Console.ReadKey();
        }

        #endregion
        #region SHA2Test
        public static void SHA2Test()
        {
            SHA2 sh = new SHA2();
            Console.WriteLine("Enter text:");
            string text = Console.ReadLine();
            Console.WriteLine("Hash is " + sh.Hash(text.ToUTF8()).ToLower());
            Console.ReadKey();
        }
        #endregion
        #region BlowfishTest
        public static void BlowfishTest()
        {
            Console.WriteLine("Enter text: ");
            string text = Console.ReadLine();
            Blowfish b = new Blowfish();
            string encr = b.Encrypt(text);
            Console.WriteLine("Encrypted text " + encr);
            Console.WriteLine("Decrypted text " + b.Decrypt(encr));
            Console.ReadKey();
        }
        #endregion
        #region AESTest
        public static void AESTest()
        {
            Console.WriteLine("Enter text: ");
            string text = Console.ReadLine();
            byte[] cipherText = new byte[16];
            byte[] decipheredText = new byte[16];
            AES aes = new AES(AES.KeySize.Bits128);
            aes.Cipher(text.ToUTF8(), cipherText);
           // aes.Dump();
            Console.WriteLine("\nEncrypted Text: ");
            BytesAsHex(cipherText);
            aes.InvCipher(cipherText, decipheredText);
           // aes.Dump();
            Console.WriteLine("\nDecrypted text: ");
            Console.WriteLine(decipheredText.FromUTF8());
            Console.ReadLine();
        }

        static void BytesAsHex(byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; ++i)
            {
                Console.Write(bytes[i].ToString("x2") + " ");
                if (i > 0 && i % 16 == 0) Console.Write("\n");
            }
            Console.WriteLine("");
        }
        #endregion

        #region DSATest
        public static void DSATest()
        {
            Console.WriteLine("Enter text: ");
            string text = Console.ReadLine();
            DSA dsa = new DSA();
            dsa.Message = 31;
            dsa.CreateSignature();
            Console.Write("Signature: ");
            Console.WriteLine(dsa.GetSignature());
            Console.WriteLine(dsa.ToString());
            Console.WriteLine("Verifying...");
            Console.WriteLine(dsa.Validate(dsa.Signature, dsa.PublicKey, dsa.Message));
            Console.WriteLine("========================BigInteger=======================");
            DSABigInteger dsabi = new DSABigInteger();
            dsabi.CreateSignature();
            Console.Write("Signature: ");
            Console.WriteLine(dsabi.GetSignature());
            Console.WriteLine(dsabi.ToString());
            Console.WriteLine("Verifying...");
            Console.WriteLine(dsabi.Validate(dsabi.Signature, dsabi.PublicKey, dsabi.Message));
            //  Random random  = RandomProvider.GetThreadRandom();            BigInteger P = new BigInteger();
            //  BigInteger L = new BigInteger();
            //  do
            //  {
            //      L = random.Next(512, 1024);

            //  }
            //  while (L % 64 != 0);
            //  L = 1024;
            //  do
            //  {
            ////      P = random.Next((int)BigInteger.Pow(2, (int)L-1), (int)BigInteger.Pow(2, (int)L));//, RandomSize(KeysSize));)
            //     // P = ;
            //  }
            //  while (IsPrime(P) == false) ;

            //  //BigInteger product = 1;
            //  //BigInteger ab = 2;

            //  //for (int i = 0; i < 1024; i++)
            //  //{
            //  //    product = BigInteger.Multiply(product, ab);
            //  //}
            //  //Console.WriteLine(BigInteger.Multiply(product, product));
            //  //Console.WriteLine("\n" + product);

            //  //for (BigInteger i = BigInteger.Pow(2, (int)L - 1); i < BigInteger.Pow(2, (int)L); i++)
            //  //{
            //  //    if (IsPrime(i))
            //  //    {       Console.WriteLine("\naaaaaaaaaaaaaaaaaAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            //  //    break;
            //  //}
            //  //    Console.WriteLine(i.ToString());
            //  //}
            //public static bool IsPrime(BigInteger Number)
            //{
            //    bool Prime = true;
            //    for (int i = 2; i <= Number / 2; i++)
            //    {
            //        if (Number % i == 0)
            //        {
            //            Prime = false;
            //            break;
            //        }
            //    }
            //    return Prime;
            //}
            //public static bool IsWhiteSpace(char c)
            //{
            //    if (IsLatin1(c))
            //        return IsWhiteSpaceLatin1(c);
            //    return IssWhiteSpace(c);
            //}
            //private static bool IsLatin1(char ch)
            //{
            //    return ch <= byte.MaxValue;
            //}
            //private static bool IsWhiteSpaceLatin1(char c)
            //{
            //    return (int)c == 32 || (int)c >= 9 && (int)c <= 13 || ((int)c == 160 || (int)c == 133);
            //}
            //internal static bool IssWhiteSpace(char c)
            //{
            //    switch (CharUnicodeInfo.GetUnicodeCategory(c))
            //    {
            //        case UnicodeCategory.SpaceSeparator:
            //        case UnicodeCategory.LineSeparator:
            //        case UnicodeCategory.ParagraphSeparator:
            //            return true;
            //        default:
            //            return false;
            //    }
            //}

            Console.ReadKey();
         }

        #endregion
        #region EllipticTest
        public static void EllipticTest()
        {
            EllipticForm ef = new EllipticForm();
            ef.ShowDialog();
            Console.ReadKey();
            
        }
        #endregion
    }
}
