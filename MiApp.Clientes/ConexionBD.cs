using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using MySql.Data.MySqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace MiApp.Clientes
{
    class ConexionBD
    {
        MySqlConnection cn;

        public void conectar()
        {
            try
            {
                cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["conex"].ConnectionString);//con el app.config
                cn.Open();
                MessageBox.Show("Conexion correcta");
            }catch(Exception e){
                MessageBox.Show("Error en la conexion con la base de datos");
            }
        }
        public void desconectar()
        {
            cn.Close();
        }
        public void ejecutar(string cadenadatos)
        {
            MySqlCommand datos = new MySqlCommand(cadenadatos,cn);
            int filas = datos.ExecuteNonQuery();

            if (filas>0)
            {
                MessageBox.Show("Operacion correcta", "Los datos se almacenaron correctamente", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error en la operacion", "No se agregaron los datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void listarclientes(DataGridView clientes, string cadenadatos)
        {
            this.conectar();
            System.Data.DataSet dataset = new System.Data.DataSet();
            MySqlDataAdapter adaptador = new MySqlDataAdapter(cadenadatos,cn);
            adaptador.Fill(dataset, "clientes");
            clientes.DataSource = dataset;
            clientes.DataMember = "clientes";
            this.desconectar();
        }
        public void listarcategoria(DataGridView categorias, string cadenadatos)
        {
            this.conectar();
            System.Data.DataSet dataset = new System.Data.DataSet();
            MySqlDataAdapter adaptador = new MySqlDataAdapter(cadenadatos,cn);
            adaptador.Fill(dataset, "categoria");
            categorias.DataSource = dataset;
            categorias.DataMember = "categoria";
            this.desconectar();
        }

    }

}
