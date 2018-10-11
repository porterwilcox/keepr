using System.ComponentModel.DataAnnotations;

namespace keepr.Models
{
    public class VaultKeepToDelete
    {
        [Required]
        public int VaultId { get; set; }

        [Required]
        public int KeepId { get; set; }
    }
    public class VaultKeep
    {
        public int Id { get; set; }
        
        [Required]
        public int VaultId { get; set; }

        [Required]
        public int KeepId { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}