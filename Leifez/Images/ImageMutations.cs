using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
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
    [ExtendObjectType(Name = "Mutation")]
    public class ImageMutations
    {
        [Authorize]
        public PayloadBase<IEnumerable<string>> CreateImages(
            [Service] IImageService imageService,
            CreateImagesInput input)
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
                return new PayloadBase<IEnumerable<string>>(errors);
            }

            var result = imageService.Add(input.Base64Images,
                string.IsNullOrEmpty(input.CollectionId) 
                    ? Guid.Empty.ToString() 
                    : input.CollectionId);

            if (!result.Any())
            {
                var error = new UserError
                (
                    message: "Failed to create images.",
                    code: "500"
                );
                errors.Add(error);
                return new PayloadBase<IEnumerable<string>>(errors);
            }

            return new PayloadBase<IEnumerable<string>>(result);
        }

        [Authorize(Roles = new[] { "Admin" })]
        public PayloadBase<bool> ChangeImage(
            [Service] IImageService imageService,
            [Service] ITagService tagService,
            ChangeImageInput input)
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
                return new PayloadBase<bool>(errors);
            }

            var image = new Image()
            {
                Guid = input.imageGuid,
                Tags = tagService.Get(input.Tags).ToList()
            };

            bool result = imageService.Change(image);
            if (!result)
            {
                var error = new UserError
                (
                    message: "Failed to change image.",
                    code: "500"
                );
                errors.Add(error);
                return new PayloadBase<bool>(errors);
            }

            return new PayloadBase<bool>(result);
        }
    }
}
