using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConnectFour
{
    public partial class Settings : Form
    {
        public int Höhe { get { return (int)numericUpDown2.Value; } }
        public int Breite { get { return (int)numericUpDown1.Value; } }
        private Größe _size = Größe.Normal;
        public Größe Feldgröße { get { return _size; } }

        public Settings()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Abort;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            //if (numericUpDown1.Value < numericUpDown2.Value)
            //    numericUpDown2.Value--;
        }
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            //if (numericUpDown1.Value < numericUpDown2.Value)
            //    numericUpDown1.Value++;
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                _size = Größe.Klein;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                _size = Größe.Normal;
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                _size = Größe.Groß;
        }
    }
}
