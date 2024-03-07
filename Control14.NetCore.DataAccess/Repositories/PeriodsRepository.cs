using Control14.NetCore.Domain;
using Control14.NetCore.Domain.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Design;

namespace Control14.NetCore.DataAccess.Repositories
{
    public class PeriodsRepository : IPeriodsRepository
    {
        private readonly string _connectionString;
        public PeriodsRepository(IOptions<E_ConnectionStrings> connectionString)
        {
            _connectionString = connectionString.Value.Sql;
        }


        public async Task<PeriodsCreateResponse> Create(PeriodsCreateRequest request)
        {
            var operationResult = new PeriodsCreateResponse();
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    using (var cmd = new SqlCommand("USP_PeriodsInsert", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros de entrada
                        cmd.Parameters.AddWithValue("@IdCompany", request.IdCompany);
                        cmd.Parameters.AddWithValue("@IYear", request.IYear);
                        cmd.Parameters.AddWithValue("@IMonth", request.IMonth ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CreatedBy", request.CreatedBy);
                        cmd.Parameters.AddWithValue("@IpAddress", request.IpAddress);

                        // Parámetros de salida
                        var iRptaParam = new SqlParameter("@iRpta", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        var sRptaParam = new SqlParameter("@sRpta", SqlDbType.VarChar, -1) { Direction = ParameterDirection.Output };
                        var bRptaParam = new SqlParameter("@bRpta", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                        cmd.Parameters.Add(iRptaParam);
                        cmd.Parameters.Add(sRptaParam);
                        cmd.Parameters.Add(bRptaParam);

                        // Abrir la conexión y ejecutar el procedimiento almacenado
                        await cn.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();

                        // Obtener los resultados de los parámetros de salida
                        operationResult.IdPeriod = iRptaParam.Value != DBNull.Value ? Convert.ToInt32(iRptaParam.Value) : 0;
                        operationResult.Message = sRptaParam.Value != DBNull.Value ? Convert.ToString(sRptaParam.Value) : string.Empty;
                        operationResult.Success = bRptaParam.Value != DBNull.Value && Convert.ToBoolean(bRptaParam.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                operationResult.IdPeriod = 0;
                operationResult.Message = "Error creating period: " + ex.Message;
                operationResult.Success = false;
            }
            return operationResult;
        }

        public async Task<PeriodsUpdateResponse> Update(PeriodsUpdateRequest request)
        {
            var operationResult = new PeriodsUpdateResponse();
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    using (var cmd = new SqlCommand("USP_PeriodsUpdate", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@IdCompany", request.IdCompany);
                        cmd.Parameters.AddWithValue("@IdPeriod", request.IdPeriod);
                        cmd.Parameters.AddWithValue("@IYear", request.IYear);
                        cmd.Parameters.AddWithValue("@IMonth", request.IMonth ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@UpdatedBy", request.UpdatedBy);
                        cmd.Parameters.AddWithValue("@IpAddress", request.IpAddress);

                        // Parámetros de salida
                        var iRptaParam = new SqlParameter("@iRpta", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        var sRptaParam = new SqlParameter("@sRpta", SqlDbType.VarChar, -1) { Direction = ParameterDirection.Output };
                        var bRptaParam = new SqlParameter("@bRpta", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                        cmd.Parameters.Add(iRptaParam);
                        cmd.Parameters.Add(sRptaParam);
                        cmd.Parameters.Add(bRptaParam);

                        // Abrir la conexión y ejecutar el procedimiento almacenado
                        await cn.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();

                        // Recuperar los resultados de los parámetros de salida
                        operationResult.Rows = iRptaParam.Value != DBNull.Value ? Convert.ToInt32(iRptaParam.Value) : 0;
                        operationResult.Message = sRptaParam.Value != DBNull.Value ? Convert.ToString(sRptaParam.Value) : string.Empty;
                        operationResult.Success = bRptaParam.Value != DBNull.Value && Convert.ToBoolean(bRptaParam.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                operationResult.Rows = 0;
                operationResult.Message = "Error updating period: " + ex.Message;
                operationResult.Success = false;
            }
            return operationResult;
        }
        public async Task<PeriodsDeleteResponse> Delete(PeriodsDeleteRequest request)
        {
            throw new NotImplementedException();
        }
        public async Task<PeriodsDeleteResponse> Delete(int idPeriod)
        {
            var operationResult = new PeriodsDeleteResponse();
            try
            {
                using (SqlConnection cn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("USP_PeriodsDelete", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdPeriod", idPeriod);
                        var iRptaParam = new SqlParameter("@iRpta", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        var sRptaParam = new SqlParameter("@sRpta", SqlDbType.VarChar, -1) { Direction = ParameterDirection.Output };
                        var bRptaParam = new SqlParameter("@bRpta", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                        cmd.Parameters.Add(iRptaParam);
                        cmd.Parameters.Add(sRptaParam);
                        cmd.Parameters.Add(bRptaParam);

                        await cn.OpenAsync();
                        await cmd.ExecuteNonQueryAsync(); // Execute the command

                        operationResult.Rows = iRptaParam.Value != DBNull.Value ? Convert.ToInt32(iRptaParam.Value) : 0; // Number of rows affected
                        operationResult.Message = sRptaParam.Value != DBNull.Value ? Convert.ToString(sRptaParam.Value) : string.Empty; // Response message
                        operationResult.Success = bRptaParam.Value != DBNull.Value && Convert.ToBoolean(bRptaParam.Value); // Success flag
                    }
                }
            }
            catch (Exception ex)
            {
                operationResult.Rows = 0;
                operationResult.Message = "Error deleting period: " + ex.Message;
                operationResult.Success = false;
            }
            return operationResult;
        }

        public async Task<IEnumerable<PeriodsReadResponse>> GetAll(int companyId)
        {
            List<PeriodsReadResponse> lista = new List<PeriodsReadResponse>();
            try
            {
                using (SqlConnection cn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("USP_PeriodsSelectAllByCompany", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CompanyId", companyId);
                        await cn.OpenAsync();

                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            while (await dr.ReadAsync())
                            {
                                lista.Add(new PeriodsReadResponse
                                {
                                    IdCompany = dr["IdCompany"] is DBNull ? 0 : Convert.ToInt32(dr["IdCompany"]),
                                    IdPeriod = dr["IdPeriod"] is DBNull ? 0 : Convert.ToInt32(dr["IdPeriod"]),
                                    IYear = dr["IYear"] is DBNull ? 0 : Convert.ToInt32(dr["IYear"]),
                                    IMonth = dr["IMonth"] is DBNull ? null : (int?)Convert.ToInt32(dr["IMonth"]),
                                    DateStart = dr["DateStart"] is DBNull ? null : (DateTime?)dr["DateStart"],
                                    DateEnd = dr["DateEnd"] is DBNull ? null : (DateTime?)dr["DateEnd"],
                                    PeriodCode = dr["PeriodCode"] is DBNull ? null : dr["PeriodCode"].ToString(),
                                    IdStatus = dr["IdStatus"] is DBNull ? null : (int?)Convert.ToInt32(dr["IdStatus"]),
                                    CreatedAt = dr["CreatedAt"] is DBNull ? null : (DateTime?)dr["CreatedAt"],
                                    CreatedBy = dr["CreatedBy"] is DBNull ? null : dr["CreatedBy"].ToString(),
                                    UpdatedAt = dr["UpdatedAt"] is DBNull ? null : (DateTime?)dr["UpdatedAt"],
                                    UpdatedBy = dr["UpdatedBy"] is DBNull ? null : dr["UpdatedBy"].ToString(),
                                    IpAddress = dr["IpAddress"] is DBNull ? null : dr["IpAddress"].ToString(),
                                    ClosedAt = dr["ClosedAt"] is DBNull ? null : (DateTime?)dr["ClosedAt"],
                                    ClosedBy = dr["ClosedBy"] is DBNull ? null : dr["ClosedBy"].ToString(),
                                    StatusName = dr["StatusName"] is DBNull ? null : dr["StatusName"].ToString(),
                                    MonthName = dr["MonthName"] is DBNull ? null : dr["MonthName"].ToString()
                                });
                            }
                        }
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving all periods for company: " + ex.Message);
            }
        }

        public Task<IEnumerable<PeriodsReadResponse>> GetAll(PeriodsReadRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<PeriodsReadResponse> GetById(int idPeriod)
        {
            PeriodsReadResponse result = new PeriodsReadResponse();
            try
            {
                using (SqlConnection cn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("USP_PeriodsById", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdPeriod", idPeriod);
                        await cn.OpenAsync();

                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            if (await dr.ReadAsync())
                            {
                                result = new PeriodsReadResponse
                                {
                                    IdCompany = dr["IdCompany"] is DBNull ? 0 : Convert.ToInt32(dr["IdCompany"]),
                                    IdPeriod = dr["IdPeriod"] is DBNull ? 0 : Convert.ToInt32(dr["IdPeriod"]),
                                    IYear = dr["IYear"] is DBNull ? 0 : Convert.ToInt32(dr["IYear"]),
                                    IMonth = dr["IMonth"] is DBNull ? null : (int?)Convert.ToInt32(dr["IMonth"]),
                                    DateStart = dr["DateStart"] is DBNull ? null : (DateTime?)dr["DateStart"],
                                    DateEnd = dr["DateEnd"] is DBNull ? null : (DateTime?)dr["DateEnd"],
                                    PeriodCode = dr["PeriodCode"] is DBNull ? null : dr["PeriodCode"].ToString(),
                                    IdStatus = dr["IdStatus"] is DBNull ? null : (int?)Convert.ToInt32(dr["IdStatus"]),
                                    CreatedAt = dr["CreatedAt"] is DBNull ? null : (DateTime?)dr["CreatedAt"],
                                    CreatedBy = dr["CreatedBy"] is DBNull ? null : dr["CreatedBy"].ToString(),
                                    UpdatedAt = dr["UpdatedAt"] is DBNull ? null : (DateTime?)dr["UpdatedAt"],
                                    UpdatedBy = dr["UpdatedBy"] is DBNull ? null : dr["UpdatedBy"].ToString(),
                                    IpAddress = dr["IpAddress"] is DBNull ? null : dr["IpAddress"].ToString(),
                                    ClosedAt = dr["ClosedAt"] is DBNull ? null : (DateTime?)dr["ClosedAt"],
                                    ClosedBy = dr["ClosedBy"] is DBNull ? null : dr["ClosedBy"].ToString()
                                };
                            }
                        }
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving period by id: " + ex.Message);
            }
        }

        public Task<PeriodsReadResponse> GetById(PeriodsReadRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResultBase> UpdateStatus(PeriodsUpdateStatusRequest request)
        {
            var operationResult = new OperationResultBase();
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    using (var cmd = new SqlCommand("USP_PeriodsUpdateStatus", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros de entrada
                        cmd.Parameters.AddWithValue("@IdPeriod", request.IdPeriod);
                        cmd.Parameters.AddWithValue("@CompanyId", request.CompanyId);
                        //cmd.Parameters.AddWithValue("@IYear", request.IYear);
                        //cmd.Parameters.AddWithValue("@IMonth", request.IMonth ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@UpdatedBy", request.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Action", request.Action);
                        //cmd.Parameters.AddWithValue("@IpAddress", request.IpAddress);

                        // Parámetros de salida
                        var iRptaParam = new SqlParameter("@iRpta", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        var sRptaParam = new SqlParameter("@sRpta", SqlDbType.VarChar, -1) { Direction = ParameterDirection.Output };
                        var bRptaParam = new SqlParameter("@bRpta", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                        cmd.Parameters.Add(iRptaParam);
                        cmd.Parameters.Add(sRptaParam);
                        cmd.Parameters.Add(bRptaParam);

                        // Abrir la conexión y ejecutar el procedimiento almacenado
                        await cn.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();

                        // Obtener los resultados de los parámetros de salida
                        operationResult.Rows = iRptaParam.Value != DBNull.Value ? Convert.ToInt32(iRptaParam.Value) : 0;
                        operationResult.Message = sRptaParam.Value != DBNull.Value ? Convert.ToString(sRptaParam.Value) : string.Empty;
                        operationResult.Success = bRptaParam.Value != DBNull.Value && Convert.ToBoolean(bRptaParam.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                operationResult.Rows = 0;
                operationResult.Message = "Error status period: " + ex.Message;
                operationResult.Success = false;
            }
            return operationResult;
        }


        public async Task<OperationResultBase> UpdateStatusProgramming(PeriodsUpdateStatusProgrammingRequest request)
        {
            var operationResult = new OperationResultBase();
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    using (var cmd = new SqlCommand("USP_PeriodsUpdateStatusProgramming", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros de entrada
                        cmd.Parameters.AddWithValue("@CompanyId", request.CompanyId);                  
                        cmd.Parameters.AddWithValue("@DateClosingStart", request.DateClosingStart);
                        cmd.Parameters.AddWithValue("@DateClosingEnd", request.DateClosingEnd);
                        cmd.Parameters.AddWithValue("@UpdatedBy", request.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Action", request.Action);
                        //cmd.Parameters.AddWithValue("@IpAddress", request.IpAddress);

                        // Parámetros de salida
                        var iRptaParam = new SqlParameter("@iRpta", SqlDbType.Int) { Direction = ParameterDirection.Output };
                        var sRptaParam = new SqlParameter("@sRpta", SqlDbType.VarChar, -1) { Direction = ParameterDirection.Output };
                        var bRptaParam = new SqlParameter("@bRpta", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                        cmd.Parameters.Add(iRptaParam);
                        cmd.Parameters.Add(sRptaParam);
                        cmd.Parameters.Add(bRptaParam);

                        // Abrir la conexión y ejecutar el procedimiento almacenado
                        await cn.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();

                        // Obtener los resultados de los parámetros de salida
                        operationResult.Rows = iRptaParam.Value != DBNull.Value ? Convert.ToInt32(iRptaParam.Value) : 0;
                        operationResult.Message = sRptaParam.Value != DBNull.Value ? Convert.ToString(sRptaParam.Value) : string.Empty;
                        operationResult.Success = bRptaParam.Value != DBNull.Value && Convert.ToBoolean(bRptaParam.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                operationResult.Rows = 0;
                operationResult.Message = "Error creating period: " + ex.Message;
                operationResult.Success = false;
            }
            return operationResult;
        }
    }
}
