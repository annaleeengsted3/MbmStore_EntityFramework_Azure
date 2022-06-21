using MbmStore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace MbmStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {

        private MbmStoreContext dataContext;

        public NavigationMenuViewComponent(MbmStoreContext dbContext)
        {
            dataContext = dbContext;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(dataContext.Products
            .Select(x => x.Category)
            .Distinct()
            .OrderBy(x => x));
        }
    }
}

