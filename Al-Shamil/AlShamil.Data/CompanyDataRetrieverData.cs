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
    public class CompanyDataRetrieverData<T>:ICompanyDataRetrieverData<T>where T:class
    {
        private readonly string? _connectionString;
        public CompanyDataRetrieverData(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("AlShamilDBConnection");
        }
        public async Task<T> GetCompanyMasterData()
        {
            using(var connection=new SqlConnection(_connectionString))
            {
                using(var command=new SqlCommand("d",connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter statusCodeParam = new SqlParameter("@p_o_StatusCode", SqlDbType.VarChar, 10);
                    statusCodeParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(statusCodeParam);

                    SqlParameter statusDescParam = new SqlParameter("@p_o_StatusDesc", SqlDbType.VarChar, 100);
                    statusDescParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(statusDescParam);
                    try
                    {
                        if (connection.State == ConnectionState.Closed)
                        {
                            await connection.OpenAsync();
                            SqlDataReader dataReader = command.ExecuteReader();
                            CompanyMasterDataDto masterDataDto = new();
                            List<CountryDto> countryDtos = new List<CountryDto>();
                            while (dataReader.Read())
                            {
                                CountryDto countryDto = new()
                                {
                                    Id = Convert.ToInt32(dataReader["Id"]),
                                    Name = dataReader["Name"].ToString(),
                                    
                                };
                                countryDtos.Add(countryDto);
                            }
                            masterDataDto.Country=countryDtos;

                            await dataReader.NextResultAsync();

                            List<StateDto> states = new List<StateDto>();
                            while(dataReader.Read())
                            {
                                StateDto stateDto = new()
                                {
                                    Id= Convert.ToInt32(dataReader["Id"]),
                                    Name = dataReader["Name"].ToString(),
                                    CountryId = Convert.ToInt32(dataReader["CountryId"]),
                                    
                                };
                                states.Add(stateDto);
                            }
                            masterDataDto.State=states;

                            await dataReader.NextResultAsync();

                            List<CurrencyDto> currencyDtos = new List<CurrencyDto>();
                            while(!dataReader.Read())
                            {
                                CurrencyDto currencyDto = new()
                                {
                                    Id = Convert.ToInt32(dataReader["Id"]),
                                    Name = dataReader["Name"].ToString(),
                                };
                                currencyDtos.Add(currencyDto);
                            }
                            masterDataDto.Currency=currencyDtos;

                            await dataReader.CloseAsync();

                            return masterDataDto as T;
                        }
                    }
                    catch
                    {
                        throw;
                    }
                    finally
                    {
                        if(connection.State==ConnectionState.Open)
                            await connection.CloseAsync();
                    }

                }
            }
            return null;
        }
    }
}
