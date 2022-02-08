using WebStore_Edu.Domain.Entityes;

namespace WebStore_Edu.Domain.ViewModels
{
    public class BreadCrumbsViewModel
    {
        public Section? Section { get; set; }
        public Section? ParentSection { get; set; }

        public Brand? Brand { get; set; }

        public string? ProductName { get; set; }
    }
}
