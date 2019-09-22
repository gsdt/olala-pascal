using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace FastColoredTextBoxNS
{
    public partial class GoToForm : Form
    {
        public int SelectedLineNumber { get; set; }
        public int TotalLineCount { get; set; }

        public GoToForm()
        {
            InitializeComponent();
        }
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.tbLineNumber.Text = this.SelectedLineNumber.ToString();

            this.label.Text = String.Format(GotoForm["Line number (1 - {0}):"], this.TotalLineCount);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            //MessageBox.Show("x");
            this.tbLineNumber.Focus();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int enteredLine;
            if (int.TryParse(this.tbLineNumber.Text, out enteredLine))
            {
                enteredLine = Math.Min(enteredLine, this.TotalLineCount);
                enteredLine = Math.Max(1, enteredLine);

                this.SelectedLineNumber = enteredLine;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
         }
        private void GoToForm_Load(object sender, EventArgs e)
        {
            string path;
            string[] line;
            //**** Go to form ****
            path ="Language\\" + Language + "\\GotoForm.lang";
            GotoForm = new Dictionary<string, string>();
            if (Language == "English") return;
            line = File.ReadAllLines(path);
            foreach (string curent in line)
            {
                if (curent == "") continue;
                string[] s = curent.Split('=');
                GotoForm.Add(s[0], s[1]);
                //System.Windows.Forms.MessageBox.Show(curent);
            }
            InitLanguage();
        }
        private Dictionary<string, string> GotoForm;
        string Language = File.ReadAllText(AppPath.Data+"lang");
            
        private string Translate(string s)
        {
            string tmp;
            if (GotoForm.TryGetValue(s, out tmp))
                return tmp;
            return s;
        }

        private void InitLanguage()
        {
            this.label.Text = Translate("Line Number (1/1):");
            this.btnOk.Text = Translate("OK");
            this.btnCancel.Text = Translate("Cancel");
            this.Text = Translate("Go To Line");
        }
    }
}
