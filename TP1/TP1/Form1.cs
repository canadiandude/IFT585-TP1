using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP1
{
    public partial class Form1 : Form
    {
        private Thread threadEmetteur, threadRecepteur, threadSupport;
        private Emetteur emetteur;
        private Recepteur recepteur;
        private SupportTransmission support;
        private bool threadStarted;

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (threadStarted)
            {
                threadRecepteur.Abort();
                threadEmetteur.Abort();
                threadSupport.Abort();
            }
        }

        private void errorBtn_Click(object sender, EventArgs e)
        {
            support.BreakTrame();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (threadStarted)
            {
                threadRecepteur.Abort();
                threadEmetteur.Abort();
                threadSupport.Abort();
            }
        }

        public Form1()
        {
            InitializeComponent();
            threadStarted = false;
        }

        private void BTN_Start_Click(object sender, EventArgs e)
        {
            support = new SupportTransmission(LBX_Support);
            emetteur = new Emetteur(LBX_Emetteur, support);
            recepteur = new Recepteur(LBX_Recepteur, support);

            threadEmetteur = new Thread(emetteur.Traiter);
            threadRecepteur = new Thread(recepteur.Traiter);
            threadSupport = new Thread(support.Traiter);

            threadSupport.Start();
            threadRecepteur.Start();
            threadEmetteur.Start();
            threadStarted = true;
            BTN_Start.Enabled = false;
        }
    }
}
