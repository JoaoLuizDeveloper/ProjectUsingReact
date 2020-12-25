using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static UsingReactAPI.Models.Doctor;

namespace UsingReactAPI.Models.DTOs
{
    public class DoctorDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string CRM { get; set; }
        [Required]
        public string CRMUF { get; set; }

        public PatientDto Patient { get; set; }

        public DateTime DateCreated { get; set; }        
    }
}