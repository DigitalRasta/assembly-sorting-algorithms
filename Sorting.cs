using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace sortingProject
{

    /*
    * Description:  Class containing sorting methods loaded from DLLs
    * Author: Jakub'Digitalrasta'Bujny
    * Version: 0.2.1
    * Changelog:
    *      0.0.0: added c# methods
    *      0.1.0: added asm methods
    *      0.2.0: upgrade of c# dll loader
     *     0.2.1: fixed problem with path to c# dll
    */
    class Sorting
    {
        Object csharp_sortingObject;

        /*
         * Description: standard constructor. Load c# dll from exe directory. Test connection with ASM dll.
         */
        public Sorting() {
            String path = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).AbsolutePath;
            String[] splitPath = path.Split('/');
            String goodPath = path.Substring(0, path.Length - splitPath[splitPath.Length - 1].Length);
            var DLL = Assembly.LoadFile(goodPath+"csharpLib.dll");
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
            IntPtr packedPointer = new IntPtr(pointer);
            method.Invoke(csharp_sortingObject, new object[] { packedPointer, length });
        }

        [DllImport("asmLib.dll")]
        public unsafe static extern void asm_bubble(int* pointer, int length);

        [DllImport("asmLib.dll")]
        public unsafe static extern void asm_insert(int* pointer, int length);

        [DllImport("asmLib.dll")]
        public unsafe static extern void asm_quick(int* pointer, int length);

        [DllImport("asmLib.dll")]
        public unsafe static extern void asm_testDll();
    }
}
