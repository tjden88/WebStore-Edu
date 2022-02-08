using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using WebStore_Edu.Domain.ViewModels;
using WebStore_Edu.Interfaces.Services;

namespace WebStore_Edu.Components
{
    public class SectionsViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;
        private readonly IMapper _Mapper;

        public SectionsViewComponent(IProductData ProductData, IMapper Mapper)
        {
            _ProductData = ProductData;
            _Mapper = Mapper;
        }

        public IViewComponentResult Invoke()
        {
            var selectedSectionId = int.TryParse(HttpContext.Request.Query["SectionId"], out var id) ? id : 0;

            var parentSectionId = 0;

            var allSections = _ProductData.GetSections().ToArray();

            var parentSectionsVm = allSections
                .Where(s => s.ParentId == null)
                .Select(s => _Mapper.Map<SectionViewModel>(s))
                .OrderBy(vm => vm.Order)
                .ToArray();

            foreach (var section in parentSectionsVm)
            {
                var childs = allSections
                        .Where(s => s.ParentId == section.Id)
                        .Select(s=>
                        {
                            var childVm = _Mapper.Map<SectionViewModel>(s);
                            childVm.Parent = section;

                            if (childVm.Id == selectedSectionId)
                                parentSectionId = section.Id;

                            return childVm;
                        })
                        .OrderBy(vm => vm.Order)
                        .ToList()
                    ;

                section.Children = childs;
            }

            ViewBag.SelectedParentSection = parentSectionId;

            return View(parentSectionsVm);
        }
    }
}
