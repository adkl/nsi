using System.Data;
namespace NSI.DocumentGenerator.Interfaces.Generators
{
    public interface IDataTableGenerator
    {
        DataTable GenerateDataTableFromJson(string contfent);
    }
}
