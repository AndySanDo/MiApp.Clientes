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
    public partial class Form1 : Form
    {
        ConexionBD objconex = new ConexionBD();
        public Form1()
        {
            InitializeComponent();
            objconex.conectar();
            
        }

        private void listarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListadeClientes listaCli = new ListadeClientes();
            listaCli.Show();
        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AgregarCliente agregarCli = new AgregarCliente();
            agregarCli.Show();
        }

        private void listarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ListaCategoria listaCat = new ListaCategoria();
            listaCat.Show();
        }

        private void agregarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AgregarCategoria agregarCat = new AgregarCategoria();
            agregarCat.Show();
        }

    }
}
