using NekoPlus.Ivory.Lexical;
using System;
using System.Collections.Generic;
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
            string msg = "";
            if (code == 1013)
                msg = "无效数字";
            Console.WriteLine($"{code} {msg}");
        }
    }
}
