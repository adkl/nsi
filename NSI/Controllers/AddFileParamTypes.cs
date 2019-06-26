using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace NSI.Api.Controllers
{
    public class AddFileParamTypes: IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.operationId == "UploadFile")  // SwaggerOperation
            {
                operation.consumes.Add("multipart/form-data");
                operation.parameters = new List<Parameter>
                {
                    new Parameter
                    {
                        name = "file",
                        required = true,
                        type = "file",
                        @in = "formData",
                        vendorExtensions = new Dictionary<string, object> { {"x-ms-media-kind", "image" } }
                    }
                };
                operation.parameters.Add(new Parameter()
                {
                    name = "fileName",
                    @in = "query",
                    required = false,
                    type = "string"
                });
            }
        }
    }
}