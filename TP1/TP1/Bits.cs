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
                bits[i] = (num & 1 << i) != 0;
            }

            Array.Reverse(bits);
        }

        public Bits(bool[] b) { bits = b; }

        public override string ToString()
        {
            String str = "";
            foreach (bool b in bits) str += b ? "1" : "0";
            return str;
        }

        public void Flip(int pos)
        {
            bits[pos] = !bits[pos];
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

        public static bool Puissance2(int x)
        {
            return (x & (x - 1)) == 0;
        }

        public static bool EstPair(int x)
        {
            return x % 2 == 0;
        }

        public static Bits Codifier(int num)
        {
            return Codifier(new Bits(num));
        }

        public static Bits Codifier(Bits data)
        {
            Bits code = new Bits(new bool[data.bits.Length + BitLength(data.bits.Length)]);

            for (int i = 0, bitIndex = 0; i < code.bits.Length; ++i)
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
                    // Pour Pos 1 : 1,3,5,7... et pour pos 2:s 2,3,6,7,10...
                    // Tout les nombres qui ont ce bit allume
                    if ((i & parity) != 0 && code.bits[i - 1])
                    {
                        ++sum;
                    }
                }
                code.bits[parity - 1] = !EstPair(sum);
            }

            return code;
        }

        public static Bits Extraire(Bits code)
        {
            Bits data = new Bits(new bool[code.bits.Length - BitLength(code.bits.Length)]);

            for (int i = 0, bitIndex = 0; i < code.bits.Length; ++i)
            {
                if (!Puissance2(i + 1))
                {
                    data.bits[bitIndex++] = code.bits[i];
                }
            }

            return data;
        }

        public static int Verifier(Bits code)
        {
            int badBit = 0;

            for (int p = 0; p < BitLength(code.bits.Length); ++p)
            {
                int parity = (int)Math.Pow(2, p);
                int sum = 0;
                for (int i = 1; i <= code.bits.Length; ++i)
                {
                    if (i != parity && (i & parity) != 0 && code.bits[i - 1])
                    {
                        ++sum;
                    }
                }

                if (code.bits[parity - 1] == EstPair(sum))
                {
                    badBit += parity;
                }
            }

            return badBit;
        }

        public static Bits Decoder(Bits code)
        {
            int badBit = Verifier(code);
            if (badBit != 0)
                code.Flip(badBit - 1);
            return Extraire(code);
        }
    }
}
