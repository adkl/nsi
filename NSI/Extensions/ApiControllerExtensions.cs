using NSI.Common.Exceptions;
using NSI.Domain.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace NSI.Api.Extensions
{
    public static class ApiControllerExtensions
    {
        public static UserDomain GetUserDetails(this ApiController apiController)
        {
            apiController.Request.Properties.TryGetValue("UserDetails", out object userDetails);

            if (userDetails == null)
            {
                throw new NsiArgumentException("userDetails");
            }

            return (UserDomain)userDetails;
        }

        public static int GetUserId(this ApiController apiController)
        {
            if (apiController.Request.Properties.TryGetValue("UserId", out object userId))
            {
                return (int)userId;
            }

            throw new NsiArgumentException("userId");
        }

        public static int GetUserTenantId(this ApiController apiController)
        {
            if (apiController.Request.Properties.TryGetValue("UserTenantId", out object userTenantId))
            {
                return (int)userTenantId;
            }

            throw new NsiArgumentException("userTenantId");
        }
    }
}