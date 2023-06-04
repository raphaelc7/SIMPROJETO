using MySql.Data.MySqlClient;
using SimProjeto.Base.Classes;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimProjeto.Principal.Classes
{
    public class Materiais
    {
        public int Orcamento;
        public string Material;
        public decimal Valor;
        public decimal Quantidade;
        public decimal Valor_Total;
        public string Unidade;


        public static void insereMaterial(Materiais material, ref string retorno)
        {
            string comando = "INSERT INTO Materiais (I_Orcamento, S_Material, D_Valor, D_Quantidade, D_ValorTotal, S_Unidade)" +
                             " VALUES (@I_Orcamento, @S_Material, @D_Valor, @D_Quantidade, @D_ValorTotal, @S_Unidade);";

            MySqlConnection conexaoMySQL = new MySqlConnection(FuncoesBD.mysql_conexao);
            MySqlCommand comand = new MySqlCommand(comando, conexaoMySQL);

            comand.Parameters.Add("@I_Orcamento", MySqlDbType.Int32).Value = material.Orcamento;
            comand.Parameters.Add("@S_Material", MySqlDbType.VarChar).Value = material.Material;
            comand.Parameters.Add("@D_Valor", MySqlDbType.Decimal).Value = material.Valor;
            comand.Parameters.Add("@D_Quantidade", MySqlDbType.Decimal).Value = material.Quantidade;
            comand.Parameters.Add("@D_ValorTotal", MySqlDbType.Decimal).Value = material.Valor_Total;
            comand.Parameters.Add("@S_Unidade", MySqlDbType.VarChar).Value = material.Unidade;

            try
            {
                conexaoMySQL.Open();
                comand.ExecuteNonQuery();
                retorno = "ok";
            }
            catch (Exception ex)
            {
                retorno = ex.Message;

            }
            finally
            {
                conexaoMySQL.Close();

            }
        }
        

    }
}
