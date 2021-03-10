using HotChocolate;
using HotChocolate.Types;
using Leifez.Application.Service.Interfaces;
using Leifez.Core.Infrastructure.Exceptions;
using Leifez.General;
using Leifez.Images.Inputs;
using System.Collections.Generic;

namespace Leifez.Images
{
    [ExtendObjectType(Name = "Mutation")]
    public class ImageMutations
    {
        public PayloadBase<string> CreateImage(
            [Service] IImageService imageService,
            CreateImageInput input)
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
                return new PayloadBase<string>(errors);
            }

            var result = imageService.AddImage(input.ContentImage);
            if (string.IsNullOrEmpty(result))
            {
                var error = new UserError
                (
                    message: "Failed to create image.",
                    code: "500"
                );
                errors.Add(error);
                return new PayloadBase<string>(errors);
            }

            return new PayloadBase<string>(result);
        }
    }
}
