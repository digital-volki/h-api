using HotChocolate.Types;
using Leifez.Application.Domain.Models;
using Leifez.Application.Service.Interfaces;
using System.Threading.Tasks;

namespace Leifez.Types
{
    public class TagType : ObjectType<Tag>
    {
        protected override void Configure(IObjectTypeDescriptor<Tag> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode((ctx, id) =>
                    Task<Tag>.Factory.StartNew(() =>
                    {
                        return ctx.Service<ITagService>().Get(id);
                    }, ctx.RequestAborted));
        }
    }
}
