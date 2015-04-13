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
    public class StockDAL
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
        SqlCommand cmd = null;

        public List<Stock> ReadAllStock()
        {
            cmd = new SqlCommand("proc_ReadAllStock", con);

            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            List<Stock> Stock = new List<Stock>();

            while (dr.Read())
            {
                Stock stock = new Stock();

                stock.StockId = Convert.ToInt32(dr["StockId"]);
                stock.Quantity = Convert.ToDecimal(dr["Current_Stock"]);
                stock.subcategory.category.CategoryId = Convert.ToInt32(dr["Category_Id"]);
                stock.subcategory.category.CategoryName = dr["Category_Name"].ToString();
                stock.subcategory.SubCategoryId = Convert.ToInt32(dr["SubCategoryId"]);
                stock.subcategory.SubCategoryName = dr["SubCategoryName"].ToString();

                Stock.Add(stock);
            }
            con.Close();
            return Stock;
        }

        public List<Stock> SetExcelData()
        {

            cmd = new SqlCommand("proc_SetExcelData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            List<Stock> stock = new List<Stock>();
            while (dr.Read())
            {
                Stock Stock = new Stock();

                Stock.subcategory.category.CategoryId = Convert.ToInt32(dr["Category_Id"]);
                Stock.subcategory.category.CategoryName = dr["Category_Name"].ToString();
                Stock.subcategory.SubCategoryId = Convert.ToInt32(dr["SubCategoryId"]);
                Stock.subcategory.SubCategoryName = dr["SubCategoryName"].ToString();
                Stock.subcategory.PackedQuantity = dr["Packed_Quantity"].ToString();
                Stock.subcategory.code = dr["Code"].ToString();
                stock.Add(Stock);

            }
            con.Close();
            return stock;

        }

        public void Create(Stock stock)
        {
            cmd = new SqlCommand("proc_CreateStock", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();

            SqlParameter outparam = new SqlParameter("@OutStockId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            cmd.Parameters.AddWithValue("@CategoryId", stock.subcategory.category.CategoryId);
            cmd.Parameters.AddWithValue("@SubCategoryId", stock.subcategory.SubCategoryId);
            cmd.Parameters.AddWithValue("@Quantity", stock.Quantity);
            cmd.Parameters.AddWithValue("@isReceived", stock.IsReceived);
            cmd.Parameters.AddWithValue("@CreatedBy", stock.CreatedBy);
            cmd.Parameters.AddWithValue("@CreatedDate", stock.CreatedOn);
            cmd.Parameters.Add(outparam);
            cmd.ExecuteNonQuery();

            int StockId = int.Parse(outparam.Value.ToString());
            con.Close();
            InsertStockDetails(StockId, stock);


        }

        private void InsertStockDetails(int StockId, Stock stock)
        {
            cmd = new SqlCommand("proc_CreateStockDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();

            cmd.Parameters.AddWithValue("@StockId", StockId);
            cmd.Parameters.AddWithValue("@Quantity", stock.Quantity);
            cmd.Parameters.AddWithValue("@isReceived", Convert.ToBoolean(1));
            cmd.Parameters.AddWithValue("@CreatedBy", "Admin");
            cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void DeleteStock(int id)
        {
            cmd = new SqlCommand("proc_DeleteStock", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Stock_Id", id);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void InsertExcelData(List<Stock> lst)
        {
            foreach (var item in lst)
            {
                cmd = new SqlCommand("proc_CreateStock", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                SqlParameter outparam = new SqlParameter("@OutStockId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                cmd.Parameters.AddWithValue("@CategoryId", item.subcategory.category.CategoryId);
                cmd.Parameters.AddWithValue("@SubCategoryId", item.subcategory.SubCategoryId);
                cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                cmd.Parameters.AddWithValue("@isReceived", Convert.ToBoolean(1));
                cmd.Parameters.AddWithValue("@CreatedBy", "Admin");
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                cmd.Parameters.Add(outparam);
                cmd.ExecuteNonQuery();

                int StockId = int.Parse(outparam.Value.ToString());
                con.Close();

                InsertStockDetails(StockId, item);
            }
            

        }

    }
}
