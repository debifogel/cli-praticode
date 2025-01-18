using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cli_praktikod
{
    
    internal class WriteTofile
    {
        public static void WriteDetailes(string name, StreamWriter file)
        {
            file.WriteLine(name);
        }

        public static void Write(string path, StreamWriter file)
        {
            string content=File.ReadAllText(path);
            file.WriteLine(content );
        }
        public static void WriteWithoutEmpty(string path, StreamWriter file)
        {
            string[] lines = File.ReadAllLines(path);
            var nonEmptyLines = lines.Where(line => !string.IsNullOrWhiteSpace(line));
            foreach (var line in nonEmptyLines)
            {
                file.WriteLine(line);
            }
        }
        private static bool goodDirectory(string name)
        {
            string[] arr = { "bin", "node_modules", "obj",".angular",".vscode","public","x64" };
            foreach (var line in arr)
            {
                if (name.Contains(line)) {  return false; }
            }
            return true;
        }
       public  static List<string> GetFilesByExtension(string directory, string extension)
        {
            List<string> fileList = new List<string>();

            try
            {
                // Get files in the current directory with the specified extension
                fileList.AddRange(Directory.GetFiles(directory, "*" + extension));

                // Get all subdirectories
                foreach (var dir in Directory.GetDirectories(directory))
                {
                    if (goodDirectory(dir))
                    {

                        // Recursively get files in subdirectories with the specified extension

                        fileList.AddRange(GetFilesByExtension(dir, extension));

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return FilterFiles(fileList);
        }
        private static bool isNot(string item)
        {
            //check what to do with the exe files
            string[] s = { 
            ".cs",
            ".py",
            ".html",
            ".ts",
            ".tsx",
            ".js",
            ".css"
            ,".ipynb" ,
             ".cpp",
             ".h"};

            foreach (var f in s)
            {
                if (item.EndsWith(f))
                    return true;
            }
            return false;
        }
        public static List<string>FilterFiles(List<string> files)=> files.Where(item => isNot(item)).ToList();

       public  static List<string> SortList(List<string> items, Func<string, string, int> comparisonFunc)
        {
            List<string> sortedList = new List<string>(items);
            sortedList.Sort(new Comparison<string>(comparisonFunc));
            return sortedList;
        }
       public static void CopiesFiles(List<string> files, StreamWriter file,Action<string,StreamWriter>CopyFunc)
       {
            foreach (var item in files)
            {
                CopyFunc(item, file);
            }
            file.Close();
       }
        public static void AllCopy(string directory, StreamWriter file
            , Action<string,StreamWriter>CopyFunc, Func<string, string, int> comparisonFunc, string end = ".*")
        {
            List<string>list= GetFilesByExtension(directory,end);
            list= SortList(list,comparisonFunc);

            CopiesFiles(list, file, CopyFunc);
        }

    }
}

