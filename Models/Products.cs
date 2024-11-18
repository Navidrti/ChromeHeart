
namespace ChromeHeart.Models
{
    public class Products
    {
        public int id { get; set; }
        public string? Name { get; set; } 
        public int Price { get; set; }
        public int Count { get; set; }
        public byte[]? img { get; set; }

		internal object Where(Func<object, bool> value)
		{
			throw new NotImplementedException();
		}
	}
}
