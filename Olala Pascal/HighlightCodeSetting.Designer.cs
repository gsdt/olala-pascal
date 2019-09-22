using OlalaPascal;
namespace OlalaPascal
{
    partial class HighlightCodeSetting
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HighlightCodeSetting));
            this.listBox = new System.Windows.Forms.ListBox();
            this.genaralGroupBox = new System.Windows.Forms.GroupBox();
            this.tb_Background = new System.Windows.Forms.Label();
            this.but_Background = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.fontSizeComboBox = new System.Windows.Forms.ComboBox();
            this.fontComboBox = new System.Windows.Forms.ComboBox();
            this.sizefontLabel = new System.Windows.Forms.Label();
            this.fontLabel = new System.Windows.Forms.Label();
            this.typeLabel = new System.Windows.Forms.Label();
            this.OptionGroupBox = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.backgroundText = new System.Windows.Forms.Label();
            this.foregroundText = new System.Windows.Forms.Label();
            this.foregroundButton = new System.Windows.Forms.Button();
            this.backgroundButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.strikeoutCheckbox = new System.Windows.Forms.CheckBox();
            this.underlineCheckbox = new System.Windows.Forms.CheckBox();
            this.italicsCheckbox = new System.Windows.Forms.CheckBox();
            this.boldCheckbox = new System.Windows.Forms.CheckBox();
            this.fastColoredTextBox = new FastColoredTextBoxNS.FastColoredTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.SampleComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.genaralGroupBox.SuspendLayout();
            this.OptionGroupBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBox)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox
            // 
            this.listBox.Font = new System.Drawing.Font("Minion Pro", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox.FormattingEnabled = true;
            this.listBox.ItemHeight = 22;
            this.listBox.Location = new System.Drawing.Point(12, 198);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(284, 224);
            this.listBox.TabIndex = 0;
            this.listBox.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // genaralGroupBox
            // 
            this.genaralGroupBox.Controls.Add(this.tb_Background);
            this.genaralGroupBox.Controls.Add(this.but_Background);
            this.genaralGroupBox.Controls.Add(this.label5);
            this.genaralGroupBox.Controls.Add(this.fontSizeComboBox);
            this.genaralGroupBox.Controls.Add(this.fontComboBox);
            this.genaralGroupBox.Controls.Add(this.sizefontLabel);
            this.genaralGroupBox.Controls.Add(this.fontLabel);
            this.genaralGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.genaralGroupBox.Location = new System.Drawing.Point(12, 12);
            this.genaralGroupBox.Name = "genaralGroupBox";
            this.genaralGroupBox.Size = new System.Drawing.Size(284, 135);
            this.genaralGroupBox.TabIndex = 1;
            this.genaralGroupBox.TabStop = false;
            this.genaralGroupBox.Text = "General";
            // 
            // tb_Background
            // 
            this.tb_Background.AutoSize = true;
            this.tb_Background.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Background.Location = new System.Drawing.Point(162, 106);
            this.tb_Background.Name = "tb_Background";
            this.tb_Background.Size = new System.Drawing.Size(61, 13);
            this.tb_Background.TabIndex = 8;
            this.tb_Background.Text = "Foreground";
            // 
            // but_Background
            // 
            this.but_Background.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.but_Background.Location = new System.Drawing.Point(84, 101);
            this.but_Background.Name = "but_Background";
            this.but_Background.Size = new System.Drawing.Size(62, 23);
            this.but_Background.TabIndex = 6;
            this.but_Background.UseVisualStyleBackColor = true;
            this.but_Background.Click += new System.EventHandler(this.but_Background_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(13, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Background";
            // 
            // fontSizeComboBox
            // 
            this.fontSizeComboBox.DropDownHeight = 100;
            this.fontSizeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fontSizeComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fontSizeComboBox.FormattingEnabled = true;
            this.fontSizeComboBox.IntegralHeight = false;
            this.fontSizeComboBox.Location = new System.Drawing.Point(84, 64);
            this.fontSizeComboBox.MaxLength = 2;
            this.fontSizeComboBox.Name = "fontSizeComboBox";
            this.fontSizeComboBox.Size = new System.Drawing.Size(79, 21);
            this.fontSizeComboBox.TabIndex = 3;
            this.fontSizeComboBox.SelectedIndexChanged += new System.EventHandler(this.fontSizeComboBox_SelectedIndexChanged);
            // 
            // fontComboBox
            // 
            this.fontComboBox.DropDownHeight = 100;
            this.fontComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fontComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fontComboBox.FormattingEnabled = true;
            this.fontComboBox.IntegralHeight = false;
            this.fontComboBox.Location = new System.Drawing.Point(84, 29);
            this.fontComboBox.Name = "fontComboBox";
            this.fontComboBox.Size = new System.Drawing.Size(174, 21);
            this.fontComboBox.Sorted = true;
            this.fontComboBox.TabIndex = 2;
            this.fontComboBox.SelectedIndexChanged += new System.EventHandler(this.fontComboBox_SelectedIndexChanged);
            // 
            // sizefontLabel
            // 
            this.sizefontLabel.AutoSize = true;
            this.sizefontLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sizefontLabel.Location = new System.Drawing.Point(13, 72);
            this.sizefontLabel.Name = "sizefontLabel";
            this.sizefontLabel.Size = new System.Drawing.Size(27, 13);
            this.sizefontLabel.TabIndex = 1;
            this.sizefontLabel.Text = "Size";
            // 
            // fontLabel
            // 
            this.fontLabel.AutoSize = true;
            this.fontLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fontLabel.Location = new System.Drawing.Point(13, 37);
            this.fontLabel.Name = "fontLabel";
            this.fontLabel.Size = new System.Drawing.Size(28, 13);
            this.fontLabel.TabIndex = 0;
            this.fontLabel.Text = "Font";
            // 
            // typeLabel
            // 
            this.typeLabel.AutoSize = true;
            this.typeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.typeLabel.Location = new System.Drawing.Point(18, 166);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(31, 13);
            this.typeLabel.TabIndex = 4;
            this.typeLabel.Text = "Type";
            // 
            // OptionGroupBox
            // 
            this.OptionGroupBox.Controls.Add(this.groupBox2);
            this.OptionGroupBox.Controls.Add(this.groupBox1);
            this.OptionGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OptionGroupBox.Location = new System.Drawing.Point(316, 12);
            this.OptionGroupBox.Name = "OptionGroupBox";
            this.OptionGroupBox.Size = new System.Drawing.Size(452, 135);
            this.OptionGroupBox.TabIndex = 5;
            this.OptionGroupBox.TabStop = false;
            this.OptionGroupBox.Text = "Optional";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.backgroundText);
            this.groupBox2.Controls.Add(this.foregroundText);
            this.groupBox2.Controls.Add(this.foregroundButton);
            this.groupBox2.Controls.Add(this.backgroundButton);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(18, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(235, 105);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Color";
            // 
            // backgroundText
            // 
            this.backgroundText.AutoSize = true;
            this.backgroundText.Location = new System.Drawing.Point(162, 65);
            this.backgroundText.Name = "backgroundText";
            this.backgroundText.Size = new System.Drawing.Size(61, 13);
            this.backgroundText.TabIndex = 5;
            this.backgroundText.Text = "Foreground";
            this.backgroundText.Click += new System.EventHandler(this.backgroundText_Click);
            // 
            // foregroundText
            // 
            this.foregroundText.AutoSize = true;
            this.foregroundText.Location = new System.Drawing.Point(162, 26);
            this.foregroundText.Name = "foregroundText";
            this.foregroundText.Size = new System.Drawing.Size(61, 13);
            this.foregroundText.TabIndex = 4;
            this.foregroundText.Text = "Foreground";
            // 
            // foregroundButton
            // 
            this.foregroundButton.Location = new System.Drawing.Point(94, 21);
            this.foregroundButton.Name = "foregroundButton";
            this.foregroundButton.Size = new System.Drawing.Size(62, 23);
            this.foregroundButton.TabIndex = 0;
            this.foregroundButton.UseVisualStyleBackColor = true;
            this.foregroundButton.Click += new System.EventHandler(this.foregroundButton_Click);
            // 
            // backgroundButton
            // 
            this.backgroundButton.Location = new System.Drawing.Point(94, 60);
            this.backgroundButton.Name = "backgroundButton";
            this.backgroundButton.Size = new System.Drawing.Size(62, 23);
            this.backgroundButton.TabIndex = 1;
            this.backgroundButton.UseVisualStyleBackColor = true;
            this.backgroundButton.Click += new System.EventHandler(this.backgroundButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Background";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Foreground";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.strikeoutCheckbox);
            this.groupBox1.Controls.Add(this.underlineCheckbox);
            this.groupBox1.Controls.Add(this.italicsCheckbox);
            this.groupBox1.Controls.Add(this.boldCheckbox);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(259, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(172, 105);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Style";
            // 
            // strikeoutCheckbox
            // 
            this.strikeoutCheckbox.AutoSize = true;
            this.strikeoutCheckbox.Location = new System.Drawing.Point(95, 23);
            this.strikeoutCheckbox.Name = "strikeoutCheckbox";
            this.strikeoutCheckbox.Size = new System.Drawing.Size(68, 17);
            this.strikeoutCheckbox.TabIndex = 3;
            this.strikeoutCheckbox.Text = "Strikeout";
            this.strikeoutCheckbox.UseVisualStyleBackColor = true;
            this.strikeoutCheckbox.Click += new System.EventHandler(this.strikeoutCheckbox_Click);
            // 
            // underlineCheckbox
            // 
            this.underlineCheckbox.AutoSize = true;
            this.underlineCheckbox.Location = new System.Drawing.Point(95, 60);
            this.underlineCheckbox.Name = "underlineCheckbox";
            this.underlineCheckbox.Size = new System.Drawing.Size(71, 17);
            this.underlineCheckbox.TabIndex = 2;
            this.underlineCheckbox.Text = "Underline";
            this.underlineCheckbox.UseVisualStyleBackColor = true;
            this.underlineCheckbox.Click += new System.EventHandler(this.underlineCheckbox_Click);
            // 
            // italicsCheckbox
            // 
            this.italicsCheckbox.AutoSize = true;
            this.italicsCheckbox.Location = new System.Drawing.Point(15, 60);
            this.italicsCheckbox.Name = "italicsCheckbox";
            this.italicsCheckbox.Size = new System.Drawing.Size(53, 17);
            this.italicsCheckbox.TabIndex = 1;
            this.italicsCheckbox.Text = "Italics";
            this.italicsCheckbox.UseVisualStyleBackColor = true;
            this.italicsCheckbox.Click += new System.EventHandler(this.italicsCheckbox_Click);
            // 
            // boldCheckbox
            // 
            this.boldCheckbox.AutoSize = true;
            this.boldCheckbox.Location = new System.Drawing.Point(15, 23);
            this.boldCheckbox.Name = "boldCheckbox";
            this.boldCheckbox.Size = new System.Drawing.Size(47, 17);
            this.boldCheckbox.TabIndex = 0;
            this.boldCheckbox.Text = "Bold";
            this.boldCheckbox.UseVisualStyleBackColor = true;
            this.boldCheckbox.Click += new System.EventHandler(this.boldCheckbox_Click);
            // 
            // fastColoredTextBox
            // 
            this.fastColoredTextBox.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.fastColoredTextBox.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>:=)\\s*(?<range>[^;]+);\r\n^\\s*(case|default)\\s*[^:" +
    "]*(?<range>:)\\s*(?<range>[^;]+);\r\n";
            this.fastColoredTextBox.AutoScrollMinSize = new System.Drawing.Size(251, 238);
            this.fastColoredTextBox.BackBrush = null;
            this.fastColoredTextBox.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.fastColoredTextBox.CharHeight = 14;
            this.fastColoredTextBox.CharWidth = 8;
            this.fastColoredTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fastColoredTextBox.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fastColoredTextBox.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.fastColoredTextBox.IsReplaceMode = false;
            this.fastColoredTextBox.Language = FastColoredTextBoxNS.Language.Pascal;
            this.fastColoredTextBox.LeftBracket = '(';
            this.fastColoredTextBox.LeftBracket2 = '[';
            this.fastColoredTextBox.Location = new System.Drawing.Point(316, 198);
            this.fastColoredTextBox.Name = "fastColoredTextBox";
            this.fastColoredTextBox.Paddings = new System.Windows.Forms.Padding(0);
            this.fastColoredTextBox.RightBracket = ')';
            this.fastColoredTextBox.RightBracket2 = ']';
            this.fastColoredTextBox.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fastColoredTextBox.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fastColoredTextBox.ServiceColors")));
            this.fastColoredTextBox.Size = new System.Drawing.Size(452, 225);
            this.fastColoredTextBox.TabIndex = 6;
            this.fastColoredTextBox.Text = resources.GetString("fastColoredTextBox.Text");
            this.fastColoredTextBox.Zoom = 100;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(322, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Preview";
            // 
            // okButton
            // 
            this.okButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.Location = new System.Drawing.Point(551, 439);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 10;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(682, 439);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(65, 23);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // colorDialog
            // 
            this.colorDialog.AnyColor = true;
            // 
            // SampleComboBox
            // 
            this.SampleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SampleComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SampleComboBox.FormattingEnabled = true;
            this.SampleComboBox.Location = new System.Drawing.Point(369, 438);
            this.SampleComboBox.Name = "SampleComboBox";
            this.SampleComboBox.Size = new System.Drawing.Size(121, 24);
            this.SampleComboBox.TabIndex = 13;
            this.SampleComboBox.SelectedIndexChanged += new System.EventHandler(this.SampleComboBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(298, 441);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "Sample";
            // 
            // HighlightCodeSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 474);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.SampleComboBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.fastColoredTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.OptionGroupBox);
            this.Controls.Add(this.typeLabel);
            this.Controls.Add(this.genaralGroupBox);
            this.Controls.Add(this.listBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "HighlightCodeSetting";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HighlightCodeSetting";
            this.Load += new System.EventHandler(this.HighlightCodeSetting_Load);
            this.genaralGroupBox.ResumeLayout(false);
            this.genaralGroupBox.PerformLayout();
            this.OptionGroupBox.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.GroupBox genaralGroupBox;
        private System.Windows.Forms.ComboBox fontComboBox;
        private System.Windows.Forms.Label sizefontLabel;
        private System.Windows.Forms.Label fontLabel;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.GroupBox OptionGroupBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox strikeoutCheckbox;
        private System.Windows.Forms.CheckBox underlineCheckbox;
        private System.Windows.Forms.CheckBox italicsCheckbox;
        private System.Windows.Forms.CheckBox boldCheckbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button backgroundButton;
        private System.Windows.Forms.Button foregroundButton;
        private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label foregroundText;
        private System.Windows.Forms.Label backgroundText;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.ComboBox fontSizeComboBox;
        private System.Windows.Forms.Label tb_Background;
        private System.Windows.Forms.Button but_Background;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox SampleComboBox;
        private System.Windows.Forms.Label label4;
    }
}