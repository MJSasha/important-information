using ImpInfCommon.Data.Models;
using Microsoft.AspNetCore.Components;

namespace ImpInfFrontCommon
{
    public partial class App
    {
        [CascadingParameter]
        public User CurrentUser { get; set; }
    }
}
