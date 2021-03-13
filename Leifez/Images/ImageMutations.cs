using HotChocolate;
using HotChocolate.Types;
using Leifez.Application.Service.Interfaces;
using Leifez.Core.Infrastructure.Exceptions;
using Leifez.General;
using Leifez.Images.Inputs;
using System.Collections.Generic;
using System.Linq;

namespace Leifez.Images
{
    [ExtendObjectType(Name = "Mutation")]
    public class ImageMutations
    {
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

            var result = imageService.AddImages(input.Base64Images);
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
    }
}
