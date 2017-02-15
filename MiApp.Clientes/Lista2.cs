using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiApp.Clientes
{
    public partial class Lista2 : Form
    {
        public Lista2()
        {
            InitializeComponent();
            
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lista1 lista = new Lista1();
            lista.Show();
            this.Close();
            
        }

        private void categoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
