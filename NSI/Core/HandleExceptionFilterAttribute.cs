using NSI.Common.Exceptions;
using NSI.DataContracts.Base;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using NSI.Common.Resources;
using NSI.Logger.Interfaces;

namespace NSI.Api.Core
{
    /// <summary>
    /// Handles exceptions.
    /// </summary>
    public class HandleExceptionFilterAttribute : ExceptionFilterAttribute, IExceptionFilter
    {
        private readonly ILoggerAdapter _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Instance of <see cref="ILoggerAdapter"/></param>
        public HandleExceptionFilterAttribute(ILoggerAdapter logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Handles exceptions accross the application and returns correct Http Status Code if possible
        /// </summary>
        /// <param name="actionExecutedContext">Instance of <see cref="HttpActionExecutedContext"/></param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            //TODO: Assign correct status codes to all custom exceptions
            if (actionExecutedContext.Exception is NsiBaseException customException)
            {
                _logger.LogException(customException, actionExecutedContext.Request, customException.Severity);

                HttpStatusCode statusCode = HttpStatusCode.BadRequest;

                if (customException is NsiNotAuthorizedException)
                {
                    statusCode = HttpStatusCode.Unauthorized;
                }
                else if (customException is NsiNotFoundException)
                {
                    statusCode = HttpStatusCode.NotFound;
                }
                else if(customException is NsiArgumentNullException || customException is NsiArgumentException)
                {
                    statusCode = HttpStatusCode.BadRequest;
                }

                CreateResponseOnException(actionExecutedContext, statusCode, customException.Message);
            }
            else
            {
                CreateResponseOnException(actionExecutedContext, HttpStatusCode.InternalServerError, ExceptionMessages.UnhandledExceptionMessage);
                _logger.LogException(actionExecutedContext.Exception, actionExecutedContext.Request, Common.Enumerations.SeverityEnum.Error);
            }
        }

        private void CreateResponseOnException(HttpActionExecutedContext context, HttpStatusCode statusCode, string message)
        {
            context.Response = context.Request.CreateResponse(statusCode, new BaseResponse<string>()
            {
                Message = message,
                Success = Common.Enumerations.ResponseStatus.Failed
            });
        }
    }
}