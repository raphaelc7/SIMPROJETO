using SimProjeto.Base.Classes;
using SimProjeto.Base.Formularios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimProjeto.Formularios
{
    public partial class RelContrato : Base.Formularios.Base
    {
        public string cod;

        public RelContrato(string codigo)
        {
            cod = codigo;
            InitializeComponent();
        }

        private void Relatorio_Load(object sender, EventArgs e)
        {

            DataTable dtOrcamento = FuncoesBD.realizaConsulta("Orcamento", " Codigo = " + cod);
            DataTable dtContrato = FuncoesBD.realizaConsulta("Contrato");
            DataTable dtClientes = FuncoesBD.realizaConsulta("Clientes", " Codigo = " + dtOrcamento.Rows[0]["I_Cliente"].ToString());

            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtf = culture.DateTimeFormat;

            DateTime data = Convert.ToDateTime(dtOrcamento.Rows[0]["A_Data"].ToString());

            string sdata = data.Day + " de " + culture.TextInfo.ToTitleCase(dtf.GetMonthName(data.Month)) + " de " + data.Year;


            rprtvwContrato.LocalReport.DataSources.Clear();
            rprtvwContrato.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("Orcamento", dtOrcamento));
            rprtvwContrato.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("Contrato", dtContrato));
            rprtvwContrato.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("Clientes", dtClientes));

            rprtvwContrato.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter("Data", sdata));

            this.rprtvwContrato.RefreshReport();
        }
    }
}
