using System.Reflection;
using System.Threading.Tasks;
using CLAP;

namespace Icm.TagManager.CommandLine
{
    internal class AsyncMethodInvoker : IMethodInvoker
    {
        public void Invoke(MethodInfo method, object obj, object[] parameters)
        {
            (method.Invoke(obj, parameters) as Task)?.Wait();
        }
    }
}