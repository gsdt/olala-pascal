using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;

namespace FastColoredTextBoxNS
{
    public partial class FindForm : Form
    {
        bool firstSearch = true;
        Place startPlace;
        FastColoredTextBox tb;
        //string Language;

        public FindForm(FastColoredTextBox tb)
        {
            InitializeComponent();
            this.tb = tb;
            //Language = tb.InterfaceLanguage;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btFindNext_Click(object sender, EventArgs e)
        {
            FindNext(tbFind.Text);
        }

        public virtual void FindNext(string pattern)
        {
            try
            {
                RegexOptions opt = cbMatchCase.Checked ? RegexOptions.None : RegexOptions.IgnoreCase;
                if (!cbRegex.Checked)
                    pattern = Regex.Escape(pattern);
                if (cbWholeWord.Checked)
                    pattern = "\\b" + pattern + "\\b";
                //
                Range range = tb.Selection.Clone();
                range.Normalize();
                //
                if (firstSearch)
                {
                    startPlace = range.Start;
                    firstSearch = false;
                }
                //
                range.Start = range.End;
                if (range.Start >= startPlace)
                    range.End = new Place(tb.GetLineLength(tb.LinesCount - 1), tb.LinesCount - 1);
                else
                    range.End = startPlace;
                //
                foreach (var r in range.GetRangesByLines(pattern, opt))
                {
                    tb.Selection = r;
                    tb.DoSelectionVisible();
                    tb.Invalidate();
                    return;
                }
                //
                if (range.Start >= startPlace && startPlace > Place.Empty)
                {
                    tb.Selection.Start = new Place(0, 0);
                    FindNext(pattern);
                    return;
                }
                MessageBox.Show(FindForm_["Not found"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tbFind_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                btFindNext.PerformClick();
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '\x1b')
            {
                Hide();
                e.Handled = true;
                return;
            }
        }

        private void FindForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
            this.tb.Focus();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnActivated(EventArgs e)
        {
            tbFind.Focus();
            ResetSerach();
        }

        void ResetSerach()
        {
            firstSearch = true;
        }

        private void cbMatchCase_CheckedChanged(object sender, EventArgs e)
        {
            ResetSerach();
        }

        private string Translate(string s)
        {
            string tmp;
            if (FindForm_.TryGetValue(s, out tmp))
                return tmp;
            return s;
        }
        public void InitLanguage()
        {
            string path;
            string[] line;
            string Language = File.ReadAllText(AppPath.Data+"lang");
            path = "Language\\" + Language + "\\FindForm.lang";
            FindForm_ = new Dictionary<string, string>();
            if (Language == "English") return;
            line = File.ReadAllLines(path);
            foreach (string curent in line)
            {
                if (curent == "") continue;
                string[] s = curent.Split('=');
                FindForm_.Add(s[0], s[1]);
                //System.Windows.Forms.MessageBox.Show(curent);
            }
            this.btClose.Text = Translate("Close");
            this.btFindNext.Text = Translate("Find next");
            this.cbRegex.Text = Translate("Regex");
            this.cbMatchCase.Text = Translate("Match case");
            this.label1.Text = Translate("Find: ");
            this.cbWholeWord.Text = Translate("Match whole word");
            this.Text = Translate("Find");


        }
        private Dictionary<string, string> FindForm_;
        
    }
}
