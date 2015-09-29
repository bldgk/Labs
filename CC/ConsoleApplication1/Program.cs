using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptographyLabrary;
using System.Threading;

namespace ConsoleApplication1
{


    public static class RandomProvider
    {
        private static Int32 seed = Environment.TickCount;

        private static ThreadLocal<Random> randomWrapper = new ThreadLocal<Random>(() =>
            new Random(Interlocked.Increment(ref seed))
        );

        public static Random GetThreadRandom()
        {
            return randomWrapper.Value;
        }
    }
    class Program
    {
        static Random Random = RandomProvider.GetThreadRandom();
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
            #region RSACalcTest

            //Console.Write("Enter expression. \n Base: ");
            //Int32 Base = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("\n Degre: ");
            //Int32 Degree = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("\n Divider: ");
            //Int32 Divider = Convert.ToInt32(Console.ReadLine());
            //Calculation Exp = new Calculation(Base, Degree, Divider);
            //Console.WriteLine("\n Remainder: " + Exp.GetRemainder());
            //Console.ReadKey();
            #endregion

            #region RSA

            // GeneratePQKeys();
            RSA RSA = new RSA();
            foreach (var n in RSA.KeysPQ)
                Console.WriteLine("Key "+ n.ToString());
            Console.WriteLine("n = " + RSA.N(RSA.KeysPQ[0], RSA.KeysPQ[1]));
            Console.WriteLine("phi = " + RSA.Phi(RSA.N(RSA.KeysPQ[0], RSA.KeysPQ[1])));
            Console.WriteLine("Enter text ");
            String text = Console.ReadLine();
            Console.WriteLine("Text " +text );
            //Console.WriteLine("Encryption if integers ");
            //Double enc = RSA.Encryption(Convert.ToInt64(text));
            //Console.WriteLine("Encrypted Text = " + enc.ToString());
            //Double dec = RSA.Decryption((Convert.ToInt64(enc)));
            //Console.WriteLine("Decrypted Text = " + dec.ToString());
            //Console.WriteLine();
            String encs = RSA.Encryption(text);
            Console.WriteLine("Encrypted Text = " + encs.ToString());
            String decs = RSA.Decryption(encs);
            Console.WriteLine("Decrypted Text = " + decs.ToString());
            Console.ReadKey();
            #endregion

        }
        //public static List<Int64> GeneratePQKeys()
        //{
        //    List<Int64> KeysPQs = new List<Int64>();

        //    Int32 KeysSize = Random.Next(10);
        //    Int64 P, Q = 0;
        //    P = 7;
        //    Q = 11;
        //    Console.WriteLine(P + ", " + Q);
        //    Console.WriteLine(N(P, Q));
        //    Console.WriteLine(Phi(P, Q));
        //    Int64 e = E((Phi(P, Q)));

        //    Console.WriteLine();

        //    Console.WriteLine(D(3, 9167368/*e,Phi(P, Q)*/).ToString());
        //    Console.ReadKey();
        //    //    Console.WriteLine(P);
        //    //}
        //    // while (IsPrime(P) == false);

        //    return KeysPQs;


        //}
        //public static Int64 E(Int64 Phi)
        //{
        //    Int64 e = 0;
        //    do
        //    {
        //        e = Random.Next(1, Convert.ToInt32(Phi));
        //    }
        //    while (IsCoprime(e, Phi) != true);
        //    return e;

        //}

        //public static Int64 D(Int64 e, Int64 Phi)
        //{
        //    Double D = 0;
        //    Int64 k = 1;
        //    while (true)
        //    {
        //        D = (1 + (k * Phi)) / (Double)e;
        //        if ((Math.Round(D, 5) % 1) == 0) //integer
        //        {
        //            return (Int64)D;
        //        }
        //        else
        //        {
        //            k++;
        //        }
        //    }
        //}
        //public static Int64 GCD(Int64 A, Int64 B)
        //{

        //    while (B != 0)
        //        B = A % (A = B);
        //    return A;
        //}


        //public static Boolean IsCoprime(Int64 A, Int64 B)
        //{
        //    return (GCD(A, B) == 1) ? true : false;
        //}
        //public static Int64 N(Int64 P, Int64 Q)
        //{
        //    return P * Q;
        //}
        //public static Int64 Phi(Int64 P, Int64 Q)
        //{
        //    return (P - 1) * (Q - 1);
        //}
        //public static Int32 RandomSize(Int32 Size)
        //{
        //    Int32 RandomSize = 1;
        //    return Convert.ToInt32(RandomSize.ToString().PadRight(Size + 1, '0'));
        //}
        //public static Boolean IsPrime(Int64 Number)
        //{
        //    Boolean Prime = true;
        //    for (Int32 i = 2; i <= Number / 2; i++)
        //    {
        //        if (Number % i == 0)
        //        {
        //            Prime = false;
        //            break;
        //        }
        //    }
        //    return Prime;
        //}
       
    }
}