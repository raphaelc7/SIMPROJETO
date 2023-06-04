using SimProjeto.Base.Classes;
using SimProjeto.Base.Formularios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimProjeto.Formularios
{
    public partial class Pesquisa : Base.Formularios.Base
    {
        public string codigo = "";
        private string table = "";

        public Pesquisa(string tabela = "Clientes")
        {
            table = tabela;

            InitializeComponent();
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            if (textCodigo.Text != string.Empty)
            {
                DataTable dt = FuncoesBD.realizaConsulta(table, " Codigo = " + textCodigo.Text, "");
                gridPesquisa.AutoGenerateColumns = false;
                gridPesquisa.DataSource = dt;
                

            }
            if (textNome.Text != string.Empty)
            {
                DataTable dt = FuncoesBD.realizaConsulta(table, " S_Nome LIKE '%" + textNome.Text + "%'", "");
                gridPesquisa.AutoGenerateColumns = false;
                gridPesquisa.DataSource = dt;
                

            }

            if (textNome.Text == string.Empty && textCodigo.Text == string.Empty)
            {
                DataTable dt = FuncoesBD.realizaConsulta(table);
                gridPesquisa.AutoGenerateColumns = false;
                gridPesquisa.DataSource = dt;
            }

         }

        private void textCodigo_Enter(object sender, EventArgs e)
        {
            textNome.Text = String.Empty;
        }

        private void gridPesquisa_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            codigo = gridPesquisa.SelectedRows[0].Cells[0].Value.ToString();
            Close();
        }
    }
}
