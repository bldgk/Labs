using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.ComponentModel;
using static System.Math;

namespace CryptographyLabrary
{
    [DataContract]
    public class RSA : Cipher
    {
        public Char[] Alphabet { get; set; }
        public List<Int64> KeysPQ {get; set; }
        List<Int64> PrivateKey { get; set; }
        List<Int64> PublicKey { get; set; }
        Random Random { get; set; }
        public RSA()
        {
            Alphabet = new Char[] { };
            KeysPQ = new List<Int64>();

            Random = RandomProvider.GetThreadRandom();
            KeysPQ = GeneratePQKeys();
            
        }
        public String Encryption(String text)
        {

            PublicKey = MakePublicKey();
            String EncryptedText = String.Empty;
            foreach (Char  Char in text)
            {
                Calculation Calculation = new Calculation(Char, PublicKey[0], PublicKey[1]);
                EncryptedText += Calculation.GetRemainder().ToString() + ",";
            }
            return EncryptedText;
        }

        public String Decryption(String Text)
        {
            List<Int64> Publickey = GetPublicKey();
            List<Int64> PrivateKey = MakePrivateKey(Publickey);
            String DecryptedText = String.Empty;
            List<String> IntegersInText = Text.Split(',').ToList();
            IntegersInText.RemoveAt(IntegersInText.Count - 1);
            List<Int64> Chars = new List<Int64>();
            foreach (String Integer in IntegersInText)
                Chars.Add(Convert.ToInt64(Integer));
            foreach (Int64 Char in Chars)
            {
                
                Calculation Calculation = new Calculation(Char, PrivateKey[0], PrivateKey[1]);
                DecryptedText += Convert.ToChar(Convert.ToInt64(Calculation.GetRemainder()));//.ToString();
            }
            return DecryptedText;
        }
        public Double Encryption(Int64 text)
        {
            
            PublicKey = MakePublicKey();
            Double EncryptedText = 0;
                Calculation Calculation = new Calculation(text, PublicKey[0], PublicKey[1]);
            EncryptedText = Calculation.GetRemainder();
            
            return EncryptedText;
        }
        public Double Decryption(Int64 text)
        {
            List<Int64> Publickey = GetPublicKey();
            List<Int64> PrivateKey = MakePrivateKey(Publickey);
            Double DecryptedText = 0;
            Calculation Calculation = new Calculation(text,PrivateKey[0],PrivateKey[1]);
            DecryptedText = Calculation.GetRemainder();
            return DecryptedText;
        }
        public List<Int64> MakePrivateKey(List<Int64>PublicKey)
        {
            List<Int64> Privatekey = new List<Int64>();
            Privatekey.Add(D(PublicKey[0],Phi(PublicKey[1])));
            Privatekey.Add(PublicKey[1]);
            return Privatekey;
        }
        public List<Int64> GetPublicKey() => PublicKey;      
        public List<Int64> MakePublicKey()
        {
            List<Int64> Publickey = new List<Int64>();
            Int64 phi = Phi(N(KeysPQ[0], KeysPQ[1]));
            Int64 e = E(phi);                
            Publickey.Add(e);
            Publickey.Add(N(KeysPQ[0], KeysPQ[1]));
            return Publickey;
        }
        public List<Int64> GeneratePQKeys()
        {
            List<Int64> KeysPQs = new List<Int64>();

            Int32 KeysSize = Random.Next(2,3);
            Int64 P, Q = 0;
            do
            {
                P = Random.Next(RandomSize(KeysSize - 1), RandomSize(KeysSize));
            }
            while (IsPrime(P) == false);
            do
            {
                Q = Random.Next(RandomSize(KeysSize - 1), RandomSize(KeysSize));
            }
            while (IsPrime(Q) == false);
            KeysPQs.Add(P);
            KeysPQs.Add(Q);
            return KeysPQs;
        }
        public  Int64 E(Int64 Phi)
        {
            Int64 e = 0;
            do
            {
                e = Random.Next(1, Convert.ToInt32(Phi));
            }
            while (IsCoprime(e, Phi) != true);
            return e;

        }
        public  Int64 D(Int64 e, Int64 Phi)
        {
            Double D = 0;
            Int64 k = 1;
            while (true)
            {
                D = (1 + (k * Phi)) / (Double)e;
                if ((Math.Round(D, 5) % 1) == 0) //integer
                {
                    return (Int64)D;
                }
                else
                {
                    k++;
                }
            }
        }
        public  Int64 GCD(Int64 A, Int64 B)
        {

            while (B != 0)
                B = A % (A = B);
            return A;
        }
        public Boolean IsCoprime(Int64 A, Int64 B) => (GCD(A, B) == 1) ? true : false;
        public Int64 N(Int64 P, Int64 Q) => P * Q;        
        public Int64 Phi(Int64 P, Int64 Q) => (P - 1) * (Q - 1);       
        public Int64 Phi(Int64 N)
        {
            Int64 Phi = 1;
            foreach (Int64 PrimeNumber in ToFactor(N))
                Phi *= PrimeNumber - 1;
            return Phi;
        }
        public Int32 RandomSize(Int32 Size)
        {
            Int32 RandomSize = 1;
            return Convert.ToInt32(RandomSize.ToString().PadRight(Size + 1, '0'));
        }
        public Boolean IsPrime(Int64 Number)
        {
            Boolean Prime = true;
            for (Int32 i = 2; i <= Number / 2; i++)
            {
                if (Number % i == 0)
                {
                    Prime = false;
                    break;
                }
            }
            return Prime;
        }
        public List<Int64> ToFactor(Int64 Number)
        {
            List<Int64> Multipliers = new List<Int64>();
            Int64 B, C;

            while ((Number % 2) == 0)
            {
                Number = Number / 2;
                Multipliers.Add(2);
            }
            B = 3; C = (int)Math.Sqrt(Number) + 1;
            while (B < C)
            {
                if ((Number % B) == 0)
                {
                    if (Number / B * B - Number == 0)
                    {
                        Multipliers.Add(B);
                        Number = Number / B;
                        C = (int)Math.Sqrt(Number) + 1;
                    }
                    else
                        B += 2;
                }
                else
                    B += 2;
            }
            Multipliers.Add(Number);
            return Multipliers;
        }
        /*
        def keys(bits):
    """
    keys(bits) -> (public, private)
    Generate public and private RSA keys of the given size.
    """
    # Pragma: use a fixed e, the fourth Fermat prime (0b10000000000000001)
    e = 2**16+1
    while True:
        # Generate two large prime numbers p and q, n = pq and φ = (p-1)(q-1)
        s = bits / 2
        mask = 0b11 << (s - 2) | 0b1 # two highest and the lowest bit
        while True:
            p = getrandbits(s) | mask
            # Pragma: check p % e here to guarantee that φ and e are coprimes
            if p % e != 1 and rmspp(p): 
                break
        s = bits - s
        mask = 0b11 << (s - 2) | 0b1 # same as above, but maybe different s
        while True:
            q = getrandbits(s) | mask
            if q != p and q % e != 1 and rmspp(q): 
                break
        n = p * q
        phi = (p - 1) * (q - 1)
        # Pragma: e is chosen already and is relative prime to φ
        # Compute d, a modular multiplicative inverse to e (i.e. e*d % φ = 1)
        d = mmi(e, phi)
        if d: # if not, the process will repeat
            break
    return (n, e), (n, d)

ef mmi(a, m):
    """
    mmi(a, m) -> x, such as ax % m = 1
    mmi is a Modular Multiplicative Inverse
    See http://en.wikipedia.org/wiki/Modular_multiplicative_inverse
    """
    gcd, x, q = egcd(a, m)
    if gcd != 1:
        # The a and m are not coprimes, so the inverse doesn't exist
        return None
    else:
        return (x + m) % m

def egcd(a, b):
    """
    egcd(a, b) -> d, x, y, such as d == gcd(a, b) == ax + by
    egcd is an Extended Greatest Common Divisor
    http://en.wikipedia.org/wiki/Extended_Euclidean_algorithm
    """
    if b == 0:
        return (a, 1, 0)
    else:
        d, x, y = egcd(b, a % b)
        return d, y, x - y * (a / b)
        */
    }
    public static class RandomProvider
    {
        private static Int32 seed = Environment.TickCount;

        private static ThreadLocal<Random> randomWrapper = new ThreadLocal<Random>(() =>
            new Random(Interlocked.Increment(ref seed))
        );

        public static Random GetThreadRandom() => randomWrapper.Value;

    }
    public class Calculation
    {
        Int64 Base { get; set; }
        Int64 Degree { get; set; }
        Double Remainder { get; set; }
        Int64 Divider { get; set; }
        List<Char> DegreeBinary { get; set; }


        public Calculation()
        {
            this.Base = 0;
            this.Degree = 0;
            this.Divider = 0;
            this.Remainder = 0;
            this.DegreeBinary = Convert.ToString(this.Degree, 2).ToList();
        }
        public Calculation(Int64 Base, Int64 Degree, Int64 Divider)
        {
            this.Base = Base;
            this.Degree = Degree;
            this.Divider = Divider;
            this.Remainder = this.Base;
            this.DegreeBinary = Convert.ToString(this.Degree, 2).ToList();
            DegreeBinary.ToString();
        }
        public Double GetRemainder()
        {
            String B_row = String.Empty;
            String A_row = Base.ToString();

            for (Int32 i = 1; i < DegreeBinary.Count(); i++)
            {
                try {
                    Remainder = NextRemainder(Remainder, DegreeBinary[i]);
                }
                catch { }
                A_row += " " + Remainder.ToString();
                B_row += DegreeBinary[i];
            }
            return Remainder;
        }
        public Double NextRemainder(Double CurrentRemainder, Char B) => (B == '1') ? (Pow(CurrentRemainder, 2) * Base) % Divider : Pow(CurrentRemainder, 2) % Divider;
    
    }
}
