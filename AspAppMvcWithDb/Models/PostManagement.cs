using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspAppMvcWithDb.Models
{
    public interface IPostManagement
    {
        IEnumerable<Post> GetPosts();
        Post Create(Post post);
        Post GetPostById(int id);
        Post Delete(Post post);
    }
    public class PostManagement : IPostManagement
    {
        List<Post> posts = new()
        {
           new Post {  Id = 1 , Title = "Skate" , Description ="Comment faire un FLip ?.. ", Creator = new Creator { Id =1 , Pseudo= "alex" }  },
           new Post {  Id = 2 , Title = "Réouverture des cinémas" , Description ="A l'occasion de la réouverture des cinémas,je suis parti voir .. ", Creator = new Creator { Id =2 , Pseudo= "momo" }  },
           new Post {  Id = 3 , Title = "Lapin" , Description ="laissez mois vous parler de mon lapin, Happy,..", Creator = new Creator { Id =3 , Pseudo= "nomis" }  },
        };

        public Post Create(Post post)
        {
            post.Id = posts.Max(post => post.Id) + 1;
            posts.Add(post);
            return post;
        }

        public Post Delete(Post post)
        {
            posts.Remove(post);
            return post;
        }

        public Post GetPostById(int id)
        {
            return posts.FirstOrDefault(post => post.Id == id);
        }

        public IEnumerable<Post> GetPosts()
        {
            return posts;
        }
    }
}
