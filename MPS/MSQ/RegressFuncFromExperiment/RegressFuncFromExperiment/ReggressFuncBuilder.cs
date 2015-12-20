using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegressFuncFromExperiment
{
    public class ReggressFuncBuilder
    {
        public string[] input;

        public double[,] xyValues;

        public List<int> ParralellsPositions = new List<int>();

        public double[] HardLineParametres;

        public double HardLineDispers;

        public double HardLineFisher;

        public double AlterHardLineFisher;

        public double[] SoftLineParametres;

        public double SoftLineDispers;

        public double SoftLineFisher;

        public double AlterSoftLineFisher;

        public double[] ParabolaParametres;

        public double ParabolaDispers;

        public double ParabolaFisher;

        public double AlterParabolaFisher;

        public double VupadDispers;

        public double SeredDuspers;

        public ReggressFuncBuilder() { }

        public ReggressFuncBuilder(string Path)
        {
            input = GetInput(Path);
            GetXValuesAndYValues(3, 20);
            GetParrallel();
            HardLineParametres = GetHardLineParametres();
            SoftLineParametres = GetSoftLineParametres();
            ParabolaParametres = GetParabolaParametres();
            HardLineDispers = GetHardLineZalDuspers();
            ParabolaDispers = GetParabolaZalDuspers();
            SoftLineDispers = GetSoftLineZalDuspers();
            VupadDispers = GetVupadPomulka();
            SeredDuspers = GetSeredDuspers();
            HardLineFisher = GetHardLineFisher();
            SoftLineFisher = GetSoftLineFisher();
            ParabolaFisher = GetParabolaFisher();
            AlterHardLineFisher = GetAlterHardLineFisher();
            AlterSoftLineFisher = GetAlterSoftLineFisher();
            AlterParabolaFisher = GetAlterParabolaFisher();
        }

        private string[] GetInput(string Path)
        {
            return System.IO.File.ReadAllLines(Path);
        }

        private void GetXValuesAndYValues(int XQuantity, int ExperimentQuantity)
        {
            xyValues = new double[ExperimentQuantity, XQuantity + 1];
            for (int i = 0; i < ExperimentQuantity; i++)
            {
                string[] buffer = input[i].Split(' ');
                for (int j = 0; j < XQuantity + 1; j++)
                {
                    xyValues[i, j] = Double.Parse(buffer[j]);
                }
            }
        }

        private void GetParrallel()
        {
            for (int i = 0; i < xyValues.GetLength(0); i++)
            {
                for (int k = 0; k < xyValues.GetLength(0); k++)
                {
                    if (i != k && xyValues[i, 0] == xyValues[k, 0] && xyValues[i, 1] == xyValues[k, 1]
                        && xyValues[i, 2] == xyValues[k, 2])
                    {
                        if (ParralellsPositions.Count == 0)
                        {
                            ParralellsPositions.Add(i);
                        }
                        ParralellsPositions.Add(k);
                    }
                }
                if (ParralellsPositions.Count > 0)
                    break;
            }
        }

        private double[,] GetTransMatrix(double[,] inputMatrix)
        {
            double[,] outputMatrix = new double[inputMatrix.GetLength(1), inputMatrix.GetLength(0)];
            for (int i = 0; i < inputMatrix.GetLength(1); i++)
            {
                for (int j = 0; j < inputMatrix.GetLength(0); j++)
                {
                    outputMatrix[i, j] = inputMatrix[j, i];
                }
            }
            return outputMatrix;
        }

        private double[,] GetMultiplyMatrix(double[,] Matrix1, double[,] Matrix2)
        {
            double[,] OutputMatrix = new double[Matrix1.GetLength(0), Matrix2.GetLength(1)];
            for (int i = 0; i < OutputMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < OutputMatrix.GetLength(1); j++)
                {
                    double Sum = 0;
                    for (int k = 0; k < Matrix1.GetLength(1); k++)
                    {
                        Sum += (Matrix1[i, k] * Matrix2[k, j]);
                    }
                    OutputMatrix[i, j] = Sum;
                }
            }
            return OutputMatrix;
        }

        private double[,] GetReverseMatrix(double[,] Matrix)
        {
            int size = Matrix.GetLength(0);
            double[,] OutputMatrix = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                OutputMatrix[i, i] = 1;
            }
            for (int i = 0; i < size; i++)
            {
                double DelMulti = Matrix[i, i];
                for (int j = 0; j < size; j++)
                {
                    Matrix[i, j] /= DelMulti;
                    OutputMatrix[i, j] /= DelMulti;
                }
                for (int k = i + 1; k < size; k++)
                {
                    double multi = Matrix[k, i];
                    for (int z = 0; z < size; z++)
                    {

                        Matrix[k, z] = Matrix[k, z] - Matrix[i, z] * multi;
                        OutputMatrix[k, z] = OutputMatrix[k, z] - OutputMatrix[i, z] * multi;
                    }
                }
            }
            for (int i = size - 1; i >= 0; i--)
            {
                for (int k = i - 1; k >= 0; k--)
                {
                    double multi = Matrix[k, i];
                    for (int z = size - 1; z >= 0; z--)
                    {
                        Matrix[k, z] = Matrix[k, z] - Matrix[i, z] * multi;
                        OutputMatrix[k, z] = OutputMatrix[k, z] - OutputMatrix[i, z] * multi;
                    }
                }
            }
            return OutputMatrix;
        }

        private double[] GetHardLineParametres()
        {
            double[] Parametres = new double[8];
            double[,] InputMatrix = GetHardLineMatrix(xyValues);
            double[,] TransMatrix = GetTransMatrix(InputMatrix);
            double[,] MultyMatrix = GetMultiplyMatrix(TransMatrix, InputMatrix);
            double[,] ReverseMartrix = GetReverseMatrix(MultyMatrix);
            double[,] LastMultyMartrix = GetMultiplyMatrix(ReverseMartrix, TransMatrix);
            double[,] Yvalues = new double[20, 1];
            for (int i = 0; i < Yvalues.GetLength(0); i++)
            {
                Yvalues[i, 0] = xyValues[i, 3];
            }
            double[,] parametres = GetMultiplyMatrix(LastMultyMartrix, Yvalues);
            for (int i = 0; i < 8; i++)
            {
                Parametres[i] = parametres[i, 0];
            }
            return Parametres;
        }

        private double[,] GetHardLineMatrix(double[,] inputMatrix)
        {
            double[,] outputMatrix = new double[20, 8];
            for (int i = 0; i < 20; i++)
            {
                outputMatrix[i, 0] = 1;
                outputMatrix[i, 1] = inputMatrix[i, 0];
                outputMatrix[i, 2] = inputMatrix[i, 1];
                outputMatrix[i, 3] = inputMatrix[i, 2];
                outputMatrix[i, 4] = outputMatrix[i, 1] * outputMatrix[i, 2];
                outputMatrix[i, 5] = outputMatrix[i, 1] * outputMatrix[i, 3];
                outputMatrix[i, 6] = outputMatrix[i, 2] * outputMatrix[i, 3];
                outputMatrix[i, 7] = outputMatrix[i, 1] * outputMatrix[i, 2] * outputMatrix[i, 3];
            }
            return outputMatrix;
        }

        private double[] GetSoftLineParametres()
        {
            double[] Parametres = new double[4];
            double[,] InputMatrix = GetSoftLineMatrix(xyValues);
            double[,] TransMatrix = GetTransMatrix(InputMatrix);
            double[,] MultyMatrix = GetMultiplyMatrix(TransMatrix, InputMatrix);
            double[,] ReverseMartrix = GetReverseMatrix(MultyMatrix);
            double[,] LastMultyMartrix = GetMultiplyMatrix(ReverseMartrix, TransMatrix);
            double[,] Yvalues = new double[20, 1];
            for (int i = 0; i < Yvalues.GetLength(0); i++)
            {
                Yvalues[i, 0] = xyValues[i, 3];
            }
            double[,] parametres = GetMultiplyMatrix(LastMultyMartrix, Yvalues);
            for (int i = 0; i < 4; i++)
            {
                Parametres[i] = parametres[i, 0];
            }
            return Parametres;
        }

        private double[,] GetSoftLineMatrix(double[,] inputMatrix)
        {
            double[,] outputMatrix = new double[20, 4];
            for (int i = 0; i < 20; i++)
            {
                outputMatrix[i, 0] = 1;
                outputMatrix[i, 1] = inputMatrix[i, 0];
                outputMatrix[i, 2] = inputMatrix[i, 1];
                outputMatrix[i, 3] = inputMatrix[i, 2];
            }
            return outputMatrix;
        }

        private double[] GetParabolaParametres()
        {
            double[] Parametres = new double[10];
            double[,] InputMatrix = GetParabolaMatrix(xyValues);
            double[,] TransMatrix = GetTransMatrix(InputMatrix);
            double[,] MultyMatrix = GetMultiplyMatrix(TransMatrix, InputMatrix);
            double[,] ReverseMartrix = GetReverseMatrix(MultyMatrix);
            double[,] LastMultyMartrix = GetMultiplyMatrix(ReverseMartrix, TransMatrix);
            double[,] Yvalues = new double[20, 1];
            for (int i = 0; i < Yvalues.GetLength(0); i++)
            {
                Yvalues[i, 0] = xyValues[i, 3];
            }
            double[,] parametres = GetMultiplyMatrix(LastMultyMartrix, Yvalues);
            for (int i = 0; i < 10; i++)
            {
                Parametres[i] = parametres[i, 0];
            }
            return Parametres;
        }

        private double[,] GetParabolaMatrix(double[,] inputMatrix)
        {
            double[,] outputMatrix = new double[20, 10];
            for (int i = 0; i < 20; i++)
            {
                outputMatrix[i, 0] = 1;
                outputMatrix[i, 1] = inputMatrix[i, 0];
                outputMatrix[i, 2] = inputMatrix[i, 1];
                outputMatrix[i, 3] = inputMatrix[i, 2];
                outputMatrix[i, 4] = outputMatrix[i, 1] * outputMatrix[i, 1];
                outputMatrix[i, 5] = outputMatrix[i, 2] * outputMatrix[i, 2];
                outputMatrix[i, 6] = outputMatrix[i, 3] * outputMatrix[i, 3];
                outputMatrix[i, 7] = outputMatrix[i, 1] * outputMatrix[i, 2];
                outputMatrix[i, 8] = outputMatrix[i, 1] * outputMatrix[i, 3];
                outputMatrix[i, 9] = outputMatrix[i, 2] * outputMatrix[i, 3];
            }
            return outputMatrix;
        }

        private double GetHardLineZalDuspers()
        {
            double duspers = 0;
            for (int i = 0; i < 20; i++)
            {
                duspers += Math.Pow(xyValues[i, 3] - (HardLineParametres[0] + HardLineParametres[1] * xyValues[i, 0] +
                    HardLineParametres[2] * xyValues[i, 1] + HardLineParametres[3] * xyValues[i, 2] +
                    HardLineParametres[4] * xyValues[i, 0] * xyValues[i, 1] +
                    HardLineParametres[5] * xyValues[i, 0] * xyValues[i, 2] +
                    HardLineParametres[6] * xyValues[i, 1] * xyValues[i, 2] +
                     HardLineParametres[7] * xyValues[i, 1] * xyValues[i, 2] * xyValues[i, 0]), 2);
            }
            duspers /= 12;
            return duspers;
        }

        private double GetParabolaZalDuspers()
        {
            double duspers = 0;
            for (int i = 0; i < 20; i++)
            {
                duspers += Math.Pow(xyValues[i, 3] - (ParabolaParametres[0] + ParabolaParametres[1] * xyValues[i, 0] +
                    ParabolaParametres[2] * xyValues[i, 1] + ParabolaParametres[3] * xyValues[i, 2] +
                    ParabolaParametres[4] * xyValues[i, 0] * xyValues[i, 0] +
                    ParabolaParametres[5] * xyValues[i, 1] * xyValues[i, 1] +
                    ParabolaParametres[6] * xyValues[i, 2] * xyValues[i, 2] +
                    ParabolaParametres[7] * xyValues[i, 0] * xyValues[i, 1] +
                    ParabolaParametres[8] * xyValues[i, 0] * xyValues[i, 2] +
                    ParabolaParametres[9] * xyValues[i, 1] * xyValues[i, 2]), 2);
            }
            duspers /= 10;
            return duspers;
        }

        private double GetSoftLineZalDuspers()
        {
            double duspers = 0;
            for (int i = 0; i < 20; i++)
            {
                duspers += Math.Pow(xyValues[i, 3] - (SoftLineParametres[0] + SoftLineParametres[1] * xyValues[i, 0] +
                    SoftLineParametres[2] * xyValues[i, 1] + SoftLineParametres[3] * xyValues[i, 2]), 2);
            }
            duspers /= 16;
            return duspers;
        }

        private double GetVupadPomulka()
        {
            double result = 0;
            double sered = 0;
            for (int i = 0; i < ParralellsPositions.Count; i++)
            {
                sered += xyValues[ParralellsPositions[i], 3];
            }
            sered /= ParralellsPositions.Count;
            for (int i = 0; i < ParralellsPositions.Count; i++)
            {
                result += Math.Pow(xyValues[ParralellsPositions[i], 3] - sered, 2);
            }
            result /= (ParralellsPositions.Count - 1);
            return result;
        }

        private double GetHardLineFisher()
        {
            return HardLineDispers / VupadDispers;
        }

        private double GetAlterHardLineFisher()
        {
            return SeredDuspers / HardLineDispers;
        }

        private double GetSoftLineFisher()
        {
            return SoftLineDispers / VupadDispers;
        }

        private double GetAlterSoftLineFisher()
        {
            return SeredDuspers / SoftLineDispers;
        }

        private double GetParabolaFisher()
        {
            return ParabolaDispers / VupadDispers;
        }

        private double GetAlterParabolaFisher()
        {
            return SeredDuspers / ParabolaDispers;
        }

        private double GetSeredDuspers()
        {
            double result = 0;
            double sered = 0;
            for (int i = 0; i < 20; i++)
            {
                sered += xyValues[i, 3];
            }
            sered /= 20;
            for (int i = 0; i < 20; i++)
            {
                result += Math.Pow((xyValues[i, 3] - sered), 2);
            }
            result /= 19;
            return result;
        }

    }
}
