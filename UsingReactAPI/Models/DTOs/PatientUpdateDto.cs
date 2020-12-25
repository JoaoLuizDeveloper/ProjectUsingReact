using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UsingReactAPI.Models.DTOs
{
    public class PatientUpdateDto
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        public DateTime DateCreated { get; set; }
        [Required]
        public Guid DoctorId { get; set; }
    }
}