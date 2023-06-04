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
    public class Profissionais
    {
        public int Orcamento;
        public string Profissional;
        public decimal Valor;
        public decimal Quantidade;
        public decimal Valor_Total;
        public string Tipo;


        public static void insereProfissional(Profissionais profissional, ref string retorno)
        {
            string comando = "INSERT INTO Profissionais (I_Orcamento, S_Profissional, D_Valor, D_Quantidade, D_ValorTotal, S_Tipo)" +
                             " VALUES (@I_Orcamento, @S_Profissional, @D_Valor, @D_Quantidade, @D_ValorTotal, @S_Tipo);";

            MySqlConnection conexaoMySQL = new MySqlConnection(FuncoesBD.mysql_conexao);
            MySqlCommand comand = new MySqlCommand(comando, conexaoMySQL);

            comand.Parameters.Add("@I_Orcamento", MySqlDbType.Int32).Value = profissional.Orcamento;
            comand.Parameters.Add("@S_Profissional", MySqlDbType.VarChar).Value = profissional.Profissional;
            comand.Parameters.Add("@D_Valor", MySqlDbType.Decimal).Value = profissional.Valor;
            comand.Parameters.Add("@D_Quantidade", MySqlDbType.Decimal).Value = profissional.Quantidade;
            comand.Parameters.Add("@D_ValorTotal", MySqlDbType.Decimal).Value = profissional.Valor_Total;
            comand.Parameters.Add("@S_Tipo", MySqlDbType.VarChar).Value = profissional.Tipo;

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
