namespace NekoPlus.Ivory.Analysis
{
    public enum ProblemType
    {
        Error,
        Warning,
        Tip,
    }

    public class Problem
    {
        public Problem(int code, ProblemType type, string message)
        {
            _code = code;
            _type = type;
            _message = message;
        }

        private readonly int _code;
        public int Code => _code;

        private ProblemType _type;
        public ProblemType Type { get => _type; set => _type = value; }

        private string _message;
        public string Message { get => _message; set => _message = value; }

        private static readonly Problem[] problems =
        {
            new Problem(1000,ProblemType.Error,"无效数字") ,
            new Problem(1001,ProblemType.Error,"未终止的字符串字面量"),
        };
        public static Problem[] Problems => problems;
    }
}