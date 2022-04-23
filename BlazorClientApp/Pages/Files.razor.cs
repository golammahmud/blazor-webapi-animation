using System.IO;
using BlazorInputFile;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;



namespace BlazorClientApp.Pages
{
    public class FilesBase:ComponentBase
    {
        [Inject]
       protected IWebHostEnvironment Environment { get; set; }
        protected List<IBrowserFile> loadedFiles { get; set; }

       protected IFileListEntry file { get; set; }
        protected string result { get; set; }

        protected string path { get; set; } = "wwwroot/files/txtfile.txt";

    public void HandleFileSelected(IFileListEntry[] files)
        {
            file = files.FirstOrDefault();

            if (File.Exists(path))
            {
                // Store each line in array of strings
                string[] lines = File.ReadAllLines(path);

                foreach (string ln in lines)
                    Console.WriteLine(ln);
            }
        }

    }
  
}
