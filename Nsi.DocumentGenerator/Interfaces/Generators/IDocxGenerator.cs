using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DocumentGenerator.Interfaces.Generators
{
    public interface IDocxGenerator
    {
        byte[] GenerateDocxFromHtml(string content);
        byte[] Generate(List<string> list, string fileName, string content);
    }
}
