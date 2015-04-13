using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOM;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace DAL
{
    public class ChallanDAL
    {


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
        SqlCommand cmd = null;


        public void Create(Challan challan,List<ChallanDetails> challanDetails)
        {

            cmd = new SqlCommand("proc_CreateChallan", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();

            SqlParameter outparam = new SqlParameter("@OutChallanId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            cmd.Parameters.AddWithValue("@CustomerId", challan.CustomerId);
            cmd.Parameters.AddWithValue("@CompanyId",challan.CompanyId);
            cmd.Parameters.AddWithValue("@Amount_without_Tax", challan.AmountWithoutTax);
            cmd.Parameters.AddWithValue("@Tax_Amount", challan.TaxAmount);
            cmd.Parameters.AddWithValue("@Total_Amount",challan.TotalAmount);
            if (challan.Remarks != null)
            {
                cmd.Parameters.AddWithValue("@Remarks", challan.Remarks);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Remarks", DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@CST", challan.cst);
            cmd.Parameters.AddWithValue("@Vat",challan.vat);
            cmd.Parameters.AddWithValue("@E_CESS", challan.ecess);
            cmd.Parameters.AddWithValue("@SHE_CESS", challan.shecess);
            cmd.Parameters.AddWithValue("@Freight", challan.freight);
            cmd.Parameters.AddWithValue("@TCS", challan.tcs);
            cmd.Parameters.AddWithValue("@CreatedBy", "Admin");
            cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
            cmd.Parameters.Add(outparam);
            cmd.ExecuteNonQuery();

            int ChallanId = int.Parse(outparam.Value.ToString());
            con.Close();

            InsertChallanDetails(ChallanId, challanDetails,challan.CompanyId);
            
        }

        private void InsertChallanDetails(int ChallanId, List<ChallanDetails> challanDetails,int CompanyId)
        {
            string BillId = string.Empty;
          
            if (CompanyId == 1)
            {
                BillId = "DL" + ChallanId;
            }
            else
            {
                BillId = "GZB" + ChallanId;
            }
            
            
            foreach (var item in challanDetails)
            {
                cmd = new SqlCommand("proc_CreateChallanDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                cmd.Parameters.AddWithValue("@ChallanId", ChallanId);
                cmd.Parameters.AddWithValue("@BillId",BillId);
                cmd.Parameters.AddWithValue("@CategoryId", item.CategoryId);
                cmd.Parameters.AddWithValue("@Rate", item.Rate);
                cmd.Parameters.AddWithValue("@Discount", item.Discount);
                cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                cmd.Parameters.AddWithValue("@CreatedBy", "Admin");
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            
        }

        public List<Challan> ReadChallanReportDetailsByChallanId(int ChallanId)
        {
             cmd = new SqlCommand("proc_ReadChallanReportByChallanId", con);

            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Challan_Id",ChallanId);

            SqlDataReader dr = cmd.ExecuteReader();

            List<Challan> challan = new List<Challan>();

            while (dr.Read())
            {
                Challan Challan = new Challan();


                Challan.ChallanId = Convert.ToInt32(dr["ChallanId"]);
                Challan.CompanyName = dr["CompanyName"].ToString();
                Challan.CompanyAddress = dr["CompanyAddress"].ToString();
                Challan.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                Challan.CustomerName = dr["CustomerName"].ToString();
                Challan.CustomerAddress = dr["CustomerAddress"].ToString();
                Challan.TinNumber = Convert.ToInt32(dr["TinNumber"]);
                Challan.AmountWithoutTax = Convert.ToDecimal(dr["AmountWithoutTax"]);
                Challan.cst = Convert.ToDecimal(dr["cst"]);
                Challan.vat = Convert.ToDecimal(dr["vat"]);
                Challan.ecess = Convert.ToDecimal(dr["ecess"]);
                Challan.shecess = Convert.ToDecimal(dr["shecess"]);
                Challan.freight = Convert.ToDecimal(dr["freight"]);
                Challan.tcs = Convert.ToDecimal(dr["tcs"]);
                Challan.TaxAmount = Convert.ToDecimal(dr["TaxAmount"]);
                Challan.TotalAmount = Convert.ToDecimal(dr["TotalAmount"]);

                challan.Add(Challan);
            }
            con.Close();

            return challan;
        }
        
        public List<ChallanDetails> ReadChallanSubReportDetailsByChallanId(int ChallanId)
        {
            cmd = new SqlCommand("proc_ReadChallanSubReportByChallanId", con);

            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Challan_Id", ChallanId);

            SqlDataReader dr = cmd.ExecuteReader();

            List<ChallanDetails> challanDetails = new List<ChallanDetails>();

            while (dr.Read())
            {
                ChallanDetails ChallanDetails = new ChallanDetails();

                ChallanDetails.CategoryName = dr["CategoryName"].ToString();
                ChallanDetails.Rate=Convert.ToDecimal(dr["Rate"]);
                ChallanDetails.Discount=Convert.ToDecimal(dr["Discount"]);
                ChallanDetails.Quantity=Convert.ToDecimal(dr["Quantity"]);

                challanDetails.Add(ChallanDetails);
            }
            con.Close();

            return challanDetails;
        }



        public List<Bill> ReadChallanDetailsByCustomerId(int customerId,string  BillId,DateTime? FromDate,DateTime? ToDate)
        {
            cmd = new SqlCommand("ReadChallanDetailsByCustomerId", con);

            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Customer_Id", customerId);
            cmd.Parameters.AddWithValue("@BillId",BillId);
            cmd.Parameters.AddWithValue("@FromDate",FromDate);
            cmd.Parameters.AddWithValue("@ToDate",ToDate);


            SqlDataReader dr = cmd.ExecuteReader();

            List<Bill> Bill = new List<Bill>();

            while (dr.Read())
            {
                Bill bill = new Bill();
           

                bill.challan.ChallanId = Convert.ToInt32(dr["Challan_Id"]);
                bill.BillId = dr["Bill_Id"].ToString();
                bill.challan.CreatedOn = Convert.ToDateTime(dr["created_date"]);
                bill.customer.CustomerName = dr["Customer_Name"].ToString();
                bill.customer.CustomerAddress = dr["Customer_Address"].ToString();
                bill.challan.AmountWithoutTax = Convert.ToDecimal(dr["Amount_without_Tax"]);
                bill.challan.TaxAmount = Convert.ToDecimal(dr["Tax_Amount"]);
                bill.challan.TotalAmount = Convert.ToDecimal(dr["Total_Amount"]);

                Bill.Add(bill);
            }
            con.Close();

            return Bill;
        }

        public List<BillDetails> ReadChallanDetailsByChallanId(int ChallanId,string BillId)
        {
            cmd = new SqlCommand("Proc_ReadChallanDetailsByChallanId", con);

            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Challan_Id", ChallanId);
            cmd.Parameters.AddWithValue("@Bill_Id", BillId);


            SqlDataReader dr = cmd.ExecuteReader();

            List<BillDetails> Bill = new List<BillDetails>();

            while (dr.Read())
            {
                BillDetails billDetails = new BillDetails();

                billDetails.CategoryId = Convert.ToInt32(dr["Category_Id"]);
                billDetails.CategoryName = dr["Category_Name"].ToString();
                billDetails.CategoryQuantity = Convert.ToDecimal(dr["Quantity"]);
                billDetails.CustomerName = dr["Customer_Name"].ToString();
                billDetails.CustomerAddress = dr["Customer_Address"].ToString();
                billDetails.ChallanId = Convert.ToInt32(dr["Challan_Id"]);
                billDetails.BillId = dr["Bill_Id"].ToString();

                //if (dr["SubCategoryId"]!=DBNull.Value)
                //{
                //    billDetails.SubCategoryId =Convert.ToInt32(dr["SubCategoryId"]);
                //}
                //if (dr["Current_Stock"]!=DBNull.Value)
                //{
                //    billDetails.CurrentStock = Convert.ToDecimal(dr["Current_Stock"]);
                //}
               


                Bill.Add(billDetails);
            }
            con.Close();

            return Bill;
        }



        public void AddSubCategory(List<BillDetails> billdetails)
        {
            foreach (var item in billdetails)
            {
                cmd = new SqlCommand("proc_AddSubCategoryDetailsInChallan", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                cmd.Parameters.AddWithValue("@BillId", item.BillId);
                cmd.Parameters.AddWithValue("@ChallanId",item.ChallanId);
                cmd.Parameters.AddWithValue("@CategoryId",item.CategoryId);
                cmd.Parameters.AddWithValue("@SubCategoryId",item.SubCategoryId);
                cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                cmd.Parameters.AddWithValue("@CreatedBy", "Admin");
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                cmd.ExecuteNonQuery();
                con.Close();

                UpdateStock(item.CategoryId, item.SubCategoryId, item.Quantity);
            }
           
            
            
        }

        private void UpdateStock(int CategoryId, int SubCategoryId, decimal Quantity)
        {
            cmd = new SqlCommand("proc_UpdateStock", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();

            SqlParameter outparam = new SqlParameter("@OutStockId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            cmd.Parameters.AddWithValue("@CategoryId", CategoryId);
            cmd.Parameters.AddWithValue("@SubCategoryId", SubCategoryId);
            cmd.Parameters.AddWithValue("@Quantity", Quantity);
            cmd.Parameters.Add(outparam);
            cmd.ExecuteNonQuery();

            int StockId = int.Parse(outparam.Value.ToString());
            con.Close();

            InsertStockDetails(StockId, Quantity);
        }


        private void InsertStockDetails(int StockId, decimal Quantity)
        {
            cmd = new SqlCommand("proc_CreateStockDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();

            cmd.Parameters.AddWithValue("@StockId", StockId);
            cmd.Parameters.AddWithValue("@Quantity",Quantity);
            cmd.Parameters.AddWithValue("@isReceived", Convert.ToBoolean(0));
            cmd.Parameters.AddWithValue("@CreatedBy", "Admin");
            cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public decimal ReadStockByCategoryIdSubCategoryId(int categoryId, int SubCategoryId)
        {
            cmd = new SqlCommand("Proc_ReadStockByCategoryIdSubCategoryId", con);

            decimal Stock = 0;

            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@CategoryId", categoryId);
            cmd.Parameters.AddWithValue("@SubCategoryId", SubCategoryId);


            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {   
                Stock = Convert.ToDecimal(dr["Current_Stock"]);
            }

            con.Close();

            return Stock ;
        }

        public List<Challan> ReadBillDetailsByChallanIdBillId(int ChallanId)
        {
            cmd = new SqlCommand("proc_ReadBillReportByChallanIdBillId", con);

            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Challan_Id", ChallanId);

            SqlDataReader dr = cmd.ExecuteReader();

            List<Challan> challan = new List<Challan>();

            while (dr.Read())
            {
                Challan Challan = new Challan();


                Challan.ChallanId = Convert.ToInt32(dr["ChallanId"]);
                Challan.CompanyName = dr["CompanyName"].ToString();
                Challan.CompanyAddress = dr["CompanyAddress"].ToString();
                Challan.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                Challan.CustomerName = dr["CustomerName"].ToString();
                Challan.CustomerAddress = dr["CustomerAddress"].ToString();
                Challan.TinNumber = Convert.ToInt32(dr["TinNumber"]);
                Challan.AmountWithoutTax = Convert.ToDecimal(dr["AmountWithoutTax"]);
                Challan.cst = Convert.ToDecimal(dr["cst"]);
                Challan.vat = Convert.ToDecimal(dr["vat"]);
                Challan.ecess = Convert.ToDecimal(dr["ecess"]);
                Challan.shecess = Convert.ToDecimal(dr["shecess"]);
                Challan.freight = Convert.ToDecimal(dr["freight"]);
                Challan.tcs = Convert.ToDecimal(dr["tcs"]);
                Challan.TaxAmount = Convert.ToDecimal(dr["TaxAmount"]);
                Challan.TotalAmount = Convert.ToDecimal(dr["TotalAmount"]);

                challan.Add(Challan);
            }
            con.Close();

            return challan;
        }

        public List<ChallanDetails> ReadBillSubReportDetailsByChallanIdBillId(int ChallanId, string BillId)
        {
            cmd = new SqlCommand("proc_ReadBillSubReportByChallanIdBillId", con);

            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Challan_Id", ChallanId);
            cmd.Parameters.AddWithValue("@Bill_Id",BillId);


            SqlDataReader dr = cmd.ExecuteReader();

            List<ChallanDetails> challanDetails = new List<ChallanDetails>();

            while (dr.Read())
            {
                ChallanDetails ChallanDetails = new ChallanDetails();

                ChallanDetails.CategoryName = dr["CategoryName"].ToString();
                ChallanDetails.SubCategoryName=dr["SubCategoryName"].ToString();
                ChallanDetails.Rate = Convert.ToDecimal(dr["Rate"]);
                ChallanDetails.Discount = Convert.ToDecimal(dr["Discount"]);
                ChallanDetails.Quantity = Convert.ToDecimal(dr["Quantity"]);

                challanDetails.Add(ChallanDetails);
            }
            con.Close();

            return challanDetails;
        }
    }
}
