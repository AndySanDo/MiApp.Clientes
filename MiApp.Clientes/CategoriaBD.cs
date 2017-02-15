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
    class CategoriaBD
    {
        MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["conex"].ConnectionString);//con el app.config

        public Boolean conectar()
        {
            try
            {
                cn.Open();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public void desconectar()
        {
            cn.Close();
        }
        //Metotodo para el listado de una categoria
        public void listarcategoria(DataGridView categorias, string cadenadatos)
        {
            System.Data.DataSet dataset = new System.Data.DataSet();
            MySqlDataAdapter adaptador = new MySqlDataAdapter(cadenadatos, cn);
            adaptador.Fill(dataset, "categoria");
            categorias.DataSource = dataset;
            categorias.DataMember = "categoria";
            categorias.ClearSelection();
            this.desconectar();
        }
        
        //Agrega un nuevo cliente
        public void guardarcategoria(string nombre)
        {
            this.conectar();
            try
            {
                string cadenadatos = "insert into categoria(id,nombre) values (NULL,'" + nombre + "')";
                this.conectar();
                MySqlCommand cmd = new MySqlCommand(cadenadatos, cn);
                MySqlDataReader reader = cmd.ExecuteReader();
                this.desconectar();
            }
            catch (Exception)
            {
                MessageBox.Show("Erros al guardar el datos \nLos datos no se almacenaran", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void editarcategoria(int id, string nombre)
        {
            this.conectar();
            try
            {
                string cadenadatos = "update categoria set  nombre='" + nombre + "' where id =" + id + ";";
                MySqlCommand cmd = new MySqlCommand(cadenadatos, cn);
                MySqlDataReader reader = cmd.ExecuteReader();
                this.desconectar();
            }
            catch (Exception)
            {

                MessageBox.Show("Erros al guardar el datos \nLos datos no se almacenaran", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void eliminar(int id)
        {
            string cadena = "delete from categoria where id = '" + id + "'";
            this.conectar();
            MySqlCommand cmd = new MySqlCommand(cadena,cn);
            MySqlDataReader reader = cmd.ExecuteReader();
            this.desconectar();
        }
    }
}
