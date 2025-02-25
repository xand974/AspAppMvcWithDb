﻿using AspAppMvcWithDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspAppMvcWithDb.Data
{
    public class DataSqlRepo : IPostManagement
    {
        private readonly AppDbContext context;

        public DataSqlRepo(AppDbContext context)
        {
            this.context = context;
        }

        public Post Create(Post post)
        {
            context.Posts.Add(post);
            context.SaveChanges();
            return post;
        }

        public Post Delete(Post post)
        {
            Post deletePost = context.Posts.FirstOrDefault(post => post.Id == post.Id);
            context.Posts.Remove(deletePost);
            context.SaveChanges();
            return deletePost;
        }

        public Post GetPostById(int id)
        {
            return context.Posts.FirstOrDefault(post => post.Id == id);
        }

        public IEnumerable<Post> GetPosts()
        {
            return context.Posts;
        }

        public Post Update(Post post)
        {
            var postFound = context.Posts.Attach(post);

            postFound.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            context.SaveChanges();

            return post;

            
        }
    }
}
