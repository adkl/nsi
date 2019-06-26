using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NSI.Common.Exceptions;
using NSI.Common.Resources.Document;
using System;
using System.Data;
using System.Linq;
namespace NSI.DocumentGenerator.Implementations.Generators
{
    public class DataTableGenerator
    {
        protected DataTableGenerator()
        {
        }

        public static DataTable GenerateDataTableFromJson(string content)
        {
            try
            {
                if (string.IsNullOrEmpty(content))
                {
                    throw new NsiArgumentNullException(DocumentMessages.DocumentContentNotFound);
                }
                var jsonLinq = JObject.Parse(content);
                // Find the first array using Linq
                var linqArray = jsonLinq.Descendants().First(x => x is JArray);
                var jsonArray = new JArray();
                foreach (JObject row in linqArray.Children<JObject>())
                {
                    var createRow = new JObject();
                    foreach (JProperty column in row.Properties())
                    {
                        // Only include JValue types
                        if (column.Value is JValue)
                        {
                            createRow.Add(column.Name, column.Value);
                        }
                    }
                    jsonArray.Add(createRow);
                }
                return JsonConvert.DeserializeObject<DataTable>(jsonArray.ToString());
            }
            catch (Exception e)
            {
                throw new NsiJsonParsingException(DocumentMessages.JsonParsingFailed, e, Common.Enumerations.SeverityEnum.Error);
            }
        }
    }
}
