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
    public partial class Principal : Base.Formularios.Base
    {
        public Principal()
        {
            InitializeComponent();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmClientes = new Clientes();
            frmClientes.MdiParent = this;
            frmClientes.Show();

        }

        private void contratoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmContrato = new Contrato();
            frmContrato.MdiParent = this;
            frmContrato.Show();

        }

        private void orcamentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmOrcamento = new Orcamento();
            frmOrcamento.MdiParent = this;
            frmOrcamento.Show();

        }
    }
}
