using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NekoPlus.Ivory.Lexical
{
    public class Scanner
    {
        Analyzer _analyzer;
        public Analyzer Analyzer => _analyzer;
        public Scanner(Analyzer analyzer)
        {
            _analyzer = analyzer;
        }

        StringBuilder buffer=new StringBuilder();

        int index=-1;
        int length=0;

        public void Read(string s)
        {
            buffer.Append(s);
        }

        public Token Scan()
        {
            if (!Next())
                return null;
            return null;
        }

        public Token String()
        {
            if (!Next())
                return null;
            if(Now()!='\'')
            {
                Back();
                return null;
            }
            while (Next())
            {
                if (Now() == '\'')
                    return Generate(TokenType.String);
            }
            return Error(1001);
        }

        public Token Number()
        {
            if (!Next())
                return null;
            if (!char.IsDigit(Now()))
            {
                Back();
                return null;
            }
            if (Now() == '0')
            {
                if (!Next())
                    return Generate(TokenType.Number);
                if (Now() == 'x' || Now()=='X')
                {
                    if (!Next()||!IsHexChar())
                    {
                        return Error(1000);
                    }
                    while(Next())
                    {
                        if(!IsHexChar())
                        {
                            Back();
                            break;
                        }
                    }
                    return Generate(TokenType.Number);
                }
                if (Now()=='.')
                {
                    if(!Next())
                    {
                        return Error(1000);
                    }
                    if (!char.IsDigit(Now()))
                    {
                        Back();
                        return Generate(TokenType.Number);
                    }
                    while (Next())
                    {
                        if (!char.IsDigit(Now()))
                        {
                            Back();
                            break;
                        }
                    }
                    return Generate(TokenType.Number);
                }
                return Error(1000);
            }
            while (Next())
            {
                if (!char.IsDigit(Now()))
                {
                    if (Now() == '.')
                    {
                        if (!Next())
                        {
                            return Error(1000);
                        }
                        if (!char.IsDigit(Now()))
                        {
                            Back();
                            return Generate(TokenType.Number);
                        }
                        while (Next())
                        {
                            if (!char.IsDigit(Now()))
                            {
                                Back();
                                break;
                            }
                        }
                        return Generate(TokenType.Number);
                    }
                    Back();
                    break;
                }
            }
            return Generate(TokenType.Number);
        }

        Token Generate(TokenType type)
        {
            Token token = new Token(type, buffer.ToString(index - length + 1, length));
            length = 0;
            return token;
        }

        Token Error(int code)
        {
            _analyzer.Report(code);
            return null;
        }

        bool IsHexChar()
        {
            char c = Now();
            if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f'))
                return true;
            return false;
        }

        char Now()
        {
            return buffer[index];
        }

        bool Next()
        {
            if (index> buffer.Length-2)
                return false;
            index++;
            length++;
            return true;
        }

        bool Back()
        {
            if (index < 0)
                return false;
            index--;
            length--;
            return true;
        }
    }
}
