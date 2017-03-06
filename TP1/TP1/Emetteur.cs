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
        private Trame[] fenetres;
        private int index;

        public Emetteur(ListBox lbx, SupportTransmission sup)
        {
            affichage = lbx;
            support = sup;
            reader = new FileStream(Config.ConfigInstance.CheminEntree, FileMode.Open);
            numTrame = 0;
            fenetres = new Trame[Int32.Parse(Config.ConfigInstance.FenetreTailleEmetteur)];
            index = -1;
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
                    // Verifier les timeout
                    int timeout = trouverTimeout();
                    if (timeout != -1)
                    {
                        index = timeout;
                        afficher("Timeout sur : " + fenetres[index].ToString());
                        resetTimeout();
                    }
                    // Lire le data et creer trame
                    else if (fenetres[index = (index + 1) % fenetres.Length] == null)
                    {
                        data = reader.ReadByte();
                        if (data == -1 && fenetreVide()) break; // End of file
                        if (data != -1)
                        {
                            //index = (index + 1) % fenetres.Length;
                            trame = new Trame(NumeroterTrame(), data, TYPE_TRAME.DATA);
                            fenetres[index] = trame;
                        }
                    }

                    if (fenetres[index] != null)
                    {
                        afficher("Index : " + index.ToString());
                        afficher("Envoyée : " + fenetres[index].ToString());
                        support.EmettreDonnee(Bits.Codifier(new Bits(fenetres[index])));
                    }
                }

                // Reception des ACK/NAK
                if (support.DonneeRecueSource)
                {
                    notif = Bits.Decoder(support.RecevoirNotif()).toTrame();
                    afficher("Reçue : " + notif.ToString());
                    retirerTrame(notif.Data);
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
            support.EmettreDonnee(Bits.Codifier(new Bits(new Trame(0, 255, TYPE_TRAME.END))));
            afficher("Fin du thread Emetteur");
        }

        private byte NumeroterTrame()
        {
            numTrame++;
            if (numTrame >= 256) numTrame = 0;
            return (byte)numTrame;
        }

        private int trouverTimeout()
        {
            DateTime now = DateTime.Now;

            for (int i = 0; i < fenetres.Length; ++i)
            {
                if (fenetres[i] != null && (now - fenetres[i].stamp) >= TimeSpan.FromMilliseconds(1500))
                    return i;
            }

            return -1;
        }

        private void retirerTrame(int num)
        {
            for (int i = 0; i < fenetres.Length; ++i)
            {
                if (fenetres[i] != null && fenetres[i].Numero == num)
                {
                    fenetres[i] = null;
                    break;
                }
            }
        }

        private void resetTimeout()
        {
            foreach (Trame t in fenetres)
            {
                if (t != null) t.stamp = DateTime.Now;
            }
        }

        private bool fenetreVide()
        {
            foreach (Trame t in fenetres)
                if (t != null) return false;
            return true;
        }

        private void afficher(String msg)
        {
            affichage.Items.Add(msg);
            affichage.TopIndex = affichage.Items.Count - 1;
        }
    }
}
