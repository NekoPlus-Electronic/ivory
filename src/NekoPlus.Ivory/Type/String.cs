using System.Text;

namespace NekoPlus.Ivory.Type
{
    public class String:Object
    {
        static String empty = new String("");
        public static new String Zero => empty;

        byte[] content;
        public String(string raw)
        {
            content = Encoding.UTF8.GetBytes(raw);
        }

        public override string ToString()
        {
            return Encoding.UTF8.GetString(content);
        }
    }
}