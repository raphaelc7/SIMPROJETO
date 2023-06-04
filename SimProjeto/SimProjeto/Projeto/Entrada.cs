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
    public partial class Entrada : Base.Formularios.Base
    {
        public Entrada()
        {
            InitializeComponent();
        }

        #region Eventos

        #endregion

        private void botaoEntrar_Click(object sender, EventArgs e)
        {
            Hide();
            Form principal = new Principal();
            principal.Closed += (s, args) => Close();
            principal.Show();
        }
    }
}
