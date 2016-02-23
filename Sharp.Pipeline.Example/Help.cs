using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Sharp.Pipeline.Example
{
    public static class ConsoleHelp
    {
        public static void Help(IEnumerable<MethodInfo> methods)
        {
            Console.WriteLine("Valid Commands");
            foreach (var m in methods)
            {
                var attribs = (DescriptionAttribute[])m.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attribs != null && attribs.Length > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(m.Name);
                    ParameterInfo[] parm = m.GetParameters();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("(");
                    for (int i = 0; i < parm.Length; i++)
                    {
                        if (i > 0)
                            Console.Write(", ");

                        Console.Write("({0}){1}", parm[i].ParameterType.Name, parm[i].Name);
                    }
                    Console.Write(")");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\n\t{0}", attribs[0].Description);
                }
            }
        }
    }
}
