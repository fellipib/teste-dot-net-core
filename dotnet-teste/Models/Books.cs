
using Microsoft.EntityFrameworkCore;

namespace dotnet_teste.Models
{
    public class Books
    {
        public int Id { get; set; }
        public int? NSU {  get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Writer { get; set; }
        public string? Category { get; set; }
        public List<Loan>? Loans { get; set; }
    }
}
