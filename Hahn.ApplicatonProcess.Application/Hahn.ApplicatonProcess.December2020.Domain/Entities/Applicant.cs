using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.Entities
{
    public class Applicant
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string FamilyName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string CountryofOrigin { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public int Age { get; set; }
        public bool Hired { get; set; }
    }
}
