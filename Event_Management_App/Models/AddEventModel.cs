using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Event_Management_App.Models
{
    public class AddEventModel
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public string Capacity { get; set; }
        public string Amount { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public IFormFile ImageFile { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }

        [Required]
        [ForeignKey("eventstatus")]
        public int Status_Id { get; set; }


    }
}
