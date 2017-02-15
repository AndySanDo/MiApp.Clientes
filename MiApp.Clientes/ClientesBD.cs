using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace MiApp.Clientes
{
    class ClientesBD
    {
        MySqlConnection conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["conex"].ConnectionString);//con el app.config

        public Boolean conectar()
        {
            try
            {
                conexion.Open();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void desconectar()
        {
            conexion.Close();
        }
        //Metodo para el listado de clientes
        public void listarclientes(DataGridView clientes, string cadenadatos)
        {
            this.conectar();
            System.Data.DataSet dataset = new System.Data.DataSet();
            MySqlDataAdapter adaptador = new MySqlDataAdapter(cadenadatos, conexion);
            adaptador.Fill(dataset, "clientes");
            clientes.DataSource = dataset;
            clientes.DataMember = "clientes";
            clientes.ClearSelection();
            this.desconectar();
        }
        //Metodo para agregar un nuevo cliente
        public void agregarcliente(string nombre, string apellidos, string telefono, string correo, int cat_id)
        {
            
            try
            {
                    this.conectar();
                    string cadenadatos = "insert into clientes(id,nombre,apellidos,telefono,correo,categoria_id) values(NULL, '" + nombre + "','" + apellidos + "','" + telefono + "','" + correo + "'," + cat_id + ");";
                    MySqlCommand cmd = new MySqlCommand(cadenadatos, conexion);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    this.desconectar();
                
            }
            catch (Exception n)
            {
                MessageBox.Show("Erros al guardar los datos \nLos datos no se almacenaran", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        
        public void editarcliente(int id, string nombre, string apellidos, long telefono, string correo, int categoria)
        {
            this.conectar();
            try
            {
                string cadenadatos = "update clientes set  nombre='" + nombre + "',apellidos='" + apellidos + "',telefono=" + telefono + ",correo='" + correo + "',categoria_id=" + categoria + " where id =" + id + ";";

                MySqlCommand cmd = new MySqlCommand(cadenadatos, conexion);
                MySqlDataReader reader = cmd.ExecuteReader();
                this.desconectar();
            }catch (Exception)
            {

                MessageBox.Show("Erros al guardar el datos \nLos datos no se almacenaran", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        //Metodo para eliminar un cliente
        public void eliminar(int id)
        {
            try
            {
                this.conectar();
                string cadena = "delete from clientes where id=" + id + "";
                MySqlCommand cmd = new MySqlCommand(cadena, conexion);
                MySqlDataReader reader = cmd.ExecuteReader();
                this.desconectar();
            }
            catch (Exception)
            {
                MessageBox.Show("No se puede eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        public Boolean existe(string apellidos)
        {
            this.conectar();
            string cadena = "select * from clientes where apellidos='" + apellidos + "'";
            MySqlCommand cmd = new MySqlCommand(cadena, conexion);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
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
