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
        private int prochaineTrame;

        public Recepteur(ListBox lbx, SupportTransmission sup)
        {
            affichage = lbx;
            support = sup;
            writer = new FileStream(Config.ConfigInstance.CheminSortie, FileMode.Create);
            prochaineTrame = 1;
        }

        public void Traiter()
        {
            Trame trame = new Trame(0, 255, TYPE_TRAME.DATA);
            while (!trame.IsEnd())
            {
                if (support.DonneeRecueDestination)
                {
                    Bits data = support.RecevoirDonnee();
                    if (Config.ConfigInstance.CodeCorrecteur || Bits.Verifier(data) == 0)
                    {
                        trame = Bits.Decoder(data).toTrame();
                        if (trame.IsEnd()) break; // End of transmission

                        if (trame.Numero == prochaineTrame)
                        {
                            afficher("Reçue : " + trame.ToString());
                            writer.WriteByte(trame.Data);

                            EnvoyerACK(trame.Numero);
                            prochaineTrame = (prochaineTrame + 1) % 256;
                        }
                        else
                        {
                            afficher("Trame rejetée");
                        }
                    }
                    else
                    {
                        afficher("ERREUR");
                    }
                }
            }
            afficher("Fin du thread Recepteur");

            writer.Close();
            writer.Dispose();
        }

        private void EnvoyerACK(byte numero)
        {
            while (!support.PretEmettreDestination) ;
            support.EmettreNotif(Bits.Codifier(new Bits(new Trame(0, numero, TYPE_TRAME.ACK))));
        }

        private void EnvoyerNAK(byte numero)
        {
            while (!support.PretEmettreDestination) ;
            support.EmettreNotif(Bits.Codifier(new Bits(new Trame(0, numero, TYPE_TRAME.NAK))));
        }

        private void afficher(String msg)
        {
            affichage.Items.Add(msg);
            affichage.TopIndex = affichage.Items.Count - 1;
        }
    }
}
