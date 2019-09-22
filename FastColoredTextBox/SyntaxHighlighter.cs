//#define debug
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace FastColoredTextBoxNS
{
    public class SyntaxHighlighter : IDisposable
    {
        
        public void SetSyntaxHighlighter(Dictionary<string,Style> hightlightsetting)
        {
            hightlightsetting.TryGetValue("CommentDoubleStyle", out ForCommentDoubleStyle);
            hightlightsetting.TryGetValue("CommentCurlyStyle", out ForCommentCurlyStyle);
            hightlightsetting.TryGetValue("CommentLineStyle", out ForCommentLineStyle);
            hightlightsetting.TryGetValue("KeywordStyle", out ForKeywordStyle);
            hightlightsetting.TryGetValue("ClassNameStyle", out ForClassNameStyle);
            hightlightsetting.TryGetValue("StringStyle", out ForStringStyle);
            hightlightsetting.TryGetValue("SharpCharStyle", out ForSharpCharStyle);
            hightlightsetting.TryGetValue("NumberStyle", out ForNumberStyle);
            hightlightsetting.TryGetValue("OperatorStyle", out ForOperatorStyle);
            hightlightsetting.TryGetValue("InstructionCompileStyle", out ForInstructionStyle);
            hightlightsetting.TryGetValue("VariableTypeStyle", out ForVariableStyle);
            hightlightsetting.TryGetValue("DefaulStyle", out ForDefaulStyle);
            
        }

        public void SetSyntaxHighlighter(XmlDocument doc)
        {
            ForCommentLineStyle = ParseStyle(doc.SelectSingleNode("HighlightSetting/CommentLineStyle"));
            ForCommentDoubleStyle = ParseStyle(doc.SelectSingleNode("HighlightSetting/CommentDoubleStyle"));
            ForCommentCurlyStyle = ParseStyle(doc.SelectSingleNode("HighlightSetting/CommentCurlyStyle"));
            ForOperatorStyle = ParseStyle(doc.SelectSingleNode("HighlightSetting/OperatorStyle"));
            ForNumberStyle = ParseStyle(doc.SelectSingleNode("HighlightSetting/NumberStyle"));
            ForKeywordStyle = ParseStyle(doc.SelectSingleNode("HighlightSetting/KeywordStyle"));
            ForInstructionStyle = ParseStyle(doc.SelectSingleNode("HighlightSetting/InstructionCompileStyle"));
            ForClassNameStyle = ParseStyle(doc.SelectSingleNode("HighlightSetting/ClassNameStyle"));
            ForSharpCharStyle = ParseStyle(doc.SelectSingleNode("HighlightSetting/SharpCharStyle"));
            ForStringStyle = ParseStyle(doc.SelectSingleNode("HighlightSetting/StringStyle"));
            ForVariableStyle = ParseStyle(doc.SelectSingleNode("HighlightSetting/VariableTypeStyle"));
            ForDefaulStyle = ParseStyle(doc.SelectSingleNode("HighlightSetting/DefaulStyle"));
        }
        /*
        private static Style ParseStyle(XmlNode styleNode)
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
            /*
            if (fontStyleA != null)
            {
                string[] lfont = fontStyleA.Value.Split('|');
                foreach (string font in lfont)
                {
                    fontStyle = (FontStyle)((FontStyle)Enum.Parse(typeof(FontStyle), font) | fontStyle);
                }
            }
            return new TextStyle(foreBrush, backBrush, fontStyle);
        }
        */
        public static TextStyle ParseStyle(XmlNode styleNode)
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
            /*
            if (fontStyleA != null)
            {
                string[] lfont = fontStyleA.Value.Split('|');
                foreach (string font in lfont)
                {
                    fontStyle = (FontStyle)((FontStyle)Enum.Parse(typeof(FontStyle), font) | fontStyle);
                }
            }*/
            return new TextStyle(foreBrush, backBrush, fontStyle);
        }

        private static Color ParseColor(string s)
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
        //styles
        protected static  Platform platformType = PlatformType.GetOperationSystemPlatform();
        /* style bị cắt bỏ.
        public  Style BlueBoldStyle = new TextStyle(Brushes.Blue, null, FontStyle.Bold);
        public  Style BlueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        public  Style BoldStyle = new TextStyle(null, null, FontStyle.Underline);
        public  Style BrownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Italic);
        public  Style GrayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
        public  Style GreenStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        public  Style MagentaStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);
        public  Style MaroonStyle = new TextStyle(Brushes.Maroon, null, FontStyle.Regular);
        public  Style RedStyle = new TextStyle(Brushes.Red, null, FontStyle.Regular);
        public  Style BlackStyle = new TextStyle(Brushes.Black, null, FontStyle.Regular);
        public  Style VioletStyle = new TextStyle(Brushes.DarkViolet, null, FontStyle.Regular);
        public Style RedBoldStyle = new TextStyle(Brushes.Red, null, FontStyle.Bold);// instructioncompile;
        public Style BrownNomalStyle = new TextStyle(Brushes.Brown, null, FontStyle.Regular); // sharp char;
        */
        ///
        /*
        public Style ForCommentDoubleStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        public Style ForCommentLineStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        public Style ForCommentCurlyStyle = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        public Style ForInstructionStyle = new TextStyle(Brushes.Red, null, FontStyle.Bold);
        public Style ForOperatorStyle = new TextStyle(Brushes.Red, null, FontStyle.Regular);
        public Style ForVariableStyle = new TextStyle(Brushes.DarkViolet, null, FontStyle.Regular);
        public Style ForSharpCharStyle = new TextStyle(Brushes.Brown, null, FontStyle.Regular);
        public Style ForKeywordStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        public Style ForClassNameStyle = new TextStyle(null, null, FontStyle.Underline);
        public Style ForStringStyle = new TextStyle(Brushes.Brown, null, FontStyle.Italic);
        public Style ForNumberStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);
        */
        public Style ForCommentDoubleStyle;// = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        public Style ForCommentLineStyle;// = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        public Style ForCommentCurlyStyle;// = new TextStyle(Brushes.Green, null, FontStyle.Italic);
        public Style ForInstructionStyle;// = new TextStyle(Brushes.Red, null, FontStyle.Bold);
        public Style ForOperatorStyle;// = new TextStyle(Brushes.Red, null, FontStyle.Regular);
        public Style ForVariableStyle;// = new TextStyle(Brushes.DarkViolet, null, FontStyle.Regular);
        public Style ForSharpCharStyle;// = new TextStyle(Brushes.Brown, null, FontStyle.Regular);
        public Style ForKeywordStyle;// = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        public Style ForClassNameStyle;// = new TextStyle(null, null, FontStyle.Underline);
        public Style ForStringStyle;// = new TextStyle(Brushes.Brown, null, FontStyle.Italic);
        public Style ForNumberStyle;// = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);
        public Style ForDefaulStyle;
        
        protected Regex PascalAttributeRegex,
                      PascalClassNameRegex;

        protected Regex PascalCommentLineRegex,
                        PascalCommentRegex,
                        PascalCommentCurlyRegex;
                      

        protected Regex PascalKeywordRegex;
        protected Regex PascalNumberRegex;
        protected Regex PascalStringRegex;
        protected Regex PascalVariableRegex;

        protected Regex PascalOperatorRegex;
        protected Regex PascalSharpCharRegex;
        protected Regex PascalInstructionCompileRegex;

        public static RegexOptions RegexCompiledOption
        {
            get
            {
                if (platformType == Platform.X86)
                    return RegexOptions.Compiled;
                else
                    return RegexOptions.None;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            
        }

        #endregion

        /// <summary>
        /// Highlights syntax for given language
        /// </summary>
        public virtual void HighlightSyntax(Language language, Range range, Range rangforComment)
        {
            switch (language)
            {
                case Language.Pascal:
                    PascalSyntaxHighlight(range, rangforComment);
                    break;
               
                default:
                    break;
            }
        }

        public virtual void AutoIndentNeeded(object sender, AutoIndentEventArgs args)
        {
            var tb = (sender as FastColoredTextBox);
            Language language = tb.Language;
            switch (language)
            {
                case Language.Pascal:
                    PascalAutoIndentNeeded(sender, args);
                    break;
                
                default:
                    break;
            }
        }

     

        protected void PascalAutoIndentNeeded(object sender, AutoIndentEventArgs args)
        {
            //block {}
            if (Regex.IsMatch(args.LineText, @"^[^""']*\bbegin.*\end\b}[^""']*$", RegexOptions.IgnoreCase) || Regex.IsMatch(args.LineText, @"^[^""']*\bcase.*\end[^""']*$", RegexOptions.IgnoreCase))
                return;
            
            if (Regex.IsMatch(args.LineText, @"^[^""']*\brepeat.*\/until\b[^""']*$", RegexOptions.IgnoreCase))
                return;
            //start of block {}
            if (Regex.IsMatch(args.LineText, @"^[^""']*\b(begin)\b", RegexOptions.IgnoreCase) || Regex.IsMatch(args.LineText, @"^[^""']*\b(case)\b", RegexOptions.IgnoreCase)
                || Regex.IsMatch(args.LineText, @"\b(repeat)\b", RegexOptions.IgnoreCase) || Regex.IsMatch(args.LineText, @"\b(record)\b", RegexOptions.IgnoreCase))
            {
                string cline=args.LineText.Trim();
                //System.Windows.Forms.MessageBox.Show("."+cline+".");
                if (cline != "begin" && cline != "case" && cline != "record" && cline != "repeat") return;
                args.ShiftNextLines = args.TabLength;
                return;
            }
            //end of block {}
            if (Regex.IsMatch(args.LineText, @"\b(end.|end|end;)[^""']*$", RegexOptions.IgnoreCase) || Regex.IsMatch(args.LineText, @"\b(until)\b", RegexOptions.IgnoreCase))
            {
                args.Shift = -args.TabLength;
                args.ShiftNextLines = -args.TabLength;
                return;
            }
            // else block{}
            if (Regex.IsMatch(args.LineText, @"\b(else)\b", RegexOptions.IgnoreCase))
            {
                //args.Shift = -args.TabLength/2;
                //args.ShiftNextLines = -args.TabLength;
                return;
            }
            
            /* bỏ ngày 2 tháng 3 năm 2016*/

            if (Regex.IsMatch(args.PrevLineText, @"^\s*(if|for|while|[\}\s]*else)\b[^{]*$",RegexOptions.IgnoreCase))
                if (!Regex.IsMatch(args.PrevLineText, @"(;\s*$)|(;\s*//)")) //operator is unclosed
                {
                    //args.Shift = args.TabLength;
                    return;
                }
        }

        

        protected void RestoreBrackets(FastColoredTextBox tb, char[] oldBrackets)
        {
            tb.LeftBracket = oldBrackets[0];
            tb.RightBracket = oldBrackets[1];
            tb.LeftBracket2 = oldBrackets[2];
            tb.RightBracket2 = oldBrackets[3];
        }

        protected char[] RememberBrackets(FastColoredTextBox tb)
        {
            return new[] { tb.LeftBracket, tb.RightBracket, tb.LeftBracket2, tb.RightBracket2 };
        }

        protected void InitCShaprRegex()
        {
            //PascalStringRegex = new Regex( @"""""|@""""|''|@"".*?""|(?<!@)(?<range>"".*?[^\\]"")|'.*?[^\\]'", RegexCompiledOption);

            PascalStringRegex =
                new Regex(
                    @"
                            # Character definitions:
                            '
                            (?> # disable backtracking
                              (?:
                                \\[^\r\n]|    # escaped meta char
                                [^'\r\n]      # any character except '
                              )*
                            )
                            '?
                            |
                            # Normal string & verbatim strings definitions:
                            (?<verbatimIdentifier>@)?         # this group matches if it is an verbatim string
                            ''
                            (?> # disable backtracking
                              (?:
                                # match and consume an escaped character including escaped double quote ("") char
                                (?(verbatimIdentifier)        # if it is a verbatim string ...
                                  ''''|                         #   then: only match an escaped double quote ("") char
                                  \\.                         #   else: match an escaped sequence
                                )
                                | # OR
            
                                # match any char except double quote char ("")
                                [^'']
                              )*
                            )
                            ''
                        ",
                    RegexOptions.ExplicitCapture | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace |
                    RegexCompiledOption
                    ); //thanks to rittergig for this regex
            
            PascalCommentLineRegex = new Regex(@"//.*$", RegexOptions.Multiline );
            PascalCommentRegex = new Regex(@"(\*\))|(\(\*)");
            PascalCommentCurlyRegex = new Regex(@"(})|({(?!\$))");
            PascalNumberRegex = new Regex(@"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b",
                                          RegexCompiledOption);
            PascalAttributeRegex = new Regex(@"^\s*(?<range>\[.+?\])\s*$", RegexOptions.Multiline | RegexCompiledOption);
            PascalClassNameRegex = new Regex(@"\b(function|class|object|procedure|program|unit)\s+(?<range>\w+?)\b", RegexCompiledOption | RegexOptions.IgnoreCase);
            PascalVariableRegex = new Regex(@"\b(integer|real|boolean|longint|word|char|byte|shortint|smallint|longword|int64|qword|cardinal|dword|single|double|extended|comp|currency|widechar|shortstring|ansistring)\b", RegexCompiledOption | RegexOptions.IgnoreCase);
            PascalKeywordRegex =
                new Regex(
                    @"\b(absolute|and|array|asm|begin|case|const|constructor|destructor|div|do|downto|else|end|file|for|goto|if|implementation|in|inherited|inline|interface|label|mod|nil|not|object|of|on|operator|or|packed|function|procedure|program|record|reintroduce|repeat|self|set|shl|shr|string|then|to|type|unit|until|uses|var|while|with|xor|as|class|dispinterface|except|exports|finalization|finally|initialization|inline|is|library|on|out|packed|property|raise|resourcestring|threadvar|try|dispose|exit|false|new|true|absolute|abstract|alias|assembler|cdecl|cppdecl|default|export|external|far|far16|forward|index|local|name|near|nostackframe|oldfpccall|override|pascal|private|protected|public|published|read|register|reintroduce|safecall|softfloat|stdcall|virtual|write)\b",
                    RegexOptions.IgnoreCase|RegexCompiledOption);
            PascalOperatorRegex = new Regex(@"(;|,|\[|\]|\+|\-|\=|\:|\(|\)|\*|\\|\>|\<|\.|\,)", RegexOptions.Multiline | RegexCompiledOption);
            PascalSharpCharRegex = new Regex(@"#(\d+)\b");
            PascalInstructionCompileRegex = new Regex(@"({\$)|({\$)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            //PascalInstructionCompileRegex = new Regex(@"({\$.*?})|({\$.*)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        }

        public void InitStyleSchema(Language lang)
        {
            
            
            switch (lang)
            {
                case Language.Pascal:
                    StringStyle = ForStringStyle;
                    CommentDoubleStyle = ForCommentDoubleStyle;
                    NumberStyle = ForNumberStyle;
                    ClassNameStyle = ForClassNameStyle;
                    KeywordStyle = ForKeywordStyle;
                    VariableStyle = ForVariableStyle;
                    OperatorStyle = ForOperatorStyle;
                    CommentCurlyStyle = ForCommentCurlyStyle;
                    CommentLineStyle = ForCommentLineStyle;
                    SharpCharStyle = ForSharpCharStyle;
                    InstructionCompileStyle = ForInstructionStyle;
                    break;
                
            }
        }

        /// <summary>
        /// Added by giaosudauto
        /// </summary>
        /// <param name="language"></param>
        /// <param name="range"></param>
        public virtual void HighlightComment(Language language, Range range)
        {
            switch (language)
            {
                case Language.Pascal:
                    range.ClearDic();
                    range.ClearStyle(CommentCurlyStyle, CommentDoubleStyle);
                    range.SetStyle(CommentDoubleStyle, PascalCommentRegex, HighlightType.CommentDouble);
                    range.SetStyle(CommentCurlyStyle, PascalCommentCurlyRegex, HighlightType.CommentCurly);
            
                    range.DoHighLight();
                    //System.Windows.Forms.MessageBox.Show("xxx");
                    //System.Windows.Forms.MessageBox.Show(range.Text);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Highlights C# code
        /// </summary>
        /// <param name="range"></param>
        public virtual void PascalSyntaxHighlight(Range range, Range rangforComment)
        {
            
            range.ClearDic();
            range.tb.CommentPrefix = "//";
            range.tb.LeftBracket = '(';
            range.tb.RightBracket = ')';
            range.tb.LeftBracket2 = '[';
            range.tb.RightBracket2 = ']';
            range.tb.BracketsHighlightStrategy = BracketsHighlightStrategy.Strategy2;
            
            range.tb.AutoIndentCharsPatterns
                = @"
^\s*[\w\.]+(\s\w+)?\s*(?<range>:=)\s*(?<range>[^;]+);
^\s*(case|default)\s*[^:]*(?<range>:)\s*(?<range>[^;]+);
";
            //clear style of changed range
            range.ClearStyle(OperatorStyle, StringStyle, CommentDoubleStyle, NumberStyle, ClassNameStyle, KeywordStyle, VariableStyle, CommentCurlyStyle, CommentLineStyle, InstructionCompileStyle, SharpCharStyle);
            //rangforComment.ClearStyle(CommentCurlyStyle, CommentStyle);
            //
            if (PascalStringRegex == null)
                InitCShaprRegex();
            
            //comment highlighting
            range.SetStyle(CommentLineStyle, PascalCommentLineRegex, HighlightType.CommentLine);
            //range.SetBrackets(CommentCurlyStyle, CommentStyle);
            range.SetBrackets(CommentCurlyStyle, CommentDoubleStyle, InstructionCompileStyle);
                    
            //range.SetStyle(CommentStyle, PascalCommentRegex, HighlightType.Comment);
            //range.SetStyle(CommentStyle, PascalCommentRegex3);
            range.SetStyle(CommentDoubleStyle, PascalCommentRegex, HighlightType.CommentDouble);
            range.SetStyle(CommentCurlyStyle, PascalCommentCurlyRegex, HighlightType.CommentCurly);
            //string highlighting
            range.SetStyle(StringStyle, PascalStringRegex,HighlightType.String);
            //number highlighting
            range.SetStyle(NumberStyle, PascalNumberRegex,HighlightType.Nummber);
            //attribute highlighting
            //range.SetStyle(AttributeStyle, PascalAttributeRegex,HighlightType.);
            //class name highlighting
            range.SetStyle(ClassNameStyle, PascalClassNameRegex,HighlightType.ClassName);
            //keyword highlighting
            range.SetStyle(KeywordStyle, PascalKeywordRegex,HighlightType.Keyword);
            // variable type highlighting
            range.SetStyle(VariableStyle, PascalVariableRegex, HighlightType.Variable);
            // operator highlight
            range.SetStyle(OperatorStyle, PascalOperatorRegex, HighlightType.Operator);
            // sharp char
            range.SetStyle(SharpCharStyle, PascalSharpCharRegex, HighlightType.SharpChar);
            // instruction commpile
            range.SetStyle(InstructionCompileStyle, PascalInstructionCompileRegex, HighlightType.InstructionCompile);
            // processing...
            //rangforComment.DoHighLight();
            range.DoHighLight();
            
            //clear folding markers
            range.ClearFoldingMarkers();
            //set folding markers
            range.SetFoldingMarkers(@"{", @"}",HighlightType.CommentCurly); //allow to collapse brackets block
            range.SetFoldingMarkers(@"\(\*", @"\*\)", HighlightType.CommentDouble); //allow to collapse comment block
            range.SetFoldingMarkers(@"\b(repeat)\b", @"\b(until)\b", RegexOptions.IgnoreCase|RegexOptions.Compiled,HighlightType.Keyword); //allow to collapse beginend; blocks
            range.SetFoldingMarkers(@"\b(begin|case|record)\b", @"\b(end)\b", RegexOptions.IgnoreCase | RegexOptions.Compiled, HighlightType.Keyword); //allow to collapse beginend. blocks
            
        }


        #region Styles
        
        public Style StringStyle { get; set; }

        public Style CommentDoubleStyle { get; set; }

        public Style CommentLineStyle { get; set; }

        public Style CommentCurlyStyle { get; set; }

        public Style NumberStyle { get; set; }

        public Style ClassNameStyle { get; set; }

        public Style KeywordStyle { get; set; }

        public Style VariableStyle { get; set; }

        public Style StatementsStyle { get; set; }

        public Style FunctionsStyle { get; set; }

        public Style TypesStyle { get; set; }

        public Style OperatorStyle { get; set; }

        public Style InstructionCompileStyle { get; set; }

        public Style SharpCharStyle { get; set; }

        #endregion
    }

    /// <summary>
    /// Language
    /// </summary>
    public enum Language
    {
        Pascal,
        Custom
    }
}