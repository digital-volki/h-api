﻿using Leifez.Application.Domain.Models;
using System.Collections.Generic;

namespace Leifez.Application.Service.Interfaces
{
    public interface IImageService
    {
        List<string> Add(IEnumerable<string> base64Images, string collectionId);
        List<Image> Get(IEnumerable<string> guids, string userId);
        bool Delete(string guid);
        bool Change(Image image);
    }
}
