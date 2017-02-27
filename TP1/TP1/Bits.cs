using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1
{
    class Bits
    {
        private bool[] bits;

        public Bits(int num)
        {
            bits = new bool[BitLength(num)];
            for (int i = 0; i < bits.Length; ++i)
            {
                bits[i] = (num & 1 << i) != 0 ? true : false;
            }

            Array.Reverse(bits);
        }

        public static int BitLength(int num)
        {
            if (num == 0) return 1;
            int count = 0;
            while (num > 0)
            {
                count++;
                num = num >> 1;
            }
            return count;
        }

        public override string ToString()
        {
            String str = "";
            foreach (bool b in bits) str += b ? "1" : "0";
            return str;
        }

        public static Bits Codifier(Bits data)
        {
            Bits code = new Bits(0);
            code.bits = new bool[data.bits.Length + BitLength(data.bits.Length)];

            int bitIndex = 0;
            for (int i = 0; i < code.bits.Length; ++i)
            {
                if (!Puissance2(i + 1))
                {
                    code.bits[i] = data.bits[bitIndex++];
                }
            }

            for (int p = 0; p < BitLength(data.bits.Length); ++p)
            {
                int parity = (int)Math.Pow(2, p);
                int sum = 0;
                for (int i = 1; i <= code.bits.Length; ++i)
                {
                    if ((i & parity) != 0 && code.bits[i - 1])
                    {
                        ++sum;
                    }
                }
                code.bits[parity - 1] = sum % 2 != 0;
            }

            return code;
        }

        public static bool Puissance2(int x)
        {
            return (x & (x - 1)) == 0;
        }
    }
}
