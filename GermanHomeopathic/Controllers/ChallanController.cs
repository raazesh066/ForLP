using DOM;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GermanHomeopathic.Models;

namespace GermanHomeopathic.Controllers
{
    public class ChallanController : Controller
    {
        CustomerDAL customerDAL = null;
        CategoryDAL categoryDAL = null;
        TaxDAL taxDAL = null;
        ChallanDAL challanDAL = null;


        public ChallanController()
        {
            customerDAL = new CustomerDAL();
            categoryDAL = new CategoryDAL();
            taxDAL = new TaxDAL();
            challanDAL = new ChallanDAL();
        }
        //
        // GET: /Bill/
        [HttpGet]
        public ActionResult Index()
        {
            ChallanViewModel billViewModel = new ChallanViewModel();
            //Read all customers

           billViewModel= GetData();

            return View(billViewModel);
        }

        [HttpPost]
        public ActionResult ReadCustomerDetailsById(int customerId)
        {
            //Read Customer details
            var customerDetails = customerDAL.ReadCustomerDetailsById(customerId);
            return Json(customerDetails);
        }

        [HttpPost]
        public ActionResult Index(ChallanViewModel model)
        {
            List<ChallanDetails> challanDetails = new List<ChallanDetails>();
            Challan challan = new Challan();

            decimal grandTotal = 0;

            if (ModelState.IsValid)
            {
                foreach (var item in model.lstChallanDetails)
                {
                    ChallanDetails challanDetail = new ChallanDetails();
                    decimal amt = Convert.ToDecimal(item.Rate * item.Quantity);
                    amt -= Convert.ToDecimal(amt * item.Discount) / 100;
                    grandTotal += amt;
                    challanDetail.CategoryId = item.CategoryId;
                    challanDetail.Rate = item.Rate;
                    challanDetail.Discount = item.Discount;
                    challanDetail.Quantity = item.Quantity;
                    challanDetails.Add(challanDetail);
                }

                model.AmountWithoutTax = grandTotal;

                if (model.vat != 0)
                {
                    decimal ecess = (grandTotal * model.ecess) / 100;
                    decimal shecess = (grandTotal * model.shecess) / 100;
                    decimal tcs = (grandTotal * model.tcs) / 100;
                    decimal freight = (grandTotal * model.freight) / 100;
                    decimal vat = (grandTotal * model.vat) / 100;

                    model.TaxAmount = ecess + shecess + tcs + freight + vat;
                }
                else
                {
                    decimal ecess = (grandTotal * model.ecess) / 100;
                    decimal shecess = (grandTotal * model.shecess) / 100;
                    decimal tcs = (grandTotal * model.tcs) / 100;
                    decimal freight = (grandTotal * model.freight) / 100;
                    decimal cst = (grandTotal * model.cst) / 100;

                    model.TaxAmount = ecess + shecess + tcs + freight + cst;
                }

                model.TotalAmount = model.AmountWithoutTax + model.TaxAmount;


                challan.AmountWithoutTax = model.AmountWithoutTax;
                challan.TaxAmount = model.TaxAmount;
                challan.TotalAmount = model.TotalAmount;
                challan.ecess = model.ecess;
                challan.shecess = model.shecess;
                challan.cst = model.cst;
                challan.vat = model.vat;
                challan.freight = model.freight;
                challan.tcs = model.tcs;
                challan.CustomerId = model.CustomerId;
                challan.Remarks = model.Remarks;
                challan.CompanyId = model.CompanyId;


                challanDAL.Create(challan, challanDetails);

                TempData["SuccessMsg"] = "Challan Created Successfully";

                return RedirectToAction("Index");
            }

            model = GetData();

            return View(model);
            
        }


        private ChallanViewModel GetData()
        {
            ChallanViewModel billViewModel = new ChallanViewModel();

            var customers = customerDAL.ReadAllCustomer();
            billViewModel.Customer.Customers = new List<CustomerViewModel>();
            billViewModel.Customer.Customers = customers.ConvertAll(x => new CustomerViewModel
            {
                CustomerId = x.CustomerId,
                CustomerName = x.CustomerName,
                CustomerAddress = x.CustomerAddress,
                TaxName = x.tax.TaxName,
                TaxValue = x.tax.Value
            });

            //Read all categories
            var lstCategory = categoryDAL.ReadAllCategory();

            billViewModel.lstChallanDetails = new List<ChallanDetails>();
            billViewModel.lstChallanDetails = lstCategory.ConvertAll(x => new ChallanDetails
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
                Rate = x.Rate,
                Discount = x.Discount
            });
            var taxes = taxDAL.ReadAllTax();

            billViewModel.ecess = taxes.Where(x => x.TaxId == (int)TaxType.ECESS).FirstOrDefault().Value;
            billViewModel.shecess = taxes.Where(x => x.TaxId == (int)TaxType.SHECESS).FirstOrDefault().Value;
            billViewModel.freight = taxes.Where(x => x.TaxId == (int)TaxType.Freight).FirstOrDefault().Value;
            billViewModel.tcs = taxes.Where(x => x.TaxId == (int)TaxType.TCS).FirstOrDefault().Value;

            return billViewModel;
        }

    }
}
