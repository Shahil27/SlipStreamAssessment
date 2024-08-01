using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ResidentialAddress { get; set; } = string.Empty;
        public string WorkAddress { get; set; } = string.Empty;
        public string PostalAddress { get; set; } = string.Empty;
        public string CellNumber { get; set; } = string.Empty;
        public string WorkNumber { get; set; } = string.Empty;
    }
}
