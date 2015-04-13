using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOM;
using DAL;
using GermanHomeopathic.Models;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data;
using System.Text;
using System.Data.OleDb;
using CsvHelper;



namespace GermanHomeopathic.Controllers
{
    public class StockController : Controller
    {

        CategoryDAL categoryDAL = new CategoryDAL();
        SubCategoryDAL subCategoryDAL = new SubCategoryDAL();
        StockDAL stockDAL = new StockDAL();

        [HttpGet]
        public ActionResult Index()
        {
            StockViewModel stockViewModel = new StockViewModel();

            var lstCategory = categoryDAL.ReadAllCategory();

            stockViewModel.lstCategory = new List<CategoryViewModel>();
            stockViewModel.lstCategory = lstCategory.ConvertAll(x => new CategoryViewModel
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName
            });

            var lst = stockDAL.ReadAllStock();

            stockViewModel.lstStock = new List<StockViewModel>();
            stockViewModel.lstStock = lst.ConvertAll(x => new StockViewModel
            {
                CategoryId = x.subcategory.category.CategoryId,
                CategoryName = x.subcategory.category.CategoryName,
                SubCategoryId = x.subcategory.SubCategoryId,
                SubCategoryName = x.subcategory.SubCategoryName,
                StockId = x.StockId,
                Quantity = x.Quantity

            });

            return View(stockViewModel);
        }


        [HttpPost]
        public ActionResult Index(StockViewModel model)
        {
            if (ModelState.IsValid)
            {
                Stock stock = new Stock();

                stock.subcategory.category.CategoryId = Convert.ToInt32(model.CategoryId);
                stock.subcategory.SubCategoryId = Convert.ToInt32(model.SubCategoryId);
                stock.Quantity = Convert.ToDecimal(model.Quantity);
                stock.IsReceived = Convert.ToBoolean(1);
                stock.CreatedBy = "Admin";
                stock.CreatedOn = DateTime.Now;

                stockDAL.Create(stock);
                TempData["SuccessMsg"] = "Stock Added Successfully";
                return RedirectToAction("Index");
            }

            var lstCategory = categoryDAL.ReadAllCategory();

            model.lstCategory = new List<CategoryViewModel>();
            model.lstCategory = lstCategory.ConvertAll(x => new CategoryViewModel
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName
            });

            var lst = stockDAL.ReadAllStock();

            model.lstStock = new List<StockViewModel>();
            model.lstStock = lst.ConvertAll(x => new StockViewModel
            {
                CategoryId = x.subcategory.category.CategoryId,
                CategoryName = x.subcategory.category.CategoryName,
                SubCategoryId = x.subcategory.SubCategoryId,
                SubCategoryName = x.subcategory.SubCategoryName,
                StockId = x.StockId,
                Quantity = x.Quantity

            });
            return View(model);

        }

        [HttpPost]
        public JsonResult GetSubcategoryByCategory(int categoryid)
        {

            List<SubCategory> lstSubCategory = new List<SubCategory>();

            lstSubCategory = subCategoryDAL.ReadSubCategoryByCategory(categoryid);

            return Json(lstSubCategory);
        }


        [HttpPost]
        public ActionResult ExportToExcel()
        {
            return Json("StockDetails.csv");
        }

        private string ToCSV(DataTable table)
        {
            var result = new StringBuilder();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                result.Append(table.Columns[i].ColumnName);
                result.Append(i == table.Columns.Count - 1 ? "\n" : ",");
            }

            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    result.Append(row[i].ToString());
                    result.Append(i == table.Columns.Count - 1 ? "\n" : ",");
                }
            }

            return result.ToString();
        }

        [HttpGet]
        public virtual ActionResult Download(string file)
        {
            List<Stock> stocks = new List<Stock>();
            stocks = stockDAL.SetExcelData();

            DataTable table = new DataTable();
            table.Columns.Add("Category_Id", typeof(int));
            table.Columns.Add("Category_Name", typeof(string));
            table.Columns.Add("SubCategory_Id", typeof(int));
            table.Columns.Add("SubCategory_Name", typeof(string));
            table.Columns.Add("Packed_Quantity", typeof(string));
            table.Columns.Add("Code", typeof(string));
            table.Columns.Add("Quantity", typeof(decimal));
            foreach (var item in stocks)
            {
                table.Rows.Add
                    (
                    item.subcategory.category.CategoryId,
                    item.subcategory.category.CategoryName,
                    item.subcategory.SubCategoryId,
                    item.subcategory.SubCategoryName,
                    item.subcategory.PackedQuantity,
                    item.subcategory.code,
                    item.Quantity);

            }
            var bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(ToCSV(table));
            MemoryStream stream = new MemoryStream(bytes);

            string fullPath = Path.Combine(Server.MapPath("~/Files"), file);
            return File(stream.ToArray(), "application/x-ms-excel", file);
        }

        [HttpParamAction]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            if (!string.IsNullOrEmpty(Request.Files["FileUpload1"].FileName))
            {

                List<Stock> lst = new List<Stock>();

                string extension = System.IO.Path.GetExtension(Request.Files["FileUpload1"].FileName);
                string strFileName = "StockDetailsAdded_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + extension;
                string path1 = string.Format("{0}/{1}", Server.MapPath("~/UploadedFile"), strFileName);

                Request.Files["FileUpload1"].SaveAs(path1);

                //Stream reader will read test.csv file in current folder
                StreamReader sr = new StreamReader(Server.MapPath("~/UploadedFile/" + strFileName));
                //Csv reader reads the stream
                CsvReader csvread = new CsvReader(sr);

                //csvread will fetch all record in one go to the IEnumerable object record
                IEnumerable<StockRecord> record = csvread.GetRecords<StockRecord>();

                foreach (var i in record)
                {
                    var stock = new Stock();

                    stock.subcategory.category.CategoryId = i.Category_Id;
                    stock.subcategory.SubCategoryId = i.SubCategory_Id;
                    stock.Quantity = i.Quantity;

                    lst.Add(stock);
                }

                sr.Close();

                stockDAL.InsertExcelData(lst);
                TempData["SuccessMsg"] = "Stock Uploaded Successfully";

                
            }
            else
            {
                TempData["SuccessMsg"] = "File Required";
            }

            return RedirectToAction("Index");
        }

        [HttpParamAction]
        public ActionResult StockExportToExcel()
        {
            StockViewModel model = new StockViewModel();

            List<Stock> lst = new List<Stock>();

            lst = stockDAL.ReadAllStock();
            DataTable table = new DataTable();
            table.Columns.Add("Category_Name", typeof(string));
            table.Columns.Add("SubCategory_Name", typeof(string));
            table.Columns.Add("Quantity", typeof(decimal));
            foreach (var item in lst)
            {
                table.Rows.Add
                    (
                    item.subcategory.category.CategoryName,
                    item.subcategory.SubCategoryName,
                    item.Quantity);
            }

            string Exportfile = "ExportCompleteStockDetails_" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".xls";
            var filePath = Path.Combine(Server.MapPath("~/StockExportFile/") + Exportfile);
            GridView gv = new GridView();

            gv.DataSource = table;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=" + Exportfile);
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());

            System.IO.File.WriteAllText(filePath, sw.ToString());

            Response.Flush();
            Response.End();



            return RedirectToAction("Index");
        }

        public class StockRecord
        {
            public int Category_Id { get; set; }
            public int SubCategory_Id { get; set; }
            public decimal? Quantity { get; set; }
        }
    }

}