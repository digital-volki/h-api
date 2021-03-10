//using Leifez.Application.Domain.Models;
//using Leifez.General;
//using System.Collections.Generic;

//namespace Leifez.Collections.Inputs
//{
//    public record CreateCollectionInput(
//        string Title,
//        string Description,
//        IEnumerable<int> Tags,
//        ContentType contentType,
//        bool IsAdult,
//        PermissionType) : IInput
//    {
//        public bool Validate()
//        {
//            return this != null
//                && !string.IsNullOrEmpty(Email)
//                && !string.IsNullOrEmpty(UserName)
//                && !string.IsNullOrEmpty(Password);
//        }
//    };
//}
