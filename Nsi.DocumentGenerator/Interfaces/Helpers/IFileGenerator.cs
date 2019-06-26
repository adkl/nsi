using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NSI.DocumentGenerator.Interfaces.Helpers
{
    public interface IFileGenerator
    {
        string GenerateRandomFileName();
        Boolean RemoveLocalFiles(string fileName, List<string> extension);
    }
}
