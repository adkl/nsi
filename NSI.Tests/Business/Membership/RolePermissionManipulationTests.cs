using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nsi.TestsCore.Mocks;
using NSI.BusinessLogic.Membership;
using NSI.Domain.Membership;
using NSI.Repository.Interfaces.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Tests.Business.Membership
{
    [TestClass]
    public class RolePermissionManipulationTests
    {
        private Mock<IRolePermissionRepository> _rolePermissionRepositoryMock;
        private Mock<IRoleRepository> _roleRepositoryMock;
        private Mock<IPermissionRepository> _permissionRepositoryMock;
        private RolePermissionManipulation _rolePermissionManipulation;

        public void Initialize()
        {
            _rolePermissionRepositoryMock = RolePermissionRepositoryMock.GetRolePermissionRepositoryMock();
            _roleRepositoryMock = RoleRepositoryMock.GetRoleRepositoryMock();
            _permissionRepositoryMock = PermissionRepositoryMock.GetPermissionRepositoryMock();
            _rolePermissionManipulation = new RolePermissionManipulation(_rolePermissionRepositoryMock.Object, 
                _roleRepositoryMock.Object, _permissionRepositoryMock.Object);
        }


    }
}
