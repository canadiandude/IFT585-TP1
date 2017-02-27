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
        private Trame EnvoieSource;
        private Trame ReceptionDestination;
        public bool PretEmettreSource;
        public bool DonneeRecueDestination;

        // Envoie des ACK/NAK Repecteur -> Emetteur
        private Trame EnvoieDestination;
        private Trame ReceptionSource;
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
            EnvoieSource = new Trame(0, (byte)255, TYPE_TRAME.DATA);
            EnvoieDestination = new Trame(0, (byte)255, TYPE_TRAME.DATA);
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

        public void EmettreDonnee(Trame data)
        {
            EnvoieSource = data;
            afficher("Reçue : " + EnvoieSource.ToString());
            PretEmettreSource = false;
        }

        public Trame RecevoirDonnee()
        {
            DonneeRecueDestination = false;
            afficher("Envoyée : " + ReceptionDestination.ToString());
            return ReceptionDestination;
        }

        public void EmettreNotif(Trame data)
        {
            EnvoieDestination = data;
            afficher("Reçue : " + EnvoieDestination.ToString());
            PretEmettreDestination = false;
        }

        public Trame RecevoirNotif()
        {
            DonneeRecueSource = false;
            afficher("Envoyée : " + ReceptionSource.ToString());
            return ReceptionSource;
        }

        private void afficher(String msg)
        {
            affichage.Items.Add(msg);
            affichage.TopIndex = affichage.Items.Count - 1;
        }
    }
}
