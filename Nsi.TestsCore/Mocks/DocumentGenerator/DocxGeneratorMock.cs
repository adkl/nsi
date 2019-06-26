using Moq;
using NSI.DocumentGenerator.Interfaces.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsi.TestsCore.Mocks.DocumentGenerator
{
    public static class DocxGeneratorMock
    {
        public static Mock<IDocxGenerator> GetDocxGeneratorMock()
        {
            var docxGenerator = new Mock<IDocxGenerator> { CallBase = true };

            docxGenerator.Setup(x => x.Generate(It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new byte[100]);

            return docxGenerator;
        }
    }
}
