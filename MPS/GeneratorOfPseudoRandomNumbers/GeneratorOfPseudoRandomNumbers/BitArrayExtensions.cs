using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorOfPseudoRandomNumbers
{
    public static class BitArrayExtensions
    {
        public static BitArray ShiftArithmeticallyRight(this BitArray bitArray, int count)
        {
            if (bitArray == null)
            {
                throw new ArgumentNullException("bitArray");
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count");
            }

            for (var i = 0; i < bitArray.Length - count; i++)
            {
                bitArray[i] = bitArray[i + count];
            }

            for (var i = bitArray.Length - count; i < bitArray.Length; i++)
            {
                bitArray[i] = false;
            }

            return bitArray;
        }

        public static BitArray ShiftArithmeticallyLeft(this BitArray bitArray, int count)
        {
            if (bitArray == null)
            {
                throw new ArgumentNullException("bitArray");
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count");
            }

            for (var i = bitArray.Length - 1; i >= count; i--)
            {
                bitArray[i] = bitArray[i - count];
            }

            for (var i = count - 1; i >= 0; i--)
            {
                bitArray[i] = false;
            }

            return bitArray;
        }
    }
}
