using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FastColoredTextBoxNS
{
    class RegexType
    {
        public static readonly Platform platformType = PlatformType.GetOperationSystemPlatform();
        public Regex PascalAttributeRegex, PascalClassNameRegex;
        public Regex PascalCommentRegex1, PascalCommentRegex2,PascalCommentRegex3,
                      PascalCommentRegex4,PascalCommentRegex5;
        public Regex PascalKeywordRegex;
        public Regex PascalNumberRegex;
        public Regex PascalStringRegex;
        public Regex PascalVariableRegex;
        public Regex PascalOperatorRegex;
        
        public RegexType()
        {
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
                            ""
                            (?> # disable backtracking
                              (?:
                                # match and consume an escaped character including escaped double quote ("") char
                                (?(verbatimIdentifier)        # if it is a verbatim string ...
                                  """"|                         #   then: only match an escaped double quote ("") char
                                  \\.                         #   else: match an escaped sequence
                                )
                                | # OR
            
                                # match any char except double quote char ("")
                                [^'']
                              )*
                            )
                            ""
                        ",
                    RegexOptions.ExplicitCapture | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase |
                    RegexCompiledOption
                    ); //thanks to rittergig for this regex

            PascalCommentRegex1 = new Regex(@"//.*$", RegexOptions.Multiline | RegexCompiledOption);
            PascalCommentRegex2 = new Regex(@"(\(\*.*?\*\))|(\(\*.*)", RegexOptions.Singleline | RegexCompiledOption);
            PascalCommentRegex3 = new Regex(@"({.*?})|({.*)", RegexOptions.Singleline | RegexCompiledOption);
            PascalNumberRegex = new Regex(@"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b",
                                          RegexCompiledOption);
            PascalAttributeRegex = new Regex(@"^\s*(?<range>\[.+?\])\s*$", RegexOptions.Multiline | RegexCompiledOption);
            PascalClassNameRegex = new Regex(@"\b(function|class|object|procedure|program|unit)\s+(?<range>\w+?)\b", RegexCompiledOption | RegexOptions.IgnoreCase);
            PascalVariableRegex = new Regex(@"\b(integer|real|boolean|longint|word|char|byte|shortint|smallint|longword|int64|qword|cardinal|dword|single|double|extended|comp|currency|widechar|shortstring|ansistring)\b", RegexCompiledOption | RegexOptions.IgnoreCase);
            PascalKeywordRegex =
                new Regex(
                    @"\b(absolute|and|array|asm|begin|case|const|constructor|destructor|div|do|downto|else|end|file|for|goto|if|implementation|in|inherited|inline|interface|label|mod|nil|not|object|of|on|operator|or|packed|function|procedure|program|record|reintroduce|repeat|self|set|shl|shr|string|then|to|type|unit|until|uses|var|while|with|xor|as|class|dispinterface|except|exports|finalization|finally|initialization|inline|is|library|on|out|packed|property|raise|resourcestring|threadvar|try|dispose|exit|false|new|true|absolute|abstract|alias|assembler|cdecl|cppdecl|default|export|external|far|far16|forward|index|local|name|near|nostackframe|oldfpccall|override|pascal|private|protected|public|published|read|register|reintroduce|safecall|softfloat|stdcall|virtual|write)\b",
                    RegexOptions.IgnoreCase | RegexCompiledOption);
            PascalOperatorRegex = new Regex(@"(;|,|\[|\]|\+|\-|\=|\:|\(|\)|\*|\\|\>|\<|\.|\,)", RegexOptions.Multiline | RegexCompiledOption);
            
        }
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
    }
}
