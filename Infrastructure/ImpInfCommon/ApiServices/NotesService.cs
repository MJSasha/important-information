﻿using ImpInfCommon.Data.Models;
using ImpInfCommon.Interfaces;
using System.Net.Http;

namespace ImpInfCommon.ApiServices
{
    public class NotesService : BaseCRUDService<Note, int>, INotes
    {
        public NotesService(string backRoot, HttpClient httpClient) : base(backRoot, httpClient) { }
    }
}
