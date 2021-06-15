using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspAppMvcWithDb.Models
{
    public class PostManagement : IPostManagement
    {
        List<Post> posts = new()
        {
           new Post {  Id = 1 , Title = "Skate" , Description ="Comment faire un Flip ? ", Creator = new Creator { Id =1 , Pseudo= "alex" }  },
           new Post {  Id = 2 , Title = "Réouverture des cinémas" , Description ="A l'occasion de la réouverture des cinémas,je suis parti voir", Creator = new Creator { Id =2 , Pseudo= "momo" }  },
           new Post {  Id = 3 , Title = "Lapin" , Description ="laissez moi vous parler de mon lapin, Happy", Creator = new Creator { Id =3 , Pseudo= "nomis" }  },
        };

        public Post Create(Post post)
        {
            post.Id = posts.Max(post => post.Id) + 1;
            post.Creator = new Creator
            {
                Id = post.Id,
                Pseudo = RandomName()
            };
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

        public string RandomName()
        {
            char[] letters = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

            Random randomNum = new Random();

            string newName = string.Empty;
            int next = 0;
            for (int i = 0; i <= next; i++)
            {
                next = randomNum.Next(letters.Length);

                newName += letters[next];
            }
            return newName;

        }
    }
}
