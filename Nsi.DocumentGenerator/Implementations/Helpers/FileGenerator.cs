using NSI.Common.Exceptions;
using NSI.Common.Resources;
using NSI.Common.Resources.Document;
using NSI.DocumentGenerator.Interfaces.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NSI.DocumentGenerator.Implementations.Helpers
{
    public class FileGenerator : IFileGenerator
    {
        private static Random random = new Random();
        private readonly String workingDir;
        public FileGenerator()
        {
            workingDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\";
        }

        public string GenerateRandomFileName() {

            // Generate random alphanumerical string for the fileName
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string fileName = new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            return fileName;
        }
        public Boolean RemoveLocalFiles(String fileName, List<String> extension) {
            try { 
                foreach (string documentExtenstion in extension)
                {
                    string fileToDelete = Path.Combine(workingDir, fileName + documentExtenstion);
                    if (File.Exists(fileToDelete))
                    {
                        File.Delete(fileToDelete);
                    }
                }
                return true;
            }
            catch (Exception e) {
                throw new NsiBaseException(ExceptionMessages.UnhandledExceptionMessage, e, Common.Enumerations.SeverityEnum.Error);
            }
        }
    }
}
