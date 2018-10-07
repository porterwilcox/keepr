using System.ComponentModel.DataAnnotations;

namespace keepr.Models
{
    public class KeepToDelete
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string SendingUserId { get; set; }
    }
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

        public int isPrivate { get; set; } = 0;

        public int Views { get; set; }

        public int Shares { get; set; }

        public int Keeps { get; set; }
    }
}