using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOM;
using System.Configuration;
using System.Data;

namespace DAL
{
    public class SubCategoryDAL
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
        SqlCommand cmd = null;


        public List<SubCategory> ReadAllSubCategory()
        {
            cmd = new SqlCommand("proc_ReadAllSubCategory", con);

            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            List<SubCategory> subCategory = new List<SubCategory>();

            while (dr.Read())
            {
                SubCategory SubCategory = new SubCategory();
                SubCategory.SubCategoryId = Convert.ToInt32(dr["SubCategoryId"]);
                SubCategory.SubCategoryName = dr["SubCategoryName"].ToString();
                SubCategory.category.CategoryId = Convert.ToInt32(dr["Category_Id"]);
                SubCategory.category.CategoryName = dr["Category_Name"].ToString();
                SubCategory.PackedQuantity = dr["Packed_Quantity"].ToString();
                SubCategory.code=dr["Code"].ToString();
                SubCategory.Rate = Convert.ToDecimal(dr["Rate"]);

                subCategory.Add(SubCategory);
            }
            con.Close();
            return subCategory;
        }

        public void Create(SubCategory subCategory)
        {
            cmd = new SqlCommand("proc_CreateSubCategory", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@CategoryId", subCategory.category.CategoryId);
            cmd.Parameters.AddWithValue("@SubCategory_Name", subCategory.SubCategoryName);

            if (subCategory.PackedQuantity != null)
            {
                cmd.Parameters.AddWithValue("@Packed_Quantity", subCategory.PackedQuantity);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Packed_Quantity", DBNull.Value);
            }

            if (subCategory.code != null)
            {
                cmd.Parameters.AddWithValue("@Code", subCategory.code);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Code", DBNull.Value);
            }

            if (subCategory.Rate != null)
            {
                cmd.Parameters.AddWithValue("@Rate", subCategory.Rate);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Rate", DBNull.Value);
            }

            cmd.Parameters.AddWithValue("@CreatedBy", subCategory.CreatedBy);
            cmd.Parameters.AddWithValue("@CreatedDate", subCategory.CreatedOn);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void DeleteSubcategory(int id)
        {
            cmd = new SqlCommand("proc_DeleteSubCategory", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@SubCategory_Id", id);
            cmd.ExecuteNonQuery();
            con.Close();
        }


        public List<SubCategory> ReadSubCategoryByCategory(int categoryid)
        {
            cmd = new SqlCommand("proc_ReadSubCategoryByCategory", con);

            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Category_Id",categoryid);

            SqlDataReader dr = cmd.ExecuteReader();

            List<SubCategory> subCategory = new List<SubCategory>();

            while (dr.Read())
            {
                SubCategory SubCategory = new SubCategory();
                SubCategory.SubCategoryId = Convert.ToInt32(dr["SubCategoryId"]);
                SubCategory.SubCategoryName = dr["SubCategoryName"].ToString();
                
                subCategory.Add(SubCategory);
            }
            con.Close();

            return subCategory;
        }

        public SubCategory ReadSubCategoryDetailsById(int id)
        {
            cmd = new SqlCommand("proc_ReadSubCategoryDetailsbyId", con);

            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@SubCategory_Id", id);

            SqlDataReader dr = cmd.ExecuteReader();

            SubCategory subcategory = new SubCategory();

            while (dr.Read())
            {
                subcategory.category.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                subcategory.category.CategoryName = dr["Category_Name"].ToString();
                subcategory.SubCategoryId = Convert.ToInt32(dr["SubCategoryId"]);
                subcategory.SubCategoryName = dr["SubCategoryName"].ToString();
                subcategory.PackedQuantity = dr["Packed_Quantity"].ToString();
                subcategory.code=dr["Code"].ToString();
                subcategory.Rate = Convert.ToDecimal(dr["Rate"]);
             
            }
            con.Close();
            return subcategory;
        }

        public void Edit(SubCategory subcategory)
        {
            cmd = new SqlCommand("proc_EditSubCategory", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();

            cmd.Parameters.AddWithValue("@Category_Id", subcategory.category.CategoryId);
            cmd.Parameters.AddWithValue("@SubCategory_Id", subcategory.SubCategoryId);
            cmd.Parameters.AddWithValue("@SubCategory_Name", subcategory.SubCategoryName);

            if (subcategory.PackedQuantity != null)
            {
                cmd.Parameters.AddWithValue("@Packed_Quantity", subcategory.PackedQuantity);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Packed_Quantity", DBNull.Value);
            }

            if (subcategory.code != null)
            {
                cmd.Parameters.AddWithValue("@Code", subcategory.code);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Code", DBNull.Value);
            }

            if (subcategory.Rate != null)
            {
                cmd.Parameters.AddWithValue("@Rate", subcategory.Rate);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Rate", DBNull.Value);
            }

            cmd.Parameters.AddWithValue("@ModifiedBy", subcategory.ModifiedBy);
            cmd.Parameters.AddWithValue("@ModifiedDate", subcategory.ModifiedOn);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
