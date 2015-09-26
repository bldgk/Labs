using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptographyLabrary;

namespace ConsoleApplication1
{
   


    class Program
    {
        static void Main(string[] args)
        {
            #region DES_test
            //Cipher Cipher = new DES();
            //Cipher.Alphabet = "123456780".ToArray();
            //string text = "12345678123456781234567812345678123456781";
            //Console.WriteLine("Text: " + text + "/n Length: " + text.Length);
            //String encrtext = Cipher.Encryption(text);
            //String decrtext = Cipher.Decryption(encrtext);
            //Console.WriteLine("Encrypted by DES: \n" + encrtext + "\n Length = " + encrtext.Length);
            //Console.WriteLine("Decrypted by DES: \n" + decrtext + "\n Length = " + decrtext.Length);

            //Cipher = new MyDES();
            //Cipher.Alphabet = "123456780".ToArray();
            //encrtext = Cipher.Encryption(text);
            //decrtext = Cipher.Decryption(encrtext);
            //Console.WriteLine("Encrypted by myDES: \n" + encrtext + "\n Length = " + encrtext.Length);
            //Console.WriteLine("Decrypted by myDES: \n" + decrtext + "\n Length = " + decrtext.Length);
            #endregion
            
            Console.Write("Enter expression. \n Base: ");
            Int32 Base = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n Degre: ");
            Int32 Degree = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n Divider: ");
            Int32 Divider = Convert.ToInt32(Console.ReadLine());
            Expression Exp = new Expression(Base, Degree, Divider);
            Console.WriteLine("\n Remainder: " + Exp.GetRemainder());
            Console.ReadKey();

        }
    }
}