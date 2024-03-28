using System;
using System.Collections.Generic;
using System.Text;

namespace NekoPlus.Ivory.Lexical
{
    public enum TokenType
    {
        LineBreak,
        Operator,
        Identifier,
        Keyword,
        Number,
        String,
    }
    public class Token
    {
        public Token(TokenType type,string lexeme)
        {
            _type = type;
            _lexeme = lexeme;
        }

        readonly TokenType _type;
        public TokenType Type => _type;

        readonly string _lexeme;
        public string Lexeme => _lexeme;
    }
}