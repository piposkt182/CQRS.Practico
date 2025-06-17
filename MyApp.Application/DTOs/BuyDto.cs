
namespace MyApp.Application.DTOs
{
    public class BuyDto
    {
        public int Codigo { get; set; }
        public int MovieId { get; set; }
        public List<ProductDto> products { get; set; }
    };
}
