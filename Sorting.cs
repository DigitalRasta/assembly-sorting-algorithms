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

        public Sorting() {
            var DLL = Assembly.LoadFile(@"C:\Users\Jakubek\workspaces\JA\sortingProject\csharpLib\bin\Debug\csharpLib.dll");
            var type = DLL.GetType("csharpLib.Sorting");
            csharp_sortingObject = Activator.CreateInstance(type);
            asm_testDll();
        }
        public unsafe void cs_bubble(int* pointer, int length)
        {
            var method = csharp_sortingObject.GetType().GetMethod("bubble");
            IntPtr packedPointer = new IntPtr(pointer);
            method.Invoke(csharp_sortingObject, new object[] {packedPointer, length });
        }

        public unsafe void cs_insert(int* pointer, int length)
        {
            var method = csharp_sortingObject.GetType().GetMethod("insert");
            IntPtr packedPointer = new IntPtr(pointer);
            method.Invoke(csharp_sortingObject, new object[] { packedPointer, length});
        }

        public unsafe void cs_quick(int* pointer, int length)
        {
            var method = csharp_sortingObject.GetType().GetMethod("quick");
            method.Invoke(csharp_sortingObject, new object[] { });
        }

        [DllImport(@"asmLib.dll")]
        public unsafe static extern void asm_bubble(int* pointer, int length);

        [DllImport(@"asmLib.dll")]
        public unsafe static extern void asm_insert(int* pointer, int length);

        [DllImport(@"asmLib.dll")]
        public unsafe static extern void asm_quick(int* pointer, int length);

        [DllImport(@"asmLib.dll")]
        public unsafe static extern void asm_testDll();
    }
}
