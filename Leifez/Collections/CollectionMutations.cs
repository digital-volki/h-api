using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;
using Leifez.Application.Domain.Models;
using Leifez.Application.Service.Interfaces;
using Leifez.Collections.Inputs;
using Leifez.Core.Infrastructure.Exceptions;
using Leifez.General;
using System.Collections.Generic;
using System.Linq;

namespace Leifez.Collections
{
    [ExtendObjectType(Name = "Mutation")]
    public class CollectionMutations
    {
        [Authorize]
        public PayloadBase<int> CreateCollection(
            [Service] ICollectionService collectionService,
            [Service] ITagService tagService,
            [CurrentUserGlobalState] CurrentUser currentUser,
            CreateCollectionInput input)
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
                return new PayloadBase<int>(errors);
            }

            var collection = new Collection()
            {
                Title = input.Title,
                Description = input.Description,
                AuthorId = currentUser.AccountId.ToString(),
                Tags = input.Tags.ToList()
            };

            var result = collectionService.Create(collection);
            if (result == -1)
            {
                var error = new UserError
                (
                    message: "Failed to create collection.",
                    code: "500"
                );
                errors.Add(error);
                return new PayloadBase<int>(errors);
            }

            return new PayloadBase<int>(result);
        }

        public PayloadBase<bool> UpdateCollection(
            [Service] ICollectionService collectionService,
            UpdateCollectionInput input)
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

            var collection = new Collection()
            {
                Id = input.Id,
                Title = input.Title,
                Description = input.Description,
                Tags = input.Tags?.ToList(),
                Images = input.Images?.ToList()
            };

            bool result = collectionService.Update(collection);
            if (!result)
            {
                var error = new UserError
                (
                    message: "Failed to create collection.",
                    code: "500"
                );
                errors.Add(error);
                return new PayloadBase<bool>(errors);
            }

            return new PayloadBase<bool>(result);
        }
    }
}