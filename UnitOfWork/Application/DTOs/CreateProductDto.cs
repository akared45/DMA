namespace UnitOfWork.Application.DTOs
{
    public class CreateProductDto
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public List<string> CategoryNames { get; set; }
    }
}
