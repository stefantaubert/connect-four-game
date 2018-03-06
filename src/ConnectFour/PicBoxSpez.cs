using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ConnectFour.Properties;
using System.Drawing;

namespace ConnectFour
{
    class PicBoxSpez : PictureBox
    {
        Farben _color = Farben.Empty;
        public Farben Farbe { get { return _color; } }
        int _spalte = -1;
        public int Spalte { get { return _spalte; } set { _spalte = value; } }

        Image _tmpImge;

        public PicBoxSpez(int size)
        {
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackColor = Color.White;
            this.Width = size;
            this.Height = size;
        }

        public void ShowImage()
        {
            if (_tmpImge != null)
                this.BackgroundImage = _tmpImge;
        }

        public void HideImage()
        {
            _tmpImge = this.BackgroundImage;
            this.BackgroundImage = null;
        }

        public void SetImage(Farben farbe)
        {
            if (farbe == Farben.Red)
                this.BackgroundImage = Resources.red;
            else if (farbe == Farben.Blue)
                this.BackgroundImage = Resources.blue;
            else if (farbe == Farben.Arrow)
            {
                this.BackgroundImage = Resources.Pfeil;
                this.BackColor = Color.LightSkyBlue;
            }         
            else this.BackgroundImage = null;
            _color = farbe;
            Refresh();
        }

    }
}
