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
    [ExtendObjectType(Name = "Query")]
    public class ImageQueries
    {
        public PayloadBase<IEnumerable<string>> GetImages(
            [Service] IImageService imageService,
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
                return new PayloadBase<IEnumerable<string>>(errors);
            }

            var result = imageService.Get(input.Guids);
            if (!result.Any())
            {
                var error = new UserError
                (
                    message: "Images not exitst.",
                    code: "404"
                );
                errors.Add(error);
                return new PayloadBase<IEnumerable<string>>(errors);
            }

            return new PayloadBase<IEnumerable<string>>(result);
        }
    }
}
