using HitchFrontEnd.Models;
using HitchFrontEnd.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication;
using HitchFrontEnd.Services;
using Microsoft.AspNetCore.Authorization;

namespace HitchFrontEnd
{
    public class SD
    {
        public static string HitchFixBase { get; set; }
        public enum ApiType
        {
            Get,
            POST,
            PUT,
            Delete
        }
    }
}
