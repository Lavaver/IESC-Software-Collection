using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Installation_Essentials_Software_Collection
{
    internal class DownloadAndInstall
    {
        static string TempFolder = "TempDownloadFolder";
        static string InstallFolder = $"InstalledSoftware\\{Guid.NewGuid().ToString("N")}"; 

        public static void DownloadAndInstallSoftware(string URL)
        {
            if (!Directory.Exists(TempFolder))
            {
                Directory.CreateDirectory(TempFolder);
            }

            if (!Directory.Exists(InstallFolder))
            {
                Directory.CreateDirectory(InstallFolder);
            }

            string fileName = Path.GetFileName(URL);
            string filePath = Path.Combine(TempFolder, fileName);

            if (!File.Exists(filePath))
            {
                using (var client = new System.Net.WebClient())
                {
                    client.DownloadFile(URL, filePath);
                }
            }

            
            string installCommand = $"/S /D={InstallFolder}";
            System.Diagnostics.Process.Start(filePath, installCommand);
        }
    }
}
