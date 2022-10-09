using System.IO;

namespace ImpInfApi.Utils
{
    public static class UtilsFunctions
    {
        public static string GetInitiallQuery()
        {
            try
            {
                var query = File.ReadAllText("Resources/Data.sql");
                return query;
            }
            catch
            {
                return "";
            }
        }
    }
}
