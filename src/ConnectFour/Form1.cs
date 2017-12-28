using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConnectFour
{
    public partial class Form1 : Form
    {
        private Farben _spieler = Farben.Red;
        private Spielfeld _spielfeld;
        private PicBoxSpez[,] _meinePictureboxen;
        private PicBoxSpez[] _meinePfeile;
        private int _bildgröße;
        private const int _abstand = 3;

        public Form1()
        {
            Laden();
        }

        private void Laden()
        {
            Settings s = new Settings();
            s.ShowDialog();
            if (s.DialogResult == System.Windows.Forms.DialogResult.OK)
                InitializeComponent();
            else
            {
                Close();
                return;
            }
            switch (s.Feldgröße)
            {
                case Größe.Klein:
                    _bildgröße = 32;
                    break;
                case Größe.Groß:
                    _bildgröße = 64;
                    break;
                default:
                    _bildgröße = 48;
                    break;
            }
            _spielfeld = new Spielfeld(s.Breite, s.Höhe);
            _spielfeld.Spielerwechsel += new Action<object>(_spielfeld_Spielerwechsel);
            _spielfeld.SpielVorbei += new Action<Farben>(_spielfeld_SpielVorbei);
            _meinePictureboxen = new PicBoxSpez[_spielfeld.Width, _spielfeld.Height];
            _meinePfeile = new PicBoxSpez[_spielfeld.Width];
            CreatePicBoxes();
            CreateArrows();
            Width = Width - panel3.Width + 2 * panel1.Location.X + panel1.Width;
            Height = Height - panel3.Height + 2 * panel1.Location.Y + panel1.Height + panel2.Height;
            _spielfeld_Spielerwechsel(null);
        }

        private void _spielfeld_Spielerwechsel(object obj)
        {
            if (_spieler == Farben.Red) _spieler = Farben.Blue;
            else _spieler = Farben.Red;
            Text = "Connect Four - " + _spieler.ToString() + "'s turn";
        }
        private void _spielfeld_SpielVorbei(Farben obj)
        {
            MessageBox.Show(obj.ToString() + " wins!", obj.ToString() + "!");
            Clear();
        }

        private void SetCoin(int spalte, Farben farbe)
        {
            int index = _spielfeld.SetzeFarbe(farbe, spalte);
            if (index != -1)
            {
                _meinePictureboxen[spalte, index].SetImage(farbe);
                _spielfeld.Gewinnprüfung();
            }
        }
        private void Clear()
        {
            for (int i = 0; i < _spielfeld.Width; i++)
                for (int j = 0; j < _spielfeld.Height; j++)
                {
                    _meinePictureboxen[i, j].SetImage(Farben.Empty);
                    _spielfeld.Felder[i, j] = Farben.Empty;
                }
        }

        private void CreatePicBoxes()
        {
            panel1.Controls.Clear();
            panel1.Height = (_bildgröße + _abstand) * _spielfeld.Height + _abstand;
            panel1.Width = (_bildgröße + _abstand) * _spielfeld.Width + _abstand;

            for (int i = 0; i < _spielfeld.Width; i++)
                for (int j = 0; j < _spielfeld.Height; j++)
                {
                    PicBoxSpez bild = new PicBoxSpez(_bildgröße);
                    bild.Location = new Point(_abstand + ((_bildgröße + _abstand) * i), panel1.Height - (_abstand + _bildgröße) * (j + 1)); //y
                    bild.Spalte = i;
                    bild.MouseClick += new MouseEventHandler(bild_MouseClick);
                    _meinePictureboxen[i, j] = bild;
                    panel1.Controls.Add(bild);
                }
        }
        private void CreateArrows()
        {
            panel2.Controls.Clear();
            panel2.Location = new Point(panel1.Location.X, panel1.Height + panel1.Location.Y);
            panel2.Height = _bildgröße + _abstand;
            panel2.Width = panel1.Width;

            for (int i = 0; i < _spielfeld.Width; i++)
            {
                PicBoxSpez bild = new PicBoxSpez(_bildgröße);
                bild.Location = new Point(_abstand + ((_bildgröße + _abstand) * i), 0); //y
                bild.Spalte = i;
                _meinePfeile[i] = bild;
                panel2.Controls.Add(bild);
                bild.MouseClick += new MouseEventHandler(bild_MouseClick);
                bild.MouseEnter += new EventHandler(bild_MouseEnter);
                bild.MouseLeave += new EventHandler(bild_MouseLeave);
                bild.SetImage(Farben.Arrow);
                bild.HideImage();
            }
        }

        private void bild_MouseLeave(object sender, EventArgs e)
        {
            ((PicBoxSpez)sender).HideImage();
        }
        private void bild_MouseEnter(object sender, EventArgs e)
        {
            ((PicBoxSpez)sender).ShowImage();
        }
        private void bild_MouseClick(object sender, MouseEventArgs e)
        {
            //Farben tmpF = Farben.Grün;
            //if (e.Button == System.Windows.Forms.MouseButtons.Left) tmpF = Farben.Rot;
            //SetCoin(((PicBoxSpez)sender).Spalte, tmpF);
            SetCoin(((PicBoxSpez)sender).Spalte, _spieler);
        }
    }
}