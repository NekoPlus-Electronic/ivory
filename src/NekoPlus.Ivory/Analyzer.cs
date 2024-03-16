using NekoPlus.Ivory.Lexical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NekoPlus.Ivory
{
    public class Analyzer
    {
        public Scanner Scanner { get; set; }

        public Analyzer()
        {
            Scanner = new Scanner(this);
        }

        public void Report(int code)
        {
            Error error = Error.Errors.First(x => x.Code == code)??new Error(code,ErrorLevel.Error,"");
            Console.WriteLine($"{error.Code} {error.Message}");
        }
    }
}
