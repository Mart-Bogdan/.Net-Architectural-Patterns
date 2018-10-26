using System;
using System.ComponentModel.DataAnnotations;
using WebApp.Core.Entity.Entities;

namespace WebAppCore.Models.BlogPostModels
{
    public class BlogPostCreateModel
    {
        [ScaffoldColumn(false)] 
        public int Id { get; set; } 
        
        
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        
        [Required]
        public string Title { get; set; }
        

    }
}