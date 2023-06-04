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
    public partial class Contrato : Base.Formularios.Base
    {
        public Contrato()
        {
            InitializeComponent();
        }

        #region Eventos

        private void buttonAjuda_Click(object sender, EventArgs e)
        {
            string inf = "Digite abaixo o contrato padrão para geração no projeto.\r\n\r\n" +
                         "Para informa o nome do cliente digite: @CLIENTE# \r\n" +
                         "Para informar o endereço do cliente digite: @ENDERECO# \r\n" +
                         "Para informar o CNPJ ou CPF do cliente digite: @CPF# \r\n" +
                         "Para informar a IE ou RG do cliente digite: @IERG# \r\n" +
                         "Para informar o estado civil do cliente digite: @ECIVIL# \r\n" +
                         "Para informar o telefone do cliente digite: @TELEFONE# \r\n" +
                         "Para informar a profissão do cliente digite: @PROFISSAO# \r\n\r\n" +
                         "O sistema fará as substituições de acordo com as informações no cadastro de clientes!\r\n" +
                         "Leve em conta que o sistema usará a formatação utilizada no cadastro de clientes!\r\n" +
                         "As informações inseridas em 'Empresa/Nome', 'CPF/CNPJ' e 'Cidade' serão utilizadas para linha de assinatura.";

            MessageBox.Show(inf, "Informações Importantes!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Contrato_Load(object sender, EventArgs e)
        {
            if (verificaCadastro())
                preencheCampos();

        }

        private void buttonSalvar_Click(object sender, EventArgs e)
        {
            if (textCodigo.Text == string.Empty)
            {
                cadastraContrato();

            }
            else
                atualizaContrato();

        }

        #endregion

        #region Funções

        private bool verificaCadastro()
        {
            DataTable dt = FuncoesBD.realizaConsulta("Contrato");
            if (dt.Rows.Count <= 0) 
                return false;
            else
                return true;
        }

        private void preencheCampos()
        {
            DataTable cliente = FuncoesBD.realizaConsulta("Contrato");
            foreach (Control crtl in groupContrato.Controls)
            {
                if (crtl.Tag == null) continue;

                if (crtl.Tag.ToString() != string.Empty)
                {
                    if (crtl is TextBox)
                        crtl.Text = cliente.Rows[0][crtl.Tag.ToString()].ToString();

                }
            }

        }

        private void cadastraContrato()
        {

            textCodigo.Text = FuncoesBD.insereRegistroPorCampos("Contrato", Funcoes.selecionaCampos((GroupBox.ControlCollection)groupContrato.Controls), ref retornoBD).ToString();

            if (!Funcoes.retornaBD(retornoBD, "Cadastro"))
                textCodigo.Text = string.Empty;

        }

        private void atualizaContrato()
        {

            FuncoesBD.atualizaRegistroPorCampos("Contrato", Funcoes.selecionaCampos((GroupBox.ControlCollection)groupContrato.Controls), ref retornoBD);

            Funcoes.retornaBD(retornoBD, "Atualização");

        }

        #endregion
    }
}
