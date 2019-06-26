using System.Collections.Generic;

namespace NSI.DocumentGenerator.Interfaces
{
    public interface IOdtGenerator
    {
        byte[] GenerateOdtFromHtml(string content);
        byte[] Generate(List<string> list, string fileName, string content);

    }
}
