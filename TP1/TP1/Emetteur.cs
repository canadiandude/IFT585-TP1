using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP1
{
    class Emetteur
    {
        private ListBox affichage;
        private SupportTransmission support;
        private StreamReader reader;

        public Emetteur(ListBox lbx, SupportTransmission sup)
        {
            affichage = lbx;
            support = sup;
            reader = new StreamReader("test.txt");
        }

        public void Traiter()
        {
            int data;
            while (!reader.EndOfStream)
            {
                if (support.SourcePrete)
                {
                    data = reader.Read();
                    afficher("Envoie de la trame : " + data.ToString());
                    support.Emettre(data);
                }
            }

            while (!support.SourcePrete) ;
            afficher("Envoie du signal de fin");
            support.Emettre(256);
            afficher("Fin du thread Emetteur");

            reader.Close();
            reader.Dispose();
        }

        private void afficher(String msg)
        {
            affichage.Items.Add(msg);
            affichage.TopIndex = affichage.Items.Count - 1;
        }
    }
}
