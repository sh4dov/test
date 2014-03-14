using System.Linq;
using System.Reflection;

namespace ConsoleApplication2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsClass);
        }
    }
}