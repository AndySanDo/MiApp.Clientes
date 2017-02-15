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
    public partial class Lista1 : Form
    {
        ClientesBD cli = new ClientesBD();
        CategoriaBD concategoria = new CategoriaBD();
        public Lista1()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(588, 426);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.label2.Text = "Bienvenido\n\n A mi aplicacion clientes";
            this.panel1.Visible = false;
            this.panel2.Visible = false;
            
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label2.Visible = false;
            this.panel1.Visible = true;
            this.panel2.Visible = false;
            this.Size = new System.Drawing.Size(588, 426);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.panel1.Location = new Point(14, 27);
            
            cli.listarclientes(dataGridView1,"Select id as Clave,nombre as Nombre,apellidos as Apellidos,telefono as Telefono,correo as Correo,id as Categoria from clientes");

        }

        private void categoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label2.Visible = false;
            this.panel1.Visible = false;
            this.panel2.Visible = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new System.Drawing.Size(337, 426);
            this.panel2.Location = new Point(12, 27);
            
            concategoria.listarcategoria(this.dataGridView2, "select id as Clave,nombre as Nombre from categoria");
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AgregarCliente nuevo = new AgregarCliente();
            nuevo.Show();
            //cli.listarclientes(dataGridView1, "Select id as Clave,nombre as Nombre,apellidos as Apellidos,telefono as Telefono,correo as Correo,id as Categoria from clientes");

        }

        private void button4_Click(object sender, EventArgs e)
        {
            AgregarCategoria nuevo = new AgregarCategoria();
            nuevo.Show();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Selected==false)
            {
                 MessageBox.Show("No ha seleccionado nada", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string id =dataGridView1.CurrentRow.Cells[0].Value.ToString();
                int idcli = Convert.ToInt32(id);
                string nombre = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                string apellidos = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                string telefono = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                string correo = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                string categoria = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                EditarCliente editar = new EditarCliente();
                editar.textBox1.Text = nombre;
                editar.textBox2.Text = apellidos;
                editar.textBox3.Text = telefono;
                editar.textBox4.Text = correo;
                editar.comboBox1.SelectedText = categoria;
                editar.Show();

                }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Selected == false)
            {
                MessageBox.Show("No ha seleccionado nada", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                int idcli = Convert.ToInt32(id);
                cli.eliminar(idcli);
                cli.listarclientes(dataGridView1, "Select id as Clave,nombre as Nombre,apellidos as Apellidos,telefono as Telefono,correo as Correo,id as Categoria from clientes");
            
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow.Selected == false)
            {
                MessageBox.Show("No ha seleccionado nada", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                
                string id =dataGridView2.CurrentRow.Cells[0].Value.ToString();
                String nombre = dataGridView2.CurrentRow.Cells[1].Value.ToString(); //Para obtener el nombre
                EditarCategoria editar = new EditarCategoria();
                editar.textBox1.Text = nombre;
                editar.textBox2.Text = id;
                editar.Show();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow.Selected == false)
            {
                MessageBox.Show("No ha seleccionado nada", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string id = dataGridView2.CurrentRow.Cells[0].Value.ToString();
                int idcat = Convert.ToInt32(id);
                concategoria.eliminar(idcat);
                cli.listarclientes(dataGridView1, "Select id as Clave,nombre as Nombre,apellidos as Apellidos,telefono as Telefono,correo as Correo,id as Categoria from clientes");
            
                
            }
        }

    }
}
