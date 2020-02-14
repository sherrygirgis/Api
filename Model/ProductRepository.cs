using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Api.Model;
//Dapper CRUD in ASP.NET Core WEB API

namespace Api.Model
{
    public class ProductRepository
    {
        private string connectionString;
        public ProductRepository()
        {
            connectionString = @"Server = tcp:mysqlserver4536.database.windows.net,1433; Initial Catalog = productdb; Persist Security Info = False; User ID = 'AzureUser'; Password ='yso3_7yate'; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30";
        }
        public System.Data.IDbConnection connection
        {
            get
            {
                return new SqlConnection(connectionString);
            }


        }
        public void Add(Product prod)
        {
            using (IDbConnection dbConnection = connection)
            {
                string squery = @"INSERT INTO product(Vendor_UID,ID,Title,Description,Price,Dietary_flag)VALUES(@Vendor_UID,@ID,@Title,@Description,@Price,@Dietary_flag)";
                dbConnection.Open();
                dbConnection.Execute(squery, prod);
            }


        }
        public IEnumerable<Product> GetAll()
        {
            using (IDbConnection dbConnection = connection)
            {
                string squery = @"SELECT * FROM product";
                dbConnection.Open();
                return dbConnection.Query<Product>(squery);

            }
        }
        public Product GetById(int id)
        {
            using (IDbConnection dbConnection = connection)
            {
                string squery = @"SELECT * FROM product where ID=@ID";
                dbConnection.Open();
                return dbConnection.Query<Product>(squery, new { ID = id }).FirstOrDefault();
            }
        }
        public void Delete(int id)
        {
            using (IDbConnection dbConnection = connection)
            {
                string squery = @"DELETE FROM product where ID=@ID";
                dbConnection.Open();
                dbConnection.Execute(squery,new { ID=id});
            }
        }
        public void Update(Product prod)
        {
            using (IDbConnection dbConnection = connection)
            {
                string squery = @"UPDATE product SET Vendor_UID=@Vendor_UID, Title=@Title,Price=@Price,Dietary_flag=@Dietary_flag  where ID=@ID";
                dbConnection.Open();
                dbConnection.Query(squery, prod);
            }
        }
    }
}