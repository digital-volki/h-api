using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;
using Leifez.Application.Service.Interfaces;
using Leifez.Common.Inputs;
using Leifez.Core.Infrastructure.Exceptions;
using Leifez.General;
using System.Collections.Generic;

namespace Leifez.Common
{
    [ExtendObjectType(Name = "Mutation")]
    public class CommonMutations
    {
        [Authorize]
        public PayloadBase<bool> Like(
            [Service] ICommonService commonService,
            [CurrentUserGlobalState] CurrentUser currentUser,
            LikeInput input)
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

            var result = commonService.Like(currentUser.AccountId.ToString(), input.Guid, input.ContentType);

            return new PayloadBase<bool>(result);
        }
    }
}
