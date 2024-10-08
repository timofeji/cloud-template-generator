using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateGenerator
{
    public class Loader
    {
        private string _SnippetFilePath;

        public string SnippetFilePath { get => _SnippetFilePath; set => _SnippetFilePath = value; }
        public Loader(string SnippetFilePath = "")
        {
            this.SnippetFilePath = SnippetFilePath;
        }
        public List<LoaderFile> FetchTemplateFilesContent(string ResourceType)
        {
            List<LoaderFile> oList = new List<LoaderFile>();
                LoaderFile lf = new LoaderFile();
            string sOut = "";
            if (File.Exists(this.SnippetFilePath + ResourceType + "Resource.txt"))
            {
                sOut = File.ReadAllText(this.SnippetFilePath + ResourceType + "Resource.txt");
                lf = new LoaderFile();
                lf.FileType = "R";
                lf.JSON = sOut;
                oList.Add(lf);
            }
            if (File.Exists(this.SnippetFilePath + ResourceType + "Parameters.txt"))
            {
                sOut = File.ReadAllText(this.SnippetFilePath + ResourceType + "Parameters.txt");
                lf = new LoaderFile();
                lf.FileType = "P";
                lf.JSON = sOut;
                oList.Add(lf);
            }
            if (File.Exists(this.SnippetFilePath + ResourceType + "Vars.txt"))
            {
                sOut = File.ReadAllText(this.SnippetFilePath + ResourceType + "Vars.txt");
                lf = new LoaderFile();
                lf.FileType = "V";
                lf.JSON = sOut;
                oList.Add(lf);
            }
            if (File.Exists(this.SnippetFilePath + ResourceType + "Output.txt"))
            {
                sOut = File.ReadAllText(this.SnippetFilePath + ResourceType + "Output.txt");
                lf = new LoaderFile();
                lf.FileType = "O";
                lf.JSON = sOut;
                oList.Add(lf);
            }
            if (oList.Count == 0)
            {
                lf.JSON = "No files found for " + ResourceType;
                oList.Add(lf);
            }
            return (oList);
        }

        public List<string> FetchResourceTypes()
        {
            List<string> oList = new List<string>();
            string[] sFiles = Directory.GetFiles(this.SnippetFilePath);
            foreach (string sFullFileName in sFiles)
            {
                int i = sFullFileName.IndexOf("Resource.txt");
                if (i > 0)
                {
                    string sName = sFullFileName.Substring(0, i);
                    sName = sName.Replace(this.SnippetFilePath, "");
                    oList.Add(sName);
                }
            }
            return (oList);
        }

        public class LoaderFile
        {
            public string JSON = "";
            public string FileType = "";
        }
    }
}
