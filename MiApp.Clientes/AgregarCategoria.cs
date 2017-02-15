using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;
using System.Configuration;

namespace MiApp.Clientes
{
    public partial class AgregarCategoria : Form
    {
        CategoriaBD objconec = new CategoriaBD();
        public AgregarCategoria()
        {
            InitializeComponent();
            this.textBox1.Select();
            this.StartPosition = FormStartPosition.CenterScreen;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nombre = textBox1.Text;
            if (textBox1.Text=="")
            {
                MessageBox.Show("El formulario esta vacio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (verificar()==true)
                {
                    MessageBox.Show("El registro ya existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = "";
                }
                else
                {
                    objconec.guardarcategoria(nombre);
                    textBox1.Text = "";
                    if (MessageBox.Show("Desea agregar otra categoria?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        textBox1.Select();
                    }
                    else
                    {
                        this.Close();
                        objconec.desconectar();
                    }
                }
            }
        }
        public Boolean verificar()
        {
            //se declara la conexion
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["conex"].ConnectionString);//app config
            conexion.Open();
            string cadena = "select * from categoria where nombre='" + textBox1.Text + "'";
            MySqlCommand buscli = new MySqlCommand(cadena, conexion);
            MySqlDataReader encli = buscli.ExecuteReader();
            if (encli.Read())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
