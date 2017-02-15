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
    public partial class ListadeClientes : Form
    {
        ClientesBD con = new ClientesBD();
        
        public ListadeClientes()
        {
            InitializeComponent();
            this.dataGridView1.AllowUserToAddRows = false;
            this.Size = new System.Drawing.Size(746, 409);
            this.label2.Text = "";
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void ListadeClientes_Load(object sender, EventArgs e)
        {
            con.listarclientes(this.dataGridView1,"select id as Clave, nombre as Nombre,apellidos as Apellidos,telefono as Telefono,correo as Correo,categoria_id as Categoria from clientes");
            textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
            getData(DataCollection);
            textBox1.AutoCompleteCustomSource = DataCollection;
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(746, 471);
            button1.Visible = false;
            this.button3.Text = "Eliminar";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            this.Size = new System.Drawing.Size(746, 471);
            button2.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string opcion = comboBox1.Text;
            string dato = textBox1.Text;
            if (button1.Visible == true && button2.Visible == false)
            {
                if (opcion=="")
                {
                    MessageBox.Show("Es necesario seleccionar una opcion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }else{
                    
                    if (opcion == "ID")
                    {
                        if (dato == "")
                        {
                            MessageBox.Show("No ha proporcionado una Clave", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            obtenercliente();
                        }
                    }
                    else if (opcion == "Apellidos")
                    {
                        if (dato == "")
                        {
                            MessageBox.Show("No ha proporcionado Apellidos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            
                            obtenercliente();
                        }
                    }
                }   
            }else if (button2.Visible == true && button1.Visible == false)
            {
                if (opcion == "")
                {
                    MessageBox.Show("Es necesario seleccionar una opcion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
                else
                {
                    if (opcion == "ID")
                    {
                        if (dato == "")
                        {
                            MessageBox.Show("No ha proporcionado una Clave", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            eliminarcliente();
                        }
                    }
                    else if (opcion == "Apellidos")
                    {
                        if (dato == "")
                        {
                            MessageBox.Show("No ha proporcionado un Apellido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            eliminarcliente();
                        }
                    }
                }   
            }
        }
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string seleccion = comboBox1.SelectedItem.ToString();
            if (seleccion == "ID")
            {
                this.label2.Text = ":ID";
                this.textBox1.Select();
                this.textBox1.Text = "";
            }
            else
            {
                this.label2.Text = ":Apellidos";
                this.textBox1.Select();
                this.textBox1.Text = "";
            }
        }

        public void obtenercliente()
        {
            
            string opcion = comboBox1.Text;
            EditarCliente edit = new EditarCliente();
            MySqlConnection nuevo = new MySqlConnection(ConfigurationManager.ConnectionStrings["conex"].ConnectionString);
            nuevo.Open();
            if (opcion=="ID")
            {
                int id = Convert.ToInt32(this.textBox1.Text);
                string cadena = "select * from clientes where id=" + id + "";

                MySqlCommand cmd = new MySqlCommand(cadena, nuevo);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    edit.textBox5.Text = Convert.ToString(reader["id"]);
                    edit.textBox1.Text = reader["nombre"] as string;
                    edit.textBox2.Text = reader["apellidos"] as string;
                    edit.textBox3.Text = Convert.ToString(reader["telefono"]);
                    edit.textBox4.Text = reader["correo"] as string;
                    edit.comboBox1.SelectedItem = Convert.ToString(reader["categoria_id"]);
                    edit.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("La clave proporcionada no existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = "";
                    textBox1.Select();
                }
            }
            else 
            {
                string apellidos = textBox1.Text;
                string cadena = "select * from clientes where apellidos='" + apellidos + "'";
                
                MySqlCommand cmd = new MySqlCommand(cadena, nuevo);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    edit.textBox1.Text = reader["nombre"] as string;
                    edit.textBox2.Text = reader["apellidos"] as string;
                    edit.textBox3.Text = Convert.ToString(reader["telefono"]);
                    edit.textBox4.Text = reader["correo"] as string;
                    edit.textBox5.Text = Convert.ToString(reader["id"]);
                    edit.comboBox1.SelectedItem = Convert.ToString(reader["categoria_id"]);
                    edit.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("El Apellido proporcionado no existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = "";
                    textBox1.Select();
                }
            }
        }

        private void getData(AutoCompleteStringCollection dataCollection)
        {
            MySqlConnection nuevo = new MySqlConnection(ConfigurationManager.ConnectionStrings["conex"].ConnectionString);
            nuevo.Open();
            string cadena = "select apellidos from clientes";
            DataSet dataset = new System.Data.DataSet();
            MySqlDataAdapter adaptador = new MySqlDataAdapter(cadena, nuevo);
            adaptador.Fill(dataset);
            foreach (DataRow row in  dataset.Tables[0].Rows)
            {
                dataCollection.Add(row[0].ToString());
            }
        }

        public void eliminarcliente()

        {

            string opcion = comboBox1.Text;
            MySqlConnection nuevo = new MySqlConnection(ConfigurationManager.ConnectionStrings["conex"].ConnectionString);
            nuevo.Open();
            if (opcion == "ID")
            {
                
                if (MessageBox.Show("Desea eliminar este registro", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (this.existe()==true)
                    {
                        int id = Convert.ToInt32(encon1);
                        string cadena = "delete from clientes where id=" + id + "";
                        MySqlCommand cmd = new MySqlCommand(cadena, nuevo);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        if (MessageBox.Show("El registro se ha eliminado correctamente", "Correcto", MessageBoxButtons.OK) == DialogResult.OK)
                        {
                            con.listarclientes(this.dataGridView1, "select id as Clave, nombre as Nombre,apellidos as Apellidos,telefono as Telefono,correo as Correo,categoria_id as Categoria from clientes");
                            this.Size = new System.Drawing.Size(746, 409);
                            button1.Visible = true;
                            textBox1.Text = "";
                        }

                    }
                    else
                    {
                        MessageBox.Show("La clave proporcionada no existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox1.Text = "";
                        textBox1.Select();
                    }
                }
                else
                {
                    this.textBox1.Select();
                }
            }
            else
            {
                if (existe()==true)
                {
                    string cadena = "delete from clientes where apellidos='" + encon2 + "'";
                    MySqlCommand cmd = new MySqlCommand(cadena, nuevo);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (MessageBox.Show("El registro se ha eliminado correctamente", "Correcto", MessageBoxButtons.OK) == DialogResult.OK)
                    {
                        con.listarclientes(this.dataGridView1, "select id as Clave, nombre as Nombre,apellidos as Apellidos,telefono as Telefono,correo as Correo,categoria_id as Categoria from clientes");
                        this.Size = new System.Drawing.Size(746, 409);
                        button1.Visible = true;
                        textBox1.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("El Apellido proporcionado no existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        string encon1;
        string encon2;
        public Boolean existe()
        {
            MySqlConnection nuevo = new MySqlConnection(ConfigurationManager.ConnectionStrings["conex"].ConnectionString);
            nuevo.Open();
            string opcion = comboBox1.Text;
            if (opcion == "ID")
            {
                int dato = Convert.ToInt32(this.textBox1.Text);
                string cadena = "select * from clientes where id =" + dato + "";
                MySqlCommand buscli = new MySqlCommand(cadena, nuevo);
                MySqlDataReader encli = buscli.ExecuteReader();
                if (encli.Read())
                {
                    encon1 = Convert.ToString(encli["id"]);
                    encon2 = encli["apellidos"] as string;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                string cadena = "select * from clientes where apellidos='" + textBox1.Text + "'";
                MySqlCommand buscli = new MySqlCommand(cadena, nuevo);
                MySqlDataReader encli = buscli.ExecuteReader();
                if (encli.Read())
                {
                    encon1 = Convert.ToString(encli["id"]);
                    encon2 = encli["apellidos"] as string;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
