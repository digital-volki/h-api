using HotChocolate;
using HotChocolate.Types;
using Leifez.Application.Domain.Models;
using Leifez.Application.Service.Interfaces;
using Leifez.General;
using System.Threading.Tasks;

namespace Leifez.Types
{
    public class CollectionType : ObjectType<Collection>
    {
        protected override void Configure(IObjectTypeDescriptor<Collection> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode((ctx, id) =>
                    Task<Collection>.Factory.StartNew(() =>
                    {
                        CurrentUser currentUser = ctx.ArgumentValue<CurrentUser>(new NameString("CurrentUserGlobalState"));
                        return ctx.Service<ICollectionService>().GetCollection(id, currentUser.AccountId.ToString());
                    }, ctx.RequestAborted));

        }
    }
}
