using NekoPlus.Ivory.Analysis;
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

        public List<Problem> Problems { get; } = new List<Problem>();

        public Analyzer()
        {
            Scanner = new Scanner(this);
        }

        public void Report(int code)
        {
            Problem problem = Problem.Problems.First(x => x.Code == code)??new Problem(code,ProblemType.Error,"");
            Console.WriteLine($"{problem.Code} {problem.Message}");
            Problems.Add(problem);
        }
    }
}
