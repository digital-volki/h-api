using HotChocolate;
using HotChocolate.Types;
using Leifez.Application.Service.Interfaces;
using Leifez.Core.Infrastructure.Exceptions;
using Leifez.General;
using Leifez.Images.Inputs;
using System.Collections.Generic;

namespace Leifez.Images
{
    [ExtendObjectType(Name = "Query")]
    public class ImageQueries
    {
        public PayloadBase<string> GetImage(
            [Service] IImageService imageService,
            GetImageInput input)
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

            var result = imageService.GetImage(input.Guid);
            if (string.IsNullOrEmpty(result))
            {
                var error = new UserError
                (
                    message: "Image not exitst.",
                    code: "404"
                );
                errors.Add(error);
                return new PayloadBase<string>(errors);
            }

            return new PayloadBase<string>(result);
        }
    }
}
