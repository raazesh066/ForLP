using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GermanHomeopathic.Models;
using DAL;
using DOM;

namespace GermanHomeopathic.Controllers
{
    public class BillController : Controller
    {
        CustomerDAL customerDAL = new CustomerDAL();
        ChallanDAL challanDAL = new ChallanDAL();
        SubCategoryDAL subcategoryDAL = new SubCategoryDAL();


        [HttpGet]
        public ActionResult Index()
        {
            BillViewModel model = new BillViewModel();
            List<Customer> lst = null;

            lst = customerDAL.ReadAllCustomer();

            model.lstCustomer = new List<CustomerViewModel>();
            model.lstCustomer = lst.ConvertAll(x => new CustomerViewModel
            {
                CustomerName = x.CustomerName,
                CustomerId = x.CustomerId
            });


            return View(model);
        }



        [HttpPost]
        public ActionResult Index(BillViewModel model)
        {
            List<Bill> bill = new List<Bill>();
            List<Customer> lst = null;

            bill = challanDAL.ReadChallanDetailsByCustomerId(model.CustomerId, model.BillId, model.FromDate, model.ToDate);

            BillViewModel billviewModel = new BillViewModel();
            billviewModel.lstBill = new List<BillViewModel>();


            billviewModel.lstBill = bill.ConvertAll(x => new BillViewModel
            {
                ChallanId = x.challan.ChallanId,
                BillId = x.BillId,
                BillDate = (x.challan.CreatedOn),
                CustomerName = x.customer.CustomerName,
                CustomerAddress = x.customer.CustomerAddress,
                AmountWithoutTax = x.challan.AmountWithoutTax,
                TaxAmount = x.challan.TaxAmount,
                TotalAmount = x.challan.TotalAmount
            });


            lst = customerDAL.ReadAllCustomer();

            billviewModel.lstCustomer = new List<CustomerViewModel>();
            billviewModel.lstCustomer = lst.ConvertAll(x => new CustomerViewModel
            {
                CustomerName = x.CustomerName,
                CustomerId = x.CustomerId
            });


            return View("Index", billviewModel);
        }


     
        public ActionResult BillDetails(int ChallanId, string BillId)
        {
            BillDetailViewModel model = new BillDetailViewModel();

            List<BillDetails> lst = new List<BillDetails>();

            lst = challanDAL.ReadChallanDetailsByChallanId(ChallanId, BillId);

            model.lstDetails = new List<BillDetailViewModel>();
            model.CustomerName = lst[0].CustomerName;
            model.CustomerAddress = lst[0].CustomerAddress;
            model.ChallanId = lst[0].ChallanId;
            model.BillId = lst[0].BillId;

            model.lstDetails = lst.ConvertAll(x => new BillDetailViewModel
           {
               CategoryId = x.CategoryId,
               CategoryName = x.CategoryName,
               CategoryQuantity = x.CategoryQuantity,
               CustomerName = x.CustomerName,
               CustomerAddress = x.CustomerAddress,
              // SubCategoryId=x.SubCategoryId,
               //CurrentStock=(decimal)(x.CurrentStock),
               lstSubCategory = subcategoryDAL.ReadSubCategoryByCategory(x.CategoryId)

           });

            return View(model);

        }

        [HttpPost]
        public ActionResult ReadStockByCategoryIdSubCategoryId(int categoryId, int subcategoryId)
        {
           // BillDetailViewModel model = new BillDetailViewModel();

            decimal Current_Stock = 0;

            Current_Stock = challanDAL.ReadStockByCategoryIdSubCategoryId(categoryId, subcategoryId);

            return Json(Current_Stock);
        }

       
        public ActionResult AddSubCategoryToCategory(BillDetailViewModel model)
        {
            List<BillDetails> details = new List<BillDetails>();

            for (int i = 0; i < model.lstDetails.Count; i++)
            {

                for (int j = 0; j < model.lstDetails[i].lstSubCategory.Count; j++)
                {
                    if (model.lstDetails[i].lstSubCategory[j].Quantity == 0 || model.lstDetails[i].lstSubCategory[j].Quantity==null)
                    {

                    }
                    else
                    {
                        BillDetails billdetails = new BillDetails();

                        billdetails.ChallanId = model.ChallanId;
                        billdetails.BillId = model.BillId;

                        billdetails.CategoryId = model.lstDetails[i].CategoryId;

                        billdetails.SubCategoryId = model.lstDetails[i].lstSubCategory[j].SubCategoryId;

                        billdetails.Quantity = Convert.ToDecimal(model.lstDetails[i].lstSubCategory[j].Quantity);

                        details.Add(billdetails);
                    }
                }

            }
            
            challanDAL.AddSubCategory(details);
            TempData["SuccessMsg"] = "Bill Created Successfully";

            return RedirectToAction("Index");
        }

    }
}
