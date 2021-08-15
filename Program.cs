using System;
using System.IO;

namespace forfile
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Not enought parameters\nforfile pattern [params]");
                Console.WriteLine("Use <FILE> and <FILENAME> to insert filename w/wo extension into the new name");
                return;
            }

            string[] files = System.IO.Directory.GetFiles(System.IO.Directory.GetCurrentDirectory(), args[0]);
            string arguments = "";
            for (int i = 2; i < args.Length; ++i)
                arguments += string.Format("\"{0}\" ", args[i]);
            foreach (string file in files)
            {
                string name = Path.GetFileName(file);
                string nameNoExtension = Path.GetFileNameWithoutExtension(file);
                System.Diagnostics.ProcessStartInfo start = new System.Diagnostics.ProcessStartInfo();
                start.FileName = args[1];
                start.Arguments = arguments.Replace("<FILE>", name).Replace("<FILENAME>", nameNoExtension);
                Console.WriteLine("Processing {0}", start.Arguments);
                start.UseShellExecute = false;
                start.RedirectStandardOutput = true;
                using (System.Diagnostics.Process process = System.Diagnostics.Process.Start(start))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string result = reader.ReadToEnd();
                        Console.Write(result);
                    }
                }
            }
        }
    }
}
