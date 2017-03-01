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
        private FileStream reader;
        private int numTrame;
        
        public Emetteur(ListBox lbx, SupportTransmission sup)
        {
            affichage = lbx;
            support = sup;
            reader = new FileStream(Config.ConfigInstance.CheminEntree, FileMode.Open);
            numTrame = 0;
        }

        public void Traiter()
        {
            int data = 0;
            Trame trame = new Trame(0, 255, TYPE_TRAME.DATA);
            Trame notif = new Trame(0, 255, TYPE_TRAME.DATA);
            while (data >= 0)
            {
                if (support.PretEmettreSource)
                {
                    data = reader.ReadByte();
                    if (data == -1) break; // End of file
                    trame = new Trame(NumeroterTrame(), data, TYPE_TRAME.DATA);
                    afficher("Envoyée : " + trame.ToString());
                    support.EmettreDonnee(trame);
                }

                if (support.DonneeRecueSource)
                {
                    notif = support.RecevoirNotif();
                    afficher("Reçue : " + notif.ToString());
                }
            }

            EnvoyerSignalFin();

            reader.Close();
            reader.Dispose();
        }

        private void EnvoyerSignalFin()
        {
            while (!support.PretEmettreSource) ;
            afficher("Envoie du signal de fin");
            support.EmettreDonnee(new Trame(0, 255, TYPE_TRAME.END));
            afficher("Fin du thread Emetteur");
        }

        private byte NumeroterTrame()
        {
            numTrame++;
            if (numTrame >= 256) numTrame = 0;
            return (byte)numTrame;
        }

        private void afficher(String msg)
        {
            affichage.Items.Add(msg);
            affichage.TopIndex = affichage.Items.Count - 1;
        }
    }
}
