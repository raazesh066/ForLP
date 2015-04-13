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
    public class SubCategoryController : Controller
    {
        CategoryDAL categoryDAL = new CategoryDAL();
        SubCategoryDAL subCategoryDAL = new SubCategoryDAL();
        
        
        [HttpGet]
        public ActionResult Index()
        {
            SubCategoryViewModel subCategoryViewModel = new SubCategoryViewModel();

            var lstCategory = categoryDAL.ReadAllCategory();

            subCategoryViewModel.lstCategory = new List<CategoryViewModel>();
            subCategoryViewModel.lstCategory = lstCategory.ConvertAll(x => new CategoryViewModel
            {
                CategoryId=x.CategoryId,
                CategoryName=x.CategoryName
            });

            var lst = subCategoryDAL.ReadAllSubCategory();

            subCategoryViewModel.lstSubCategories = new List<SubCategoryViewModel>();
            subCategoryViewModel.lstSubCategories = lst.ConvertAll(x => new SubCategoryViewModel
            {
               SubCategoryId=x.SubCategoryId,
               SubcategoryName=x.SubCategoryName,
               PackedQuantity=x.PackedQuantity,
               Code=x.code,
               Rate=x.Rate,
               CategoryId=x.category.CategoryId,
               CategoryName=x.category.CategoryName
            });

            subCategoryViewModel.IsEdit = false;

            return View(subCategoryViewModel);
        }


        [HttpPost]
        public ActionResult Index(SubCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                SubCategory subCategory = new SubCategory();

                subCategory.category.CategoryId = model.CategoryId;
                subCategory.SubCategoryName = model.SubcategoryName;
                subCategory.PackedQuantity = model.PackedQuantity;
                subCategory.code = model.Code;
                subCategory.Rate = Convert.ToDecimal(model.Rate);
                subCategory.CreatedBy = "Admin";
                subCategory.CreatedOn = DateTime.Now;

                subCategoryDAL.Create(subCategory);
                TempData["SuccessMsg"] = "Sub Category Created Successfully";
                return RedirectToAction("Index");
            }

            var lstCategory = categoryDAL.ReadAllCategory();

            model.lstCategory = new List<CategoryViewModel>();
            model.lstCategory = lstCategory.ConvertAll(x => new CategoryViewModel
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName
            });

            var lst = subCategoryDAL.ReadAllSubCategory();

            model.lstSubCategories = new List<SubCategoryViewModel>();
            model.lstSubCategories = lst.ConvertAll(x => new SubCategoryViewModel
            {
                SubCategoryId = x.SubCategoryId,
                SubcategoryName = x.SubCategoryName,
                PackedQuantity = x.PackedQuantity,
                Code = x.code,
                Rate = x.Rate,
                CategoryId = x.category.CategoryId,
                CategoryName = x.category.CategoryName
            });

            model.IsEdit = false;

            return View(model);
            
        }


        public ActionResult Delete(int id)
        {
            subCategoryDAL.DeleteSubcategory(id);
            TempData["SuccessMsg"] = "Sub Category Deleted Successfully";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            SubCategory subcategory = new SubCategory();
            SubCategoryViewModel model = new SubCategoryViewModel();

            subcategory = subCategoryDAL.ReadSubCategoryDetailsById(id);

            model.CategoryId = subcategory.category.CategoryId;
            model.CategoryName = subcategory.category.CategoryName;
            model.SubCategoryId = subcategory.SubCategoryId;
            model.SubcategoryName = subcategory.SubCategoryName;
            model.PackedQuantity = subcategory.PackedQuantity;
            model.Code = subcategory.code;
            model.Rate = subcategory.Rate;

            
            var lstCategory = categoryDAL.ReadAllCategory();

            model.lstCategory = new List<CategoryViewModel>();
            model.lstCategory = lstCategory.ConvertAll(x => new CategoryViewModel
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName
            });


            var lst = subCategoryDAL.ReadAllSubCategory();

            model.lstSubCategories = new List<SubCategoryViewModel>();
            model.lstSubCategories = lst.ConvertAll(x => new SubCategoryViewModel
            {
                SubCategoryId = x.SubCategoryId,
                SubcategoryName = x.SubCategoryName,
                PackedQuantity = x.PackedQuantity,
                Code = x.code,
                Rate = x.Rate,
                CategoryId = x.category.CategoryId,
                CategoryName = x.category.CategoryName
            });

            model.IsEdit = true;

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Edit(SubCategoryViewModel model)
        {

            SubCategory subcategory = new SubCategory();

            if (ModelState.IsValid)
            {
                subcategory.SubCategoryId = model.SubCategoryId;
                subcategory.SubCategoryName = model.SubcategoryName;
                subcategory.category.CategoryId = model.CategoryId;
                subcategory.category.CategoryName = model.CategoryName;
                subcategory.PackedQuantity = model.PackedQuantity;
                subcategory.code = model.Code;
                subcategory.Rate = Convert.ToDecimal(model.Rate);
                subcategory.ModifiedBy = "Admin";
                subcategory.ModifiedOn = DateTime.Now;

                subCategoryDAL.Edit(subcategory);
                TempData["SuccessMsg"] = "Sub Category Updated Successfully";
                return RedirectToAction("Index");
            }

            subcategory = new SubCategory();

            subcategory = subCategoryDAL.ReadSubCategoryDetailsById(model.SubCategoryId);

            model.CategoryId = subcategory.category.CategoryId;
            model.CategoryName = subcategory.category.CategoryName;
            model.SubCategoryId = subcategory.SubCategoryId;
            model.SubcategoryName = subcategory.SubCategoryName;
            model.PackedQuantity = subcategory.PackedQuantity;
            model.Code = subcategory.code;
            model.Rate = subcategory.Rate;


            var lstCategory = categoryDAL.ReadAllCategory();

            model.lstCategory = new List<CategoryViewModel>();
            model.lstCategory = lstCategory.ConvertAll(x => new CategoryViewModel
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName
            });


            var lst = subCategoryDAL.ReadAllSubCategory();

            model.lstSubCategories = new List<SubCategoryViewModel>();
            model.lstSubCategories = lst.ConvertAll(x => new SubCategoryViewModel
            {
                SubCategoryId = x.SubCategoryId,
                SubcategoryName = x.SubCategoryName,
                PackedQuantity = x.PackedQuantity,
                Code = x.code,
                Rate = x.Rate,
                CategoryId = x.category.CategoryId,
                CategoryName = x.category.CategoryName
            });

            model.IsEdit = true;

            return View("Index", model);
           
        }
    }
}
