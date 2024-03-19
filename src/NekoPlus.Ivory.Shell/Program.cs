using NekoPlus.Ivory.Lexical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NekoPlus.Ivory.Shell
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Analyzer analyzer = new Analyzer();
            analyzer.Scanner.Read("     6554.1    \'45744srysg\'    ");
            Token token = analyzer.Scanner.Number();
            if(token!=null)
                Console.WriteLine(token.Lexeme);
            else
                Console.WriteLine("null");
            token = analyzer.Scanner.String();
            if (token != null)
                Console.WriteLine(token.Lexeme);
            else
                Console.WriteLine("null");
            Console.Write(":");
            Console.ReadKey();
        }
    }
}
