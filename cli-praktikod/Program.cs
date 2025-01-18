using cli_praktikod;
using System;
using System.CommandLine;
using System.Diagnostics.SymbolStore;

int SortName(string a, string b) { return a.CompareTo(b); }
//implements this function
int SortSoog(string a, string b) { 
    string s = a.Substring(a.IndexOf('.') + 1);
    string s2 = b.Substring(b.IndexOf('.') + 1);
    return s.CompareTo(s2);
}

#region option command bundle

var rootComand = new RootCommand("root command for file");
var bundleOptionOpenFile = new Option<FileInfo>("--output","file name and path");
bundleOptionOpenFile.AddAlias("-o");
var bundleOptionLanguge = new Option<string> ("--language", "which kode to copy") { IsRequired = true };
bundleOptionLanguge.FromAmong(
            ".cs",
            ".py",
            ".html",
            ".ts",
            ".tsx",
            ".js",
            ".css", ".cpp", ".h"
            , "all"
            );
bundleOptionLanguge.AddAlias("-l");
var bundleOptionSourseKod = new Option<bool> ("--source",()=>true ," write with source kod") { IsRequired = false };
bundleOptionSourseKod.AddAlias("-sk");
var bundleOptionSort = new Option<string> ("--sort",()=>"ab", "how to sort").FromAmong("ab","language","",null);
bundleOptionSort.IsRequired = false;
bundleOptionSort.AddAlias("-sr");
var bundleOptionRemoveEmpty = new Option<bool> ("--remove-empty",()=>true, "remove empty lines") { IsRequired = false };
bundleOptionRemoveEmpty.AddAlias("-rmempty");
var bundleOptionWriterName = new Option<string> ("--name", "write the name of writer") { IsRequired = false };
bundleOptionWriterName.AddAlias("-n");
#endregion
#region command bundle
var bundleComand = new Command("bundle", "bundle code to do a one file");
bundleComand.AddOption(bundleOptionWriterName);
bundleComand.AddOption (bundleOptionRemoveEmpty);
bundleComand.AddOption(bundleOptionSort);
bundleComand.AddOption(bundleOptionSourseKod);
bundleComand.AddOption(bundleOptionLanguge);
bundleComand.AddOption(bundleOptionOpenFile);
#endregion
#region command create rsp
var rspComand = new Command("create-rsp", "rsp for the command");
rspComand.SetHandler(
    () => {
        try
        {
            if (File.Exists("result.rsp"))

            {

                File.Delete("result.rsp");

            }
            StreamWriter file = new StreamWriter("result.rsp");
            file.Write("bundle ");
            string s;
            char c;
            Console.WriteLine(bundleOptionOpenFile.Name);
            s=Console.ReadLine();
            file.Write("-o "+s+" ");
            Console.WriteLine(bundleOptionLanguge.Name);
            s = Console.ReadLine();
            file.Write("-l " + s+" ");
            Console.WriteLine(bundleOptionWriterName.Name);
            s = Console.ReadLine();
            file.Write("-n " + s + " ");
            Console.WriteLine(bundleOptionSort.Name);
            s = Console.ReadLine();
            file.Write("-sr " + s + " ");
            Console.WriteLine($"{bundleOptionRemoveEmpty.Name} (y/n):");
            var key = Console.ReadKey(); // Wait for a key press
            Console.WriteLine(); // Move to the next line
            if (key.KeyChar == 'y')
                file.Write("-rmempty ");
            else if (key.KeyChar == 'n')
                file.Write("-rmempty " + "false" + " ");
            else
                throw new Exception("Invalid input. Please enter 'y' or 'n'.");
            Console.WriteLine($"{bundleOptionSourseKod.Name} (y/n):");
            key = Console.ReadKey();
            Console.WriteLine();
            if (key.KeyChar == 'y')
                file.Write("-sk ");
            else if (key.KeyChar == 'n')
                file.Write("-sk " + "false" + " ");
            else
                throw new Exception("Invalid input. Please enter 'y' or 'n'.");

            Console.WriteLine("the file result.rsp is ready run it");
            file.Close();

        }
        catch (Exception)
        {

            throw;
        }
        
        
       
    });

#endregion
bundleComand.SetHandler((openFile,language,sourceKod,sort,removeEmpty,name) =>
{

    try
    {
        StreamWriter file = new StreamWriter(openFile.Name);
        if (name != ""||name!=null)
        { WriteTofile.WriteDetailes("//"+name, file); }
        if (sourceKod)
        { WriteTofile.WriteDetailes("//"+Directory.GetCurrentDirectory(), file); }
        

        WriteTofile.AllCopy(Directory.GetCurrentDirectory(), file,
            removeEmpty ? WriteTofile.WriteWithoutEmpty : WriteTofile.Write,
            sort == "ab"||sort==null||sort=="" ? SortName : SortSoog,
            language == "all" ? ".*" : language);
        file.Close();
    }
    
    catch (Exception e) { Console.WriteLine("Eror:the path is not valid"); }
},bundleOptionOpenFile, bundleOptionLanguge,bundleOptionSourseKod,bundleOptionSort,bundleOptionRemoveEmpty,bundleOptionWriterName);
rootComand.AddCommand(bundleComand);
rootComand.AddCommand(rspComand);
await rootComand.InvokeAsync(args);
