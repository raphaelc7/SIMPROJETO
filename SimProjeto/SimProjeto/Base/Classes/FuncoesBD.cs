using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SimProjeto.Base.Classes
{
    public class FuncoesBD
    {
        public static MySqlConnection conexaoMySQL = null;

        public static string mysql_conexao = "datasource=localhost;username=root;password=root;database=simprojeto";

        private static MySqlDbType retornaTipoDado(Control controle)
        {
            if (controle.Tag.ToString().Substring(0, 1) == "I")
                return MySqlDbType.Int32;

            if (controle.Tag.ToString().Substring(0, 1) == "S")
                return MySqlDbType.VarChar;

            if (controle.Tag.ToString().Substring(0, 1) == "D")
                return MySqlDbType.Decimal;

            if (controle.Tag.ToString().Substring(0, 1) == "B")
                return MySqlDbType.Bit;

            if (controle.Tag.ToString().Substring(0, 1) == "A")
                return MySqlDbType.DateTime;

            return MySqlDbType.VarChar;
        }

        public static DataTable realizaConsulta(string tabela)
        {
            DataTable dt = new DataTable();

            string comando = "SELECT * FROM " + tabela + ";";

            conexaoMySQL = new MySqlConnection(mysql_conexao);
            MySqlCommand comand = new MySqlCommand(comando, conexaoMySQL);

            conexaoMySQL.Open();
            MySqlDataReader reader = comand.ExecuteReader();

            if (reader.HasRows == false)
            {
                conexaoMySQL.Close();
                return new DataTable();
            }
            else
            {                

                DataTable schemaTable = reader.GetSchemaTable();
                DataTable schemaDest = new DataTable();

                foreach (DataRow row in schemaTable.Rows)
                {
                    if (!schemaDest.Columns.Contains(row["ColumnName"].ToString()))
                    {
                        DataColumn col = new DataColumn()
                        {
                            ColumnName = row["ColumnName"].ToString(),
                            Unique = Convert.ToBoolean(row["IsUnique"]),
                            AllowDBNull = Convert.ToBoolean(row["AllowDBNull"]),
                            ReadOnly = Convert.ToBoolean(row["IsReadOnly"])
                        };
                        schemaDest.Columns.Add(col);
                    }
                }

                while (reader.Read())
                {
                    DataRow row = schemaDest.NewRow();
                    for (int i = 0; i < schemaDest.Columns.Count; i++)
                    {
                        row[i] = reader.GetValue(i);
                    }
                    schemaDest.Rows.Add(row);
                }

                dt = schemaDest;
                conexaoMySQL.Close();



            }

            return dt;
        }       

        public static DataTable realizaConsulta(string tabela, string condicao  = "", string campos = "")
        {
            DataTable dt = new DataTable();

            string comando = "SELECT " + campos + " FROM " + tabela + " WHERE " + condicao + ";";

            if (condicao == string.Empty)
                comando = "SELECT " + campos + " FROM " + tabela + "; ";

            if (campos == string.Empty)
                comando = "SELECT * FROM " + tabela + " WHERE " + condicao + ";";

            conexaoMySQL = new MySqlConnection(mysql_conexao);
            MySqlCommand comand = new MySqlCommand(comando, conexaoMySQL);

            conexaoMySQL.Open();
            MySqlDataReader reader = comand.ExecuteReader();

            if (reader.HasRows == false)
            {
                conexaoMySQL.Close();
                return new DataTable();
            }
            else
            {

                DataTable schemaTable = reader.GetSchemaTable();
                DataTable schemaDest = new DataTable();

                foreach (DataRow row in schemaTable.Rows)
                {
                    if (!schemaDest.Columns.Contains(row["ColumnName"].ToString()))
                    {
                        DataColumn col = new DataColumn()
                        {
                            ColumnName = row["ColumnName"].ToString(),
                            Unique = Convert.ToBoolean(row["IsUnique"]),
                            AllowDBNull = Convert.ToBoolean(row["AllowDBNull"]),
                            ReadOnly = Convert.ToBoolean(row["IsReadOnly"])
                        };
                        schemaDest.Columns.Add(col);
                    }
                }

                while (reader.Read())
                {
                    DataRow row = schemaDest.NewRow();
                    for (int i = 0; i < schemaDest.Columns.Count; i++)
                    {
                        row[i] = reader.GetValue(i);
                    }
                    schemaDest.Rows.Add(row);
                }

                dt = schemaDest;
                conexaoMySQL.Close();

            }

            return dt;
        }

        public static int insereRegistroPorCampos(string tabela, List<Control> campos, ref string retorno)
        {

            int lastId = 0;
            string camposInseridos = "";
            string camposAInserir = "";
            foreach (Control o in campos)
            {
                if (o.Name.ToLower() == "textcodigo") continue;

                camposInseridos += o.Tag.ToString() + ",";
                camposAInserir += "@" + o.Tag.ToString() + ",";

            }
            camposInseridos = camposInseridos.Substring(0, camposInseridos.Length - 1);
            camposAInserir = camposAInserir.Substring(0, camposAInserir.Length - 1);        
            

           
            string comando = "INSERT INTO " + tabela + " (" + camposInseridos + ")" + " VALUES (" + camposAInserir + ");";

            conexaoMySQL = new MySqlConnection(mysql_conexao);
            MySqlCommand comand = new MySqlCommand(comando, conexaoMySQL);

            

            foreach (Control o in campos)
            {
                if (o.Name.ToLower() == "textcodigo") continue;

                if ( o is TextBox)
                    comand.Parameters.Add("@" + o.Tag.ToString(), retornaTipoDado(o)).Value = o.Text;


                if (o is MaskedTextBox)
                {
                    MySqlDbType tipo = retornaTipoDado(o);

                    if (tipo == MySqlDbType.DateTime)
                        comand.Parameters.Add("@" + o.Tag.ToString(), tipo).Value = Convert.ToDateTime(o.Text).ToString("yyyy/MM/dd");
                    else
                        comand.Parameters.Add("@" + o.Tag.ToString(), tipo).Value = o.Text;
                }
                

                if ( o is CheckBox)
                {
                    CheckBox chk = (CheckBox)o;
                    comand.Parameters.Add("@" + o.Tag.ToString(), retornaTipoDado(o)).Value = chk.Checked;

                }               

            }

            try
            {
                conexaoMySQL.Open();
                comand.ExecuteNonQuery();
                comand.CommandText = "Select @@Identity";
                lastId = Convert.ToInt32(comand.ExecuteScalar());
                retorno = "ok";
            }
            catch (Exception ex)
            {
                retorno = ex.Message;
                lastId = 0;

            }
            finally
            {
                conexaoMySQL.Close();

            }

            return lastId;

        }

        public static void atualizaRegistroPorCampos(string tabela, List<Control> campos, ref string retorno)
        {
            string codigo = "";
            string camposInseridos = "";
            foreach (Control o in campos)
            {
                if (o.Name.ToLower() == "textcodigo")
                {   codigo = o.Text;
                    continue;
                }                

                camposInseridos += o.Tag.ToString() + "= @" + o.Tag.ToString() + ",";

            }
            camposInseridos = camposInseridos.Substring(0, camposInseridos.Length - 1);



            string comando = "UPDATE " + tabela + " SET " + camposInseridos + " WHERE Codigo = " + codigo +";";

            conexaoMySQL = new MySqlConnection(mysql_conexao);
            MySqlCommand comand = new MySqlCommand(comando, conexaoMySQL);

            foreach (Control o in campos)
            {
                if (o.Name.ToLower() == "textcodigo") continue;


                if (o is TextBox)
                    comand.Parameters.Add("@" + o.Tag.ToString(), retornaTipoDado(o)).Value = o.Text;

                if (o is MaskedTextBox)
                {
                    MySqlDbType tipo = retornaTipoDado(o);

                    if (tipo == MySqlDbType.DateTime)
                        comand.Parameters.Add("@" + o.Tag.ToString(), tipo).Value = Convert.ToDateTime(o.Text).ToString("yyyy/MM/dd");
                    else
                        comand.Parameters.Add("@" + o.Tag.ToString(), tipo).Value = o.Text;
                }
                

                if (o is CheckBox)
                {
                    CheckBox chk = (CheckBox)o;
                    comand.Parameters.Add("@" + o.Tag.ToString(), retornaTipoDado(o)).Value = chk.Checked;

                }

            }

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

        public static void deletaRegistro(string tabela, string codigo, ref string retorno)
        {

            string comando = "DELETE FROM " + tabela + " WHERE Codigo = " + codigo + ";";

            conexaoMySQL = new MySqlConnection(mysql_conexao);
            MySqlCommand comand = new MySqlCommand(comando, conexaoMySQL);

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
