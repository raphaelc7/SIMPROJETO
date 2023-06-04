using SimProjeto.Base.Classes;
using SimProjeto.Base.Formularios;
using SimProjeto.Principal.Classes;
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
    public partial class Orcamento : Base.Formularios.Base
    {
        public Orcamento()
        {
            InitializeComponent();
        }

        #region Eventos Orçamento


        private void buttonSalvar_Click(object sender, EventArgs e)
        {
            if (textCodigo.Text == string.Empty)
            {
                if (!validaCampos()) return;

                cadastraOrcamento();
            }
            else
            {
                if (!validaCampos()) return;

                atualizaOrcamento();
            }

        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
           
            var frmPesquisa = new Pesquisa("Orcamento");
            frmPesquisa.ShowDialog();

            string codigo = frmPesquisa.codigo;
            if (codigo == string.Empty)
                return;

            preencheCampos(codigo);
            preencheMateriais();
            preencheProfissionais();



        }

        private void buttonLimpar_Click(object sender, EventArgs e)
        {
            Funcoes.limpaCampos(Funcoes.selecionaCampos((GroupBox.ControlCollection)groupCabecalho.Controls));
            Funcoes.limpaCampos(Funcoes.selecionaCampos((GroupBox.ControlCollection)groupLancamentos.Controls));
            Funcoes.limpaCampos(Funcoes.selecionaCampos((GroupBox.ControlCollection)groupMateriais.Controls));
            Funcoes.limpaCampos(Funcoes.selecionaCampos((GroupBox.ControlCollection)tbPageMateriais.Controls));
            Funcoes.limpaCampos(Funcoes.selecionaCampos((GroupBox.ControlCollection)tbPageProfissionais.Controls));
            calculaMateriais();
            calculaProfissionais();
        }

        private void buttonExcluir_Click(object sender, EventArgs e)
        {
            if (textCodigo.Text == string.Empty) return;

            if (MessageBox.Show("Confirma a exclusão do registro?", "Essa ação é irreversível!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

            {
                
                FuncoesBD.deletaRegistro("Orcamento", textCodigo.Text, ref retornoBD);

                if (Funcoes.retornaBD(retornoBD, "Exclusão"))

                    Funcoes.limpaCampos(Funcoes.selecionaCampos((GroupBox.ControlCollection)groupCabecalho.Controls));
            }
        }

        private void buttonBuscaCliente_Click(object sender, EventArgs e)
        {
            textCodigoCliente.Clear();
            textNomeCliente.Clear();

            var frmPesquisa = new Pesquisa();
            frmPesquisa.ShowDialog();

            string codigo = frmPesquisa.codigo;
            if (codigo == string.Empty)
                return;

            preencheCamposClientes(codigo);

        }

        private void textCodigo_TextChanged(object sender, EventArgs e)
        {
            if (textCodigo.Text == string.Empty)
            {
                groupLancamentos.Enabled = false;
                buttonRelatorio.Enabled = false;
                buttonContrato.Enabled = false;
            }
            else
            {
                groupLancamentos.Enabled = true;
                buttonRelatorio.Enabled = true;
                buttonContrato.Enabled = true;
            }
        }

        private void buttonAjuda_Click(object sender, EventArgs e)
        {
            string info = "Caso esteja marcada a opção 'Projeto', o relatório será exibido com a descrição 'Projeto'\r\n" +
                          "Caso contrário, será exibido como 'Orçamento'.";

            MessageBox.Show(info, "Informações Importantes!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Funções Orçamento

        private void cadastraOrcamento()
        {      

            textCodigo.Text = (FuncoesBD.insereRegistroPorCampos("Orcamento", Funcoes.selecionaCampos((GroupBox.ControlCollection)groupCabecalho.Controls), ref retornoBD)).ToString();

            Funcoes.retornaBD(retornoBD, "Cadastro");           

        }
        
        private void atualizaOrcamento()
        {

            FuncoesBD.atualizaRegistroPorCampos("Orcamento", Funcoes.selecionaCampos((GroupBox.ControlCollection)groupCabecalho.Controls), ref retornoBD);

            Funcoes.retornaBD(retornoBD, "Atualização");           

        }

        private void preencheCampos(string codigo)
        {
            DataTable dt = FuncoesBD.realizaConsulta("Orcamento", " Codigo = " + codigo, "");
            foreach (Control crtl in groupCabecalho.Controls)
            {
                if (crtl.Tag == null) continue;

                if (crtl.Tag.ToString() != string.Empty)
                {
                    if (crtl is TextBox)
                        crtl.Text = dt.Rows[0][crtl.Tag.ToString()].ToString();

                    if (crtl is MaskedTextBox)
                        crtl.Text = dt.Rows[0][crtl.Tag.ToString()].ToString();

                    if (crtl is CheckBox)
                    {
                        CheckBox chk = (CheckBox)crtl;
                        chk.Checked = dt.Rows[0][crtl.Tag.ToString()].ToString() == "1" ? true : false;
                    }
                        
                }
            }

        }

        private bool validaCampos()
        {
            if (textNome.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Nome deve ser preenchido!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textNome.Focus();
                return false;
            }
            if (!Funcoes.validaDateTime(textData.Text.ToString()))
            {
                MessageBox.Show("Data deve ser preenchida corretamente!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textData.Focus();
                return false;
            }
            if (textCodigoCliente.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Cliente deve ser preenchido!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                buttonBuscaCliente.Focus();
                return false;
            }

            return true;
        }

        private void preencheCamposClientes(string codigo)
        {
            DataTable dt = FuncoesBD.realizaConsulta("Clientes", " Codigo = " + codigo, "");

            textCodigoCliente.Text = dt.Rows[0]["Codigo"].ToString();
            textNomeCliente.Text = dt.Rows[0]["S_Nome"].ToString();

        }


        #endregion

        #region Materiais

        private void buttonInsereMaterial_Click(object sender, EventArgs e)
        {
            if (validaCamposMateriais())
            {
                Materiais material = new Materiais();
                material.Orcamento = Convert.ToInt32(textCodigo.Text);
                material.Material = textMaterial.Text;
                material.Valor = Convert.ToDecimal(textValorMaterial.Text);
                material.Quantidade = Convert.ToDecimal(textQuantidadeMaterial.Text);
                material.Valor_Total = Math.Round(material.Valor * material.Quantidade, 2);
                material.Unidade = textUnidadeMaterial.Text;

                Materiais.insereMaterial(material, ref retornoBD);

                if (Funcoes.retornaBD(retornoBD, "Inserção"))
                {
                    Funcoes.limpaCampos(Funcoes.selecionaCampos((GroupBox.ControlCollection)groupMateriais.Controls));
                    preencheMateriais();
                }

            }
        }

        private void buttonApagarMaterial_Click(object sender, EventArgs e)
        {
            if (gridMateriais.Rows.Count == 0) return;
            if (gridMateriais.SelectedRows.Count == 0) return;

            if (MessageBox.Show("Confirma a exclusão do material selecionado?", "Essa ação é irreversível!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            string codigo = gridMateriais.SelectedRows[0].Cells["Codigo"].Value.ToString();

            FuncoesBD.deletaRegistro("Materiais", codigo, ref retornoBD);

            if (Funcoes.retornaBD(retornoBD, "Exclusão"))
                preencheMateriais();


        }

        private void preencheMateriais()
        {
            DataTable dt = FuncoesBD.realizaConsulta("Materiais", " I_Orcamento = " + textCodigo.Text, "");
            gridMateriais.AutoGenerateColumns = false;
            gridMateriais.DataSource = dt;
            calculaMateriais();          
        }

        private bool validaCamposMateriais()
        {
            if (textMaterial.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Material deve ser preenchido!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textMaterial.Focus();
                return false;
            }
            if (!Funcoes.validaDecimal(textQuantidadeMaterial.Text))
            {
                MessageBox.Show("Quantidade deve ser preenchida corretamente!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textQuantidadeMaterial.Focus();
                return false;
            }
            if (!Funcoes.validaDecimal(textValorMaterial.Text))
            {
                MessageBox.Show("Valor deve ser preenchido corretamente!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textValorMaterial.Focus();
                return false;
            }
            if (textUnidadeMaterial.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Unidade deve ser preenchida!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textUnidadeMaterial.Focus();
                return false;
            }

            return true;
        }

        private void calculaMateriais()
        {
            if (gridMateriais.Rows.Count == 0)
            {
                labelMateriais.Text = "Total Materiais: R$ 0,00";
                return;
            }

            decimal valortotal = 0;
            foreach (DataGridViewRow material in gridMateriais.Rows)
            {


                valortotal += Convert.ToDecimal(material.Cells["D_ValorTotal"].Value);
            }

            labelMateriais.Text = "Total Materiais: R$ " + valortotal.ToString("N2");
        }

        #endregion

        #region Profissionais

        private void buttonInsereProfissionais_Click(object sender, EventArgs e)
        {
            if (validaCamposProfissionais())
            {
                Profissionais profissional = new Profissionais();
                profissional.Orcamento = Convert.ToInt32(textCodigo.Text);
                profissional.Profissional = textProfisisonal.Text;
                profissional.Valor = Convert.ToDecimal(textValorProfissional.Text);
                profissional.Quantidade = Convert.ToDecimal(textQuantidadeProfissional.Text);
                profissional.Valor_Total = Math.Round(profissional.Valor * profissional.Quantidade, 2);
                profissional.Tipo = textTipoProfissional.Text;

                Profissionais.insereProfissional(profissional, ref retornoBD);

                if (Funcoes.retornaBD(retornoBD, "Inserção"))
                {
                    Funcoes.limpaCampos(Funcoes.selecionaCampos((GroupBox.ControlCollection)groupProfissionais.Controls));
                    preencheProfissionais();
                }

            }
        }

        private void buttonApagarProfissional_Click(object sender, EventArgs e)
        {
            if (gridProfissionais.Rows.Count == 0) return;
            if (gridProfissionais.SelectedRows.Count == 0) return;

            if (MessageBox.Show("Confirma a exclusão do profissional selecionado?", "Essa ação é irreversível!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            string codigo = gridProfissionais.SelectedRows[0].Cells["CodigoP"].Value.ToString();

            FuncoesBD.deletaRegistro("Profissionais", codigo, ref retornoBD);

            if (Funcoes.retornaBD(retornoBD, "Exclusão"))
                preencheProfissionais();

        }

        private void preencheProfissionais()
        {
            DataTable dt = FuncoesBD.realizaConsulta("Profissionais", " I_Orcamento = " + textCodigo.Text, "");
            gridProfissionais.AutoGenerateColumns = false;
            gridProfissionais.DataSource = dt;
            calculaProfissionais();
        }

        private bool validaCamposProfissionais()
        {
            if (textProfisisonal.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Profissional deve ser preenchido!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textProfisisonal.Focus();
                return false;
            }
            if (!Funcoes.validaDecimal(textQuantidadeProfissional.Text))
            {
                MessageBox.Show("Quantidade deve ser preenchida corretamente!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textQuantidadeProfissional.Focus();
                return false;
            }
            if (!Funcoes.validaDecimal(textValorProfissional.Text))
            {
                MessageBox.Show("Valor deve ser preenchido corretamente!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textValorProfissional.Focus();
                return false;
            }
            if (textTipoProfissional.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Tipo deve ser preenchido!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textTipoProfissional.Focus();
                return false;
            }

            return true;
        }

        private void calculaProfissionais()
        {
            if (gridProfissionais.Rows.Count == 0)
            {
                labelProfissionais.Text = "Total Profissionais: R$ 0,00";
                return;
            }

            decimal valortotal = 0;
            foreach (DataGridViewRow profissional in gridProfissionais.Rows)
            {


                valortotal += Convert.ToDecimal(profissional.Cells["D_ValorTotalP"].Value);
            }

            labelProfissionais.Text = "Total Profissionais: R$ " + valortotal.ToString("N2"); 
        }

        #endregion

        #region Relatórios

        private void buttonRelatorio_Click(object sender, EventArgs e)
        {
            if (textCodigo.Text == string.Empty) return;

            RelOrcamento frmRel = new RelOrcamento(textCodigo.Text);
            frmRel.Show();
        }

        private void buttonContrato_Click(object sender, EventArgs e)
        {
            if (textCodigo.Text == string.Empty) return;

            RelContrato frmRel = new RelContrato(textCodigo.Text);
            frmRel.Show();

        }


        #endregion

       
    }
}
