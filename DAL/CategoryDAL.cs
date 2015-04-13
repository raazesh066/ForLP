using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DOM;

namespace DAL
{
    public class CategoryDAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
        SqlCommand cmd = null;

        public List<Category> ReadAllCategory()
        {
            cmd = new SqlCommand("proc_ReadAllCategory", con);

            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            List<Category> category = new List<Category>();

            while (dr.Read())
            {
                Category Category = new Category();

                Category.CategoryId = Convert.ToInt32(dr["Category_Id"]);
                Category.CategoryName = dr["Category_Name"].ToString();
                Category.Rate = Convert.ToDecimal(dr["Rate"]);
                Category.Discount = Convert.ToDecimal(dr["Discount"]);
                category.Add(Category);
            }
            con.Close();
            return category;
        }

        public void Create(Category category)
        {
            cmd = new SqlCommand("proc_CreateCategory", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Category_Name", category.CategoryName);
            cmd.Parameters.AddWithValue("@Rate", category.Rate);
            cmd.Parameters.AddWithValue("@Discount",category.Discount);
            cmd.Parameters.AddWithValue("@CreatedBy", category.CreatedBy);
            cmd.Parameters.AddWithValue("@CreatedDate", category.CreatedOn);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void DeleteCategory(int id)
        {
            cmd = new SqlCommand("proc_DeleteCategory", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Category_Id", id);
            cmd.ExecuteNonQuery();
            con.Close();
        }

      

        public Category ReadCategoryDetailsById(int id)
        {
            cmd = new SqlCommand("proc_ReadCategoryDetailsbyId", con);

            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Category_Id",id);

            SqlDataReader dr = cmd.ExecuteReader();

            Category category = new Category();

            while (dr.Read())
            {
                category.CategoryId = Convert.ToInt32(dr["Category_Id"]);
                category.CategoryName = dr["Category_Name"].ToString();
                category.Rate = Convert.ToDecimal(dr["Rate"]);
                category.Discount = Convert.ToDecimal(dr["Discount"]);
            }
            con.Close();
            return category;
        }

        public void Edit(Category category)
        {
            cmd = new SqlCommand("proc_EditCategory", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();

            cmd.Parameters.AddWithValue("@Category_Id",category.CategoryId);
            cmd.Parameters.AddWithValue("@Category_Name", category.CategoryName);
            cmd.Parameters.AddWithValue("@Rate", category.Rate);
            cmd.Parameters.AddWithValue("@Discount",category.Discount);
            cmd.Parameters.AddWithValue("@ModifiedBy", category.ModifiedBy);
            cmd.Parameters.AddWithValue("@ModifiedDate", category.ModifiedOn);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
