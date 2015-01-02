using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowerSlides
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (args.Name.Contains("ImageResizer"))
            {
                // Looking for the Mydll.dll assembly, load it from our own embedded resource
                foreach (string res in Assembly.GetExecutingAssembly().GetManifestResourceNames())
                {
                    if (res.EndsWith("ImageResizer.dll"))
                    {
                        Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream(res);
                        byte[] buff = new byte[s.Length];
                        s.Read(buff, 0, buff.Length);
                        return Assembly.Load(buff);
                    }
                }
            }
            return null;
        }
    }
}
