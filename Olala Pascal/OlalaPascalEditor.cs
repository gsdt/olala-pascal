using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FarsiLibrary.Win;
using FastColoredTextBoxNS;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using OlalaPascal;
using System.Globalization;
using System.Xml;
using System.Media;

namespace OlalaPascal
{
    public partial class PowerfulPascalEditor : Form
    {
        //private SettingStatus currentSetting = new SettingStatus();
      
        string[] keywords = { 
            "absolute", "and", "array", "asm", "begin", "case", "const", "constructor", "destructor", "div",  "downto", "do", 
            "else", "end", "file", "for", "function", "goto", "if", "implementation", "in", "inherited", "inline", "interface",  
            "label", "mod", "nil", "not", "object", "of", "on", "operator", "or", "packed", "procedure", "program", "record",  
            "reintroduce", "repeat", "self", "set", "shl", "shr", "string", "then", "to", "type", "unit", "until", "uses", "var",  
            "while", "with", "xor", "as", "class", "dispinterface", "except", "exports", "finalization", "finally", "initialization",
            "inline", "is", "library", "on", "out", "packed", "property", "raise", "resourcestring", "threadvar", "try", "dispose",
            "exit", "false", "new", "true", "abstract", "alias", "assembler", "cdecl", "cppdecl", "default", "export", 
            "external", "far", "far16", "forward", "index", "local", "name", "near", "nostackframe", "oldfpccall", "override", 
            "pascal", "private", "protected", "public", "published", "read", "register", "reintroduce", "safecall", "softfloat",
            "stdcall", "virtual", "write",
            //kiểu dữ liệu...
            "integer","real","boolean","longint","word","char","byte","shortint", "smallint",
            "longword","int64","qword", "cardinal","dword", "single","double","extended","comp",
            "currency", "widechar","shortstring","ansistring"};

        string[] methods = {/* "Equals()", "GetHashCode()", "GetType()", "ToString()" */};
        string[] snippets = { "if (^) then \nbegin\n\nend;","if (^) then\nelse", "for ^ to  do", "while (^) do \n", "repeat\n^\nuntil ;", "case (^) of\n\nend;" };
       
        string[] declarationSnippets = {/* 
               "public class ^\n{\n}", "private class ^\n{\n}", "internal class ^\n{\n}",
               "public struct ^\n{\n;\n}", "private struct ^\n{\n;\n}", "internal struct ^\n{\n;\n}",
               "public void ^()\n{\n;\n}", "private void ^()\n{\n;\n}", "internal void ^()\n{\n;\n}", "protected void ^()\n{\n;\n}",
               "public ^{ get; set; }", "private ^{ get; set; }", "internal ^{ get; set; }", "protected ^{ get; set; }"
               */};
        
        Style invisibleCharsStyle = new InvisibleCharsRenderer(Pens.Gray);
        Color currentLineColor = Color.FromArgb(100, 210, 210, 255);
        Color changedLineColor = Color.FromArgb(255, 243, 168, 218);
        
        public PowerfulPascalEditor(string[] args)
        {
            if(!Directory.Exists(AppPath.Data))
                Directory.CreateDirectory(AppPath.Data);

            new Startup().Show();
            
            SettingStatus.GetHighlightOption();
            
            InitializeComponent();
            
            //init menu images
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PowerfulPascalEditor));
            copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripButton.Image")));
            cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripButton.Image")));
            pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripButton.Image")));
            History.Load();
            for (int i = History.List().Count - 1; i >= 0; i--)
            {
                if(!(History.List()[i].Trim()==""))
                    if(File.Exists(History.List()[i].Trim())) CreateTab(History.List()[i]);
            }
            foreach(string file in args)
            {
                CreateTab(file);
            }
        }


        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateTab("");
        }

        private Style sameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(50, Color.Gray)));
        public TextStyle ParseStyle(XmlNode styleNode)
        {
            XmlAttribute colorA = styleNode.Attributes["foreColor"];
            XmlAttribute backColorA = styleNode.Attributes["backColor"];
            XmlAttribute fontStyleA = styleNode.Attributes["fontStyle"];
            //System.Windows.Forms.MessageBox.Show(colorA.Value+"-"+backColorA.Value+"-"+fontStyleA.Value);
            //colors
            SolidBrush foreBrush = null;
            if (colorA != null)
                foreBrush = new SolidBrush(ParseColor(colorA.Value));
            SolidBrush backBrush = null;
            if (backColorA != null)
                backBrush = new SolidBrush(ParseColor(backColorA.Value));
            //fontStyle
            FontStyle fontStyle = FontStyle.Regular;
            if (styleNode.Attributes["underline"].Value == "yes") fontStyle = fontStyle | FontStyle.Underline;
            if (styleNode.Attributes["italic"].Value == "yes") fontStyle = fontStyle | FontStyle.Italic;
            if (styleNode.Attributes["bold"].Value == "yes") fontStyle = fontStyle | FontStyle.Bold;
            if (styleNode.Attributes["strikeout"].Value == "yes") fontStyle = fontStyle | FontStyle.Strikeout;

            return new TextStyle(foreBrush, backBrush, fontStyle);
        }
        private Color ParseColor(string s)
        {
            if (s.StartsWith("#"))
            {
                if (s.Length <= 7)
                    return Color.FromArgb(255,
                                          Color.FromArgb(Int32.Parse(s.Substring(1), NumberStyles.AllowHexSpecifier)));
                else
                    return Color.FromArgb(Int32.Parse(s.Substring(1), NumberStyles.AllowHexSpecifier));
            }
            else
                return Color.FromName(s);
        }
        private void CreateTab(TextboxState ts)
        {
            string fileName = ts.Name;
            try
            {
                
                var tb = new FastColoredTextBox();
                tb.SyntaxHighlighter.SetSyntaxHighlighter(SettingStatus.doc);
                tb.BackColor = ParseColor(SettingStatus.doc.SelectSingleNode("HighlightSetting/Background").Attributes["color"].Value);
                tb.DefaultStyle = ParseStyle(SettingStatus.doc.SelectSingleNode("HighlightSetting/DefaulStyle"));
                
                tb.HighlightingRangeType = HighlightingRangeType.VisibleRange;
                //tb.Font = new Font("Consolas", 9.75f);
                //tb.Font = (Font)SettingStatus.font.Clone();
                tb.Font = new Font(SettingStatus.font.Name, (float)SettingStatus.font.Size);
                //MessageBox.Show(tb.Font.Name);
                tb.ContextMenuStrip = cmMain;
                tb.Dock = DockStyle.Fill;
                tb.IsCompiled = false;
                tb.BorderStyle = BorderStyle.Fixed3D;
                tb.AutoCompleteBrackets = false;
                tb.AutoIndentChars = true;
                
                //tb.VirtualSpace = true;
                tb.LeftPadding = 17;
                tb.AddStyle(sameWordsStyle);//same words style
                var tab = new FATabStripItem(fileName != "" ? Path.GetFileName(fileName) : "[new]", tb);
                tab.Tag = fileName;
                tab.Name = fileName;
                tb.Language = Language.Custom;
                string extension = Path.GetExtension(tab.Tag.ToString()).ToLower();
                if (fileName != null && (extension == ".pas" || extension == ".pp" || extension == ".inc"))
                    tb.Language = Language.Pascal;
                //if (fileName != null)
                //    tb.OpenFile(fileName);
                tb.Text = ts.Text;
                tb.Tag = new TbInfo();
                tsFiles.AddTab(tab);
                tsFiles.SelectedItem = tab;
                Save(tab);
                tb.Focus();
                tb.DelayedTextChangedInterval = 1000;
                tb.DelayedEventsInterval = 500;
                //tb.TextChangedDelayed += new EventHandler<TextChangedEventArgs>(tb_TextChangedDelayed);
                tb.SelectionChangedDelayed += new EventHandler(tb_SelectionChangedDelayed);
                tb.KeyDown += new KeyEventHandler(tb_KeyDown);
                tb.MouseMove += new MouseEventHandler(tb_MouseMove);
                tb.ZoomChanged += new EventHandler(Zoom_Change);
                tb.ChangedLineColor = changedLineColor;
                tb.TabStop = true;
                tb.ToolTipNeeded += new EventHandler<ToolTipNeededEventArgs>(tb_ToolTipNeeded);
                if (btHighlightCurrentLine.Checked)
                    tb.CurrentLineColor = currentLineColor;
                tb.ShowFoldingLines = btShowFoldingLines.Checked;
                tb.HighlightingRangeType = HighlightingRangeType.VisibleRange;
                //create autocomplete popup menu
                AutocompleteMenu popupMenu = new AutocompleteMenu(tb);
                popupMenu.Items.ImageList = ilAutocomplete;
                popupMenu.Opening += new EventHandler<CancelEventArgs>(popupMenu_Opening);
                BuildAutocompleteMenu(popupMenu);
                (tb.Tag as TbInfo).popupMenu = popupMenu;

                // show zoom size...
                this.toolStripStatusZoom.Text = this.CurrentTB.Zoom.ToString() + "%";
                tb.HorizontalScroll.Value = ts.HorizontalScroll;
                tb.VerticalScroll.Value = ts.VerticalScroll;
                tb.IsChanged = ts.IsChanged;

                foreach (int changed in ts.ChangedList)
                {
                    tb.TextSource[changed].IsChanged = true;
                }
                tb.Selection.Start = ts.Selection;
                //tb.Refresh();
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.Message, Translator.Translate("Error"), MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Retry)
                    CreateTab(fileName);
            }

        }

        private void CreateTab(string fileName)
        {
            for (int i = 0; i < tsFiles.Items.Count; i++ )
            {
                if(tsFiles.Items[i].Name==fileName)
                {
                    tsFiles.SelectedItem = tsFiles.Items[i];
                    return;
                }
            }
            try
            {
                var tb = new FastColoredTextBox();
                tb.SyntaxHighlighter.SetSyntaxHighlighter(SettingStatus.doc);
                tb.BackColor = ParseColor(SettingStatus.doc.SelectSingleNode("HighlightSetting/Background").Attributes["color"].Value);
                tb.HighlightingRangeType = HighlightingRangeType.VisibleRange;
                //tb.Font = new Font("Consolas", 9.75f);
                //tb.Font = (Font)SettingStatus.font.Clone();
                tb.Font = new Font(SettingStatus.font.Name, (float)SettingStatus.font.Size);
                //MessageBox.Show(tb.Font.Name);
                tb.ContextMenuStrip = cmMain;
                tb.Dock = DockStyle.Fill;
                tb.BorderStyle = BorderStyle.Fixed3D;
                tb.AutoCompleteBrackets = false;
                tb.AutoIndentChars = true;
                tb.IsCompiled = false;
                //tb.VirtualSpace = true;
                tb.LeftPadding = 17;
                tb.AddStyle(sameWordsStyle);//same words style
                var tab = new FATabStripItem(fileName!=""?Path.GetFileName(fileName):"[new]", tb);
                tab.Tag = fileName;
                tab.Name = fileName;
                tb.Language = Language.Custom;
                string extension = Path.GetExtension(tab.Tag.ToString()).ToLower();
                if (fileName != "" && (extension == ".pas" || extension == ".pp" || extension == ".inc"))
                    tb.Language = Language.Pascal;
                if (fileName != "" )
                    tb.OpenFile(fileName);
                tb.Tag = new TbInfo();
                tsFiles.AddTab(tab);
                tsFiles.SelectedItem = tab;
                if (fileName == "")
                {
                    tab.Name = null;
                    tab.Tag = null;
                }
                Save(tab);
                tb.Focus();
                tb.DelayedTextChangedInterval = 1000;
                tb.DelayedEventsInterval = 500;
                //tb.TextChangedDelayed += new EventHandler<TextChangedEventArgs>(tb_TextChangedDelayed);
                tb.SelectionChangedDelayed += new EventHandler(tb_SelectionChangedDelayed);
                tb.KeyDown += new KeyEventHandler(tb_KeyDown);
                tb.MouseMove += new MouseEventHandler(tb_MouseMove);
                tb.ZoomChanged += new EventHandler(Zoom_Change);
                tb.ChangedLineColor = changedLineColor;
                tb.TabStop = true;
                tb.ToolTipNeeded += new EventHandler<ToolTipNeededEventArgs>(tb_ToolTipNeeded);
                if(btHighlightCurrentLine.Checked)
                    tb.CurrentLineColor = currentLineColor;
                tb.ShowFoldingLines = btShowFoldingLines.Checked;
                tb.HighlightingRangeType = HighlightingRangeType.VisibleRange;
                //create autocomplete popup menu
                AutocompleteMenu popupMenu = new AutocompleteMenu(tb);
                popupMenu.Items.ImageList = ilAutocomplete;
                popupMenu.Opening += new EventHandler<CancelEventArgs>(popupMenu_Opening);
                BuildAutocompleteMenu(popupMenu);
                (tb.Tag as TbInfo).popupMenu = popupMenu;
                tb.DefaultStyle = ParseStyle(SettingStatus.doc.SelectSingleNode("HighlightSetting/DefaulStyle"));
                
                // show zoom size...
                this.toolStripStatusZoom.Text = this.CurrentTB.Zoom.ToString() + "%";
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.Message, Translator.Translate("Error"), MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Retry)
                    CreateTab(fileName);
            }
            
        }


        private void tb_ToolTipNeeded(object sender, ToolTipNeededEventArgs e)
        {
            ToolTipContens contents = new ToolTipContens();
            if (!string.IsNullOrEmpty(e.HoveredWord))
            {
                string word = e.HoveredWord.ToLower();
                if(contents.Exist(word))
                {
                    e.ToolTipTitle = word;
                    e.ToolTipText = contents.Text(word);
                    //MessageBox.Show(word);
                }
            }

        }

        class ToolTipContens
        {
            private Dictionary<string, string> tooltipText = new Dictionary<string, string>();
            
            public ToolTipContens()
            {
                // data type...
                tooltipText.Add("byte", Translator.Translate("Integer type.")+"\n"+Translator.Translate("Value ranges: 0..255")+"\n"+Translator.Translate("Size: 1 bytes."));
                tooltipText.Add("integer",Translator.Translate("Integer type.")+"\n"+Translator.Translate("Value ranges: -32768..32767")+"\n"+Translator.Translate("Size: 2 bytes."));
                tooltipText.Add("shortint",Translator.Translate("Integer type.")+"\n"+Translator.Translate("Value ranges: -128..127")+"\n"+Translator.Translate("Size: 1 bytes."));
                tooltipText.Add("smallint", Translator.Translate("Integer type.")+"\n"+Translator.Translate("Value ranges: -32768.. 32767")+"\n"+Translator.Translate("Size: 2 bytes."));
                tooltipText.Add("word", Translator.Translate("Integer type.")+"\n"+Translator.Translate("Value ranges: 0..65535")+"\n"+Translator.Translate("Size: 2 bytes."));
                tooltipText.Add("longint", Translator.Translate("Integer type.")+"\n"+Translator.Translate("Value ranges: -2147483648.. 2147483647")+"\n"+Translator.Translate("Size: 4 bytes."));
                tooltipText.Add("longword", Translator.Translate("Integer type.")+"\n"+Translator.Translate("Value ranges: 0.. 4294967295")+"\n"+Translator.Translate("Size: 4 bytes."));
                tooltipText.Add("int64", Translator.Translate("Integer type.")+"\n"+Translator.Translate("Value ranges: -9223372036854775808.. 9223372036854775807")+"\n"+Translator.Translate("Size: 8 bytes."));
                tooltipText.Add("qword", Translator.Translate("Integer type.")+"\n"+Translator.Translate("Value ranges: 0.. 18446744073709551615")+"\n"+Translator.Translate("Size: 8 bytes."));
                tooltipText.Add("cardinal", Translator.Translate("Integer type.")+"\n"+Translator.Translate("Value ranges: 0.. 4294967295")+"\n"+Translator.Translate("Size: 4 bytes."));
                tooltipText.Add("dword", Translator.Translate("Integer type.")+"\n"+Translator.Translate("Value ranges: 0.. 4294967295")+"\n"+Translator.Translate("Size: 4 bytes."));

                tooltipText.Add("real", Translator.Translate("Real type.")+"\n"+Translator.Translate("Value ranges: 2,9*10^-38...1,7*10^38")+"\n"+Translator.Translate("Number of significant digits: unknown.")+"\n"+Translator.Translate("Size: 8 bytes."));
                tooltipText.Add("single", Translator.Translate("Real type.") + "\n" + Translator.Translate("Value ranges: 1,5*10^−45.. 3,4*10^38") + "\n" + Translator.Translate("Number of significant digits: 7 or 8.") + "\n" + Translator.Translate("Size: 4 bytes."));
                tooltipText.Add("double", Translator.Translate("Real type.") + "\n" + Translator.Translate("Value ranges: 5,0*10^−324.. 1,7*10^308") + "\n" + Translator.Translate("Number of significant digits: 15 or 16.") + "\n" + Translator.Translate("Size: 8 bytes."));
                tooltipText.Add("extended", Translator.Translate("Real type.") + "\n" + Translator.Translate("Value ranges: 1,9*10^−4932.. 1,1*10^4932") + "\n" + Translator.Translate("Number of significant digits: 19 or 20.") + "\n" + Translator.Translate("Size: 10 bytes."));
                tooltipText.Add("comp", Translator.Translate("Real type.") + "\n" + Translator.Translate("Value ranges: -2*10^64+1.. 2*10^63-1") + "\n" + Translator.Translate("Number of significant digits: 19 or 20.") + "\n" + Translator.Translate("Size: 8 bytes."));
                tooltipText.Add("currency", Translator.Translate("Real type.") + "\n" + Translator.Translate("Value ranges: -922337203685477.5808.. 922337203685477.5807") + "\n" + Translator.Translate("Number of significant digits: 19 or 20.") + "\n" + Translator.Translate("Size: 8 bytes."));

                tooltipText.Add("char", Translator.Translate("Character type.")+"\n"+Translator.Translate("Number of characters: 1.")+"\n"+Translator.Translate("Size: 1 bytes."));
                tooltipText.Add("widechar", Translator.Translate("String type.")+"\n"+Translator.Translate("Number of characters: unknown.")+"\n"+Translator.Translate("Size: unknown."));
                tooltipText.Add("string", Translator.Translate("String type.")+"\n"+Translator.Translate("Number of characters: 255.")+"\n"+Translator.Translate("Size: unknown."));
                tooltipText.Add("shortstring", Translator.Translate("String type.")+"\n"+Translator.Translate("Number of characters: 255.")+"\n"+Translator.Translate("Size: unknown."));
                tooltipText.Add("ansistring", Translator.Translate("String type.")+"\n"+Translator.Translate("Number of characters: depending on memory.")+"\n"+Translator.Translate("Size: unknown."));

                tooltipText.Add("boolean", Translator.Translate("Logic type.")+"\n"+Translator.Translate("Value ranges: true or false.")+"\n"+Translator.Translate("Size: 1 bit."));
                
                Import("absolute");
                Import("abstract");
                Import("all");
                Import("and");
                Import("array");
                Import("as");
                Import("asm");
                Import("asmname");
                Import("attribute");
                Import("begin");
                Import("bindable");
                Import("case");
                Import("class");
                Import("const");
                Import("constructor");
                Import("destructor");
                Import("div");
                Import("do");
                Import("downto");
                Import("else");
                Import("end");
                Import("export");
                Import("exports");
                Import("external");
                Import("far");
                Import("file");
                Import("finalization");
                Import("for");
                Import("forward");
                Import("function");
                Import("goto");
                Import("if");
                Import("implementation");
                Import("import");
                Import("in");
                Import("inherited");
                Import("initialization");
                Import("interface");
                Import("interrupt");
                Import("is");
                Import("label");
                Import("library");
                Import("mod");
                Import("module");
                Import("name");
                Import("near");
                Import("nil");
                Import("not");
                Import("object");
                Import("of");
                Import("only");
                Import("operator");
                Import("or");
                Import("otherwise");
                Import("packed");
                Import("pow");
                Import("private");
                Import("procedure");
                Import("program");
                Import("property");
                Import("protected");
                Import("public");
                Import("published");
                Import("qualified");
                Import("record");
                Import("repeat");
                Import("resident");
                Import("restricted");
                Import("segment");
                Import("set");
                Import("shl");
                Import("shr");
                Import("then");
                Import("to");
                Import("type");
                Import("unit");
                Import("until");
                Import("uses");
                Import("value");
                Import("var");
                Import("view");
                Import("virtual");
                Import("while");
                Import("with");
                Import("xor");

            }

            private void Import(string s)
            {
                tooltipText.Add(s, Translator.Translate2(s));
            }
            public bool Exist(string s)
            {
                return tooltipText.ContainsKey(s);
                
            }
            
            public string Text(string s)
            {
                return tooltipText[s];
            }
        }

        void popupMenu_Opening(object sender, CancelEventArgs e)
        {
            //---block autocomplete menu for comments
            //get index of green style (used for comments)
            var iGreenStyle = CurrentTB.GetStyleIndex(CurrentTB.SyntaxHighlighter.CommentLineStyle);
            if (iGreenStyle >= 0)
                if (CurrentTB.Selection.Start.iChar > 0)
                {
                    //current char (before caret)
                    var c = CurrentTB[CurrentTB.Selection.Start.iLine][CurrentTB.Selection.Start.iChar - 1];
                    //green Style
                    var greenStyleIndex = Range.ToStyleIndex(iGreenStyle);
                    //if char contains green style then block popup menu
                    if ((c.style & greenStyleIndex) != 0)
                        e.Cancel = true;
                }
        }

        private void Zoom_Change(object sender, EventArgs e)
        {
            this.toolStripStatusZoom.Text = this.CurrentTB.Zoom.ToString()+"%";
        }

        private void BuildAutocompleteMenu(AutocompleteMenu popupMenu)
        {
            List<AutocompleteItem> items = new List<AutocompleteItem>();

            foreach (var item in snippets)
                items.Add(new SnippetAutocompleteItem(item) { ImageIndex = 1 });
            foreach (var item in declarationSnippets)
                items.Add(new DeclarationSnippet(item) { ImageIndex = 0 });
            foreach (var item in methods)
                items.Add(new MethodAutocompleteItem(item) { ImageIndex = 2 });
            foreach (var item in keywords)
                items.Add(new AutocompleteItem(item));

            items.Add(new InsertSpaceSnippet());
            items.Add(new InsertSpaceSnippet(@"^(\w+)([=<>!:]+)(\w+)$"));
            items.Add(new InsertEnterSnippet());

            //set as autocomplete source
            popupMenu.Items.SetAutocompleteItems(items);
            popupMenu.SearchPattern = @"[\w\.:=!<>]";
        }

        void tb_MouseMove(object sender, MouseEventArgs e)
        {
            var tb = sender as FastColoredTextBox;
            var place = tb.PointToPlace(e.Location);
            var r = new Range(tb, place, place);

            string text = r.GetFragment("[a-zA-Z0-9]").Text;
            lbWordUnderMouse.Text = text;
        }

        void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.OemMinus)
            {
                NavigateBackward();
                e.Handled = true;
            }

            if (e.Modifiers == (Keys.Control|Keys.Shift) && e.KeyCode == Keys.OemMinus)
            {
                NavigateForward();
                e.Handled = true;
            }

            if (e.KeyData == (Keys.K | Keys.Control))
            {
                //forced show (MinFragmentLength will be ignored)
                (CurrentTB.Tag as TbInfo).popupMenu.Show(true);
                e.Handled = true;
            }
        }

        void tb_SelectionChangedDelayed(object sender, EventArgs e)
        {
            var tb = sender as FastColoredTextBox;
            //remember last visit time
            if (tb.Selection.IsEmpty && tb.Selection.Start.iLine < tb.LinesCount)
            {
                if (lastNavigatedDateTime != tb[tb.Selection.Start.iLine].LastVisit)
                {
                    tb[tb.Selection.Start.iLine].LastVisit = DateTime.Now;
                    lastNavigatedDateTime = tb[tb.Selection.Start.iLine].LastVisit;
                }
            }

            //highlight same words
            tb.VisibleRange.ClearStyle(sameWordsStyle);
            if (!tb.Selection.IsEmpty)
                return;//user selected diapason
            //get fragment around caret
            var fragment = tb.Selection.GetFragment(@"\w");
            string text = fragment.Text;
            if (text.Length == 0)
                return;
            //highlight same words
            Range[] ranges = tb.VisibleRange.GetRanges("\\b" + text + "\\b").ToArray();

            if (ranges.Length > 1)
                foreach (var r in ranges)
                    r.SetStyle(sameWordsStyle, HighlightType.SameWord);
            
        }
        
        List<ExplorerItem> explorerList = new List<ExplorerItem>();

        
        enum ExplorerItemType
        {
            Class, Method, Property, Event
        }
        
        class ExplorerItem
        {
            private ExplorerItemType _type;
            private string _title;
            private int _position;
            public ExplorerItemType type
            {
                set { _type = value; }
                get { return _type; }
            }
            public string title
            {
                set { _title = value; }
                get { return _title; }
            }
            public int position
            {
                set { _position = value; }
                get { return _position; }
            }
        }
        
        class ExplorerItemComparer : IComparer<ExplorerItem>
        {
            public int Compare(ExplorerItem x, ExplorerItem y)
            {
                return x.title.CompareTo(y.title);
            }
        }

        private void tsFiles_TabStripItemClosing(TabStripItemClosingEventArgs e)
        {
            //MessageBox.Show("Closeing");
            try
            {
                if ((e.Item.Controls[0] as FastColoredTextBox).IsChanged)
                {
                    switch (MessageBox.Show(Translator.Translate("Do you want to save ") + e.Item.Title.Substring(0) + " ?", Translator.Translate("Save"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information))
                    {
                        case System.Windows.Forms.DialogResult.Yes:
                            if (!Save(e.Item))
                            {
                                e.Cancel = true;
                            }
                            break;
                        case DialogResult.Cancel:
                            e.Cancel = true;
                            break;
                    }
                }
            }
            catch
            { }
        }

        private bool Save(FATabStripItem tab)
        {
            var tb = (tab.Controls[0] as FastColoredTextBox);
            if (tab.Tag == null)
            {
                if (sfdMain.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return false;
                tab.Title = Path.GetFileName(sfdMain.FileName);
                tab.Tag = sfdMain.FileName;
                tab.Name = sfdMain.FileName;
            }

            try
            {
                File.WriteAllText(tab.Tag as string, tb.Text);
                tb.IsChanged = false;
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.Message, Translator.Translate("Error"), MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    return Save(tab);
                else
                    return false;
            }

            
            if (tsFiles.SelectedItem.Tag != null && (Path.GetExtension(tsFiles.SelectedItem.Tag.ToString()) == ".pas" || Path.GetExtension(tsFiles.SelectedItem.Tag.ToString()) == ".pp" || Path.GetExtension(tsFiles.SelectedItem.Tag.ToString()) == ".inc"))
                CurrentTB.Language = Language.Pascal;
            CurrentTB.SyntaxHighlighter.PascalSyntaxHighlight(CurrentTB.Range, CurrentTB.AllRange);
            tb.Invalidate();
            return true;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tsFiles.SelectedItem != null)
                Save(tsFiles.SelectedItem);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tsFiles.SelectedItem != null)
            {
                string oldFile = tsFiles.SelectedItem.Tag as string;
                tsFiles.SelectedItem.Tag = null;
                if (!Save(tsFiles.SelectedItem))
                if(oldFile!=null)
                {
                    tsFiles.SelectedItem.Tag = oldFile;
                    tsFiles.SelectedItem.Title = Path.GetFileName(oldFile);
                }
            }
            
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ofdMain.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string filename in ofdMain.FileNames)
                    CreateTab(filename);
            }
        }

        FastColoredTextBox CurrentTB
        {
            get {
                if (tsFiles.Items.Count == 0) return null;
                if (tsFiles.SelectedItem == null)
                    return null;
                
                return (tsFiles.SelectedItem.Controls[0] as FastColoredTextBox);
            }

            set
            {
                tsFiles.SelectedItem = (value.Parent as FATabStripItem);
                value.Focus();
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.Selection.SelectAll();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentTB.UndoEnabled)
                CurrentTB.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentTB.RedoEnabled)
                CurrentTB.Redo();
        }

        private void tmUpdateInterface_Tick(object sender, EventArgs e)
        {
            try
            {
                if(CurrentTB != null && tsFiles.Items.Count>0)
                {
                    var tb = CurrentTB;
                    undoStripButton.Enabled = undoToolStripMenuItem.Enabled = tb.UndoEnabled;
                    redoStripButton.Enabled = redoToolStripMenuItem.Enabled = tb.RedoEnabled;
                    saveToolStripButton.Enabled = saveToolStripMenuItem.Enabled = tb.IsChanged;
                    saveAsToolStripMenuItem.Enabled = true;
                    pasteToolStripButton.Enabled = pasteToolStripMenuItem.Enabled = true;
                    cutToolStripButton.Enabled = cutToolStripMenuItem.Enabled =
                    copyToolStripButton.Enabled = copyToolStripMenuItem.Enabled = !tb.Selection.IsEmpty;
                    printToolStripButton.Enabled = true;
                    
                    // giaosudauto added....
                    ssMain.Enabled = true;
                    toolStripMenuItemClose.Enabled = true;
                    toolStripMenuItemCloseAll.Enabled = true;
                    buildToolStripMenuItem.Enabled = true;
                    searchToolStripMenuItem.Enabled = true;
                    editToolStripMenuItem.Enabled = true;
                    toolStripButtonCompile.Enabled = true;
                    toolStripButtonRun.Enabled = true;
                    showProcess.Enabled = true;
                    backStripButton.Enabled = true;
                    forwardStripButton.Enabled = true;
                    bookmarkPlusButton.Enabled = true;
                    bookmarkMinusButton.Enabled = true;
                    gotoButton.Enabled = true;
                    tbFind.Enabled = true;
                    this.toolStripStatusSaveStatus.Text = (this.CurrentTB.IsChanged ? Translate("Unsaved") : Translate("Saved"));
                    if (this.toolStripStatusSaveStatus.Text == Translate("Unsaved"))
                    {
                        this.toolStripStatusSaveStatus.ForeColor = Color.Red;
                        if (this.tsFiles.SelectedItem.Title[0] != '*')
                            this.tsFiles.SelectedItem.Title = "*" + this.tsFiles.SelectedItem.Title;
                        //if (this.tsFiles.SelectedItem.ToString()[0] != '*')
                        //    this.tsFiles.SelectedItem = "*" + this.tsFiles.SelectedItem.ToString();
                        //this.tsFiles.SelectedItem = "*" + this.tsFiles.SelectedItem.Name;
                    }
                    else
                    {
                        this.toolStripStatusSaveStatus.ForeColor = Color.Green;
                        if (this.tsFiles.SelectedItem.Title[0] == '*')
                            this.tsFiles.SelectedItem.Title=this.tsFiles.SelectedItem.Title.Substring(1);
                        //this.tsFiles.SelectedItem.Tag = this.tsFiles.SelectedItem.Name;
                    }
                    this.toolStripStatusRow.Text = Translate("Row: ") + (CurrentTB.Selection.Start.iLine + 1).ToString() + " |";
                    this.toolStripStatusColumn.Text = Translate("Column: ") + (CurrentTB.Selection.Start.iChar + 1).ToString() + " |";
                    this.toolStripStatusLines.Text = Translate("Total Lines: ") + CurrentTB.LinesCount.ToString() + " |";
                    this.toolStripStatusLengths.Text = Translate("Total Chars: ") + CurrentTB.Text.Length.ToString() + " |";
                    if (CurrentTB.IsChanged) CurrentTB.IsCompiled = false;
                    
                }
                else
                {
                    ssMain.Enabled = false;
                    saveToolStripButton.Enabled = saveToolStripMenuItem.Enabled = false;
                    saveAsToolStripMenuItem.Enabled = false;
                    cutToolStripButton.Enabled = cutToolStripMenuItem.Enabled =
                    copyToolStripButton.Enabled = copyToolStripMenuItem.Enabled = false;
                    pasteToolStripButton.Enabled = pasteToolStripMenuItem.Enabled = false;
                    printToolStripButton.Enabled = false;
                    undoStripButton.Enabled = undoToolStripMenuItem.Enabled = false;
                    redoStripButton.Enabled = redoToolStripMenuItem.Enabled = false;
                    //dgvObjectExplorer.RowCount = 0;
                    toolStripMenuItemClose.Enabled = false;
                    toolStripMenuItemCloseAll.Enabled = false;
                    buildToolStripMenuItem.Enabled = false;
                    searchToolStripMenuItem.Enabled = false;
                    editToolStripMenuItem.Enabled = false;
                    toolStripButtonCompile.Enabled = false;
                    toolStripButtonRun.Enabled = false;
                    showProcess.Enabled = false;
                    backStripButton.Enabled = false;
                    forwardStripButton.Enabled = false;
                    bookmarkPlusButton.Enabled = false;
                    bookmarkMinusButton.Enabled = false;
                    gotoButton.Enabled = false;
                    tbFind.Enabled = false;
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            if(CurrentTB!=null)
            {
                var settings = new PrintDialogSettings();
                settings.Title = tsFiles.SelectedItem.Title;
                settings.Header = "&b&w&b";
                settings.Footer = "&b&p";
                CurrentTB.Print(settings);
            }
        }

        bool tbFindChanged = false;

        private void tbFind_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' && CurrentTB != null)
            {
                Range r = tbFindChanged?CurrentTB.Range.Clone():CurrentTB.Selection.Clone();
                tbFindChanged = false;
                r.End = new Place(CurrentTB[CurrentTB.LinesCount - 1].Count, CurrentTB.LinesCount - 1);
                var pattern = Regex.Escape(tbFind.Text);
                foreach (var found in r.GetRanges(pattern))
                {
                    found.Inverse();
                    CurrentTB.Selection = found;
                    CurrentTB.DoSelectionVisible();
                    return;
                }
                MessageBox.Show(Translator.Translate("Not found."));
            }
            else
                tbFindChanged = true;
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentTB == null) return;
            CurrentTB.ShowFindDialog();
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentTB == null) return;
            CurrentTB.ShowReplaceDialog();
        }

        private void PowerfulPascalEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            List<FATabStripItem> list = new List<FATabStripItem>();
            History.Clear();
            foreach (FATabStripItem tab in tsFiles.Items)
            {
                list.Add(tab);
                History.Add(tab.Name);
            }
            foreach (var tab in list)
            {
                TabStripItemClosingEventArgs args = new TabStripItemClosingEventArgs(tab);
                tsFiles_TabStripItemClosing(args);
                if (args.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                tsFiles.RemoveTab(tab);
            }
            History.Save();
        }

      

        private void tsFiles_TabStripItemSelectionChanged(TabStripItemChangedEventArgs e)
        {
            if (CurrentTB != null)
            {
                CurrentTB.Focus();
                string text = CurrentTB.Text;
                
            }
        }

        private void backStripButton_Click(object sender, EventArgs e)
        {
            NavigateBackward();
        }

        private void forwardStripButton_Click(object sender, EventArgs e)
        {
            NavigateForward();
        }

        DateTime lastNavigatedDateTime = DateTime.Now;

        private bool NavigateBackward()
        {
            DateTime max = new DateTime();
            int iLine = -1;
            FastColoredTextBox tb = null;
            for (int iTab = 0; iTab < tsFiles.Items.Count; iTab++)
            {
                var t = (tsFiles.Items[iTab].Controls[0] as FastColoredTextBox);
                for (int i = 0; i < t.LinesCount; i++)
                    if (t[i].LastVisit < lastNavigatedDateTime && t[i].LastVisit > max)
                    {
                        max = t[i].LastVisit;
                        iLine = i;
                        tb = t;
                    }
            }
            if (iLine >= 0)
            {
                tsFiles.SelectedItem = (tb.Parent as FATabStripItem);
                tb.Navigate(iLine);
                lastNavigatedDateTime = tb[iLine].LastVisit;
                Console.WriteLine("Backward: " + lastNavigatedDateTime);
                tb.Focus();
                tb.Invalidate();
                return true;
            }
            else
                return false;
        }

        private bool NavigateForward()
        {
            DateTime min = DateTime.Now;
            int iLine = -1;
            FastColoredTextBox tb = null;
            for (int iTab = 0; iTab < tsFiles.Items.Count; iTab++)
            {
                var t = (tsFiles.Items[iTab].Controls[0] as FastColoredTextBox);
                for (int i = 0; i < t.LinesCount; i++)
                    if (t[i].LastVisit > lastNavigatedDateTime && t[i].LastVisit < min)
                    {
                        min = t[i].LastVisit;
                        iLine = i;
                        tb = t;
                    }
            }
            if (iLine >= 0)
            {
                tsFiles.SelectedItem = (tb.Parent as FATabStripItem);
                tb.Navigate(iLine);
                lastNavigatedDateTime = tb[iLine].LastVisit;
                Console.WriteLine("Forward: " + lastNavigatedDateTime);
                tb.Focus();
                tb.Invalidate();
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// This item appears when any part of snippet text is typed
        /// </summary>
        class DeclarationSnippet : SnippetAutocompleteItem
        {
            public DeclarationSnippet(string snippet)
                : base(snippet)
            {
            }

            public override CompareResult Compare(string fragmentText)
            {
                var pattern = Regex.Escape(fragmentText);
                if (Regex.IsMatch(Text, "\\b" + pattern, RegexOptions.IgnoreCase))
                    return CompareResult.Visible;
                return CompareResult.Hidden;
            }
        }

        /// <summary>
        /// Divides numbers and words: "123AND456" -> "123 AND 456"
        /// Or "i=2" -> "i = 2"
        /// </summary>
        class InsertSpaceSnippet : AutocompleteItem
        {
            string pattern;

            public InsertSpaceSnippet(string pattern)
                : base("")
            {
                this.pattern = pattern;
            }

            public InsertSpaceSnippet()
                : this(@"^(\d+)([a-zA-Z_]+)(\d*)$")
            {
            }

            public override CompareResult Compare(string fragmentText)
            {
                if (Regex.IsMatch(fragmentText, pattern))
                {
                    Text = InsertSpaces(fragmentText);
                    if (Text != fragmentText)
                        return CompareResult.Visible;
                }
                return CompareResult.Hidden;
            }

            public string InsertSpaces(string fragment)
            {
                var m = Regex.Match(fragment, pattern);
                if (m == null)
                    return fragment;
                if (m.Groups[1].Value == "" && m.Groups[3].Value == "")
                    return fragment;
                return (m.Groups[1].Value + " " + m.Groups[2].Value + " " + m.Groups[3].Value).Trim();
            }

            public override string ToolTipTitle
            {
                get
                {
                    return Text;
                }
            }
        }

        /// <summary>
        /// Inerts line break after '}'
        /// </summary>
        class InsertEnterSnippet : AutocompleteItem
        {
            Place enterPlace = Place.Empty;

            public InsertEnterSnippet()
                : base("[Line break]")
            {
            }

            public override CompareResult Compare(string fragmentText)
            {
                var r = Parent.Fragment.Clone();
                while (r.Start.iChar > 0)
                {
                    if (r.CharBeforeStart == '}')
                    {
                        enterPlace = r.Start;
                        return CompareResult.Visible;
                    }

                    r.GoLeftThroughFolded();
                }

                return CompareResult.Hidden;
            }

            public override string GetTextForReplace()
            {
                //extend range
                Range r = Parent.Fragment;
                Place end = r.End;
                r.Start = enterPlace;
                r.End = r.End;
                //insert line break
                return Environment.NewLine + r.Text;
            }

            public override void OnSelected(AutocompleteMenu popupMenu, SelectedEventArgs e)
            {
                base.OnSelected(popupMenu, e);
                if (Parent.Fragment.tb.AutoIndent)
                    Parent.Fragment.tb.DoAutoIndent();
            }

            public override string ToolTipTitle
            {
                get
                {
                    return "Insert line break after '}'";
                }
            }
        }

        private void autoIndentSelectedTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.DoAutoIndent();
        }

        private void btHighlightCurrentLine_Click(object sender, EventArgs e)
        {
            foreach (FATabStripItem tab in tsFiles.Items)
            {
                if (btHighlightCurrentLine.Checked)
                    (tab.Controls[0] as FastColoredTextBox).CurrentLineColor = currentLineColor;
                else
                    (tab.Controls[0] as FastColoredTextBox).CurrentLineColor = Color.Transparent;
            }
            if (CurrentTB != null)
                CurrentTB.Invalidate();
            Properties.Settings.Default.ShowHighlightCurrentLine = btHighlightCurrentLine.Checked;
            this.highlightCurrentLineToolStripMenuItem.Checked = btHighlightCurrentLine.Checked;
            Properties.Settings.Default.Save();
        }

        private void commentSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.InsertLinePrefix("//");
        }

        private void uncommentSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.RemoveLinePrefix("//");
        }

        private void cloneLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //expand selection
            CurrentTB.Selection.Expand();
            //get text of selected lines
            string text = Environment.NewLine + CurrentTB.Selection.Text;
            //move caret to end of selected lines
            CurrentTB.Selection.Start = CurrentTB.Selection.End;
            //insert text
            CurrentTB.InsertText(text);
        }

        private void cloneLinesAndCommentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //start autoUndo block
            CurrentTB.BeginAutoUndo();
            //expand selection
            CurrentTB.Selection.Expand();
            //get text of selected lines
            string text = Environment.NewLine + CurrentTB.Selection.Text;
            //comment lines
            CurrentTB.InsertLinePrefix("//");
            //move caret to end of selected lines
            CurrentTB.Selection.Start = CurrentTB.Selection.End;
            //insert text
            CurrentTB.InsertText(text);
            //end of autoUndo block
            CurrentTB.EndAutoUndo();
        }

        private void bookmarkPlusButton_Click(object sender, EventArgs e)
        {
            if(CurrentTB == null) 
                return;
            CurrentTB.BookmarkLine(CurrentTB.Selection.Start.iLine);
        }

        private void bookmarkMinusButton_Click(object sender, EventArgs e)
        {
            if (CurrentTB == null)
                return;
            CurrentTB.UnbookmarkLine(CurrentTB.Selection.Start.iLine);
        }

        private void gotoButton_DropDownOpening(object sender, EventArgs e)
        {
            gotoButton.DropDownItems.Clear();
            foreach (Control tab in tsFiles.Items)
            {
                FastColoredTextBox tb = tab.Controls[0] as FastColoredTextBox;
                foreach (var bookmark in tb.Bookmarks)
                {
                    var item = gotoButton.DropDownItems.Add(bookmark.Name + " [" + Path.GetFileNameWithoutExtension(tab.Tag as String) + "]");
                    item.Tag = bookmark;
                    item.Click += (o, a) => {
                        var b = (Bookmark)(o as ToolStripItem).Tag;
                        try
                        {
                            CurrentTB = b.TB;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }
                        b.DoVisible();
                    };
                }
            }
        }

        private void btShowFoldingLines_Click(object sender, EventArgs e)
        {
            foreach (FATabStripItem tab in tsFiles.Items)
                (tab.Controls[0] as FastColoredTextBox).ShowFoldingLines = btShowFoldingLines.Checked;
            if (CurrentTB != null)
                CurrentTB.Invalidate();
            Properties.Settings.Default.ShowFoldingLines = btShowFoldingLines.Checked;
            this.foldingLineToolStripMenuItem.Checked = btShowFoldingLines.Checked;
            Properties.Settings.Default.Save();
        }

        private void Zoom_click(object sender, EventArgs e)
        {
            if (CurrentTB != null)
                CurrentTB.Zoom = int.Parse((sender as ToolStripItem).Tag.ToString());
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentTB == null) return;
            //runandcompile = new RunandCompile();
            if (this.tsFiles.SelectedItem.Tag == null)
            {
                if (sfdMain.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return ;
                this.tsFiles.SelectedItem.Title = Path.GetFileName(sfdMain.FileName);
                this.tsFiles.SelectedItem.Tag = sfdMain.FileName;
            }
            if (CurrentTB.IsChanged)
            {
                try
                {

                    File.WriteAllText(this.tsFiles.SelectedItem.Tag as string, CurrentTB.Text);
                    CurrentTB.IsChanged = false;
                }
                catch { }
            }
            //FPC\bin\i386-win32\ppc386.exe
            //MessageBox.Show("");
            /*
            if (!File.Exists(@"FPC\bin\i386-win32\ppc386.exe"))
            {
                showProcess.Items.Add(Translator.Translate("Fatal: Compiler not found!"));
                File.WriteAllText(AppPath.Data + "error-compiler.log", AppPath.Install + @"FPC\bin\i386-win32\ppc386.exe");
                AppSound.Play("fail.wav");
                return;
            }*/
            runandcompile.SetInfo(@"FPC\bin\i386-win32\ppc386.exe", this.tsFiles.SelectedItem.Tag as string);
            if (runandcompile.Compile(ref this.showProcess, true))
            {
                this.CurrentTB.IsCompiled = true;
                AppSound.Play("success.wav");
            }
            else AppSound.Play("fail.wav");
        }
        
        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentTB == null) return;
            //runandcompile = new RunandCompile();
            runandcompile.SetInfo(@"FPC\bin\i386-win32\ppc386.exe", this.tsFiles.SelectedItem.Tag as string);
            if (this.tsFiles.SelectedItem.Tag == null)
            {
                if (sfdMain.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;
                this.tsFiles.SelectedItem.Title = Path.GetFileName(sfdMain.FileName);
                this.tsFiles.SelectedItem.Tag = sfdMain.FileName;
            }
            if (!CurrentTB.IsCompiled)
            {
                DialogResult dres = MessageBox.Show(Translator.Translate("This code has not compiled, do you want to compile it?"), Translator.Translate("Caution"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dres == DialogResult.Yes)
                {
                    try
                    {

                        File.WriteAllText(this.tsFiles.SelectedItem.Tag as string, CurrentTB.Text);
                        CurrentTB.IsChanged = false;
                    }
                    catch 
                    {
                        MessageBox.Show(Translator.Translate("An error while save file, please try again later!"),Translator.Translate("Error saving"),MessageBoxButtons.OK,MessageBoxIcon.Stop);
                        return;
                    }
                    if (runandcompile.Compile(ref this.showProcess, true)) this.CurrentTB.IsCompiled = true;
                    else
                    {
                        this.CurrentTB.IsCompiled = false;
                        return;
                    }
                }
                if (dres == DialogResult.Cancel)
                {
                    return;
                }
            }
            
            runandcompile.Run(ref this.showProcess);
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index > -1)
            {

                string s = this.showProcess.Items[e.Index].ToString();
                if (s.Contains(Translator.Translate("Fatal: Compiler not found!")))
                {
                    e.Graphics.FillRectangle(Brushes.Red, e.Bounds);
                    using (Brush textBrush = new SolidBrush(Color.White))
                    {
                        e.Graphics.DrawString(showProcess.Items[e.Index].ToString(), e.Font, textBrush, e.Bounds.Location);
                    }
                    //this.listBox1.SelectedIndex = e.Index;
                    return;
                }
                if (s.Contains(Translator.Translate("Error:")) || (s.Contains(Translator.Translate("Fatal:")) && (s.Contains(runandcompile.fileName()))))
                {
                    e.DrawBackground();
                    using (Brush textBrush = new SolidBrush(Color.Red))
                    {
                        e.Graphics.DrawString(showProcess.Items[e.Index].ToString(), e.Font, textBrush, e.Bounds.Location);
                        return;
                    }
                }
                if (s.Contains(Translator.Translate("Warning:")))
                {
                    e.DrawBackground();
                    using (Brush textBrush = new SolidBrush(Color.Blue))
                    {
                        e.Graphics.DrawString(showProcess.Items[e.Index].ToString(), e.Font, textBrush, e.Bounds.Location);
                        return;
                    }
                }
                if (s.Contains(Translator.Translate("Note:")))
                {
                    e.DrawBackground();
                    using (Brush textBrush = new SolidBrush(Color.Blue))
                    {
                        e.Graphics.DrawString(showProcess.Items[e.Index].ToString(), e.Font, textBrush, e.Bounds.Location);
                        return;
                    }
                }
                if (s.Contains(Translator.Translate("Fatal: Compilation aborted")))
                {
                    e.Graphics.FillRectangle(Brushes.Red, e.Bounds);
                    using (Brush textBrush = new SolidBrush(Color.White))
                    {
                        e.Graphics.DrawString(showProcess.Items[e.Index].ToString(), e.Font, textBrush, e.Bounds.Location);
                    }
                    //this.listBox1.SelectedIndex = e.Index;
                    return;
                }
                if (s.Contains(Translator.Translate("lines compiled")) /*|| s.Contains(Translator.Translate("Compiling"))*/)
                {
                    e.Graphics.FillRectangle(Brushes.Green, e.Bounds);
                    using (Brush textBrush = new SolidBrush(Color.White))
                    {
                        e.Graphics.DrawString(showProcess.Items[e.Index].ToString(), e.Font, textBrush, e.Bounds.Location);
                    }
                    //this.listBox1.SelectedIndex = e.Index;
                    return;
                }

                if (s.Contains(Translator.Translate("Process terminated with status")) ) 
                {
                    Font nfont = new Font(e.Font.Name, e.Font.Size, FontStyle.Italic|FontStyle.Bold);
                    e.DrawBackground();
                        
                    if(s.Contains(Translator.Translate("Process terminated with status 0")))
                    {
                        using (Brush textBrush = new SolidBrush(Color.Green))
                        {
                            e.Graphics.DrawString(showProcess.Items[e.Index].ToString(), nfont, textBrush, e.Bounds.Location);
                            return;
                        }
                    }
                    else
                    {
                        using (Brush textBrush = new SolidBrush(Color.Red))
                        {
                            e.Graphics.DrawString(showProcess.Items[e.Index].ToString(), nfont, textBrush, e.Bounds.Location);
                            return;
                        }
                    }
                }

                if (s.Contains(Translator.Translate("exited successful")))
                {
                    Font nfont = new Font(e.Font.Name, e.Font.Size, FontStyle.Italic | FontStyle.Bold);
                    e.DrawBackground();
                    using (Brush textBrush = new SolidBrush(Color.Green))
                    {
                        e.Graphics.DrawString(showProcess.Items[e.Index].ToString(), nfont, textBrush, e.Bounds.Location);
                        return;
                    }

                }
                e.DrawBackground();
                using (Brush textBrush = new SolidBrush(ForeColor))
                {
                    e.Graphics.DrawString(showProcess.Items[e.Index].ToString(), e.Font, textBrush, e.Bounds.Location);
                }

            }
        }

        //int n1, n2;

        private void CloseCurrentStripItem()
        {
            if (this.tsFiles.Items.Count < 0) return;
            this.tsFiles.SelectedItem.Dispose();
            this.tsFiles.RemoveTab(this.tsFiles.SelectedItem);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            TabStripItemClosingEventArgs ef = new TabStripItemClosingEventArgs(this.tsFiles.SelectedItem);
            if ((ef.Item.Controls[0] as FastColoredTextBox).IsChanged)
            {
                switch (MessageBox.Show(Translator.Translate("Do you want to save ") + ef.Item.Title.Substring(1) + " ?", Translator.Translate("Save"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        if (!Save(ef.Item))
                            return;
                        break;
                    case DialogResult.Cancel:
                        return;
                }
            }
            this.CloseCurrentStripItem();
        }

        private void toolStripMenuItemCloseAll_Click(object sender, EventArgs e)
        {
            DialogResult diag= MessageBox.Show(Translator.Translate("Are you want to close all unsaved files?"), Translator.Translate("Closing all files caution!"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (diag == DialogResult.No) return;
            while (this.tsFiles.Items.Count > 0)
                this.CloseCurrentStripItem();
            
        }

        private void gotoLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CurrentTB == null) return;
            CurrentTB.ShowGoToDialog();
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //listBox1_MouseDoubleClick(null, null);
            if (runandcompile.fileName() == null) return;
            if (this.showProcess.SelectedItem == null) return;
            //bool ok = false;
            string tmp = this.showProcess.SelectedItem.ToString();
            //string outstr = "";
            string[] word = tmp.Split(' ');
            if (Regex.IsMatch(word[0], @"\(\d+\,\d+\)"))
            {
                Regex rg = new Regex(@"(\(\d+\,\d+\))|(\(\d+\))");
                MatchCollection mclolection = rg.Matches(tmp);
                if(mclolection.Count>0)
                {
                    Regex newrg = new Regex(@"\d+");
                    MatchCollection matchcolection = newrg.Matches(mclolection[0].Value);
                    if(matchcolection.Count==1)
                    {
                        this.CurrentTB.Selection.Start = new Place(0,Convert.ToInt32(matchcolection[0].Value)-1);
                    }
                    else
                    {
                        this.CurrentTB.Selection.Start = new Place(Convert.ToInt32(matchcolection[1].Value)-1,Convert.ToInt32(matchcolection[0].Value)-1);
                    }
                }
                CurrentTB.DoSelectionVisible();
            }
            this.showProcess.SelectedItem = null;
            this.CurrentTB.Select();
        }

        public void PowerfulPascalEditor_Load(object sender, EventArgs e)
        {
            Directory.SetCurrentDirectory(Path.GetDirectoryName( System.Reflection.Assembly.GetExecutingAssembly().Location));
            //MessageBox.Show(System.Reflection.Assembly.GetExecutingAssembly().Location);
            InitLanguage();
            Translator.SetLang();
            runandcompile = new RunandCompile();

            this.bulletinToolStripMenuItem.Checked = Properties.Settings.Default.ShowBullentin;
            tableLayoutPanel1.RowStyles[1].Height = Properties.Settings.Default.ShowBullentin ? 26.5f : 0f;

            soundStripButton.Image = soundToolStripMenuItem.Image = Properties.Settings.Default.Sound ? Properties.Resources.soundon : Properties.Resources.soundoff;

            this.btHighlightCurrentLine.Checked = this.highlightCurrentLineToolStripMenuItem.Checked = Properties.Settings.Default.ShowHighlightCurrentLine;
            this.btShowFoldingLines.Checked = this.foldingLineToolStripMenuItem.Checked = Properties.Settings.Default.ShowFoldingLines;
            tsMain.Visible = this.toolBarToolStripMenuItem.Checked = Properties.Settings.Default.ShowToolBar;

            btShowFoldingLines_Click(null, null);
            btHighlightCurrentLine_Click(null, null);
            //tsMain.Size = new Size(0, 0);
            //tsMain.Visible = false;
        }

        private void toolStripButtonTest_Click(object sender, EventArgs e)
        {
            MessageBox.Show(tsFiles.SelectedItem.TabIndex.ToString());
            //new Memory_Size().ShowDialog();
            //new HotkeysEditorForm(CurrentTB.HotkeysMapping).ShowDialog();
        }

        class TextboxState
        {
            public int HorizontalScroll { get; set; }
            public int VerticalScroll { get; set; }
            public List<int> ChangedList { get; set; }
            public bool IsChanged { get; set; }
            public string Name { get; set; }
            public string Text { get; set; }
            public Place Selection { get; set; }
            public TextboxState(FATabStripItem tab, FastColoredTextBox tb)
            {
                ChangedList = new List<int>();
                this.HorizontalScroll = tb.HorizontalScroll.Value;
                this.VerticalScroll = tb.VerticalScroll.Value;
                this.IsChanged = tb.IsChanged;
                this.Name = tab.Name;
                this.Text = tb.Text;
                this.Selection = tb.Selection.Start;
                for (int i = 0; i < tb.TextSource.Count; i++)
                    if (tb.TextSource[i].IsChanged) ChangedList.Add(i);
            }
            
        }

        //string currentlang;
        //string spath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\";
        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.WriteAllText(AppPath.Data+"lang", "English");
            InitLanguage();
            Translator.SetLang();
            englishToolStripMenuItem.Checked = true;
            vietnameseToolStripMenuItem.Checked = false;
            showProcess.Items.Clear();
        }

        private void vietnameseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.WriteAllText(AppPath.Data+"lang", "Vietnamese");
            InitLanguage();
            Translator.SetLang();
            englishToolStripMenuItem.Checked = false;
            vietnameseToolStripMenuItem.Checked = true;
            showProcess.Items.Clear();
        }
        
        private void calculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Environment.SystemDirectory + "\\calc.exe");
            }
            catch
            {
                MessageBox.Show(Translate("Can't run calculator."), Translate("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //MessageBox.Show(Environment.SystemDirectory + "\\calc.exe");
        }

        private void aSCIITableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ASCII().Show();
        }

        private void editorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res = new HighlightCodeSetting().ShowDialog();
            if (res == DialogResult.OK)
            {
                SettingStatus.GetHighlightOption();
                if (CurrentTB == null) return;
            
                Stack<TextboxState> list = new Stack<TextboxState>();
                int selected = 0;
                //MessageBox.Show(selected.ToString());
                for (int i = 0; i < tsFiles.Items.Count; i++)
                {
                    if (tsFiles.Items[i] == tsFiles.SelectedItem)
                    {
                        selected = i;
                    }
                }
                for (int i = 0; i < tsFiles.Items.Count; )
                {
                    list.Push(new TextboxState(tsFiles.Items[i], tsFiles.Items[i].Controls[0] as FastColoredTextBox));
                    this.tsFiles.Items[i].Dispose();
                    this.tsFiles.RemoveTab(this.tsFiles.Items[i]);

                }

                foreach (TextboxState tb in list)
                {
                    CreateTab(tb);
                }
                tsFiles.SelectedItem = tsFiles.Items[selected];   
                //(tsFiles.SelectedItem.Controls[0] as FastColoredTextBox);
                    //MessageBox.Show(tsFiles.SelectedItem.TabIndex.ToString());
            }
        }

        private void compilerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CompilerSwitches().ShowDialog();
        }

        private void toolStripMenuItem5_Click_1(object sender, EventArgs e)
        {
            if (CurrentTB != null)
            {
                var settings = new PrintDialogSettings();
                settings.Title = tsFiles.SelectedItem.Title;
                settings.Header = "&b&w&b";
                settings.Footer = "&b&p";
                CurrentTB.Print(settings);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
            
        }

        private void bulletinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ShowBullentin = !Properties.Settings.Default.ShowBullentin;
            Properties.Settings.Default.Save();
            tableLayoutPanel1.RowStyles[1].Height = Properties.Settings.Default.ShowBullentin ? 26.5f : 0f;
        }

        /*
        class InterfaceSetting
        {
            XmlDocument doc = new XmlDocument();
            bool showFoldingLines, showBulletin, showHighlightCurrentLine;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Olala Pascal\\InterfaceSetting.xml";
            public InterfaceSetting()
            {
                doc.Load(path);
                showBulletin = doc.SelectSingleNode("Interface/ShowBulletin").Attributes["status"].Value == "yes";
                showFoldingLines = doc.SelectSingleNode("Interface/ShowFoldingLines").Attributes["status"].Value == "yes";
                showHighlightCurrentLine = doc.SelectSingleNode("Interface/ShowHighlightCurrentLine").Attributes["status"].Value == "yes";
            }
            public bool ShowFoldingLines
            {
                get
                {
                    return showFoldingLines;
                }
                set
                {
                    showFoldingLines = value;
                    doc.SelectSingleNode("Interface/ShowFoldingLines").Attributes["status"].Value = showFoldingLines ? "yes" : "no";
                    doc.Save(path);
                }
            }
            public bool ShowBulletin
            {
                get
                {
                    return showBulletin;
                }
                set
                {
                    showBulletin = value;
                    doc.SelectSingleNode("Interface/ShowBulletin").Attributes["status"].Value = showBulletin ? "yes" : "no";
                    doc.Save(path);
                }
            }
            public bool ShowHighlightCurrentLine
            {
                get
                {
                    return showHighlightCurrentLine;
                }
                set
                {
                    showHighlightCurrentLine = value;
                    doc.SelectSingleNode("Interface/ShowHighlightCurrentLine").Attributes["status"].Value = showHighlightCurrentLine ? "yes" : "no";
                    doc.Save(path);
                }
            }
        }
        */
        private void highlightCurrentLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btHighlightCurrentLine.Checked = !btHighlightCurrentLine.Checked;
            foreach (FATabStripItem tab in tsFiles.Items)
            {
                if (btHighlightCurrentLine.Checked)
                    (tab.Controls[0] as FastColoredTextBox).CurrentLineColor = currentLineColor;
                else
                    (tab.Controls[0] as FastColoredTextBox).CurrentLineColor = Color.Transparent;
            }
            if (CurrentTB != null)
                CurrentTB.Invalidate();
            Properties.Settings.Default.ShowHighlightCurrentLine = btHighlightCurrentLine.Checked;
            this.highlightCurrentLineToolStripMenuItem.Checked = btHighlightCurrentLine.Checked;
            Properties.Settings.Default.Save();
        }

        private void foldingLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btShowFoldingLines.Checked = !btShowFoldingLines.Checked;
            foreach (FATabStripItem tab in tsFiles.Items)
                (tab.Controls[0] as FastColoredTextBox).ShowFoldingLines = btShowFoldingLines.Checked;
            if (CurrentTB != null)
                CurrentTB.Invalidate();
            Properties.Settings.Default.ShowFoldingLines = btShowFoldingLines.Checked;
            this.foldingLineToolStripMenuItem.Checked = btShowFoldingLines.Checked;
            Properties.Settings.Default.Save();
        }

        private void toolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsMain.Visible = toolBarToolStripMenuItem.Checked;
            Properties.Settings.Default.ShowToolBar = toolBarToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

        private void fullScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.FullScreen = !Properties.Settings.Default.FullScreen;
            Properties.Settings.Default.Save();
            if (Properties.Settings.Default.FullScreen)
            {
                tsMain.Visible = false;
                tableLayoutPanel1.RowStyles[1].Height = 0;
                msMain.Visible = false;
            }
            else
            {
                tsMain.Visible = Properties.Settings.Default.ShowToolBar;
                tableLayoutPanel1.RowStyles[1].Height = Properties.Settings.Default.ShowBullentin? 26.5f:0;
                msMain.Visible = true;
            }
        }

        private void soundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Sound = !Properties.Settings.Default.Sound;
            Properties.Settings.Default.Save();
            soundStripButton.Image = soundToolStripMenuItem.Image = Properties.Settings.Default.Sound ? Properties.Resources.soundon : Properties.Resources.soundoff;
        }

        private void soundStripButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Sound = !Properties.Settings.Default.Sound;
            Properties.Settings.Default.Save();
            soundStripButton.Image = soundToolStripMenuItem.Image = Properties.Settings.Default.Sound ? Properties.Resources.soundon : Properties.Resources.soundoff;
        }

        private void guildToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("manual.chm");
            }catch
            {
                MessageBox.Show(Translate("Help file can not found!"), Translate("Error"),MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void feedbackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Feedback().Show();
        }

    }

    public class InvisibleCharsRenderer : Style
    {
        Pen pen;

        public InvisibleCharsRenderer(Pen pen)
        {
            this.pen = pen;
        }

        public override void Draw(Graphics gr, Point position, Range range)
        {
            var tb = range.tb;
            using(Brush brush = new SolidBrush(pen.Color))
            foreach (var place in range)
            {
                switch (tb[place].c)
                {
                    case ' ':
                        var point = tb.PlaceToPoint(place);
                        point.Offset(tb.CharWidth / 2, tb.CharHeight / 2);
                        gr.DrawLine(pen, point.X, point.Y, point.X + 1, point.Y);
                        break;
                }

                if (tb[place.iLine].Count - 1 == place.iChar)
                {
                    var point = tb.PlaceToPoint(place);
                    point.Offset(tb.CharWidth, 0);
                    gr.DrawString("¶", tb.Font, brush, point);
                }
            }
        }
    }

    
    public class TbInfo
    {
        public AutocompleteMenu popupMenu;
    }
}
