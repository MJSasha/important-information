using ImpInfCommon.Data.Models;
using System.Net.Http;

namespace ImpInfCommon.ApiServices
{
    public class NotesService : BaseCRUDService<Note, int>
    {
        public NotesService(string backRoot, HttpClient httpClient) : base(backRoot, httpClient) { }
    }
}
