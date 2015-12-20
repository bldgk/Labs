using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorOfPseudoRandomNumbers
{
	public class Shuffle : IGenerator
	{
		/// <summary>
		/// Початкове число R0.
		/// </summary>
		public double R0
		{
			get; set;
		}

		public int B
		{
			get; set;
		}
		/// <summary>
		/// Степінь двійки.
		/// </summary>
		public int Degree
		{
			get; set;
		}
		/// <summary>
		/// Кількість тестів.
		/// </summary>
		public int CountOfTests
		{
			get; set;
		}
		public Shuffle()
		{
		}
		/// <summary>
		/// Конструктор з параметрами R0, b, та степінь двійки.
		/// </summary>
		/// <param name="r0"></param>
		/// <param name="b"></param>
		/// <param name="degree"></param>
		public Shuffle(double r0, int b, int degree, int countOfTests)
		{
			R0 = r0;
			B = b;
			Degree = degree;
			CountOfTests = countOfTests;
		}
		/// <summary>
		/// Метод, що обраховує криетерій згоди Хі.
		/// </summary>
		/// <param name="frequencies"></param>
		/// <returns></returns>

		public double CriterionX(List<int> frequencies)
		{
			double xi = 0;
			frequencies.ForEach(p =>
				xi += (Math.Pow(p / (double)CountOfTests - 0.1, 2) / 0.1));
			return xi;
		}
		/// <summary>
		/// Метод, що повертає список з частотами попаднь чисел в певний інтервал.
		/// </summary>
		/// <param name="countOfTests"></param>
		/// <returns></returns>
		public List<int> FrequenciesTest()
		{
			double x;
			x = R0;
			//Список інтервалів. Кількість 10.
			List<int> Frequencies = new List<int>(new int[10]);
			for(int i = 0; i < CountOfTests; i++)
			{
				//Генерація нового числа.
				x = Method(x);
				double x1 = Math.Abs(Math.Cos(Math.Log10(x)));
				int k = 0;
				//Визначення інтервали, в який попало нове згенероване число х.
				for(double j = 0.1; j < 1; j += 0.1)
				{
					if(x1 <= Math.Round(j, 2))
					{
						Frequencies[k]++;
						break;
					}
					k++;
				}

			}
			return Frequencies;
		}

		/// <summary>
		///  Метод перемішування.
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		public double Method(double x)
		{
			var q = BitConverter.GetBytes(x);
			BitArray bits = new BitArray(q);
			var left = bits.Clone() as BitArray;
			var right = bits.Clone() as BitArray;
			//Кількість розрядів для здвигу.
			var offset = 2;
			//Здвиг
			for(int i = 0; i < offset; i++)
			{
				left = ShiftLeft(left);
				right = ShiftRight(right);
			}
			//Додавання лівої та правої частин.
			var newRandomNumberBitArray = Add(left, right);
			//Переведення масиву бітів в десяткове число.
			var newRandomNumber = BitConverter.ToDouble(BitArrayToByteArray(newRandomNumberBitArray), 0);
			var newnubmer1 = BitConverter.ToDouble(BitArrayToByteArray(left), 0);
			var newnumber2 = BitConverter.ToDouble(BitArrayToByteArray(right), 0);
			var sum = newnubmer1 + newnumber2;
			return Math.Abs(newRandomNumber);
		}
		/// <summary>
		/// Складання двох бітів.
		/// </summary>
		/// <param name="one"></param>
		/// <param name="two"></param>
		/// <param name="temp"></param>
		/// <returns></returns>
		public bool add(bool one, bool two, ref bool temp)
		{
			var value = one ^ two ^ temp;
			if(one == two == true)
				temp = true;
			else
				temp = false;
			return value;
		}
		/// <summary>
		/// Додавання масивів біітв.
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <returns></returns>
		public BitArray Add(BitArray first, BitArray second)
		{
			BitArray newarr = new BitArray(new bool[64]);
			var current = true;
			var temp = false;
			for(int i = 63; i >= 0; i--)
			{
				current = add(first[i], second[i], ref temp);
				newarr[i] = current;
			}
			return newarr;
		}
		/// <summary>
		/// Метод, що конвертує масив бітів в масив байтів.
		/// </summary>
		/// <param name="bits"></param>
		/// <returns></returns>
		public static byte[] BitArrayToByteArray(BitArray bits)
		{
			byte[] ret = new byte[(bits.Length - 1) / 8 + 1];
			bits.CopyTo(ret, 0);
			return ret;
		}
		/// <summary>
		/// Здвиг вправо.
		/// </summary>
		/// <param name="aSource"></param>
		/// <returns></returns>
		public BitArray ShiftRight(BitArray aSource)
		{
			bool[] new_arr = new bool[(aSource.Count)];
			for(int i = 0; i < aSource.Count - 1; i++)
				new_arr[i + 1] = aSource[i];
			new_arr[0] = aSource[aSource.Count - 1];
			return new BitArray(new_arr);
		}
		/// <summary>
		/// Здвиг вліво.
		/// </summary>
		/// <param name="aSource"></param>
		/// <returns></returns>
		public BitArray ShiftLeft(BitArray aSource)
		{
			bool[] new_arr = new bool[(aSource.Count)];
			for(int i = 1; i < aSource.Count; i++)
			{
				new_arr[i - 1] = aSource[i];
			}
			new_arr[aSource.Count - 1] = aSource[0];
			return new BitArray(new_arr);
		}
		/// <summary>
		/// Вивід масиву бітів на екран.
		/// </summary>
		/// <param name="aSource"></param>
		/// <returns></returns>
		string PrintBitArray(BitArray aSource)
		{
			StringBuilder sb = new StringBuilder();
			foreach(var bit in aSource)
			{
				sb.Append((bool)bit ? 1 : 0);
			}
			return sb.ToString();
		}
	}
}