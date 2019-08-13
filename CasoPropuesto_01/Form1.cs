using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace CasoPropuesto_01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CasoPropuesto_01"].ConnectionString);

        public void ListaClientes()
        {
            
            using(SqlDataAdapter df = new SqlDataAdapter("Usp_Caso_Primero", cn))
            {
                df.SelectCommand.CommandType = CommandType.StoredProcedure;
                using (DataSet Da = new DataSet())
                {
                    df.Fill(Da, "Clientes");
                    dg1.DataSource = Da.Tables["Clientes"];
                    lbl1.Text = Da.Tables["Clientes"].Rows.Count.ToString();
                }
            }
        }

        public void BuscarCliente()
        {
            string variable = textBox1.Text;

            using (SqlDataAdapter df = new SqlDataAdapter("Usp_Caso_Busqueda_Primero", cn))
            {
                df.SelectCommand.CommandType = CommandType.StoredProcedure;
                using (DataSet Da2 = new DataSet())
                {
                    df.SelectCommand.CommandType = CommandType.StoredProcedure;
                    SqlParameter sqlParameter = new SqlParameter();
                    sqlParameter.SqlDbType = SqlDbType.VarChar;
                    sqlParameter.SqlValue = variable;
                    sqlParameter.Size = 100;
                    sqlParameter.ParameterName = "@idCliente";


                    df.SelectCommand.Parameters.Add(sqlParameter);

                    using (DataSet Da = new DataSet())
                    {
                        df.Fill(Da, "Clientes");
                        dg1.DataSource = Da.Tables["Clientes"];
                        lbl1.Text = Da.Tables["Clientes"].Rows.Count.ToString();
                    }
                }
            } 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListaClientes();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            BuscarCliente();
        }
    }
}
