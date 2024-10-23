using AlShamil.Data.Interface;
using AlShamil.Model.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AlShamil.Data
{
    public class UserData<T> : IUserData<T>
    {
        private readonly string? _connectionString;
        //private readonly IDbConnectionFactory _dbconnectionFactory;

        public UserData(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AlShamilDBConnection");
            //_dbconnectionFactory = dbConnectionFactory;
        }
        

        public async Task<IEnumerable<T>?> GetAllUsersAsync()
        {
            List<UserDto> userDtos = new List<UserDto>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("sp_GetUsers", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        var paramStatusCode = new SqlParameter("@p_o_StatusCode", SqlDbType.VarChar, 10)
                        {
                            Direction = ParameterDirection.Output
                        };
                        var paramStatusDesc = new SqlParameter("@p_o_StatusDesc", SqlDbType.VarChar, 100)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(paramStatusCode);
                        command.Parameters.Add(paramStatusDesc);

                        if (connection.State == ConnectionState.Closed)
                        {
                            await connection.OpenAsync();

                            using (var dataReader = await command.ExecuteReaderAsync())
                            {
                                while (await dataReader.ReadAsync())
                                {
                                    UserDto userDto = new UserDto
                                    {
                                        Id = Convert.ToInt32(dataReader["Id"]),
                                        Guid = dataReader["Guid"].ToString(),
                                        FirstName = dataReader["FirstName"].ToString(),
                                        LastName = dataReader["LastName"].ToString(),
                                        EmailAddress = dataReader["EmailAddress"].ToString(),
                                        Password = dataReader["Password"].ToString(),
                                        PhoneNumber = dataReader["PhoneNumber"].ToString(),
                                        RoleId = Convert.ToInt32(dataReader["RoleId"]),
                                        //CreatedOn = Convert.ToDateTime(dataReader["CreatedOn"]),
                                        //CreatedBy = dataReader["CreatedBy"].ToString(),
                                        //ModifiedOn = Convert.ToDateTime(dataReader["ModifiedOn"]),
                                        //ModifiedBy = dataReader["ModifiedBy"].ToString(),
                                        //IsActive = Convert.ToBoolean(dataReader["IsActive"])
                                    };

                                    userDtos.Add(userDto);
                                }
                            }
                            string? statusCode = Convert.ToString(command.Parameters["@p_o_StatusCode"].Value);
                            string? statusDesc = Convert.ToString(command.Parameters["@p_o_StatusDesc"].Value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching users: {ex.Message}");
                throw;
            }

            return userDtos as IEnumerable<T>;
        }
    }
}
