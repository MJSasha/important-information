using ImpInfCommon.Data.Models;

namespace ImpInfCommon.ApiServices
{
    public class NotesService : BaseCRUDService<Note, int>
    {
        public NotesService(string backRoot, string token = "") : base(backRoot, token: token) { }
    }
}
