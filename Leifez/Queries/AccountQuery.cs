using HotChocolate.Types;
using Leifez.Models;
using System;

namespace Leifez.Queries
{
    [ExtendObjectType(Name = "Query")]
    public class AccountQuery
    {
        const string ABC = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";



        public Account GetAccount(int id)
        {
            var random = new Random();

            return new Account()
            {
                Id = id,
                Login = ABC[random.Next(0, ABC.Length - 1)].ToString(),
                Password = "ya_ebu_sobak"
            };
        }
    }
}
