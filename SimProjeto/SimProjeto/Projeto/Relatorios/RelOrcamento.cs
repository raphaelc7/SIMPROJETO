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
    public partial class RelOrcamento : Base.Formularios.Base
    {
        public string cod;
        public RelOrcamento(string codigo)
        {
            cod = codigo;
            InitializeComponent();
        }

        private void Relatorio_Load(object sender, EventArgs e)
        {
            DataTable dtMateriais = FuncoesBD.realizaConsulta("Materiais", " I_Orcamento = " + cod);
            DataTable dtProfissionais = FuncoesBD.realizaConsulta("Profissionais", " I_Orcamento = " + cod);
            DataTable dtOrcamento = FuncoesBD.realizaConsulta("Orcamento", " Codigo = " + cod);

            rptrvwOrcamento.LocalReport.DataSources.Clear();
            rptrvwOrcamento.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("Materiais", dtMateriais));
            rptrvwOrcamento.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("Profissionais", dtProfissionais));
            rptrvwOrcamento.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("Orcamento", dtOrcamento));
            this.rptrvwOrcamento.RefreshReport();
        }
    }
}
