using HotChocolate;
using System;
using System.Collections.Generic;

namespace Leifez.General
{
    public class CurrentUser
    {
        public Guid AccountId { get; }
        public List<string> Claims { get; }

        public CurrentUser(Guid accountId, List<string> claims)
        {
            AccountId = accountId;
            Claims = claims;
        }
    }

    public class CurrentUserGlobalState : GlobalStateAttribute
    {
        public CurrentUserGlobalState() : base("currentUser")
        {
        }
    }
}
