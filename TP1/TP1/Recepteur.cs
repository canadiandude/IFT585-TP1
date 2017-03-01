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
            Config config = Config.ConfigInstance;
            writer = new FileStream(config.CheminSortie, FileMode.Create);
        }

        public void Traiter()
        {
            Trame trame = new Trame(0, 255, TYPE_TRAME.DATA);
            while (!trame.IsEnd())
            {
                if (support.DonneeRecueDestination)
                {
                    trame = support.RecevoirDonnee();
                    if (trame.IsEnd()) break; // End of transmission
                    afficher("Reçue : " + trame.ToString());
                    writer.WriteByte(trame.Data);

                    EnvoyerACK(trame.Numero);
                }
            }
            afficher("Fin du thread Recepteur");

            writer.Close();
            writer.Dispose();
        }

        private void EnvoyerACK(byte numero)
        {
            while (!support.PretEmettreDestination) ;
            support.EmettreNotif(new Trame(0, numero, TYPE_TRAME.ACK));
        }

        private void afficher(String msg)
        {
            affichage.Items.Add(msg);
            affichage.TopIndex = affichage.Items.Count - 1;
        }
    }
}
