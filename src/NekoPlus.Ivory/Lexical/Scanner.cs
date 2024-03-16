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


        public Token Number()
        {
            length = 0;
            if (!Next())
                return null;
            if (!char.IsDigit(buffer[index]))
            {
                Back();
                return null;
            }
            if (buffer[index] == '0')
            {
                if (!Next())
                    return Generate(TokenType.Number);
                if (buffer[index] == 'x' || buffer[index]=='X')
                {
                    if (!Next()||!IsHexChar())
                    {
                        _analyzer.Report(1013);
                        return null;
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
                if (buffer[index]=='.')
                {
                    if(!Next())
                    {
                        _analyzer.Report(1013);
                        return null;
                    }
                    if (!char.IsDigit(buffer[index]))
                    {
                        Back();
                        return Generate(TokenType.Number);
                    }
                    while (Next())
                    {
                        if (!char.IsDigit(buffer[index]))
                        {
                            Back();
                            break;
                        }
                    }
                    return Generate(TokenType.Number);
                }
                _analyzer.Report(1013);
                return null;
            }
            while (Next())
            {
                if (!char.IsDigit(buffer[index]))
                {
                    if (buffer[index] == '.')
                    {
                        if (!Next())
                        {
                            _analyzer.Report(1013);
                            return null;
                        }
                        if (!char.IsDigit(buffer[index]))
                        {
                            Back();
                            return Generate(TokenType.Number);
                        }
                        while (Next())
                        {
                            if (!char.IsDigit(buffer[index]))
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

        Token Generate(TokenType type) => new Token(type, buffer.ToString(index-length+1, length));

        bool IsHexChar()
        {
            char c = buffer[index];
            if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f'))
                return true;
            return false;
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
