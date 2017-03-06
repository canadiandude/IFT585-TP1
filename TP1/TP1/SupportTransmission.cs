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
        private bool isBreak;
        Random r = new Random();

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
                Thread.Sleep(500);
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
            if (isBreak)
            {
                int pos = r.Next(ReceptionDestination.Length);
                ReceptionDestination.Flip(pos);
                afficher("Envoyée avec erreur : " + afficherTrame(ReceptionDestination));
                isBreak = false;
            }
            else
            {
                afficher("Envoyée : " + afficherTrame(ReceptionDestination));
            }
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


        private String afficherTrame(Bits bits)
        {
            return Bits.Extraire(bits).toTrame().ToString();
        }

        public void BreakTrame()
        {
            isBreak = true;
        }

        private void afficher(String msg)
        {
            affichage.Items.Add(msg);
            affichage.TopIndex = affichage.Items.Count - 1;
        }
    }
}
