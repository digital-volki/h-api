using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;
using Leifez.Application.Domain.Models;
using Leifez.Application.Service.Interfaces;
using Leifez.Collections.Inputs;
using Leifez.Core.Infrastructure.Exceptions;
using Leifez.General;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Leifez.Collections
{
    [ExtendObjectType(Name = "Mutation")]
    public class CollectionMutations
    {
        [Authorize]
        public PayloadBase<Collection> CreateCollection(
            [Service] ICollectionService collectionService,
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
                return new PayloadBase<Collection>(errors);
            }

            var collection = new Collection()
            {
                Title = input.Title,
                Description = input.Description,
                AuthorId = currentUser.AccountId.ToString(),
                Tags = input.Tags.ToList()
            };

            Collection result = collectionService.Create(collection);
            if (result == null)
            {
                var error = new UserError
                (
                    message: "Failed to create collection.",
                    code: "500"
                );
                errors.Add(error);
                return new PayloadBase<Collection>(errors);
            }

            return new PayloadBase<Collection>(result);
        }

        [Authorize]
        public PayloadBase<bool> UpdateCollection(
            [Service] ICollectionService collectionService,
            [CurrentUserGlobalState] CurrentUser currentUser,
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

            if (!collectionService.IsBelong(currentUser.AccountId.ToString()))
            {
                var error = new UserError
                (
                    message: "This collection does not belong to you ",
                    code: "403"
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

        [Authorize]
        public PayloadBase<bool> AddCollectionYourSelf(
            [Service] ICollectionService collectionService,
            [CurrentUserGlobalState] CurrentUser currentUser,
            AddCollectionYourSelfInput input)
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

            bool result = collectionService.AddCollectionToUser(input.CollectionId, currentUser.AccountId.ToString());

            return new PayloadBase<bool>(result);
        }
    }
}