using Ninject;
using NSI.BusinessLogic.Interfaces.Membership;
using NSI.BusinessLogic.Membership;
using NSI.EF;
using NSI.Repository.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace NSI.Api.Authorization
{
    public class NsiAuthorizationAttribute : AuthorizeAttribute
    {
        
        public NsiAuthorizationAttribute()
        {
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            try
            {
                var logAdapter = new Logger.Implementations.NLogAdapter();
                logAdapter.LogDebug($"Successfully authenticated as: {actionContext.RequestContext.Principal.Identity.Name}");
                var name = actionContext.RequestContext.Principal.Identity.Name.Split('#').Last();
                if (string.IsNullOrWhiteSpace(name))
                {
                    return false;
                }

                using (var dbContext = new NsiContext())
                {
                    var userRepository = new UserRepository(dbContext);
                    var user = userRepository.GetUserByEmail(name);
                    if (user == null)
                    {
                        return false;
                    }

                    // Attach details to context
                    actionContext.Request.Properties["UserId"] = user.Id;
                    actionContext.Request.Properties["UserDetails"] = user;
                    actionContext.Request.Properties["UserTenantId"] = user.TenantId;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}