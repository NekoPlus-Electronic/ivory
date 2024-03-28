﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NekoPlus.Ivory.Lexical
{
    public class Scanner
    {
        StringBuilder buffer = new StringBuilder();

        int index = -1;
        int length = 0;

        Analyzer _analyzer;
        public Analyzer Analyzer => _analyzer;

        public char Now => buffer[index];

        public Scanner(Analyzer analyzer)
        {
            _analyzer = analyzer;
        }

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

        public Token LineBreak()
        {
            Trim();
            if (!Next())
                return null;
            if(Now!='\r'&&Now!='\n')
            {
                Back();
                return null;
            }
            while(Next())
            {
                if (Now != '\r' && Now != '\n')
                {
                    Back();
                    break;
                }
            }
            return Generate(TokenType.LineBreak);
        }

        public Token String()
        {
            Trim();
            if (!Next())
                return null;
            if(Now!='\'')
            {
                Back();
                return null;
            }
            while (Next())
            {
                if (Now == '\'')
                    return Generate(TokenType.String);
            }
            return Error(1001);
        }

        public Token Number()
        {
            Trim();
            if (!Next())
                return null;
            if (!IsDigitChar())
            {
                Back();
                return null;
            }
            if (Now == '0')
            {
                if (!Next())
                    return Generate(TokenType.Number);
                if (Now == 'x' || Now=='X')
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
                if (Now=='.')
                {
                    if(!Next())
                    {
                        return Error(1000);
                    }
                    if (!IsDigitChar())
                    {
                        Back();
                        return Generate(TokenType.Number);
                    }
                    while (Next())
                    {
                        if (!IsDigitChar())
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
                if (!IsDigitChar())
                {
                    if (Now == '.')
                    {
                        if (!Next())
                        {
                            return Error(1000);
                        }
                        if (!IsDigitChar())
                        {
                            Back();
                            return Generate(TokenType.Number);
                        }
                        while (Next())
                        {
                            if (!IsDigitChar())
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

        bool IsDigitChar()
        {
            char c = Now;
            if (c >= '0' && c <= '9')
                return true;
            return false;
        }

        bool IsHexChar()
        {
            char c = Now;
            if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f'))
                return true;
            return false;
        }

        void Trim()
        {
            while(Next(true))
            {
                char c = Now;
                if (!char.IsWhiteSpace(c) || c == '\r' || c == '\n')
                {
                    index--;
                    break;
                }
            }
        }

        bool Next(bool eat=false)
        {
            if (index> buffer.Length-2)
                return false;
            index++;
            if (!eat)
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