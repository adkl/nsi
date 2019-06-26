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
    public class RoleMemberRepository : IRoleMemberRepository
    {
        /// <summary>
        /// Context, instance of <see cref="NsiContext"/>
        /// </summary>
        private readonly NsiContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Instance of <see cref="NsiContext"/></param>
        public RoleMemberRepository(NsiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds new roleMmber to the database
        /// </summary>
        /// <param name="roleMember">RoleMmber information to be added. Instance of <see cref="RoleMemberDomain"/></param>
        /// <returns>RoleMemberID of the newly created module</returns>
        public int Add(RoleMemberDomain roleMember)
        {
            var roleMemberDb = new RoleMember().FromDomainModel(roleMember);
            _context.RoleMember.Add(roleMemberDb);
            _context.SaveChanges();
            return roleMemberDb.RoleMemberId;
        }

        /// <summary>
        /// Delete roleMember with provided user Id
        /// </summary>
        /// <param name="userId"></param>
        public void DeleteByUserId(int userId)
        {
            var rolesMemberDb = _context.RoleMember.Where(x => x.UserInfoId == userId);
            foreach(RoleMember rm in rolesMemberDb)
            {
                if (rm != null)
                {
                    _context.RoleMember.Remove(rm);
                }
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// Retrieves all roleMembers from the database
        /// </summary>
        /// <returns><see cref="ICollection{RoleMemberDomain}"/></returns>
        public ICollection<RoleMemberDomain> GetAll()
        {
            var result = _context.RoleMember.ToList();

            return result.Select(x => x.ToDomainModel()).ToList();
        }

        /// <summary>
        /// Retrieves roleMember with provided ID
        /// </summary>
        /// <param name="roleMemberId">RoleMember ID</param>
        /// <returns>RoleMember if it exists, instance of <see cref="RoleMemberDomain"/>. Else null.</returns>
        public RoleMemberDomain GetById(int roleMemberId)
        {
            return _context.RoleMember.FirstOrDefault(x => x.RoleMemberId == roleMemberId).ToDomainModel();
        }

        /// <summary>
        /// Update Role Member
        /// </summary>
        /// <param name="roleMember"></param>
        /// <returns></returns>
        public void Update(RoleMemberDomain roleMember)
        {
            var roleMemberDb = _context.RoleMember.FirstOrDefault(x => x.RoleMemberId == roleMember.Id);
            roleMemberDb.FromDomainModel(roleMember);
            _context.SaveChanges();
        }
        

    }
}
