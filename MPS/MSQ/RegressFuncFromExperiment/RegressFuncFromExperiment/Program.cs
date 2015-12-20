using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegressFuncFromExperiment
{
    class Program
    {
        static void Main(string[] args)
        {
            ReggressFuncBuilder builder = new ReggressFuncBuilder("data.txt");
            Console.WriteLine("Hipoteza 1 - RF linear");
            Console.WriteLine("b:");
            for (int i = 0; i < builder.SoftLineParametres.Count(); i++)
            {
                Console.WriteLine("B" + i + " = " + builder.SoftLineParametres[i]);
            }
            Console.WriteLine();Console.WriteLine("RF View: ");
            Console.WriteLine(BuildSoftFunc(builder));
            Console.WriteLine("Total dispersion: " + builder.SoftLineDispers);
            Console.WriteLine("Dispersion relatively to average: " + builder.SeredDuspers);
            Console.WriteLine("Random mistake: " + builder.VupadDispers);
            Console.WriteLine("Criterion Fisher(random mistake):" + builder.SoftLineFisher);
            Console.WriteLine("Criterion Fisher(Dispersion relatively to average):" + builder.AlterSoftLineFisher);
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Гипотеза2- РФ сложная линейная");
            Console.WriteLine("Коэфициенты b:");
            for (int i = 0; i < builder.HardLineParametres.Count(); i++)
            {
                Console.WriteLine("B" + i + " = " + builder.HardLineParametres[i]);
            }
            Console.WriteLine(); Console.WriteLine("РФ имеет вид: ");
            Console.WriteLine(BuildHardFunc(builder));
            Console.WriteLine("Остаточная дисперсия: " + builder.HardLineDispers);
            Console.WriteLine("Дисперсия относительно среднего: " + builder.SeredDuspers);
            Console.WriteLine("Случайная ошибка: " + builder.VupadDispers);
            Console.WriteLine("Критерий Фишера(случайная ошибка):" + builder.HardLineFisher);
            Console.WriteLine("Критерий Фишера(дисперсия относительно среднего):" + builder.AlterHardLineFisher);
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Гипотеза3- РФ параболическая");
            Console.WriteLine("Коэфициенты b:");
            for (int i = 0; i < builder.ParabolaParametres.Count(); i++)
            {
                Console.WriteLine("B" + i + " = " + builder.ParabolaParametres[i]);
            }
            Console.WriteLine(); Console.WriteLine("РФ имеет вид: ");
            Console.WriteLine(BuildParabolaFunc(builder));
            Console.WriteLine("Остаточная дисперсия: " + builder.ParabolaDispers);
            Console.WriteLine("Дисперсия относительно среднего: " + builder.SeredDuspers);
            Console.WriteLine("Случайная ошибка: " + builder.VupadDispers);
            Console.WriteLine("Критерий Фишера(случайная ошибка):" + builder.ParabolaFisher);
            Console.WriteLine("Критерий Фишера(дисперсия относительно среднего):" + builder.AlterParabolaFisher);
            Console.WriteLine("--------------------------------------------------------------");
            Console.ReadKey();
        }

        static string BuildSoftFunc(ReggressFuncBuilder builder)
        {
            StringBuilder result = new StringBuilder("");
            result.Append("y=" + builder.SoftLineParametres[0]);
            if (builder.SoftLineParametres[1] > 0)
                result.Append("+" + builder.SoftLineParametres[1].ToString() + "X1");
            else
                result.Append(builder.SoftLineParametres[1].ToString() + "X1");
            if (builder.SoftLineParametres[2]>0)
                result.Append("+" + builder.SoftLineParametres[2].ToString() + "X2");
            else
                result.Append(builder.SoftLineParametres[2].ToString() + "X2");
            if (builder.SoftLineParametres[3] > 0)
                result.Append("+" + builder.SoftLineParametres[3].ToString() + "X3");
            else
                result.Append(builder.SoftLineParametres[3].ToString() + "X3");

            return result.ToString();
        }

        static string BuildHardFunc(ReggressFuncBuilder builder)
        {
            StringBuilder result = new StringBuilder("");
            result.Append("y=" + builder.HardLineParametres[0]);
            if (builder.HardLineParametres[1] > 0)
                result.Append("+" + builder.HardLineParametres[1].ToString() + "X1");
            else
                result.Append(builder.HardLineParametres[1].ToString() + "X1");
            if (builder.HardLineParametres[2] > 0)
                result.Append("+" + builder.HardLineParametres[2].ToString() + "X2");
            else
                result.Append(builder.HardLineParametres[2].ToString() + "X2");
            if (builder.HardLineParametres[3] > 0)
                result.Append("+" + builder.HardLineParametres[3].ToString() + "X3");
            else
                result.Append(builder.HardLineParametres[3].ToString() + "X3");
            if (builder.HardLineParametres[4] > 0)
                result.Append("+" + builder.HardLineParametres[4].ToString() + "X1X2");
            else
                result.Append(builder.HardLineParametres[4].ToString() + "X1X2");
            if (builder.HardLineParametres[5] > 0)
                result.Append("+" + builder.HardLineParametres[5].ToString() + "X1X3");
            else
                result.Append(builder.HardLineParametres[5].ToString() + "X1X3");
            if (builder.HardLineParametres[6] > 0)
                result.Append("+" + builder.HardLineParametres[6].ToString() + "X2X3");
            else
                result.Append(builder.HardLineParametres[6].ToString() + "X2X3");
            if (builder.HardLineParametres[7] > 0)
                result.Append("+" + builder.HardLineParametres[5].ToString() + "X1X2X3");
            else
                result.Append(builder.HardLineParametres[5].ToString() + "X1X2X3");
            return result.ToString();
        }

        static string BuildParabolaFunc(ReggressFuncBuilder builder)
        {
            StringBuilder result = new StringBuilder("");
            result.Append("y=" + builder.ParabolaParametres[0]);
            if (builder.ParabolaParametres[1] > 0)
                result.Append("+" + builder.ParabolaParametres[1].ToString() + "X1");
            else
                result.Append(builder.ParabolaParametres[1].ToString() + "X1");
            if (builder.ParabolaParametres[2] > 0)
                result.Append("+" + builder.ParabolaParametres[2].ToString() + "X2");
            else
                result.Append(builder.ParabolaParametres[2].ToString() + "X2");
            if (builder.ParabolaParametres[3] > 0)
                result.Append("+" + builder.ParabolaParametres[3].ToString() + "X3");
            else
                result.Append(builder.ParabolaParametres[3].ToString() + "X3");
            if (builder.ParabolaParametres[4] > 0)
                result.Append("+" + builder.ParabolaParametres[4].ToString() + "X1X1");
            else
                result.Append(builder.ParabolaParametres[4].ToString() + "X1X1");
            if (builder.ParabolaParametres[5] > 0)
                result.Append("+" + builder.ParabolaParametres[5].ToString() + "X2X2");
            else
                result.Append(builder.ParabolaParametres[5].ToString() + "X2X2");
            if (builder.ParabolaParametres[6] > 0)
                result.Append("+" + builder.ParabolaParametres[6].ToString() + "X3X3");
            else
                result.Append(builder.ParabolaParametres[6].ToString() + "X3X3");
            if (builder.ParabolaParametres[7] > 0)
                result.Append("+" + builder.ParabolaParametres[7].ToString() + "X1X2");
            else
                result.Append(builder.ParabolaParametres[7].ToString() + "X1X2");
            if (builder.ParabolaParametres[8] > 0)
                result.Append("+" + builder.ParabolaParametres[8].ToString() + "X1X3");
            else
                result.Append(builder.ParabolaParametres[8].ToString() + "X1X3");
            if (builder.ParabolaParametres[9] > 0)
                result.Append("+" + builder.ParabolaParametres[9].ToString() + "X2X3");
            else
                result.Append(builder.ParabolaParametres[9].ToString() + "X2X3");
            return result.ToString();
        }
    }
}
