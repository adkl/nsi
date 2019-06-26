using Moq;
using NSI.DocumentGenerator.Interfaces;
using NSI.DocumentGenerator.Interfaces.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsi.TestsCore.Mocks.DocumentGenerator
{
    public static class OdtGeneratorMock
    {
        public static Mock<IOdtGenerator> GetOdtGeneratorMock()
        {
            var odtGenerator = new Mock<IOdtGenerator> { CallBase = true };

            odtGenerator.Setup(x => x.Generate(It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new byte[100]);

            return odtGenerator;
        }
    }
}
