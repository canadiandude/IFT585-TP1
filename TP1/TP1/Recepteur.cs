using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP1
{
    class Recepteur
    {
        private ListBox affichage;
        private SupportTransmission support;
        private FileStream writer;

        public Recepteur(ListBox lbx, SupportTransmission sup)
        {
            affichage = lbx;
            support = sup;
            writer = new FileStream("new.txt", FileMode.Create);
        }

        public void Traiter()
        {
            Trame trame = new Trame(255, TYPE_TRAME.DATA);
            while (!trame.IsEnd())
            {
                if (support.DonneeRecue)
                {
                    trame = support.Recevoir();
                    if (trame.IsEnd()) break; // End of transmission
                    afficher("Reçue : " + trame.ToString());
                    writer.WriteByte(trame.Data);
                }
            }
            afficher("Fin du thread Recepteur");

            writer.Close();
            writer.Dispose();
        }

        private void afficher(String msg)
        {
            affichage.Items.Add(msg);
            affichage.TopIndex = affichage.Items.Count - 1;
        }
    }
}
