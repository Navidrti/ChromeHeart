namespace ChromeHeart.ViewModels
{
	public class ProductViewModels
	{
		public int id { get; set; }
        public string? Name { get; set; } 
		public int Price { get; set; }
		public int Count { get; set; }
		public IFormFile? img { get; set; }
		public byte[]? imgBytes { get; set; } 
    }
}
