using AlShamil.Data.Interface;
using AlShamil.Model.Dto;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.Data
{
    public class ForgotPasswordData<T>:IForgotPasswordData<T> where T:class
    {
        private readonly string? _connectionString;
        public ForgotPasswordData(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AlShamilDBConnection");
        }
        public async Task<T> FindByEmailAsync(string email)
        {
            if (email == null)
            {
                throw new ArgumentNullException(nameof(email));
            }
            
            using(SqlConnection connection=new SqlConnection(_connectionString))
            {
                var qry = $"SELECT * FROM USER WHERE Email={email}";
                using (SqlCommand command = new SqlCommand("sp_GetUserByEmail", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@p_i_EmailAddress", email);

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
                    try
                    {
                        if (connection.State == ConnectionState.Closed)
                        {
                            await connection.OpenAsync();
                            SqlDataReader dataReader = await command.ExecuteReaderAsync();
                            while (await dataReader.ReadAsync())
                            {
                                UserDto userDto = new UserDto()
                                {
                                    Id = Convert.ToInt32(dataReader["Id"]),
                                    FirstName = dataReader["FirstName"].ToString(),
                                    LastName = dataReader["LastName"].ToString(),
                                    EmailAddress = dataReader["EmailAddress"].ToString(),
                                };
                                return userDto as T;
                            }
                            dataReader.Close();
                        }
                    }
                    catch
                    {
                        await connection.CloseAsync();
                        throw;
                    }
                    finally
                    {
                        if(connection.State== ConnectionState.Open)
                        {
                            await connection.CloseAsync();
                        }
                    }
                }
            }
            return null;
        }

        public async Task<bool> IsEmailConfirmedAsync(T user)
        {
            var userData=user as UserDto;
            using(SqlConnection  connection = new SqlConnection(_connectionString))
            {
                using(SqlCommand command=new SqlCommand("sp_GetUserById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@p_i_Id", userData.Id);

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

                    try
                    {
                        if(connection.State == ConnectionState.Closed)
                        {
                            await connection.OpenAsync();
                            var emailAddress =  command.ExecuteScalar();
                            if(emailAddress == userData.EmailAddress)
                            {
                                return true;
                            }
                        }
                    }
                    catch
                    {
                        throw;
                    }
                    finally
                    {
                        if(connection.State == ConnectionState.Open)
                        {
                            await connection.CloseAsync();
                        }
                    }
                } 
            }
            return false;
        }

        public async Task<bool> UpdatePasswordAsync(T user)
        {
            var userData = user as UserDto;
            using(var connection=new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_ResetPassword", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@p_i_Id", userData.Id);
                    command.Parameters.AddWithValue("@p_i_Password", userData.Password);

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

                    try
                    {
                        if (connection.State == ConnectionState.Closed)
                        {
                            await connection.OpenAsync();
                            int result = command.ExecuteNonQuery();
                            return result == 1;
                        }
                    }
                    catch
                    {
                        throw;
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open)
                            await connection.CloseAsync();
                    }
                }
            }
            return false;
        }
    }
}
