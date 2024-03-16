using System;
using System.Collections.Generic;
using System.Text;

namespace NekoPlus.Ivory
{
    public enum ErrorLevel
    {
        Error,
        Warning
    }

    public class Error
    {
        public Error(int code,ErrorLevel level,string message)
        {
            _code = code;
            _level = level;
            _message = message;
        }

        readonly int _code;
        public int Code => _code;

        ErrorLevel _level;
        public ErrorLevel Level { get => _level; set=>_level = value; }

        string _message;
        public string Message { get => _message; set => _message = value; }

        static readonly List<Error> errors = new List<Error>
        {
            new Error(1000,ErrorLevel.Error,"无效数字") ,
            new Error(1001,ErrorLevel.Error,"未终止的字符串字面量"),
        };
        public static List<Error> Errors => errors;
    }
}
