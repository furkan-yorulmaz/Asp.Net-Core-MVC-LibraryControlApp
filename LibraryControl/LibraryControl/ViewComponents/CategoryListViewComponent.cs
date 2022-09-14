using LibraryControl.Business.Abstract;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace LibraryControl.ViewComponents
{
    public class CategoryListViewComponent : ViewComponent
    {
        ICategoryService _categoryService;
        public CategoryListViewComponent(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }
    }
}
