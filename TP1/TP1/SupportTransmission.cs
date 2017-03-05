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
        // Envoie du data Emetteur -> Recepteur
        private Bits EnvoieSource;
        private Bits ReceptionDestination;
        public bool PretEmettreSource;
        public bool DonneeRecueDestination;

        // Envoie des ACK/NAK Repecteur -> Emetteur
        private Bits EnvoieDestination;
        private Bits ReceptionSource;
        public bool PretEmettreDestination;
        public bool DonneeRecueSource;

        private ListBox affichage;

        public SupportTransmission(ListBox lbx)
        {
            affichage = lbx;
            PretEmettreSource = true;
            PretEmettreDestination = true;
            DonneeRecueDestination = false;
            DonneeRecueSource = false;
            EnvoieSource = new Bits(0);
            EnvoieDestination = new Bits(0);
        }

        public void Traiter()
        {
            while (true)
            {
                Thread.Sleep(250);
                if (!PretEmettreSource && !DonneeRecueDestination)
                {
                    ReceptionDestination = EnvoieSource;
                    PretEmettreSource = DonneeRecueDestination = true;
                }

                if (!PretEmettreDestination && !DonneeRecueSource)
                {
                    ReceptionSource = EnvoieDestination;
                    PretEmettreDestination = DonneeRecueSource = true;
                }
            }
        }

        public void EmettreDonnee(Bits data)
        {
            EnvoieSource = data;
            afficher("Reçue : " + afficherTrame(EnvoieSource));
            PretEmettreSource = false;
        }

        public Bits RecevoirDonnee()
        {
            DonneeRecueDestination = false;
            afficher("Envoyée : " + afficherTrame(ReceptionDestination));
            return ReceptionDestination;
        }

        public void EmettreNotif(Bits data)
        {
            EnvoieDestination = data;
            afficher("Reçue : " + afficherTrame(EnvoieDestination));
            PretEmettreDestination = false;
        }

        public Bits RecevoirNotif()
        {
            DonneeRecueSource = false;
            afficher("Envoyée : " + afficherTrame(ReceptionSource));
            return ReceptionSource;
        }

        private void afficher(String msg)
        {
            affichage.Items.Add(msg);
            affichage.TopIndex = affichage.Items.Count - 1;
        }

        private String afficherTrame(Bits bits)
        {
            return Bits.Decoder(bits).toTrame().ToString();
        }
    }
}
