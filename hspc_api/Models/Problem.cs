using System;
using System.ComponentModel.DataAnnotations;

namespace hspc_api.Models
{
    public class Problem
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
    }
}
