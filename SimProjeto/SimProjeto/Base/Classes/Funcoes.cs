using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimProjeto.Base.Classes
{
    public class Funcoes
    {

        public static void limpaCampos(List<Control> campos)
        {
            List<TextBox> listtxt = new List<TextBox>();
            List<CheckBox> listchk = new List<CheckBox>();
            List<ComboBox> listcmb = new List<ComboBox>();
            List<MaskedTextBox> listmasked = new List<MaskedTextBox>();
            List<DataGridView> listgrid = new List<DataGridView>();

            foreach (Object control in campos)
            {
                if (control is Button || control is Label)
                    continue;

                if (control is TextBox)
                {
                    listtxt.Add((TextBox)control);

                }

                if (control is CheckBox)
                {
                    listchk.Add((CheckBox)control);

                }

                if (control is ComboBox)
                {
                    listcmb.Add((ComboBox)control);

                }

                if (control is MaskedTextBox)
                {
                    listmasked.Add((MaskedTextBox)control);

                }
                if (control is DataGridView)
                {
                    listgrid.Add((DataGridView)control);

                }
            }

            foreach (TextBox txt in listtxt)
            {
                txt.Clear();
            }

            foreach (CheckBox chk in listchk)
            {
                chk.Checked = false;
            }
            foreach (ComboBox cb in listcmb)
            {
                cb.SelectedIndex = 0;
            }
            foreach (MaskedTextBox msk in listmasked)
            {
                msk.Clear();

            }
            foreach (DataGridView grid in listgrid)
            {
                grid.DataSource = null;

            }





        }

        public static List<Control> selecionaCampos(GroupBox.ControlCollection formControls)
        {

            List<Control> controles = new List<Control>();
            foreach (Control control in formControls)
            {
                if (control is TextBox)
                    controles.Add(control);

                if (control is ComboBox)
                    controles.Add(control);

                if (control is CheckBox)
                    controles.Add(control);

                if (control is MaskedTextBox)
                    controles.Add(control);
                
                if (control is DataGridView)
                    controles.Add(control);

            }

            return controles;

        }

        public static bool validaDateTime (string data)
        {
            try 
            { 
                Convert.ToDateTime(data);
            }
            catch 
            {
                return false;
            }

            return true;
        }

        public static bool validaDecimal(string dec)
        {
            try
            {
                Convert.ToDecimal(dec);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static bool retornaBD(string retornoBD, string tipo)
        {
            if (retornoBD != "ok")
            {
                MessageBox.Show(retornoBD, "Erro no banco de dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            MessageBox.Show(tipo + " realizado(a) com sucesso!!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }

        public static string trataDecimal(string valor)
        {
            valor = valor.Trim();
            valor = valor.Replace(".", "");
            valor = valor.Replace(",", ".");

            return valor;
        }
    }
}
