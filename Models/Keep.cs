using System.ComponentModel.DataAnnotations;

namespace keepr.Models
{
    public class Keep
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string UserId { get; set; }

        public string Img { get; set; } = "";

        public bool isPrivate { get; set; } = false;

        public int Views { get; set; }

        public int Shares { get; set; }

        public int Keeps { get; set; }
    }
}