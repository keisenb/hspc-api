using System;
using System.ComponentModel.DataAnnotations;

namespace hspc_api.Models
{
    public class Team
    {
        public Team()
        {
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Score { get; set; }
    }
}
