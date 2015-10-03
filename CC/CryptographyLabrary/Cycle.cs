using System;
using System.Collections.Generic;
using System.Linq;
using CenterSpace.NMath.Core;
namespace CryptographyLabrary
{
    public class Cycle:ICipher
    {
        public char[] Alphabet { get; set; }
        Polynomial FormativePolinome { get; set; }
        Polynomial Polynome { get; set; }
        public Cycle(string polinome)
        {
            Alphabet = new char[] { };
            FormativePolinome = ToPolinomial(polinome);
        }
        public Polynomial ToPolinomial(string Coefs)
        {
            var Coeffs = new DoubleVector(Coefs);
            Polynomial PolynomeTemp = new Polynomial(Coeffs);
            return PolynomeTemp;
        }
        public string StringReverse(string word)
        {
            word = word.Remove(word.Length - 1);

            char[] arr = word.ToCharArray();
            Array.Reverse(arr);
            word = String.Empty;
            foreach (var a in arr)
                word += a.ToString();
            return word;
        }        
        public string Encryption(string text)
        {
            string EcnryptedText = "";
            string Coefficients = "";
            double[] NewCoefficients = new double[] { };
            foreach (char c in text)
            {
                Coefficients += (Array.IndexOf(Alphabet, c).ToString() + " ");
            }
            Polynome = ToPolinomial(Coefficients);//StringReverse(Coefficients));
            NewCoefficients = Polynomial.Multiply(Polynome, FormativePolinome).Coeff.ToArray();//.Reverse();
            foreach (double coeff in NewCoefficients)
            {
                EcnryptedText += Alphabet[(int)coeff];
            }
            return EcnryptedText;
        }      
        public string Decryption(string text)
        {
            string DecryptedText = "";
            string Coefficients = "";
            double[] NewCoefficients = new double[] { };
            foreach (char c in text)
            {
                Coefficients += (Array.IndexOf(Alphabet, c).ToString() + " ");
            }
            Polynome = ToPolinomial(Coefficients);
            NewCoefficients = Division(Polynome, FormativePolinome).Coeff.ToArray();
            foreach (double coeff in NewCoefficients)
            {
                DecryptedText += Alphabet[(int)coeff];
            }
            return DecryptedText;
        }
        public Polynomial Division(Polynomial DividedPolynome, Polynomial DividerPolynome)
        {
            Polynomial FractionPolynome = new Polynomial();
            string coefs = "";
            List<double> DividedPolynomeCoeffs = DividedPolynome.Coeff.ToList();
            List<double> DividerPolynomeCoeffs = DividerPolynome.Coeff.ToList();
            int FractionPolynomeDegree = DividedPolynome.Degree - DividerPolynome.Degree;
            Polynomial NewDividerPolynome = new Polynomial();
            Polynomial RestPolynome = new Polynomial();
            int j = 0;
            List<double> FractionPolynomeCoeffs = new List<double>();
            do
            {
                for (int k = 0; k < FractionPolynomeDegree; k++)
                {
                    coefs += "0 ";
                }
                FractionPolynomeCoeffs.Insert(0, DividedPolynomeCoeffs[DividedPolynomeCoeffs.Count - 1] / DividerPolynomeCoeffs[DividerPolynomeCoeffs.Count - 1]);
                coefs += (DividedPolynomeCoeffs[DividedPolynomeCoeffs.Count - 1] / DividerPolynomeCoeffs[DividerPolynomeCoeffs.Count - 1]).ToString() + " ";
                FractionPolynome = new Polynomial(new DoubleVector(coefs));
                NewDividerPolynome = Polynomial.Multiply(FractionPolynome, DividerPolynome);
                RestPolynome = Polynomial.Subtract(DividedPolynome, NewDividerPolynome);
                DividedPolynome = RestPolynome;
                DividedPolynomeCoeffs = DividedPolynome.Coeff.ToList();
                FractionPolynomeDegree--;
                coefs = String.Empty;
                j++;
            } while ((RestPolynome.Degree != 0));
            FractionPolynomeCoeffs.Insert(0, DividedPolynomeCoeffs[DividedPolynomeCoeffs.Count - 1] / DividerPolynomeCoeffs[DividerPolynomeCoeffs.Count - 1]);
            string FractionPolynomeCoefficients = String.Empty;
            foreach (var coef in FractionPolynomeCoeffs)
            {
                FractionPolynomeCoefficients += Convert.ToString(coef) + " ";
            }
            DoubleVector FractionPolynomeVector = new DoubleVector(FractionPolynomeCoefficients);
            FractionPolynome = new Polynomial(FractionPolynomeVector);
            return FractionPolynome;
        }
    }
}
