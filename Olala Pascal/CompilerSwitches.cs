using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OlalaPascal
{
    public partial class CompilerSwitches : Form
    {
        public CompilerSwitches()
        {
            InitializeComponent();
        }

        private void CompilerSwitches_Load(object sender, EventArgs e)
        {
            SetLanguage();
            // tab Sytax...
               // SyntaxSwitches...
            this.stopAfterFirstErrorCheckbox.Checked = Properties.Settings.Default.StopAffterFirstErrorCheck;
            this.allowLABELandGOTOCheckbox.Checked = Properties.Settings.Default.AllowLABELandGOTO;
            this.enableMacrosCheckbox.Checked = Properties.Settings.Default.EnableMacros;
            this.allowInlineCheckbox.Checked = Properties.Settings.Default.AllowInline;
            this.includeAssertionCodeCheckbox.Checked = Properties.Settings.Default.IncludeAssertionCode;
            this.useAnsiStringsCheckbox.Checked = Properties.Settings.Default.UseAnsiString;
            this.loadKylixcompactCheckbox.Checked = Properties.Settings.Default.LoadKylixcompact;
            this.allowStaticCheckbox.Checked = Properties.Settings.Default.AllowSTATICinObject;
            this.clikeOperatorCheckbox.Checked = Properties.Settings.Default.CLikeOperator;
               // Compiler Mode
            switch(Properties.Settings.Default.CompilerMode)
            {
                case 'O': this.objectPascalRadio.Checked = true; break;
                case 'T': this.turboPascalRadio.Checked = true; break;
                case 'D': this.delphiRadio.Checked = true; break;
                case 'M': this.macintoshPascalRadio.Checked = true; break;
                default: this.freePascalRadio.Checked = true; break;
                
            }
            // Generated Code...
               // Code generation
            this.rangeCheckingCheckbox.Checked = Properties.Settings.Default.RangeChecking;
            this.stackCheckingCheckbox.Checked = Properties.Settings.Default.StackChecking;
            this.IOCheckingCheckbox.Checked = Properties.Settings.Default.IOChecking;
            this.integerOverflowCheckingCheckbox.Checked = Properties.Settings.Default.IntegerOverflowChecking;
            this.objectMetholCallCheckingCheckbox.Checked = Properties.Settings.Default.ObjectMethodCallChecking;
            this.positionIndependentCodeCheckbox.Checked = Properties.Settings.Default.PositionIndepentCode;
            this.createSmartlinkableUnitsCheckbox.Checked = Properties.Settings.Default.CreateSmartlinkableUnits;
               // Optimizations
            this.generateSmallerCodeCheckbox.Checked = Properties.Settings.Default.GenerateSmallerCode;
            this.useRegisterVariablesCheckbox.Checked = Properties.Settings.Default.UseResisterVariable;
            this.uncertainOptimizationsCheckbox.Checked = Properties.Settings.Default.UncertainOptimizations;
            this.levelOneOptimizationsCheckbox.Checked = Properties.Settings.Default.Level1Optimizations;
            this.levelTwoOptimizationsCheckbox.Checked = Properties.Settings.Default.Level2Optimizations;
            this.levelThreeOptimizationsCheckbox.Checked = Properties.Settings.Default.Level3Optimizations;
            // Processors...
               //Optimization...
            switch(Properties.Settings.Default.OptimizationTargetProcessor)
            {
                case '1': this.pentiumRadio.Checked = true; break;
                case '2': this.pentiumMMXRadio.Checked = true; break;
                case '3': this.pentium2Radio.Checked = true; break;
                case '4': this.pentium4Radio.Checked = true; break;
                case '5': this.PentiumMRadio.Checked = true; break;
                default: this._80386Radio.Checked = true; break;
            }
              //Compiler mode...
            switch (Properties.Settings.Default.CompilerMode)
            {
                case '1': this.pentiumRadio2.Checked = true; break;
                case '2': this.pentiumMMXRadio2.Checked = true; break;
                case '3': this.pentium2Radio2.Checked = true; break;
                case '4': this.pentium4Radio2.Checked = true; break;
                case '5': this.PentiumMRadio2.Checked = true; break;
                default: this._80386Radio2.Checked = true; break;
            }
            // verbose...
            this.warnigCheckBox.Checked = Properties.Settings.Default.Warnings;
            this.notesCheckbox.Checked = Properties.Settings.Default.Notes;
            this.hintsCheckbox.Checked = Properties.Settings.Default.Hints;
            this.generalInfoCheckbox.Checked = Properties.Settings.Default.GeneralInfo;
            this.useTryedCheckbox.Checked = Properties.Settings.Default.UseTryedInfo;
            this.showallErrorCheckbox.Checked = Properties.Settings.Default.ShowallProcedures;
            this.allCheckbox.Checked = Properties.Settings.Default.All;
            // browser...
            switch(Properties.Settings.Default.Brownser)
            {
                case 'O': this.onlyGlobalbrowserRadio.Checked = true; break;
                case 'L': this.localAndGlobalRadio.Checked = true; break;
                default: this.noBrowserRadio.Checked = true; break;
            }
            // Assembler...
            switch(Properties.Settings.Default.AssemblerReader)
            {
                case 'A': this.ATandTAssRadio.Checked = true; break;
                case 'I': this.intelAssRadio.Checked = true; break;
                default: this.defaulAssRadio.Checked = true; break;

            }

            this.listSourceCheckbox.Checked = Properties.Settings.Default.ListSource;
            this.listRegisterCheckbox.Checked = Properties.Settings.Default.ListRegisterAllocation;
            this.listTempCheckbox.Checked = Properties.Settings.Default.ListTempAllocation;
            this.listNodeCheckbox.Checked = Properties.Settings.Default.ListNodeAllocation;
            this.usePipeCheckbox.Checked = Properties.Settings.Default.UsePipeWithAssembler;
            // stringing...
            this.conditionalcomboBox.Text = Properties.Settings.Default.AdditionalCompilerArgs;
            this.auditionalComboBox.Text = Properties.Settings.Default.ConditionalDefines;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            char res;// = 'F';
            // tab Sytax...
            // SyntaxSwitches...
            Properties.Settings.Default.StopAffterFirstErrorCheck = this.stopAfterFirstErrorCheckbox.Checked;
            Properties.Settings.Default.AllowLABELandGOTO = this.allowLABELandGOTOCheckbox.Checked;
            Properties.Settings.Default.EnableMacros = this.enableMacrosCheckbox.Checked;
            Properties.Settings.Default.AllowInline = this.allowInlineCheckbox.Checked;
            Properties.Settings.Default.IncludeAssertionCode = this.includeAssertionCodeCheckbox.Checked;
            Properties.Settings.Default.UseAnsiString=this.useAnsiStringsCheckbox.Checked;
            Properties.Settings.Default.LoadKylixcompact = this.loadKylixcompactCheckbox.Checked;
            Properties.Settings.Default.AllowSTATICinObject = this.allowStaticCheckbox.Checked;
            Properties.Settings.Default.CLikeOperator = this.clikeOperatorCheckbox.Checked;
            // Compiler Mode
            
            res = 'F';
            if (this.objectPascalRadio.Checked) res = 'O';
            if (this.turboPascalRadio.Checked) res = 'T';
            if (this.delphiRadio.Checked) res = 'D';
            if (this.macintoshPascalRadio.Checked) res = 'M';
            Properties.Settings.Default.CompilerMode = res;

            // Generated Code...
            // Code generation
            Properties.Settings.Default.RangeChecking = this.rangeCheckingCheckbox.Checked;
            Properties.Settings.Default.StackChecking = this.stackCheckingCheckbox.Checked;
            Properties.Settings.Default.IOChecking = this.IOCheckingCheckbox.Checked;
            Properties.Settings.Default.IntegerOverflowChecking = this.integerOverflowCheckingCheckbox.Checked;
            Properties.Settings.Default.ObjectMethodCallChecking = this.objectMetholCallCheckingCheckbox.Checked;
            Properties.Settings.Default.PositionIndepentCode = this.positionIndependentCodeCheckbox.Checked;
            Properties.Settings.Default.CreateSmartlinkableUnits = this.createSmartlinkableUnitsCheckbox.Checked;
            // Optimizations
            Properties.Settings.Default.GenerateSmallerCode = this.generateSmallerCodeCheckbox.Checked;
            Properties.Settings.Default.UseResisterVariable = this.useRegisterVariablesCheckbox.Checked;
            Properties.Settings.Default.UncertainOptimizations = this.uncertainOptimizationsCheckbox.Checked;
            Properties.Settings.Default.Level1Optimizations = this.levelOneOptimizationsCheckbox.Checked;
            Properties.Settings.Default.Level2Optimizations = this.levelTwoOptimizationsCheckbox.Checked;
            Properties.Settings.Default.Level3Optimizations = this.levelThreeOptimizationsCheckbox.Checked;
            // Processors...
            //Optimization...
            res = '0';
            if (this.pentiumRadio.Checked) res = '1';
            if (this.pentiumMMXRadio.Checked) res = '2';
            if (this.pentium2Radio.Checked) res = '3';
            if (this.pentium4Radio.Checked) res = '4';
            if (this.PentiumMRadio.Checked) res = '5';
            Properties.Settings.Default.OptimizationTargetProcessor=res;
            //Compiler mode...
            res = '0';
            if (this.pentiumRadio.Checked) res = '1';
            if (this.pentiumMMXRadio.Checked) res = '2';
            if (this.pentium2Radio.Checked) res = '3';
            if (this.pentium4Radio.Checked) res = '4';
            if (this.PentiumMRadio.Checked) res = '5';
            Properties.Settings.Default.CompilerMode=res;
            // verbose...
            Properties.Settings.Default.Warnings = this.warnigCheckBox.Checked;
            Properties.Settings.Default.Notes = this.notesCheckbox.Checked;
            Properties.Settings.Default.Hints = this.hintsCheckbox.Checked;
            Properties.Settings.Default.GeneralInfo = this.generalInfoCheckbox.Checked;
            Properties.Settings.Default.UseTryedInfo = this.useTryedCheckbox.Checked;
            Properties.Settings.Default.ShowallProcedures = this.showallErrorCheckbox.Checked;
            Properties.Settings.Default.All = this.allCheckbox.Checked;
            
            res = 'N';
            if (this.onlyGlobalbrowserRadio.Checked) res = 'O';
            if (this.localAndGlobalRadio.Checked) res = 'L';
            Properties.Settings.Default.Brownser=res;
            // Assembler...
            res = 'D';
            if (this.ATandTAssRadio.Checked) res = 'A';
            if (this.intelAssRadio.Checked) res = 'I';
            Properties.Settings.Default.AssemblerReader=res;

            Properties.Settings.Default.ListSource = this.listSourceCheckbox.Checked;
            Properties.Settings.Default.ListRegisterAllocation = this.listRegisterCheckbox.Checked;
            Properties.Settings.Default.ListTempAllocation = this.listTempCheckbox.Checked;
            Properties.Settings.Default.ListNodeAllocation = this.listNodeCheckbox.Checked;
            Properties.Settings.Default.UsePipeWithAssembler = this.usePipeCheckbox.Checked;
            // ...stringing
            Properties.Settings.Default.AdditionalCompilerArgs = this.conditionalcomboBox.Text;
            Properties.Settings.Default.ConditionalDefines = this.auditionalComboBox.Text;

            Properties.Settings.Default.Save();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //string spath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)+"\\";
        private void SetLanguage()
        {
            string path;
            string[] line;
            string Language = File.ReadAllText(AppPath.Data+"lang");
            if (Language == "English") return;
            //**** Go to form ****
            path = "Language\\" + Language + "\\CompilerForm.lang";
            CompilerForm = new Dictionary<string, string>();
            line = File.ReadAllLines(path);
            foreach (string curent in line)
            {
                if (curent == "") continue;
                string[] s = curent.Split('=');
                string tmp;
                if (!CompilerForm.TryGetValue(s[0], out tmp)) CompilerForm.Add(s[0], s[1]);
                //System.Windows.Forms.MessageBox.Show(curent);
            }
            InitInterface();
        }

        private string Translate(string s)
        {
            string res;
            if (CompilerForm.TryGetValue(s, out res))
                return res;
            return s;
        }

        private void InitInterface()
        {
            this.syntaxTabPage.Text = Translate("Syntax");
            this.compilerModeGoupBox.Text = Translate("Compiler Mode");
            this.macintoshPascalRadio.Text = Translate("Macintosh Pascal dialect");
            this.delphiRadio.Text = Translate("Delphi compatible");
            this.turboPascalRadio.Text = Translate("Turbo Pascal compatible");
            this.objectPascalRadio.Text = Translate("Object Pascal extension on");
            this.freePascalRadio.Text = Translate("Free Pascal dealect");
            this.syntaxSwitchesGroupBox.Text = Translate("Syntax Switches");
            this.includeAssertionCodeCheckbox.Text = Translate("Include assertion code");
            this.clikeOperatorCheckbox.Text = Translate("C-like operators");
            this.allowStaticCheckbox.Text = Translate("Allow STATIC in objects");
            this.loadKylixcompactCheckbox.Text = Translate("Load Kylix compat. unit");
            this.useAnsiStringsCheckbox.Text = Translate("Use Ansi Strings");
            this.allowInlineCheckbox.Text = Translate("Allow inline");
            this.enableMacrosCheckbox.Text = Translate("Enable Macros");
            this.allowLABELandGOTOCheckbox.Text = Translate("Allow LABEL and GOTO");
            this.stopAfterFirstErrorCheckbox.Text = Translate("Stop affter first error");
            this.generatedCodetabPage.Text = Translate("Generated Code");
            this.optimizationsGroupbox.Text = Translate("Optimizations");
            this.levelThreeOptimizationsCheckbox.Text = Translate("Level 3 optimizations");
            this.levelTwoOptimizationsCheckbox.Text = Translate("Level 2 optimizations");
            this.uncertainOptimizationsCheckbox.Text = Translate("Uncertain optimizations");
            this.levelOneOptimizationsCheckbox.Text = Translate("Level 1 optimizations");
            this.useRegisterVariablesCheckbox.Text = Translate("Use register-variables");
            this.generateSmallerCodeCheckbox.Text = Translate("Generate smaller code");
            this.codeGenerationGroupbox.Text = Translate("Code generation");
            this.createSmartlinkableUnitsCheckbox.Text = Translate("Create smartlinkable units");
            this.positionIndependentCodeCheckbox.Text = Translate("Position independent code");
            this.objectMetholCallCheckingCheckbox.Text = Translate("Object method call checking");
            this.integerOverflowCheckingCheckbox.Text = Translate("Integer overflow checking");
            this.IOCheckingCheckbox.Text = Translate("I/O checking");
            this.stackCheckingCheckbox.Text = Translate("Stack checking");
            this.rangeCheckingCheckbox.Text = Translate("Range checking");
            this.processorTabPage.Text = Translate("Processor");
            this.codeGenerationTargetGroupbox.Text = Translate("Code generation target processor");
            this.PentiumMRadio2.Text = Translate("PentiumM");
            this.pentium4Radio2.Text = Translate("Pentium4");
            this.pentium2Radio2.Text = Translate("Pentium2/PetiumM/AMD");
            this.pentiumMMXRadio2.Text = Translate("PentiumMMX (tm)");
            this.pentiumRadio2.Text = Translate("Pentium (tm)");
            this._80386Radio2.Text = Translate("80386");
            this.optimizationTagetProcessor.Text = Translate("Optimization targer processor");
            this.PentiumMRadio.Text = Translate("PentiumM");
            this.pentium4Radio.Text = Translate("Pentium4");
            this.pentium2Radio.Text = Translate("Pentium2/PetiumM/AMD");
            this.pentiumMMXRadio.Text = Translate("PentiumMMX (tm)");
            this.pentiumRadio.Text = Translate("Pentium (tm)");
            this._80386Radio.Text = Translate("80386");
            this.verboseTabPage.Text = Translate("Verbose");
            this.verboseSwitchesGroupbox.Text = Translate("Verbose Switches");
            this.showallErrorCheckbox.Text = Translate("Show all Procedures if error");
            this.allCheckbox.Text = Translate("All");
            this.useTryedCheckbox.Text = Translate("Used, tryed info");
            this.generalInfoCheckbox.Text = Translate("General infos");
            this.hintsCheckbox.Text = Translate("Hints");
            this.notesCheckbox.Text = Translate("Notes");
            this.warnigCheckBox.Text = Translate("Warnings");
            this.browserTabPage.Text = Translate("Browser");
            this.browserGroupbox.Text = Translate("Browser");
            this.localAndGlobalRadio.Text = Translate("Local and global browser");
            this.onlyGlobalbrowserRadio.Text = Translate("Only Global browser");
            this.noBrowserRadio.Text = Translate("No browser");
            this.assemblerTabPage.Text = Translate("Assembler");
            this.assOuputGroupbox.Text = Translate("Assembler output");
            this.useDefaulOutRadio.Text = Translate("Use defaul output");
            this.assInfoGroupbox.Text = Translate("Assembler info");
            this.usePipeCheckbox.Text = Translate("Use pipe with assembler");
            this.listNodeCheckbox.Text = Translate("List node allocation");
            this.listTempCheckbox.Text = Translate("List temp allocation");
            this.listRegisterCheckbox.Text = Translate("List register allocation");
            this.listSourceCheckbox.Text = Translate("List source");
            this.assReaderGroupbox.Text = Translate("Assembler reader");
            this.intelAssRadio.Text = Translate("Intel style assembler");
            this.ATandTAssRadio.Text = Translate("AT&&T style assembler");
            this.defaulAssRadio.Text = Translate("Default style assembler");
            this.button2.Text = Translate("Cancel");
            this.okButton.Text = Translate("OK");
            this.additionalCompilerArgsLable.Text = Translate("Additional compiler args");
            this.conditionalDefinesLable.Text = Translate("Conditional defines");
            this.Text = Translate("CompilerSwitches");

        }
        Dictionary<string, string> CompilerForm;

    }
}
