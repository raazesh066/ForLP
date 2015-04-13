using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOM;
using DAL;
using GermanHomeopathic.Models;


namespace GermanHomeopathic.Controllers
{
    public class CategoryController : Controller
    {
        //
        // GET: /Category/
        CategoryDAL categoryDal = new CategoryDAL();

        [HttpGet]
        public ActionResult Index()
        {
            var lst = categoryDal.ReadAllCategory();

            CategoryViewModel categoryViewModel = new CategoryViewModel();
            categoryViewModel.lstCategory = new List<CategoryViewModel>();
            categoryViewModel.lstCategory = lst.ConvertAll(x => new CategoryViewModel
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
                Rate = x.Rate,
                Discount = x.Discount
            });
            categoryViewModel.IsEdit = false;
            return View(categoryViewModel);
        }

        [HttpPost]
        public ActionResult Index(CategoryViewModel model)
        {

            Category category = new Category();
            if (ModelState.IsValid)
            {

                category.CategoryName = model.CategoryName;
                category.Rate = Convert.ToDecimal(model.Rate);
                category.Discount = Convert.ToDecimal(model.Discount);
                category.CreatedBy = "Admin";
                category.CreatedOn = DateTime.Now;
                categoryDal.Create(category);
                TempData["SuccessMsg"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            var lst = categoryDal.ReadAllCategory();
            model.lstCategory = new List<CategoryViewModel>();
            model.lstCategory = lst.ConvertAll(x => new CategoryViewModel
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
                Rate = x.Rate,
                Discount = x.Discount
            });
            model.IsEdit = false;
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            categoryDal.DeleteCategory(id);

            TempData["SuccessMsg"] = "Category Deleted Successfully";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Category category = new Category();
            CategoryViewModel model = new CategoryViewModel();

            category = categoryDal.ReadCategoryDetailsById(id);

            model.CategoryId = category.CategoryId;
            model.CategoryName = category.CategoryName;
            model.Rate = category.Rate;
            model.Discount = category.Discount;


            var lst = categoryDal.ReadAllCategory();

            model.lstCategory = new List<CategoryViewModel>();
            model.lstCategory = lst.ConvertAll(x => new CategoryViewModel
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
                Rate = x.Rate,
                Discount = x.Discount
            });

            model.IsEdit = true;

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Edit(CategoryViewModel model)
        {
            Category category = new Category();

            if (ModelState.IsValid)
            {
                category = new Category();
                category.CategoryId = model.CategoryId;
                category.CategoryName = model.CategoryName;
                category.Rate = Convert.ToDecimal(model.Rate);
                category.Discount = Convert.ToDecimal(model.Discount);
                category.ModifiedBy = "Admin";
                category.ModifiedOn = DateTime.Now;
                categoryDal.Edit(category);
                TempData["SuccessMsg"] = "Category Updated Successfully";
                return RedirectToAction("Index");
            }

            category = new Category();

            category = categoryDal.ReadCategoryDetailsById(model.CategoryId);

            model.CategoryId = category.CategoryId;
            model.CategoryName = category.CategoryName;
            model.Rate = category.Rate;
            model.Discount = category.Discount;

            var lst = categoryDal.ReadAllCategory();

            model.lstCategory = new List<CategoryViewModel>();
            model.lstCategory = lst.ConvertAll(x => new CategoryViewModel
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
                Rate = x.Rate,
                Discount = x.Discount
            });

            model.IsEdit = true;

            return View("Index", model);
        }

    }
}
