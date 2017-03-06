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
        public byte Numero;
        public byte Data;
        public byte type;
        public DateTime stamp;

        public Trame(int num, int d, TYPE_TRAME t)
        {

            Numero = (byte)num;
            Data = (byte)d;
            type = (byte)t;
            stamp = DateTime.Now;
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
                case TYPE_TRAME.DATA: str = "#" + Numero.ToString() + " DATA : "; break;
                case TYPE_TRAME.ACK: str = "ACK : "; break;
                case TYPE_TRAME.NAK: str = "NAK : "; break;
                case TYPE_TRAME.END: str = "END"; break;
                default: str = "FAULT : "; break;
            }

            if (!IsEnd())
            {
                str += Data.ToString();
            }

            return str;
        }
    }
}
