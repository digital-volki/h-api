﻿using HotChocolate;
using HotChocolate.Types;
using Leifez.Application.Domain.Models;
using Leifez.Application.Service.Interfaces;
using Leifez.Core.Infrastructure.Exceptions;
using Leifez.General;
using Leifez.Images.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Leifez.Images
{
    [ExtendObjectType(Name = "Query")]
    public class ImageQueries
    {
        public PayloadBase<IEnumerable<Image>> GetImages(
            [Service] IImageService imageService,
            [CurrentUserGlobalState] CurrentUser currentUser,
            GetImagesInput input)
        {
            var errors = new List<UserError>();

            if (!input.Validate())
            {
                var error = new UserError
                (
                    message: "Input empty or incomplete.",
                    code: "400"
                );
                errors.Add(error);
                return new PayloadBase<IEnumerable<Image>>(errors);
            }

            var result = imageService.Get(input.Guids, currentUser?.AccountId.ToString());
            if (!result.Any())
            {
                var error = new UserError
                (
                    message: "Images not exitst.",
                    code: "404"
                );
                errors.Add(error);
                return new PayloadBase<IEnumerable<Image>>(errors);
            }

            return new PayloadBase<IEnumerable<Image>>(result);
        }
    }
}
