using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VisitsTaskWebAPI_MohammedElmorsy.Models
{
    public class Visit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Number { get; set; }
        public string CarPlateNumber { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }

        public  int UserId { get; set; }
        public  string UserFullName { get; set; }
        public virtual Visitor Visitor { get; set; }

    }
}
