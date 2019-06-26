using Moq;
using NSI.Domain.Notifications;
using NSI.Repository.Interfaces.Notifications;
using System;
using System.Collections.Generic;

namespace Nsi.TestsCore.Mocks.Notifications
{
    public static class AttachmentRepositoryMock
    {

        public static Mock<IAttachmentRepository> GetAttachmentRepositoryMock()
        {
            // Always set CallBase to false, we don't want to really hit the DB
            var attachmentRepository = new Mock<IAttachmentRepository> { CallBase = false };

            byte[] myByteArray = getByteArray();
            DateTime dateCreated = DateTime.Now;


            attachmentRepository.Setup(x => x.Add(It.IsAny<AttachmentDomain>())).Returns(1);

            attachmentRepository.Setup(x => x.GetById(1)).Returns(
                new AttachmentDomain
                {
                    File = myByteArray,
                    Id = 1,
                    DateCreated = dateCreated
                });

            attachmentRepository.Setup(x => x.GetByFile(myByteArray)).Returns(
                new AttachmentDomain
                {
                    File = myByteArray,
                    Id = 1,
                    DateCreated = dateCreated
                });

            attachmentRepository.Setup(x => x.GetAll()).Returns(
                new List<AttachmentDomain>
                {
                    new AttachmentDomain
                    {
                        File = myByteArray,
                        Id = 1,
                        DateCreated = dateCreated
                    }
                });

            return attachmentRepository;
        }

        public static byte[] getByteArray()
        {
            string bytes = "38,05,e1,5f,aa,5f,aa,d0";
            string[] holder = bytes.Split(',');
            byte [] myByteArray = new byte[holder.Length];
            int i = 0;
            foreach (string item in holder)
            {
                myByteArray[i++] = byte.Parse(item, System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            return myByteArray;
        }
    }
}
