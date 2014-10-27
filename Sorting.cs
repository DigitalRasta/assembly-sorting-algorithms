using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace sortingProject
{
    class Sorting
    {
        //MethodInfo sortingMethod;
        Object csharp_sortingObject;

        /*[DllImport(@"C:\Users\Jakubek\workspaces\JA\sortingProject\csharpLib\bin\Debug\csharpLib.dll")]
        public static extern void asm_bubble(String msg); */
        public Sorting() {
            var DLL = Assembly.LoadFile(@"C:\Users\Jakubek\workspaces\JA\sortingProject\csharpLib\bin\Debug\csharpLib.dll");
            var type = DLL.GetType("csharpLib.Sorting");
            csharp_sortingObject = Activator.CreateInstance(type);
        }
        public void cs_bubble(String msg)
        {
            var method = csharp_sortingObject.GetType().GetMethod("bubble");
            method.Invoke(csharp_sortingObject, new object[] { msg});
        }
    }
}
