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
    public class CustomerController : Controller
    {
        CustomerDAL customerDAL = new CustomerDAL();
        TaxDAL taxDAL = new TaxDAL();

        [HttpGet]
        public ActionResult Index()
        {
            CustomerViewModel customerViewModel = new CustomerViewModel();
            //cst or vat

            var lstTax = taxDAL.ReadAllTaxes();

            customerViewModel.lstTax = new List<TaxViewModel>();
            customerViewModel.lstTax = lstTax.ConvertAll(x => new TaxViewModel
            {
                TaxId = x.TaxId,
                TaxName = x.TaxName,
                Value = x.Value
            });

            var lst = customerDAL.ReadAllCustomer();

            customerViewModel.Customers = new List<CustomerViewModel>();
            customerViewModel.Customers = lst.ConvertAll(x => new CustomerViewModel
            {
                CustomerId = x.CustomerId,
                CustomerName = x.CustomerName,
                CustomerAddress = x.CustomerAddress,
                CustomerPhone = x.CustomerPhone,
                TinNumber = x.TinNumber,
                TaxId = x.tax.TaxId,
                TaxName = x.tax.TaxName,
                TaxValue = x.tax.Value
            });

            customerViewModel.IsEdit = false;

            return View(customerViewModel);
        }


        [HttpPost]
        public ActionResult Index(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                Customer customer = new Customer();
                customer.CustomerName = model.CustomerName;
                customer.CustomerAddress = model.CustomerAddress;
                customer.CustomerPhone = Convert.ToInt32(model.CustomerPhone);
                customer.TinNumber = Convert.ToInt32(model.TinNumber);
                customer.tax.TaxId = model.TaxId;
                customer.CreatedBy = "Admin";
                customer.CreatedOn = DateTime.Now;

                customerDAL.Create(customer);
                TempData["SuccessMsg"] = "Customer Created Successfully";
                return RedirectToAction("Index");
            }

            var lstTax = taxDAL.ReadAllTaxes();

            model.lstTax = new List<TaxViewModel>();
            model.lstTax = lstTax.ConvertAll(x => new TaxViewModel
            {
                TaxId = x.TaxId,
                TaxName = x.TaxName,
                Value = x.Value
            });

            var lst = customerDAL.ReadAllCustomer();

            model.Customers = new List<CustomerViewModel>();
            model.Customers = lst.ConvertAll(x => new CustomerViewModel
            {
                CustomerId = x.CustomerId,
                CustomerName = x.CustomerName,
                CustomerAddress = x.CustomerAddress,
                CustomerPhone = x.CustomerPhone,
                TinNumber = x.TinNumber,
                TaxId = x.tax.TaxId,
                TaxName = x.tax.TaxName,
                TaxValue = x.tax.Value
            });

            model.IsEdit = false;

            return View(model);
            
        }


        public ActionResult Delete(int id)
        {
            customerDAL.DeleteCustomer(id);
            TempData["SuccessMsg"] = "Customer Deleted Successfully";

            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            CustomerViewModel model = new CustomerViewModel();
            Customer cust = new Customer();

            cust = customerDAL.ReadCustomerDetailsById(id);

            model.CustomerId = cust.CustomerId;
            model.CustomerName = cust.CustomerName;
            model.CustomerAddress = cust.CustomerAddress;
            model.CustomerPhone = cust.CustomerPhone;
            model.TinNumber = cust.TinNumber;
            model.TaxId = cust.tax.TaxId;
            model.TaxName = cust.tax.TaxName;


            var lstTax = taxDAL.ReadAllTax();

            model.lstTax = new List<TaxViewModel>();
            model.lstTax = lstTax.ConvertAll(x => new TaxViewModel
            {
                TaxId = x.TaxId,
                TaxName = x.TaxName,
                Value = x.Value
            });


            var lst = customerDAL.ReadAllCustomer();

            model.Customers = new List<CustomerViewModel>();
            model.Customers = lst.ConvertAll(x => new CustomerViewModel
            {
                CustomerId = x.CustomerId,
                CustomerName = x.CustomerName,
                CustomerAddress = x.CustomerAddress,
                CustomerPhone = x.CustomerPhone,
                TinNumber = x.TinNumber,
                TaxId = x.tax.TaxId,
                TaxName = x.tax.TaxName,
                TaxValue = x.tax.Value
            });

            model.IsEdit = true;
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Edit(CustomerViewModel model)
        {
            Customer cust = new Customer();

            if (ModelState.IsValid)
            {
                cust.CustomerId = model.CustomerId;
                cust.CustomerName = model.CustomerName;
                cust.CustomerAddress = model.CustomerAddress;
                cust.CustomerPhone = Convert.ToInt32(model.CustomerPhone);
                cust.TinNumber = Convert.ToInt32(model.TinNumber);
                cust.tax.TaxId = model.TaxId;
                cust.ModifiedBy = "Admin";
                cust.ModifiedOn = DateTime.Now;

                customerDAL.Edit(cust);
                TempData["SuccessMsg"] = "Customer Updated Successfully";
                return RedirectToAction("Index");
            }


            cust = new Customer();

            cust = customerDAL.ReadCustomerDetailsById(model.CustomerId);

            model.CustomerId = cust.CustomerId;
            model.CustomerName = cust.CustomerName;
            model.CustomerAddress = cust.CustomerAddress;
            model.CustomerPhone = cust.CustomerPhone;
            model.TinNumber = cust.TinNumber;
            model.TaxId = cust.tax.TaxId;
            model.TaxName = cust.tax.TaxName;


            var lstTax = taxDAL.ReadAllTax();

            model.lstTax = new List<TaxViewModel>();
            model.lstTax = lstTax.ConvertAll(x => new TaxViewModel
            {
                TaxId = x.TaxId,
                TaxName = x.TaxName,
                Value = x.Value
            });


            var lst = customerDAL.ReadAllCustomer();

            model.Customers = new List<CustomerViewModel>();
            model.Customers = lst.ConvertAll(x => new CustomerViewModel
            {
                CustomerId = x.CustomerId,
                CustomerName = x.CustomerName,
                CustomerAddress = x.CustomerAddress,
                CustomerPhone = x.CustomerPhone,
                TinNumber = x.TinNumber,
                TaxId = x.tax.TaxId,
                TaxName = x.tax.TaxName,
                TaxValue = x.tax.Value
            });

            model.IsEdit = true;

            return View("Index", model);


        }

    }
}
