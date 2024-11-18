using ChromeHeart.Areas.Identity.Data;

namespace ChromeHeart.Models
{
	public class Orders
	{
        public int id { get; set; }
        public string? name { get; set; }
        public int price { get; set; }
        public int count { get; set; }
        public string Userid { get; set; }
        public string UserName { get; set; }
    }
}
