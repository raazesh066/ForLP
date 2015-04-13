using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOM;
using System.Data;

namespace DAL
{
    public class CustomerDAL
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
        SqlCommand cmd = null;


        public List<Customer> ReadAllCustomer()
        {
            cmd = new SqlCommand("proc_ReadAllCustomer", con);

            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            List<Customer> customer = new List<Customer>();

            while (dr.Read())
            {
                Customer Customer = new Customer();

                Customer.CustomerId = Convert.ToInt32(dr["Customer_Id"]);
                Customer.CustomerName = dr["Customer_Name"].ToString();
                Customer.CustomerAddress=dr["Customer_Address"].ToString();
                Customer.CustomerPhone = Convert.ToInt32(dr["Customer_Phone_No"]);
                Customer.TinNumber = Convert.ToInt32(dr["Tin_No"]);
                Customer.tax.TaxId = Convert.ToInt32(dr["Tax_Id"]);
                Customer.tax.TaxName = dr["Tax_Name"].ToString();
                Customer.tax.Value = Convert.ToDecimal(dr["Tax_Value"]);

                customer.Add(Customer);
            }
            con.Close();
            return customer;
        }

        public void Create(Customer customer)
        {
            cmd = new SqlCommand("proc_CreateCustomer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Customer_Name", customer.CustomerName);
            cmd.Parameters.AddWithValue("@Customer_Address", customer.CustomerAddress);
            cmd.Parameters.AddWithValue("@Customer_Phone", customer.CustomerPhone);
            cmd.Parameters.AddWithValue("@Tin_No", customer.TinNumber);
            cmd.Parameters.AddWithValue("@TaxId", customer.tax.TaxId);
            cmd.Parameters.AddWithValue("@CreatedBy", customer.CreatedBy);
            cmd.Parameters.AddWithValue("@CreatedDate", customer.CreatedOn);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void DeleteCustomer(int id)
        {
            cmd = new SqlCommand("proc_DeleteCustomer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Customer_Id", id);
            cmd.ExecuteNonQuery();
            con.Close();
        }


        public Customer ReadCustomerDetailsById(int id)
        {
            cmd = new SqlCommand("proc_ReadCustomerDetailsById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Customer_Id",id);
            SqlDataReader dr = cmd.ExecuteReader();

            Customer customer = new Customer();


            while (dr.Read())
            {
                customer.CustomerId = Convert.ToInt32(dr["Customer_Id"]);
                customer.CustomerName=dr["Customer_Name"].ToString();
                customer.CustomerAddress=dr["Customer_Address"].ToString();
                customer.CustomerPhone = Convert.ToInt32(dr["Customer_Phone_No"]);
                customer.TinNumber = Convert.ToInt32(dr["Tin_No"]);
                customer.tax.TaxId = Convert.ToInt32(dr["Tax_Id"]);
                customer.tax.TaxName=dr["Tax_Name"].ToString();
                customer.tax.Value = Convert.ToDecimal(dr["Tax_Value"]);
            }
            con.Close();
            return customer;

        }

        public void Edit(Customer cust)
        {
            cmd = new SqlCommand("proc_EditCustomer",con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Customer_Id",cust.CustomerId);
            cmd.Parameters.AddWithValue("@Customer_Name", cust.CustomerName);
            cmd.Parameters.AddWithValue("@Customer_Address", cust.CustomerAddress);
            cmd.Parameters.AddWithValue("@Customer_Phone", cust.CustomerPhone);
            cmd.Parameters.AddWithValue("@Tin_No", cust.TinNumber);
            cmd.Parameters.AddWithValue("@TaxId", cust.tax.TaxId);
            cmd.Parameters.AddWithValue("@ModifiedBy", cust.ModifiedBy);
            cmd.Parameters.AddWithValue("@ModifiedDate", cust.ModifiedOn);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
