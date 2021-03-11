using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace belediyeProgram
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new girisForm());

            bool calisiyor = false;
            foreach (Process prog in Process.GetProcesses())
            {
                if ((Process.GetProcessesByName(Assembly.GetEntryAssembly().GetName().Name).Count() > 1))
                {
                    calisiyor = true;
                }
                else
                {
                    calisiyor = false;
                }
            }
            if (!calisiyor)
            {
                Application.EnableVisualStyles();
                Application.Run(new girisForm());
            }

            else
            {
                MessageBox.Show("Program zaten çalışıyor.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
    }
}
