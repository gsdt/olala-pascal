using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlalaPascal
{
    public partial class PowerfulPascalEditor
    {
        private Dictionary<string, string> MainForm;
        
        private string Translate(string s)
        {
            string res;
            if (MainForm.TryGetValue(s, out res))
                return res;
            return s;
        }
        private void SetLanguage()
        {
            string path;
            //string spath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\";
            string[] line;
            if (!File.Exists(AppPath.Data + "lang")) File.WriteAllText(AppPath.Data + "lang", "Vietnamese");
            string Language = File.ReadAllText(AppPath.Data + "lang");
            
            MainForm = new Dictionary<string, string>();
            if (Language == "English")
            {
                englishToolStripMenuItem.Checked = true;
                return;
            }
            vietnameseToolStripMenuItem.Checked = true;
            path = "Language\\"+Language + "\\MainForm.lang";
            line = File.ReadAllLines(path);
                                       //E:\VisualC#Project\OlalaPascal - backup1 ---\Olala Pascal\bin\Debug\Language\Vietnamese\MainForm.lang
            foreach (string curent in line)
            {
                if (curent == "") continue;
                string[] s = curent.Split('=');
                string tmp;
                if (!MainForm.TryGetValue(s[0], out tmp)) MainForm.Add(s[0], s[1]);
            }
        }

        public void InitLanguage()
        {
            SetLanguage();
            this.toolStripStatusRow.ToolTipText = Translate("Current Row");
            this.toolStripStatusColumn.ToolTipText = Translate("Current Column");
            this.toolStripStatusLines.ToolTipText = Translate("Total Lines");
            this.toolStripStatusLengths.ToolTipText = Translate("Total Chars");
            this.btHighlightCurrentLine.ToolTipText = Translate("Highlight current line");
            this.toolStripButtonCompile.ToolTipText = Translate("Compile (F9)");
            this.toolStripButtonRun.ToolTipText = Translate("Run (Ctr+F9)");
            this.soundToolStripMenuItem.Text = Translate("Sound");
            this.soundStripButton.ToolTipText = Translate("Sound");

            this.msMain.Text = Translate("menuStrip1");
            this.fileToolStripMenuItem.Text = Translate("&File");
            this.newToolStripMenuItem.Text = Translate("&New");
            this.openToolStripMenuItem.Text = Translate("&Open");
            this.saveToolStripMenuItem.Text = Translate("&Save");
            this.saveAsToolStripMenuItem.Text = Translate("Save &as ...");
            this.toolStripMenuItem5.Text = Translate("&Print");
            this.toolStripMenuItemClose.Text = Translate("&Close");
            this.toolStripMenuItemCloseAll.Text = Translate("Close a&ll");
            this.quitToolStripMenuItem.Text = Translate("&Quit");
            this.editToolStripMenuItem.Text = Translate("&Edit");
            this.undoToolStripMenuItem1.Text = Translate("&Undo");
            this.redoToolStripMenuItem1.Text = Translate("&Redo");
            this.cutToolStripMenuItem1.Text = Translate("Cu&t");
            this.copyToolStripMenuItem1.Text = Translate("&Copy");
            this.pasteToolStripMenuItem1.Text = Translate("&Paste");
            this.searchToolStripMenuItem.Text = Translate("&Search");
            this.findToolStripMenuItem1.Text = Translate("&Find");
            this.replaceToolStripMenuItem1.Text = Translate("&Replace");
            this.gotoLineToolStripMenuItem.Text = Translate("&Go to line");
            this.viewToolStripMenuItem.Text = Translate("&View");
            this.bulletinToolStripMenuItem.Text = Translate("&Bulletin board");
            this.toolBarToolStripMenuItem.Text = Translate("&Tool bar");
            this.highlightCurrentLineToolStripMenuItem.Text = Translate("&Highlight current line");
            this.foldingLineToolStripMenuItem.Text = Translate("&Folding line");
            this.fullScreenToolStripMenuItem.Text = Translate("Full &Screen");
            this.buildToolStripMenuItem.Text = Translate("&Build");
            this.compileToolStripMenuItem.Text = Translate("&Compile");
            this.runToolStripMenuItem.Text = Translate("&Run");
            this.toolStripMenuItem.Text = Translate("&Tool");
            this.calculatorToolStripMenuItem.Text = Translate("&Calculator");
            this.aSCIITableToolStripMenuItem.Text = Translate("&ASCII Table");
            this.optionToolStripMenuItem.Text = Translate("&Option");
            this.editorToolStripMenuItem.Text = Translate("&Editor");
            this.compilerToolStripMenuItem.Text = Translate("&Compiler");
            this.languageToolStripMenuItem.Text = Translate("Language");
            this.englishToolStripMenuItem.Text = Translate("&English");
            this.vietnameseToolStripMenuItem.Text = Translate("&Vietnamese");
            this.helpToolStripMenuItem.Text = Translate("&Help");
            this.guildToolStripMenuItem.Text = Translate("&User manual");
            this.aboutToolStripMenuItem.Text = Translate("&About");
            this.ssMain.Text = Translate("statusStrip1");
            this.toolStripStatusSaveStatus.Text = Translate("Saved");
            this.toolStripStatusRow.Text = Translate("Row:     ");
            this.toolStripStatusColumn.Text = Translate("Column:     ");
            this.toolStripStatusLines.Text = Translate("Lines:    ");
            this.toolStripStatusLengths.Text = Translate("Length:    ");
            this.toolStripStatusZoom.Text = Translate("Zoom");
            this.btZoom.Text = Translate("Zoom");
            this.toolStripMenuItem11.Text = Translate("300%");
            this.toolStripMenuItem10.Text = Translate("200%");
            this.toolStripMenuItem9.Text = Translate("150%");
            this.toolStripMenuItem8.Text = Translate("100%");
            this.toolStripMenuItem7.Text = Translate("50%");
            this.toolStripMenuItem6.Text = Translate("25%");
            this.tsMain.Text = Translate("toolStrip1");
            this.newToolStripButton.Text = Translate("&New");
            this.openToolStripButton.Text = Translate("&Open");
            this.saveToolStripButton.Text = Translate("&Save");
            this.printToolStripButton.Text = Translate("&Print");
            this.cutToolStripButton.Text = Translate("C&ut");
            this.copyToolStripButton.Text = Translate("&Copy");
            this.pasteToolStripButton.Text = Translate("&Paste");
            this.btHighlightCurrentLine.Text = Translate("Highlight current line");
            this.btShowFoldingLines.Text = Translate("Show folding lines");
            this.undoStripButton.Text = Translate("Undo (Ctrl+Z)");
            this.redoStripButton.Text = Translate("Redo (Ctrl+R)");
            this.backStripButton.Text = Translate("Navigate Backward (Ctrl+ -)");
            this.forwardStripButton.Text = Translate("Navigate Forward (Ctrl+Shift+ -)");
            this.FindBoxToolStripLabel.Text = Translate("Find: ");
            this.bookmarkPlusButton.Text = Translate("Add bookmark (Ctrl-B)");
            this.bookmarkMinusButton.Text = Translate("Remove bookmark (Ctrl-Shift-B)");
            this.gotoButton.Text = Translate("Goto...");
            this.cutToolStripMenuItem.Text = Translate("Cut");
            this.copyToolStripMenuItem.Text = Translate("Copy");
            this.pasteToolStripMenuItem.Text = Translate("Paste");
            this.selectAllToolStripMenuItem.Text = Translate("Select all");
            this.undoToolStripMenuItem.Text = Translate("Undo");
            this.redoToolStripMenuItem.Text = Translate("Redo");
            this.findToolStripMenuItem.Text = Translate("Find");
            this.replaceToolStripMenuItem.Text = Translate("Replace");
            this.autoIndentSelectedTextToolStripMenuItem.Text = Translate("AutoIndent selected text");
            this.commentSelectedToolStripMenuItem.Text = Translate("Comment selected");
            this.uncommentSelectedToolStripMenuItem.Text = Translate("Uncomment selected");
            this.cloneLinesToolStripMenuItem.Text = Translate("Clone line(s)");
            this.cloneLinesAndCommentToolStripMenuItem.Text = Translate("Clone line(s) and comment");
            this.tsFiles.Text = Translate("faTabStrip1");
            this.Text = Translate("Olala Pascal (c) 2015 by Nguyễn Anh Tuấn");



        }
    }
}
