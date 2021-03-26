using HotChocolate;
using HotChocolate.Types;
using Leifez.Application.Domain.Models;
using Leifez.Application.Service.Interfaces;
using Leifez.Core.Infrastructure.Exceptions;
using Leifez.General;
using System.Collections.Generic;
using System.Linq;

namespace Leifez.Tags
{
    [ExtendObjectType(Name = "Query")]
    public class TagQueries
    {
        public PayloadBase<List<Tag>> GetTags(
            [Service] ITagService tagService)
        {
            var errors = new List<UserError>();

            var result = tagService.GetAll().ToList();
            
            if (result == null || !result.Any())
            {
                var error = new UserError
                (
                    message: "Tags not exitst.",
                    code: "404"
                );
                errors.Add(error);
                return new PayloadBase<List<Tag>>(errors);
            }

            return new PayloadBase<List<Tag>>(result);
        }
    }
}
