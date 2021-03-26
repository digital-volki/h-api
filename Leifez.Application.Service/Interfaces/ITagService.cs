using Leifez.Application.Domain.Models;
using System.Collections.Generic;

namespace Leifez.Application.Service.Interfaces
{
    public interface ITagService
    {
        IEnumerable<Tag> GetAll();
        IEnumerable<Tag> Get(IEnumerable<int> ids);
        Tag Get(int id);
    }
}
