using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using _4_wins.Properties;
using System.Drawing;

namespace _4_wins
{
    class pins
    {
        public int Spalte { get { return aktuelleSpalte[aktuelleReihe]; } set { aktuelleSpalte[aktuelleReihe] = value; } }

        PictureBox[,] meinFeld = new PictureBox[7, 7];
        int aktuelleReihe = 0;
        int[] aktuelleSpalte = new int[7];
        public PictureBox stein;

        public void AddPin(Bitmap farbe)
        {
            stein = new PictureBox();          
            meinFeld[aktuelleReihe, aktuelleSpalte[aktuelleReihe]] = new PictureBox();
            stein = meinFeld[aktuelleReihe, aktuelleSpalte[aktuelleReihe]]; 
            stein.Image = farbe;
            aktuelleReihe++;
        }
    }
}
