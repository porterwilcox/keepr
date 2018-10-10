using System.ComponentModel.DataAnnotations;

namespace keepr.Models
{
    public class VaultToDelete
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string SendingUserId { get; set; }
    }
    public class Vault
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}