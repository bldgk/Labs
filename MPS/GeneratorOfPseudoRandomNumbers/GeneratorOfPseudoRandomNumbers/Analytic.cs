	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	namespace GeneratorOfPseudoRandomNumbers
	{
		public class Analytic : IGenerator
		{
			/// <summary>
			/// Степінь двійки
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
			/// <summary>
			/// Коефіцієнт К
			/// </summary>
			public int K
			{
				get; set;
			}
			/// <summary>
			/// Приріст R
			/// </summary>
			public int R
			{
				get; set;
			}
			/// <summary>
			/// Початкове Х0
			/// </summary>
			public double X0
			{
				get; set;
			}

			public Analytic()
			{

			}
			/// <summary>
			/// Конструктор з параметрами степінь, коефіцієнт, приріст і початкове значення
			/// </summary>
			/// <param name="degree"></param>
			/// <param name="k"></param>
			/// <param name="r"></param>
			/// <param name="x0"></param>
			public Analytic(int degree, int k, int r, double x0,int countOfTests)
			{
				Degree = degree;
				K = k;
				R = r;
				X0 = x0;
				CountOfTests = countOfTests;
			}
			/// <summary>
			/// Аналітичний метод.
			/// </summary>
			/// <param name="x"></param>
			/// <returns></returns>
			public double Method(double x)
			{
				return (K * x + R) % Math.Pow(2, Degree);
			}
			/// <summary>
			/// Метод, що обраховує криетерій згоди Хі.
			/// </summary>
			/// <param name="frequencies"></param>
			/// <returns></returns>
			public double CriterionX(List<int> frequencies)
			{

				double xi = 0;
				//frequencies = GenerationOfNumbers(CountOfTests);
				//Обрахунок Хі, 0.1 - ймовірність попадання числа в певний інтервал
				frequencies.ForEach(p =>
					xi += (Math.Pow(p / (double)CountOfTests - 0.1, 2) / 0.1));
				return xi;
			}
			/// <summary>
			/// Метод, що повертає список з частотами попаднь чисел в певний інтервал.
			/// </summary>
			/// <returns></returns>
			public List<int> FrequenciesTest()
			{
				double x = X0;
				//Список інтервалів. Кількість 10.
				List<int> Frequencies = new List<int>(new int[10]);
				for (int i = 0; i < CountOfTests; i++)
				{
					//Генерація нового числа.
					x = Method(x);
					double x1 = x / Math.Pow(2, Degree);
					int k = 0;
					//Визначення інтервали, в який попало нове згенероване число х.
					for (double j = 0.1; j < 1; j += 0.1)
					{
						if (x1 < j)
						{
							Frequencies[k]++;
							break;
						}
						k++;
					}
				}
				return Frequencies;
			}
		}
	}