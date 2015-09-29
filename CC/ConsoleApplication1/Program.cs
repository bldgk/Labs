using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptographyLabrary;
using System.Threading;

namespace ConsoleApplication1
{


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose mode: 1 - DES, 2 - RSA Calc, 3 - RSA");
            ;
            switch (Console.ReadLine())
            {

                case "1": DESTest(); break;
                case "2": RSACalc(); break;
                case "3": RSATest(); break;
            }

            Console.ReadKey();
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

            Cipher Cipher = new DES();
            Cipher.Alphabet = "123456780".ToArray();
            String Text = "12345678123456781234567812345678123456781"; //String Text = text;
            Console.WriteLine("Text: " + Text + "/n Length: " + Text.Length);
            String encrtext = Cipher.Encryption(Text);
            String decrtext = Cipher.Decryption(encrtext);
            Console.WriteLine("Encrypted by DES: \n" + encrtext + "\n Length = " + encrtext.Length);
            Console.WriteLine("Decrypted by DES: \n" + decrtext + "\n Length = " + decrtext.Length);

            Cipher = new MyDES();
            Cipher.Alphabet = "123456780".ToArray();
            encrtext = Cipher.Encryption(Text);
            decrtext = Cipher.Decryption(encrtext);
            Console.WriteLine("Encrypted by myDES: \n" + encrtext + "\n Length = " + encrtext.Length);
            Console.WriteLine("Decrypted by myDES: \n" + decrtext + "\n Length = " + decrtext.Length);

        }
        #endregion
        #region RSACalcTest
        public static void RSACalc()
        {
            Console.Write("Enter expression. \n Base: ");
            Int32 Base = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n Degre: ");
            Int32 Degree = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n Divider: ");
            Int32 Divider = Convert.ToInt32(Console.ReadLine());
            Calculation Exp = new Calculation(Base, Degree, Divider);
            Console.WriteLine("\n Remainder: " + Exp.GetRemainder());
            Console.ReadKey();
        }
        #endregion

    }
}