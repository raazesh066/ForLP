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
    public class TaxDAL
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
        SqlCommand cmd = null;

        public List<Tax> ReadAllTax()
        {
            cmd = new SqlCommand("proc_ReadAllTax", con);

            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            List<Tax> tax = new List<Tax>();

            while (dr.Read())
            {
                Tax Tax = new Tax();

                Tax.TaxId = Convert.ToInt32(dr["Tax_Id"]);
                Tax.TaxName = dr["Tax_Name"].ToString();
                Tax.Value = Convert.ToDecimal(dr["Tax_Value"]);
                tax.Add(Tax);
            }
            con.Close();
            return tax;


        }

        public void Create(Tax tax)
        {
            cmd = new SqlCommand("proc_CreateTax", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Tax_Name", tax.TaxName);
            cmd.Parameters.AddWithValue("@Value", tax.Value);
            cmd.Parameters.AddWithValue("@CreatedBy", tax.CreatedBy);
            cmd.Parameters.AddWithValue("@CreatedDate", tax.CreatedOn);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void Delete(int id)
        {
            cmd = new SqlCommand("proc_DeleteTax", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Tax_Id", id);
            cmd.ExecuteNonQuery();
            con.Close();
        }


        public Tax ReadTaxById(int id)
        {
            cmd = new SqlCommand("proc_ReadTaxbyId",con);

            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Tax_Id", id);

            SqlDataReader dr = cmd.ExecuteReader();

            Tax tax = new Tax();

            while (dr.Read())
            {
                tax.TaxId = Convert.ToInt32(dr["Tax_Id"]);
                tax.TaxName=dr["Tax_Name"].ToString();
                tax.Value = Convert.ToDecimal(dr["Tax_Value"]);

            }
            con.Close();
            return tax;

        }

        public void Edit(Tax tax)
        {
            cmd = new SqlCommand("proc_EditTax",con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Tax_Id",tax.TaxId);
            cmd.Parameters.AddWithValue("@Tax_Name", tax.TaxName);
            cmd.Parameters.AddWithValue("@Value", tax.Value);
            cmd.Parameters.AddWithValue("@ModifiedBy", tax.ModifiedBy);
            cmd.Parameters.AddWithValue("@ModifiedDate", tax.ModifiedOn);
            cmd.ExecuteNonQuery();
            con.Close();
        }




        //only cst and vat for customer tax type

        public List<Tax> ReadAllTaxes()
        {
            cmd = new SqlCommand("proc_ReadAllTaxes", con);

            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            List<Tax> tax = new List<Tax>();

            while (dr.Read())
            {
                Tax Tax = new Tax();

                Tax.TaxId = Convert.ToInt32(dr["Tax_Id"]);
                Tax.TaxName = dr["Tax_Name"].ToString();
                Tax.Value = Convert.ToDecimal(dr["Tax_Value"]);
                tax.Add(Tax);
            }
            con.Close();
            return tax;
        }
    }
}
