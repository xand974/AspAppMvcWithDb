using System.Collections.Generic;

namespace AspAppMvcWithDb.Models
{
    public interface IPostManagement
    {
        IEnumerable<Post> GetPosts();
        Post Create(Post post);
        Post GetPostById(int id);
        Post Delete(int id);
        Post Update(Post post);
    }
}
