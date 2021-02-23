using HotChocolate.Types;
using Leifez.Application.Domain.Models;
using Leifez.Application.Service.Interfaces;
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
                        return ctx.Service<ICollectionService>().GetCollection(id); 
                    }, ctx.RequestAborted));

        }
    }
}
