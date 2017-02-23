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
        private StreamWriter writer;

        public Recepteur(ListBox lbx, SupportTransmission sup)
        {
            affichage = lbx;
            support = sup;
            writer = new StreamWriter("new.txt");
        }

        public void Traiter()
        {
            int data = -1;
            while (data < 256)
            {
                if (support.DonneeRecue)
                {
                    data = support.Recevoir();
                    if (data < 256)
                    {
                        afficher("Reception de de la trame : " + data.ToString());
                        writer.Write((char)data);
                    }
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
