using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AspNetSampleMusicStore.Models
{
    public class Order:IValidatableObject
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        [Remote("CheckUserName","Accout")]
        public string Username { get; set; }
        [Required]
        [StringLength(160,MinimumLength =3)]
        [Display(Name ="First Name", Order =15000)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(160)]
        [MaxLength(10,ErrorMessage ="Too many Words in {0}")]
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string Email { get; set; }
        [Required]
        [Range(typeof(decimal), "0.00", "22.55")]
        public decimal Total { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(LastName != null && LastName.Split(' ').Length>10)
            {
                yield return new ValidationResult("The last name has too many words!",new[] { "LastName"});
            }
        }
    }
}