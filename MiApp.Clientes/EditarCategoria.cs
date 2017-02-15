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
    public partial class EditarCategoria : Form
    {
        CategoriaBD conccategoria = new CategoriaBD();
        public EditarCategoria()
        {
            InitializeComponent();
            this.textBox2.Visible = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox2.Text);
            string nombre = textBox1.Text;
            if (nombre == "")
            {
                MessageBox.Show("Faltan datos ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                conccategoria.editarcategoria(id,nombre);
                ListaCategoria listacat = new ListaCategoria();
                this.Close();
                listacat.Show();
            }
        }
    }
}
