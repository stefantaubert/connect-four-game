﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ConnectFour
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new Form1());
            }
            catch (ObjectDisposedException) { }
        }
    }
}
