using System.Data;

using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using static System.Net.Mime.MediaTypeNames;

namespace SimpleLeads.Data
{
    public class SimpleLeadsDB
    {
        private readonly string _connectionString;

        public SimpleLeadsDB(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int AddListing(Listing listing)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO Listings (DateCreated, Text, PhoneNumber)
                                    VALUES (@DateCreated, @Text, @PhoneNumber)
                                    SELECT SCOPE_IDENTITY()";
            command.Parameters.AddWithValue("@DateCreated", listing.DateCreated);
            command.Parameters.AddWithValue("@Text", listing.Text);
            command.Parameters.AddWithValue("@PhoneNumber", listing.PhoneNumber);

            connection.Open();

            return (int)(decimal)command.ExecuteScalar();
        }

        public List<Listing> GetListings()
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = @"SELECT * FROM Listings
                                    ORDER BY DateCreated DESC";
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            var listings = new List<Listing>();

            while(reader.Read())
            {
                Listing listing = new Listing
                {
                    DateCreated = (DateTime)reader["DateCreated"],
                    Text = (string)reader["Text"],
                    PhoneNumber = (string)reader["PhoneNumber"],
                    Id = (int)reader["Id"]
                };
                listings.Add(listing);
            }
            return listings;
        }

        public void DeletLising(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = connection.CreateCommand();
            command.CommandText = @"DELETE FROM Listings
                                    WHERE Id = @Id";
            command.Parameters.AddWithValue("@Id", id);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
