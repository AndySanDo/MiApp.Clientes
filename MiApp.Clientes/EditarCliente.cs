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
    public partial class EditarCliente : Form
    {
        
        ClientesBD concliente = new ClientesBD();
        public EditarCliente()
        {
            InitializeComponent();
            llenarcombo();
            this.textBox5.Visible = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox5.Text);
            string nombre = textBox1.Text;
            string apellidos = textBox2.Text;
            string numero = textBox3.Text;
            string correo = textBox4.Text;
            int categoria =Convert.ToInt32(comboBox1.SelectedValue.ToString());


            if (nombre == "" || apellidos == "" || numero == "" || correo == "")
            {
                MessageBox.Show("Faltan datos ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                long telefono = Convert.ToInt64(textBox3.Text);
                concliente.editarcliente(id, nombre, apellidos, telefono, correo, categoria);
                ListadeClientes lista = new ListadeClientes();
                this.Close();
                lista.Show();
            }
        }
        public void llenarcombo()
        {
            //se declara la conexion
            MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["conex"].ConnectionString);//app config
            System.Data.DataSet ds = new System.Data.DataSet();//para llenar el combo
            MySqlDataAdapter da = new MySqlDataAdapter("select * from categoria", conexion);//consulta
            da.Fill(ds, "categoria");//nombre de la tabla
            comboBox1.DataSource = ds.Tables[0].DefaultView;
            comboBox1.DisplayMember = "nombre";
            comboBox1.ValueMember = "id";//campo de la tabla
        }

        
        
    }
}
