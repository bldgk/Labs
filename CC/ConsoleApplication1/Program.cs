using System;
using System.Linq;
using CryptographyLabrary;
using System.Collections.Generic;
using static System.Math;
using System.Text;

namespace ConsoleApplication1
{


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose mode: 1 - DES,\n 2 - Mod Calc, \n 3 - RSA, 4 - El-Gammal, \n 5 - Diffie-Hellman, \n 6 - MD5, \n 7 - SHA1, \n 8 - SHA2, \n 9 - Blowfish, \n 10 - AES");
            switch (Console.ReadLine())
            {

                case "1": DESTest(); break;
                case "2": ModCalc(); break;
                case "3": RSATest(); break;
                case "4": El_GammalTest(); break;
                case "5": Diffie_Hellman(); break;
                case "6": MD5Test(); break;
                case "7": SHA1Test(); break;
                case "8": SHA2Test(); break;
                case "9": BlowfishTest(); break;
                case "10": AESTest(); break;
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
            Console.ReadKey();
        }
        #endregion
        #region DES_test
        public static void DESTest()
        {

            DES Des = new DES();
            Des.Alphabet = "123456780".ToArray();
            Console.WriteLine("Enter text: ");
            String Text = Console.ReadLine();// Rea//"12345678123456781234567812345678123456781"; //String Text = text;
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

        #region 
        public static void BlowfishTest()
        {
            Console.WriteLine("Enter text: ");
            string text = Console.ReadLine();
            Blowfish b = new Blowfish();
            //string str = Convert.ToString(Convert.ToInt64(text), 16);
            //while (str.Length < 16)
            //    str = "0" + str;
            //var temp = str.Substring(0, 8) + " " + str.Substring(8, 8);
            string encr = b.Encrypt(text);
            Console.WriteLine("Encrypted text " + encr);


            Console.WriteLine("Decrypted text " + b.Decrypt(encr));
            //string text = "aaaaaaaa";
            //char[] chars = text.ToCharArray();
            //StringBuilder stringBuilder = new StringBuilder();
            //foreach (char c in chars)
            //{
            //    stringBuilder.Append(((Int16)c).ToString("x"));
            //}
            //var t = Convert.ToUInt64(stringBuilder.ToString());
            //var textAsHex = Convert.ToUInt64(stringBuilder.ToString(), 16);
            //Console.WriteLine(textAsHex.ToString());

            //string hexValue = textAsHex.ToString("X");
            //string hexString = "6161616161616161";
            //var bytes = new byte[hexString.Length / 2];
            //for (var i = 0; i < bytes.Length; i++)
            //{
            //    bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            //}
            //System.Text.Encoding enc = new UTF8Encoding(true, true);
            //    Console.WriteLine(enc.GetString(bytes)); // returns: "Hello world" for "48656C6C6F20776F726C64"

            Console.ReadKey();
        }
        #endregion

        #region
        public static void AESTest()
        {
            Console.WriteLine("Enter text: ");
            string text = Console.ReadLine();
            byte[] cipherText = new byte[16];
            byte[] decipheredText = new byte[16];
            AES aes = new AES(AES.KeySize.Bits128);
            aes.Cipher(text.ToUTF8(), cipherText);
            aes.Dump();
            Console.WriteLine("\nEncrypted Text: ");
            BytesAsHex(cipherText);
            aes.InvCipher(cipherText, decipheredText);
            aes.Dump();
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
    }
    #endregion
}
