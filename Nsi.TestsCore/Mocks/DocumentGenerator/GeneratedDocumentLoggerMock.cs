using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NSI.DocumentGenerator.Interfaces.Helpers;

namespace Nsi.TestsCore.Mocks.DocumentGenerator
{
    public static class GeneratedDocumentLoggerMock
    {
        public static Mock<IGeneratedDocumentLogger> GetGeneratedDOcumentLogger()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var generatedDocumentLogger = new Mock<IGeneratedDocumentLogger> { CallBase = false };

            return generatedDocumentLogger;
        }
    }
}
