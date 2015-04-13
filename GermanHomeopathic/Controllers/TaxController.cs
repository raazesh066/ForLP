using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DOM;
using GermanHomeopathic.Models;

namespace GermanHomeopathic.Controllers
{
    public class TaxController : Controller
    {

        TaxDAL taxDal = new TaxDAL();

        [HttpGet]
        public ActionResult Index()
        {
            var lst = taxDal.ReadAllTax();

            TaxViewModel taxViewModel = new TaxViewModel();
            taxViewModel.lst = new List<TaxViewModel>();
            taxViewModel.lst = lst.ConvertAll(x => new TaxViewModel
            {
                TaxId = x.TaxId,
                TaxName = x.TaxName,
                Value = x.Value
            });
            taxViewModel.IsEdit = false;

            return View(taxViewModel);

        }

        [HttpPost]
        public ActionResult Index(TaxViewModel model)
        {
            Tax tax = new Tax();

            if (ModelState.IsValid)
            {
                tax.TaxName = model.TaxName;
                tax.Value = Convert.ToDecimal(model.Value);
                tax.CreatedBy = "Admin";
                tax.CreatedOn = DateTime.Now;

                taxDal.Create(tax);
                TempData["SuccessMsg"] = "Tax Created Successfully";
                return RedirectToAction("Index");
            }

            var lst = taxDal.ReadAllTax();
            model.lst = new List<TaxViewModel>();
            model.lst = lst.ConvertAll(x => new TaxViewModel
            {
                TaxId = x.TaxId,
                TaxName = x.TaxName,
                Value = x.Value
            });
            model.IsEdit = false;
            return View(model);
        }


        public ActionResult Delete(int id)
        {
            taxDal.Delete(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Tax tax = new Tax();
            TaxViewModel model = new TaxViewModel();

            tax = taxDal.ReadTaxById(id);

            model.TaxId = tax.TaxId;
            model.TaxName = tax.TaxName;
            model.Value = tax.Value;

            var lst = taxDal.ReadAllTax();
            model.lst = new List<TaxViewModel>();
            model.lst = lst.ConvertAll(x => new TaxViewModel
            {
                TaxId = x.TaxId,
                TaxName = x.TaxName,
                Value = x.Value
            });
            model.IsEdit = true;


            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Edit(TaxViewModel model)
        {
            Tax tax = new Tax();

            if (ModelState.IsValid)
            {
                tax.TaxId = model.TaxId;
                tax.TaxName = model.TaxName;
                tax.Value = Convert.ToDecimal(model.Value);
                tax.ModifiedBy = "Admin";
                tax.ModifiedOn = DateTime.Now;

                taxDal.Edit(tax);
                TempData["SuccessMsg"] = "Tax Updated Successfully";
                return RedirectToAction("Index");
            }

            tax = new Tax();

            tax = taxDal.ReadTaxById(model.TaxId);

            model.TaxId = tax.TaxId;
            model.TaxName = tax.TaxName;
            model.Value = tax.Value;

            var lst = taxDal.ReadAllTax();
            model.lst = new List<TaxViewModel>();
            model.lst = lst.ConvertAll(x => new TaxViewModel
            {
                TaxId = x.TaxId,
                TaxName = x.TaxName,
                Value = x.Value
            });
            model.IsEdit = true;

            return View("Index", model);
        }

    }
}
