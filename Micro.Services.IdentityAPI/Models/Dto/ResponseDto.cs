﻿using System.Diagnostics.Contracts;

namespace Micro.Services.IdentityAPI.Models.Dto
{
    public class ResponseDto
    {
        public bool IsSuccess { get; set; } = true;
        public object Result { get; set; }
        public string Message { get; set; } = "";
    }
}