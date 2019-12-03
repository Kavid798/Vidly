using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.DTO
{
    public class CustomerDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        //[Min18YearsIfAMember]
        
        public DateTime? BirthDate { get; set; }

        public bool IsSubscribedToNewsLetter { get; set; }
        
        public MembershipTypeDto MembershipType { get; set; }
        public byte MembershipTypeDtoId { get; set; }
    }
}