using NSI.Domain.DeviceManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.Repository.Extensions
{
    public static class ActionExtension
    {
        public static ActionDomain ToDomainModel(this EF.Action obj)
        {
            return obj == null ? null : new ActionDomain()
            {
                ActionId = obj.ActionId,
                Name = obj.Name,
                IsActive = obj.IsActive,
            };
        }

        public static EF.Action FromDomainModel(this EF.Action obj, ActionDomain domain)
        {
            if (obj == null)
            {
                obj = new EF.Action();
            }

            obj.ActionId = domain.ActionId;
            obj.Name = domain.Name;
            obj.IsActive = domain.IsActive;

            return obj;

        }
    
}
}
