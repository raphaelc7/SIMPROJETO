namespace SimProjeto.Formularios
{
    partial class Contrato
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Contrato));
            this.groupButtons = new System.Windows.Forms.GroupBox();
            this.buttonAjuda = new System.Windows.Forms.Button();
            this.buttonSalvar = new System.Windows.Forms.Button();
            this.groupContrato = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textCidade = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textCodigo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textCPFCNPJ = new System.Windows.Forms.TextBox();
            this.textNome = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textContrato = new System.Windows.Forms.TextBox();
            this.groupButtons.SuspendLayout();
            this.groupContrato.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupButtons
            // 
            this.groupButtons.Controls.Add(this.buttonAjuda);
            this.groupButtons.Controls.Add(this.buttonSalvar);
            resources.ApplyResources(this.groupButtons, "groupButtons");
            this.groupButtons.Name = "groupButtons";
            this.groupButtons.TabStop = false;
            // 
            // buttonAjuda
            // 
            resources.ApplyResources(this.buttonAjuda, "buttonAjuda");
            this.buttonAjuda.AccessibleRole = System.Windows.Forms.AccessibleRole.Cursor;
            this.buttonAjuda.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonAjuda.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonAjuda.Name = "buttonAjuda";
            this.buttonAjuda.UseVisualStyleBackColor = false;
            this.buttonAjuda.Click += new System.EventHandler(this.buttonAjuda_Click);
            // 
            // buttonSalvar
            // 
            resources.ApplyResources(this.buttonSalvar, "buttonSalvar");
            this.buttonSalvar.AccessibleRole = System.Windows.Forms.AccessibleRole.Cursor;
            this.buttonSalvar.BackColor = System.Drawing.Color.MediumAquamarine;
            this.buttonSalvar.Name = "buttonSalvar";
            this.buttonSalvar.UseVisualStyleBackColor = false;
            this.buttonSalvar.Click += new System.EventHandler(this.buttonSalvar_Click);
            // 
            // groupContrato
            // 
            this.groupContrato.Controls.Add(this.label4);
            this.groupContrato.Controls.Add(this.textCidade);
            this.groupContrato.Controls.Add(this.label3);
            this.groupContrato.Controls.Add(this.textCodigo);
            this.groupContrato.Controls.Add(this.label5);
            this.groupContrato.Controls.Add(this.textCPFCNPJ);
            this.groupContrato.Controls.Add(this.textNome);
            this.groupContrato.Controls.Add(this.label2);
            this.groupContrato.Controls.Add(this.label1);
            this.groupContrato.Controls.Add(this.textContrato);
            resources.ApplyResources(this.groupContrato, "groupContrato");
            this.groupContrato.Name = "groupContrato";
            this.groupContrato.TabStop = false;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // textCidade
            // 
            resources.ApplyResources(this.textCidade, "textCidade");
            this.textCidade.Name = "textCidade";
            this.textCidade.Tag = "S_Cidade";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textCodigo
            // 
            resources.ApplyResources(this.textCodigo, "textCodigo");
            this.textCodigo.Name = "textCodigo";
            this.textCodigo.Tag = "Codigo";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // textCPFCNPJ
            // 
            resources.ApplyResources(this.textCPFCNPJ, "textCPFCNPJ");
            this.textCPFCNPJ.Name = "textCPFCNPJ";
            this.textCPFCNPJ.Tag = "S_CPFCNPJ";
            // 
            // textNome
            // 
            resources.ApplyResources(this.textNome, "textNome");
            this.textNome.Name = "textNome";
            this.textNome.Tag = "S_Nome";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textContrato
            // 
            resources.ApplyResources(this.textContrato, "textContrato");
            this.textContrato.Name = "textContrato";
            this.textContrato.Tag = "S_Contrato";
            // 
            // Contrato
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupContrato);
            this.Controls.Add(this.groupButtons);
            this.Name = "Contrato";
            this.Load += new System.EventHandler(this.Contrato_Load);
            this.groupButtons.ResumeLayout(false);
            this.groupContrato.ResumeLayout(false);
            this.groupContrato.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupButtons;
        private System.Windows.Forms.Button buttonSalvar;
        private System.Windows.Forms.GroupBox groupContrato;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textContrato;
        private System.Windows.Forms.Button buttonAjuda;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textNome;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textCPFCNPJ;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textCodigo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textCidade;
    }
}