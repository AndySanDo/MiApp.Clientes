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
    public partial class ListaCategoria : Form
    {
        CategoriaBD concategoria = new CategoriaBD();
        public ListaCategoria()
        {
            InitializeComponent();
            this.dataGridView1.AllowUserToAddRows=false;
            this.Size = new System.Drawing.Size(341, 363);
            this.label2.Text = "";
            this.button3.Text = "";
            this.StartPosition = FormStartPosition.CenterScreen;
            

        }

        private void ListaCategoria_Load(object sender, EventArgs e)
        {
            concategoria.listarcategoria(this.dataGridView1,"select id as Clave,nombre as Nombre from categoria");
            textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
            getData(DataCollection);
            textBox1.AutoCompleteCustomSource = DataCollection;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string opcion = comboBox1.Text;
            string dato = textBox1.Text;
            MySqlConnection nuevo = new MySqlConnection(ConfigurationManager.ConnectionStrings["conex"].ConnectionString);
            nuevo.Open();
            if (button1.Visible == true && button2.Visible == false)
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
                            obtenercategoria();
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

                            obtenercategoria();
                        }
                    }
                }
            }
            else if (button2.Visible == true && button1.Visible == false)
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
                            eliminarcategoria();
                        }
                    }
                    else if (opcion == "Nombre")
                    {
                        if (dato == "")
                        {
                            MessageBox.Show("No ha proporcionado un Nombre", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            eliminarcategoria();
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(341, 473);
            button2.Visible = false;
            this.button3.Text = "Buscar";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(341, 473);
            button1.Visible = false;
            this.button3.Text = "Aceptar";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string seleccion = comboBox1.SelectedItem.ToString();
            if (seleccion == "ID")
            {
                this.label2.Text = "ID:";
                this.textBox1.Select();
                this.textBox1.Text = "";
            }
            else
            {
                this.label2.Text = "Nombre:";
                this.textBox1.Select();
                this.textBox1.Text = "";
            }
        }

        public void obtenercategoria()
        {

            string opcion = comboBox1.Text;
            EditarCategoria editar = new EditarCategoria();
            MySqlConnection nuevo = new MySqlConnection(ConfigurationManager.ConnectionStrings["conex"].ConnectionString);
            nuevo.Open();
            if (opcion == "ID")
            {
                int id = Convert.ToInt32(this.textBox1.Text);
                string cadena = "select * from categoria where id=" + id + "";

                MySqlCommand cmd = new MySqlCommand(cadena, nuevo);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    editar.textBox2.Text = Convert.ToString(reader["id"]);
                    editar.textBox1.Text = reader["nombre"] as string;
                    editar.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("La clave proporcionada no existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                string apellidos = textBox1.Text;
                string cadena = "select * from categoria where apellidos='" + apellidos + "'";

                MySqlCommand cmd = new MySqlCommand(cadena, nuevo);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    editar.textBox1.Text = reader["nombre"] as string;
                    editar.textBox2.Text = Convert.ToString(reader["id"]);
                    editar.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("El Apellido proporcionado no existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void eliminarcategoria()
        {
            string opcion = this.comboBox1.Text;
            MySqlConnection nuevo = new MySqlConnection(ConfigurationManager.ConnectionStrings["conex"].ConnectionString);
            nuevo.Open();
            if (opcion =="ID")
            {
                if (MessageBox.Show("Desea eliminar este registro", "Advertencia", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (existe() == true)
                    {
                        if (obtenercliente() == true)
                        {
                            MessageBox.Show("No se puede eliminar el registro esta en uso", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textBox1.Text = "";
                        }
                        else
                        {
                            int id = Convert.ToInt32(textBox1.Text);
                            string cadena = "delete from categoria where id=" + id + "";
                            MySqlCommand cmd = new MySqlCommand(cadena, nuevo);
                            MySqlDataReader reader = cmd.ExecuteReader();
                            if (MessageBox.Show("El registro se ha eliminado correctamente", "Correcto", MessageBoxButtons.OK)==DialogResult.OK)
                            {
                                textBox1.Text = "";
                                concategoria.listarcategoria(this.dataGridView1,"select id as Clave, nombre as Nombre from categoria");
                                this.Size = new System.Drawing.Size(341, 363);
                                button1.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("La clave proporcionada no existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox1.Text = "";
                    }
                }
                else
                {
                    textBox1.Text = "";
                }
            }
            else
            {
                if (MessageBox.Show("Desea eliminar este registro", "Advertebcia", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (existe() == true)
                    {
                        if (obtenercliente() == true)
                        {

                            MessageBox.Show("No se puede eliminar el registro esta en uso", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textBox1.Text = "";
                        }
                        else
                        {
                            string nombre = textBox1.Text;
                            string cadena = "delete from categoria where nombre='" + nombre + "'";
                            MySqlCommand cmd = new MySqlCommand(cadena, nuevo);
                            MySqlDataReader reader = cmd.ExecuteReader();
                            if(MessageBox.Show("El registro se ha eliminado correctamente", "Correcto", MessageBoxButtons.OK) == DialogResult.OK)
                            {
                                textBox1.Text = "";
                                concategoria.listarcategoria(this.dataGridView1, "select id as Clave,nombre as Nombre from categoria");
                                this.Size = new System.Drawing.Size(341, 363);
                                button1.Visible = true;

                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("El Nombre proporcionado no existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox1.Text = "";
                    }
                }
                else
                {
                    textBox1.Text = "";
                }
            }
        }
        private void getData(AutoCompleteStringCollection dataCollection)
        {
            MySqlConnection nuevo = new MySqlConnection(ConfigurationManager.ConnectionStrings["conex"].ConnectionString);
            nuevo.Open();
            string cadena = "select nombre from categoria";
            DataSet dataset = new System.Data.DataSet();
            MySqlDataAdapter adaptador = new MySqlDataAdapter(cadena, nuevo);
            adaptador.Fill(dataset);
            foreach (DataRow row in dataset.Tables[0].Rows)
            {
                dataCollection.Add(row[0].ToString());
            }
        }
        string encon1;
        string encon2;
        public Boolean existe()
        {
            MySqlConnection nuevo = new MySqlConnection(ConfigurationManager.ConnectionStrings["conex"].ConnectionString);
            nuevo.Open();
            string opcion = comboBox1.Text;
            if (opcion=="ID")
            {
                string cadena = "select * from categoria where id='" + textBox1.Text + "'";
                MySqlCommand buscli = new MySqlCommand(cadena, nuevo);
                MySqlDataReader encli = buscli.ExecuteReader();
                if (encli.Read())
                {
                    encon2 = Convert.ToString(encli["id"]);
                    encon1 = encli["nombre"] as string;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                string cadena = "select * from categoria where nombre='" + textBox1.Text + "'";
                MySqlCommand buscli = new MySqlCommand(cadena, nuevo);
                MySqlDataReader encli = buscli.ExecuteReader();
                if (encli.Read())
                {
                    encon2 = Convert.ToString(encli["id"]);
                    encon1 = encli["nombre"] as string;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public Boolean obtenercliente()
        {
            string opcion = comboBox1.Text;
            MySqlConnection nuevo = new MySqlConnection(ConfigurationManager.ConnectionStrings["conex"].ConnectionString);
            nuevo.Open();
            if (opcion=="ID")
            {
                string cadena = "select * from clientes where categoria_id='" + encon2 + "'";
                MySqlCommand buscli = new MySqlCommand(cadena, nuevo);
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
            else
            {
                string cadena = "select * from clientes where nombre='" + encon1 + "'";
                MySqlCommand buscli = new MySqlCommand(cadena, nuevo);
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
}
