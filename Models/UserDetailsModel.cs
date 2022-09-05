using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagementAPI.Models
{
    public class UserDetailsModel
    {
        [Key]
        public int UserId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string FullName { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string Password { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string Mobile { get; set; }

        public int? Score { get; set; }
        public int? TimeTaken { get; set; }
       
    }

    public class UserRestult
    {
        public int UserId { get; set; }

        public int Score { get; set; }

        public int TimeTaken { get; set; }
    }
}

