using cli_praktikod;
using System;
using System.CommandLine;

int SortName(string a, string b) { return a.CompareTo(b); }
//implements this function
int SortSoog(string a, string b) { 
    string s = a.Substring(a.IndexOf('.') + 1);
    string s2 = b.Substring(b.IndexOf('.') + 1);
    return s.CompareTo(s2);
}

#region option command bundle


var rootComand = new RootCommand();
rootComand.Name = ("root command for file");
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
            ".css");
bundleOptionLanguge.AddAlias("-l");
var bundleOptionSourseKod = new Option<bool> ("--source",()=>true ," write with source kod");
bundleOptionSourseKod.AddAlias("-sk");
var bundleOptionSort = new Option<string> ("--sort",()=>"ab", "how to sort").FromAmong("ab","writer");
bundleOptionSort.AddAlias("-sr");
var bundleOptionRemoveEmpty = new Option<bool> ("--remove-empty",()=>true, "remove empty lines");
bundleOptionRemoveEmpty.AddAlias("-rmempty");
var bundleOptionWriterName = new Option<string> ("--name", "write the name of writer");
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
var bundleComandtest = new Command("bundletest", "bundle code to do a one file test");
bundleComandtest.SetHandler(() =>
{
    try
    {
        File.Create("C:\\Users\\User\\Documents\\learns\\praktiKod\\cli-praktikod\\test.txt");

        WriteTofile.AllCopy(Directory.GetCurrentDirectory(), "test.txt",
                     WriteTofile.WriteWithoutEmpty,
                    SortName,
                    ".*");
    }
    catch (Exception e) { Console.WriteLine("Eror:the path is not valid"); }
});

string[] currentDirectoryContext=Directory.GetDirectories(Directory.GetCurrentDirectory());
bundleComand.SetHandler((openFile,language,sourceKod,sort,removeEmpty,name)=>
{
    try
    {
        File.Create(bundleOptionOpenFile.Name);
        StreamWriter file = new StreamWriter(bundleOptionOpenFile.Name);
        if (name != "")
        { WriteTofile.WriteDetailes(name, file); }
        if (sourceKod)
        { WriteTofile.WriteDetailes("//"+Environment.ProcessPath, file); }
        file.Close();
        WriteTofile.AllCopy(Directory.GetCurrentDirectory(), bundleOptionOpenFile.Name,
            removeEmpty ? WriteTofile.WriteWithoutEmpty : WriteTofile.Write,
            sort == "ab" ? SortName : SortSoog,
            language == "all" ? ".*" : language);
    }
    
    catch (Exception e) { Console.WriteLine("Eror:the path is not valid"); }
},bundleOptionOpenFile, bundleOptionLanguge,bundleOptionSourseKod,bundleOptionSort,bundleOptionRemoveEmpty,bundleOptionWriterName);
rootComand.AddOption(bundleOptionOpenFile);

rootComand.AddCommand(bundleComand);
rootComand.AddCommand(bundleComandtest);
rootComand.InvokeAsync(args);
