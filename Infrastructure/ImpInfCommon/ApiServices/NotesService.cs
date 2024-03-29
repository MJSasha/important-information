﻿using ImpInfCommon.Data.Models;
using ImpInfCommon.Interfaces;
using System.Net.Http;

namespace ImpInfCommon.ApiServices
{
    public class NotesService : BaseCRUDService<Note, int>, INotesService
    {
        public NotesService(HttpClient httpClient, IErrorsHandler errorsHandler) : base(httpClient, errorsHandler) { }
    }
}
