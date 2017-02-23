using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP1
{
    // Thread 3
    class SupportTransmission
    {
        private int envoie;
        private int reception;
        public bool SourcePrete;
        public bool DonneeRecue;
        private ListBox affichage;

        public SupportTransmission(ListBox lbx)
        {
            affichage = lbx;
            SourcePrete = true;
            DonneeRecue = false;
            envoie = -42;
        }

        public void Traiter()
        {
            while (true)
            {
                Thread.Sleep(1000);
                if (!SourcePrete && !DonneeRecue)
                {
                    reception = envoie;
                    SourcePrete = DonneeRecue = true;
                }
            }
        }

        public void Emettre(int data)
        {
            envoie = data;
            afficher("Trame recue : " + envoie.ToString());
            SourcePrete = false;
        }

        public int Recevoir()
        {
            DonneeRecue = false;
            afficher("Trame envoyée : " + reception.ToString());
            return reception;
        }

        private void afficher(String msg)
        {
            affichage.Items.Add(msg);
            affichage.TopIndex = affichage.Items.Count - 1;
        }
    }
}
