using HotChocolate.Types;
using Leifez.Models;

namespace Leifez.Queries
{
    [ExtendObjectType(Name = "Query")]
    public class AccountQuery
    {
        public Account GetAccount(int id)
        {
            return new Account()
            {
                Id = id,
                Login = "Pidorok228",
                Password = "ya_ebu_sobak"
            };
        }
    }
}
