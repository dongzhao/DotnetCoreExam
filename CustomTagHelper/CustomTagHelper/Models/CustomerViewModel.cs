using System.ComponentModel.DataAnnotations;

namespace CustomTagHelper.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        //[MaxLength(20)]
        public string? Firstname {  get; set; }
        //[MaxLength(20)]
        public string? Lastname { get; set; }
        //[Required]
        //[StringLength(12)]
        public string Username { get; set; }
        public DateTime? BirthDate { get; set; }
        //[MaxLength(20)]
        public string Email { get; set; }
    }
}
