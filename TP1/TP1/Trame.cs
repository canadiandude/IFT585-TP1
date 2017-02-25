using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1
{
    public enum TYPE_TRAME { DATA = 0, ACK, NAK, END };
    class Trame
    { 
        public byte Data;
        private byte type;

        public Trame(int d, TYPE_TRAME t)
        {
            Data = (byte)d;
            type = (byte)t;
        }

        public bool IsACK()
        {
            return type == (byte)TYPE_TRAME.ACK;
        }

        public bool IsNAK()
        {
            return type == (byte)TYPE_TRAME.NAK;
        }

        public bool IsEnd()
        {
            return type == (byte)TYPE_TRAME.END;
        }

        public override string ToString()
        {
            String str = "";
            switch((TYPE_TRAME)type)
            {
                case TYPE_TRAME.DATA: str = "DATA : "; break;
                case TYPE_TRAME.ACK: str = "ACK : "; break;
                case TYPE_TRAME.NAK: str = "NAK : "; break;
                case TYPE_TRAME.END: str = "END"; break;
            }

            if (!IsEnd())
            {
                str += Data.ToString();
            }

            return str;
        }
    }
}
