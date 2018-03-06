using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ConnectFour.Properties;
using System.Drawing;

namespace ConnectFour
{
    class Spielfeld
    {
        const int _anzGleiche = 4;
        int _x_max, _y_max;
        Farben[,] _farben;

        //  event Action xyz erfordert Linq?!
        public event Action<object> Spielerwechsel;
        public event Action<Farben> SpielVorbei;

        public int Height { get { return _y_max; } }
        public int Width { get { return _x_max; } }
        public Farben[,] Felder { get { return _farben; } set { _farben = value; } }

        public Spielfeld(int x, int y)
        {
            if (x < _anzGleiche) x = _anzGleiche;
            if (y < _anzGleiche) y = _anzGleiche;
            _x_max = x;
            _y_max = y;
            _farben = new Farben[x, y];

            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                    _farben[i, j] = Farben.Empty;
        }

        public int SetzeFarbe(Farben farbe, int reihe)
        {
            if (reihe >= _x_max || farbe == Farben.Empty) return -1;
            int neuerIndex = GetMaxValue(reihe);
            if (neuerIndex != -1)
            {
                _farben[reihe, neuerIndex] = farbe;
                if (Spielerwechsel != null) Spielerwechsel(null);
                return neuerIndex;
            }
            else MessageBox.Show("The column is full!");
            return -1;
        }
        private int GetMaxValue(int reihe)
        {
            Farben aktFarbe;
            int zähler = -1;
            do
            {
                zähler++;
                if (zähler < _y_max)
                    aktFarbe = _farben[reihe, zähler];
                else { zähler = -1; break; }
            }
            while (aktFarbe != Farben.Empty);
            return zähler;
        }

        public void Gewinnprüfung()
        {
            List<List<Farben>> prüfreihen = new List<List<Farben>>();
            Farben[,] tmpFarben = _farben;

            int xMax = _x_max;
            int yMax = _y_max;

            // jede Höhe
            for (int i = 0; i < xMax; i++)
            {
                prüfreihen.Add(new List<Farben>());
                for (int j = 0; j < yMax; j++)
                    prüfreihen[prüfreihen.Count - 1].Add(tmpFarben[i, j]);
            }
            // jede Breite
            for (int i = 0; i < yMax; i++)
            {
                prüfreihen.Add(new List<Farben>());
                for (int j = 0; j < xMax; j++)
                    prüfreihen[prüfreihen.Count - 1].Add(tmpFarben[j, i]);
            }
            // jede Diagonale
            for (int durchlauf = 0; durchlauf < 2; durchlauf++)
            {


                int breite = xMax - _anzGleiche + 1;
                for (int x = 0; x < breite; x++) // >
                {
                    for (int i = 0; i < 4; i++)
                        prüfreihen.Add(new List<Farben>());

                    int richtwertMaxHöhe = xMax + yMax - (xMax + x);
                    richtwertMaxHöhe = xMax - x - yMax;
                    if (richtwertMaxHöhe < 0) richtwertMaxHöhe = yMax + richtwertMaxHöhe;
                    else richtwertMaxHöhe = yMax;
                    for (int y = 0; y < richtwertMaxHöhe; y++) // ^
                    {
                        int _x = x + y;

                        Farben ul_or = tmpFarben[xMax - _x - 1, y];
                        Farben ur_ol = tmpFarben[_x, y];
                        Farben ol_ur = tmpFarben[_x, yMax - y - 1];
                        Farben or_ul = tmpFarben[xMax - _x - 1, yMax - y - 1];

                        prüfreihen[prüfreihen.Count - 1].Add(ul_or); // unten links -> oben rechts
                        prüfreihen[prüfreihen.Count - 2].Add(ur_ol); // unten rechts -> oben links
                        prüfreihen[prüfreihen.Count - 3].Add(ol_ur); // oben links -> unten rechts
                        prüfreihen[prüfreihen.Count - 4].Add(or_ul); // oben rechts -> unten links
                    }
                }
                if (durchlauf == 0)
                {
                    tmpFarben = new Farben[yMax, xMax];

                    for (int i = 0; i < yMax; i++)
                        for (int j = 0; j < xMax; j++)
                            tmpFarben[i, j] = _farben[j, i];

                    // Array 90 Drehen
                    int tmpX = xMax;
                    xMax = yMax;
                    yMax = tmpX;
                }
            }

            foreach (List<Farben> item in prüfreihen)
            {
                Farben winner = ContainsFour(item.ToArray());
                if (winner != Farben.Empty && SpielVorbei != null)
                {
                    SpielVorbei(winner);
                    break;
                }
            }
        }
        private Farben ContainsFour(Farben[] farben)
        {
            int zähler = _anzGleiche;
            Farben letzeFarbe = Farben.Empty;
            foreach (var farbe in farben)
            {
                // prüfen ob noch möglich
                if (farbe == Farben.Empty) { zähler = _anzGleiche; continue; }
                if (letzeFarbe == Farben.Empty) letzeFarbe = farbe;
                if (farbe == letzeFarbe)
                {
                    zähler--;
                    if (zähler == 0)
                        return letzeFarbe;
                }
                else
                    //bei änderung ist die neue farbe -> zähler--
                    zähler = _anzGleiche - 1;
                letzeFarbe = farbe;
            }
            return Farben.Empty;
        }

    }
}
