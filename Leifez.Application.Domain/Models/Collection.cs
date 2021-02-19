using System.Collections.Generic;

namespace Leifez.Application.Domain.Models
{
    //[Authorize]
    public class Collection
    {
        public int Id { get; set; }

        public string Title { get; set; }

        //[Authorize(Roles = new[] { "Admin", "Moderator"})]
        public string Description { get; set; }

        public string Author { get; set; }

        public string Image { get; set; }

        public List<Tag> Tags { get; set; }
    }
}
