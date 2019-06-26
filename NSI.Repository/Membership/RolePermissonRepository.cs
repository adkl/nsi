using NSI.Domain.Membership;
using NSI.EF;
using NSI.Repository.Extensions;
using NSI.Repository.Interfaces.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Membership
{
    public class RolePermissonRepository : IRolePermissionRepository
    {
        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public RolePermissonRepository(NsiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds new rolePermission to the database
        /// </summary>
        /// <param name="rolePermission">RolePermission information to be added. Instance of <see cref="RolePermissionDomain"/></param>
        /// <returns>RolePermissionID of the newly created rolePermission</returns>
        public int Add(RolePermissionDomain rolePermission)
        {
            var rolePermissionDb = new RolePermission().FromDomainModel(rolePermission);
            _context.RolePermission.Add(rolePermissionDb);
            _context.SaveChanges();
            return rolePermissionDb.RolePermissionId;
        }

        /// <summary>
        /// Delete rolePermissions with provided permissionId
        /// </summary>
        /// <param name="permissionId"></param>
        public void DeleteByPermissionId(int permissionId)
        {
            var rolesPermissionDb = _context.RolePermission.Where(x => x.PermissionId == permissionId);
            foreach(RolePermission rp in rolesPermissionDb)
            {
                if (rp != null)
                {
                    _context.RolePermission.Remove(rp);
                }
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// Delete rolePermissions with provided roleId
        /// </summary>
        /// <param name="roleId"></param>
        public void DeleteByRoleId(int roleId)
        {
            var rolePermissionsDb = _context.RolePermission.Where(x => x.RoleId == roleId);
            foreach(RolePermission rp in rolePermissionsDb)
            {
                if (rp != null)
                {
                    _context.RolePermission.Remove(rp);
                }
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// Retrieves all rolePermission from the database
        /// </summary>
        /// <returns><see cref="ICollection{RolePermissionDomain}"/></returns>
        public ICollection<RolePermissionDomain> GetAll()
        {
            var result = _context.RolePermission
                //.Include(x => x.Role)
                //.Include(x => x.Permission)
                .ToList();

            return result.Select(x => x.ToDomainModel()).ToList();
        }

        /// <summary>
        /// Retrieves rolePermission with provided ID
        /// </summary>
        /// <param name="rolePermissionId">RolePermission ID</param>
        /// <returns>RolePermission if it exists, instance of <see cref="RolePermissionDomain"/>. Else null.</returns>
        public RolePermissionDomain GetById(int rolePermissionId)
        {
            return _context.RolePermission.FirstOrDefault(x => x.RolePermissionId == rolePermissionId).ToDomainModel();
        }

        /// <summary>
        /// Update RolePermission
        /// </summary>
        /// <param name="rolePermission"></param>
        /// <returns></returns>
        public void Update(RolePermissionDomain rolePermission)
        {
            var rolePermissionDb = _context.RolePermission.FirstOrDefault(x => x.RolePermissionId == rolePermission.Id);
            rolePermissionDb.FromDomainModel(rolePermission);
            _context.SaveChanges();
        }
    }
}
