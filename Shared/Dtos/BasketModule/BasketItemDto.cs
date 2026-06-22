using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.BasketModule
{
    public record BasketItemDto
    {
        public int Id { get; init; }
        public string ProductName { get; init; } = string.Empty;
        public string PictureUrl { get; init; } = string.Empty;
        [Range(0,double.MaxValue)]
        public decimal price { get; init; }
        [Range(0,99)]
        public int Quantity { get; set; }
    }
}