using System;
using System.Linq;
using CryptographyLabrary;
using System.Collections.Generic;
using static System.Math;

namespace ConsoleApplication1
{


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose mode: 1 - DES, 2 - Mod Calc, 3 - RSA, 4 - El-Gammal, 5 - Diffie-Hellman");
            switch (Console.ReadLine())
            {

                case "1": DESTest(); break;
                case "2": ModCalc(); break;
                case "3": RSATest(); break;
                case "4": El_GammalTest(); break;
                case "5": Diffie_Hellman(); break;
            }
            
        }              
        #region RSATest
        public static void RSATest()
        {
            RSA RSA = new RSA();
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
    }
}