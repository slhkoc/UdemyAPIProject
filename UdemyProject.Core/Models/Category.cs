using System;
using System.Collections.Generic;
using System.Text;

namespace UdemyProject.Core.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
