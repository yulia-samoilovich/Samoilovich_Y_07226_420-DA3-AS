using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Samoilovich_Y_07226_420_DA3_AS.Utils;

namespace Samoilovich_Y_07226_420_DA3_AS.Models
{
    internal class Customer:IModel<Customer>
    {
        private static readonly string DATABASE_TABLE_NAME = "dbo.Customer";

        private string _firstName;
        private string _lastName;       
        private string _email;

        public int Id { get; set; }

        public string FirstName
        {
            get { return this._firstName; }
            set
            {
                if(value.Length > 50)
                {
                    throw new Exception($"Field validation exception: length of value " +
                        $"for{this.GetType().FullName}. FirstName must be less or equal " +
                        $"to 50 characters. Received: {value.Length}.");
                }
                this._firstName = value;
            }
        }
        public string LastName
        {
            get { return this._lastName; }
            set
            {
                if (value.Length > 50)
                {
                    throw new Exception($"Field validation exception: lenght of value " +
                        $"for {this.GetType().FullName}.LastName must be less of equal " +
                        $"to 50 characters. Recieved: {value.Length}.");
                }
                this._lastName = value;
            }
        }
        public string Email
        {
            get { return this._email; }
            set
            {
                if(value.Length > 120)
                {
                    throw new Exception($"Field validation exception: length of value " +
                        $"for {this.GetType().FullName}.Email must be less or equal " +
                        $"to 128 characters. Received: {value.Length}.");
                }
                this._email = value;
            }
        }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public Customer(int id)
        {
            this.Id = id;
        }
        public Customer(string email): this(email, "", "")
        {

        }
        public Customer(string email, string firstName, string lastName)
        {
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
        }
        public static Customer GetById(int id)
        {
            Customer customer = new Customer(id);
            customer.GetById();
            return customer;
        }
        public void Delete()
        {
            if(this.Id == 0)
            {
                throw new Exception($"Cannot use method{this.GetType().FullName}.Delete() : Id value is 0.");
            }
            using (SqlConnection connection = DbUtils.GetDefaultConnection())
            {
                string statement = $"DELETE FROM {DATABASE_TABLE_NAME} WHERE Id = @id;";
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = statement;

                SqlParameter param = cmd.CreateParameter();
                param.ParameterName = "@id";
                param.DbType = DbType.Int32;
                param.Value = this.Id;
                cmd.Parameters.Add(param);

                int affectedRows = cmd.ExecuteNonQuery();

                if(!(affectedRows > 0))
                {
                    throw new Exception($"Failed to delete {this.GetType().FullName}: No database entry found for Id# {this.Id}.");
                }
            }
        }
        public Customer GetById()
        {
            if(this.Id == 0)
            {
                throw new Exception($"Cannot use methos {this.GetType().FullName}.GetById() : Id value is 0.");
            }
            using (SqlConnection connection = DbUtils.GetDefaultConnection())
            {
                string statement = $"SELECT * FROM {DATABASE_TABLE_NAME} WHERE Id = @id;";
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText =statement;

                SqlParameter param = cmd.CreateParameter();
                param.ParameterName = "@id";
                param.DbType= DbType.Int32;
                param.Value = this.Id;
                cmd.Parameters.Add(param);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();

                    if (!reader.IsDBNull(1))
                    {
                        this.FirstName = reader.GetString(1);
                    }
                    if (!reader.IsDBNull(2))
                    {
                        this.LastName = reader.GetString(2);
                    }
                    this.Email = reader.GetString(3);
                    this.CreatedAt = reader.GetDateTime(4);

                    if (!reader.IsDBNull(5))
                    {
                        this.DeletedAt = reader.GetDateTime(5);
                    }
                    return this;
                }
                else
                {
                    throw new Exception($"No database entry for {this.GetType().FullName} with id# {this.Id}.";
                }
            }
        }
        public Customer Insert()
        {
            if(this.Id > 0)
            {
                throw new Exception($"Cannot use method {this.GetType().FullName}.Insert() : Id value is not 0 [{this.Id}].");
            }
            using (SqlConnection connection = DbUtils.GetDefaultConnection())
            {
                DateTime createTime = DateTime.Now;

                string statement = $"INSERT INTO {DATABASE_TABLE_NAME} (firstName, lastName, email, createdAt) " +
                    "VALUES (@firstName, @lastName, @email, @createdAt); SELECT CAST(SCOPE_IDENTITY() AS int;)";
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = statement;

                SqlParameter firstNameParam = cmd.CreateParameter();
                firstNameParam.ParameterName = "@firstName";
                firstNameParam.DbType = DbType.String;
                if (String.IsNullOrEmpty(this.FirstName))
                {
                    firstNameParam.Value = DBNull.Value;
                }
                else
                {
                    firstNameParam.Value = this.FirstName;
                }
                cmd.Parameters.Add(firstNameParam);

                SqlParameter lastNameParam = cmd.CreateParameter();
                lastNameParam.ParameterName = "@lastName";
                lastNameParam.DbType = DbType.String;
                if (String.IsNullOrEmpty(this.LastName))
                {
                    lastNameParam.Value = DBNull.Value;
                }
                else
                {
                    lastNameParam.Value = this.LastName;
                }
                cmd.Parameters.Add(lastNameParam);

            }
        }
        public Customer Update()
        {

        }
    
    }
}
