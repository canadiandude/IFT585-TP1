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
        private Trame envoie;
        private Trame reception;
        public bool SourcePrete;
        public bool DonneeRecue;
        private ListBox affichage;

        public SupportTransmission(ListBox lbx)
        {
            affichage = lbx;
            SourcePrete = true;
            DonneeRecue = false;
            envoie = new Trame((byte)255, TYPE_TRAME.DATA);
        }

        public void Traiter()
        {
            while (true)
            {
                Thread.Sleep(250);
                if (!SourcePrete && !DonneeRecue)
                {
                    reception = envoie;
                    SourcePrete = DonneeRecue = true;
                }
            }
        }

        public void Emettre(Trame data)
        {
            envoie = data;
            afficher("Reçue : " + envoie.ToString());
            SourcePrete = false;
        }

        public Trame Recevoir()
        {
            DonneeRecue = false;
            afficher("Envoyée : " + reception.ToString());
            return reception;
        }

        private void afficher(String msg)
        {
            affichage.Items.Add(msg);
            affichage.TopIndex = affichage.Items.Count - 1;
        }
    }
}
