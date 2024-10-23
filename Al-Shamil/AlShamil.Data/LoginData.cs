using AlShamil.Data.Interface;
using AlShamil.Model.Dto;
using AlShamil.Model.Request;
using AlShamil.Model.Response;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.Data
{
    public class LoginData<T>:ILoginData<T>
    {
        private readonly string? _connectionString;
        public LoginData(IConfiguration connectionString)
        {
            _connectionString = connectionString.GetConnectionString("AlShamilDBConnection");
        }
        
        public async Task<UserDto> CheckUserCredentialsAsync(T credentials)
        {

            var login = credentials as LoginDto;

            //IEnumerable<UserDto> users = (IEnumerable<UserDto>)await _userData.GetAllUsersAsync();
            //UserDto userDto1 = users.FirstOrDefault(x => x.EmailAddress == login.Email && x.Password == login.Password);
            //return userDto1;

            using (var connection = new SqlConnection(_connectionString))
            {
                try
                {
                    using (var command = new SqlCommand("sp_CheckUserCrendentials", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@p_i_Email", login.Email);
                        command.Parameters.AddWithValue("@p_i_Passowrd", login.Password);

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
                            using (SqlDataReader dataReader = command.ExecuteReader())
                            {
                                while (await dataReader.ReadAsync())
                                {
                                    UserDto userDto = new UserDto
                                    {
                                        Id = Convert.ToInt32(dataReader["Id"]),
                                        FirstName = dataReader["FirstName"].ToString(),
                                        LastName = dataReader["LastName"].ToString(),
                                        EmailAddress = dataReader["EmailAddress"].ToString(),
                                        Password = dataReader["Password"].ToString(),
                                        PhoneNumber = dataReader["PhoneNumber"].ToString(),
                                        RoleId = Convert.ToInt32(dataReader["RoleId"]),
                                        //RoleName = dataReader["RoleName"].ToString()
                                    };
                                    return userDto;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching users: {ex.Message}");
                    return null;
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        await connection.CloseAsync();
                    }
                }

            }
            return null;
        }
    }
}
