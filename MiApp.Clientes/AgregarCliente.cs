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
using System.Text.RegularExpressions;

namespace MiApp.Clientes
{
    public partial class AgregarCliente : Form
    {
        ClientesBD concliente = new ClientesBD();
        public AgregarCliente()
        {
            InitializeComponent();
            llenarcombo();
            this.StartPosition = FormStartPosition.CenterScreen;
                
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            string nombre = textBox1.Text;
            string apellidos = textBox2.Text;
            string telefono = textBox3.Text;
            string correo = textBox4.Text;
            string idcat = comboBox1.Text;
            
                if (nombre == "" || apellidos == "")
                {
                    MessageBox.Show("Faltan datos principales", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else if (idcat == "")
                {
                    if (MessageBox.Show("Categoria vacia \n Desea agregar una categoria", "Advertencia", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        AgregarCategoria agcat = new AgregarCategoria();
                        agcat.Show();
                        this.Close();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    if (correo!="")
                    {
                        validarcorreo(correo);
                        if (validarcorreo(correo)==false)
                        {
                            MessageBox.Show("Error en el formato del correo", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    
                        }
                    }
                    else
                    {
                        int cat_id = Convert.ToInt32(comboBox1.SelectedValue.ToString());
                        concliente.agregarcliente(nombre, apellidos, telefono, correo, cat_id);
                        
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        if (MessageBox.Show("Desea agregar otro registro?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            textBox1.Select();
                        }
                        else
                        {
                            this.Close();
                            concliente.desconectar();
                        }
                    }
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


        private Boolean validarcorreo(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }
    }
}
