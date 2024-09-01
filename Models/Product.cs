using System.Collections.Generic;

namespace Atula_dotnet_test.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }

        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}


