using System.Linq;
using System.Reflection;

namespace ClassLibrary1
{
    public class Class1
    {
        public void Method()
        {
            Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsClass);
        }
    }

    public class Class2
    {
    }
}