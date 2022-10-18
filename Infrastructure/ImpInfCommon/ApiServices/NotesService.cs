using ImpInfCommon.Data.Models;
using ImpInfCommon.Interfaces;

namespace ImpInfCommon.ApiServices
{
    public class NotesService : BaseCRUDService<Note, int>
    {
        public NotesService(string backRoot, ITokenProvider tokenProvider) : base(backRoot, tokenProvider) { }
    }
}
