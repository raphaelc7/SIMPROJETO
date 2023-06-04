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
    public partial class Clientes : Base.Formularios.Base
    {
        public Clientes()
        {
            InitializeComponent();
        }

        #region Eventos

        private void Clientes_Load(object sender, EventArgs e)
        {
           
        }

        private void buttonSalvar_Click(object sender, EventArgs e)
        {
            if (textCodigo.Text == string.Empty)
            {
                if (!validaNome()) return;
                
                cadastraCliente();
            }
            else
            {
                if (!validaNome()) return;

                atualizaCliente();
            }

        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
           
            var frmPesquisa = new Pesquisa();
            frmPesquisa.ShowDialog();

            string codigo = frmPesquisa.codigo;
            if (codigo == string.Empty)
                return;

            populaCampos(codigo);



        }

        private void buttonLimpar_Click(object sender, EventArgs e)
        {
            Funcoes.limpaCampos(Funcoes.selecionaCampos((GroupBox.ControlCollection)groupCampos.Controls));
        }

        private void buttonExcluir_Click(object sender, EventArgs e)
        {
            if (textCodigo.Text == string.Empty) return;

            if (MessageBox.Show("Confirma a exclusão do registro?", "Essa ação é irreversível!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {
                FuncoesBD.deletaRegistro("Clientes", textCodigo.Text, ref retornoBD);

                if (retornoBD != "ok")
                {
                    MessageBox.Show(retornoBD, "Erro no banco de dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Registro apagado com sucesso!!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Funcoes.limpaCampos(Funcoes.selecionaCampos((GroupBox.ControlCollection)groupCampos.Controls));
            }
        }

        #endregion

        #region Funções

        private void cadastraCliente()
        {      

            textCodigo.Text = (FuncoesBD.insereRegistroPorCampos("Clientes", Funcoes.selecionaCampos((GroupBox.ControlCollection)groupCampos.Controls), ref retornoBD)).ToString();

            Funcoes.retornaBD(retornoBD, "Cadastro");

        }
        
        private void atualizaCliente()
        {

            FuncoesBD.atualizaRegistroPorCampos("Clientes", Funcoes.selecionaCampos((GroupBox.ControlCollection)groupCampos.Controls), ref retornoBD);

            Funcoes.retornaBD(retornoBD, "Atualização");

        }

        private void populaCampos(string codigo)
        {
            DataTable cliente = FuncoesBD.realizaConsulta("Clientes", " Codigo = " + codigo, "");
            foreach (Control crtl in groupCampos.Controls)
            {
                if (crtl.Tag == null) continue;

                if (crtl.Tag.ToString() != string.Empty)
                {
                    if (crtl is TextBox)
                        crtl.Text = cliente.Rows[0][crtl.Tag.ToString()].ToString();

                    if (crtl is CheckBox)
                    {
                        CheckBox chk = (CheckBox)crtl;
                        chk.Checked = cliente.Rows[0][crtl.Tag.ToString()].ToString() == "1" ? true : false;
                    }
                        
                }
            }

        }

        private bool validaNome()
        {
            if (textNome.Text == string.Empty)
            {
                MessageBox.Show("Nome deve ser preenchido!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            return true;
        }

        #endregion

    }
}
