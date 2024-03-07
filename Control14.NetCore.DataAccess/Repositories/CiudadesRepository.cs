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
    public class CiudadesRepository : ICiudadesRepository
    {
        private readonly string _connectionString;
        public CiudadesRepository(IOptions<E_ConnectionStrings> connectionString)
        {
            _connectionString = connectionString.Value.Sql;
        }

        public async Task<CiudadesCreateResponse> Create(CiudadesCreateRequest request)
        {
            var operationResult = new CiudadesCreateResponse();
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    using (var cmd = new SqlCommand("USP_CiudadesInsert", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros de entrada
                        cmd.Parameters.AddWithValue("@NombreCiudad", request.NombreCiudad);
                        cmd.Parameters.AddWithValue("@Pais", request.Pais);
                        cmd.Parameters.AddWithValue("@Poblacion", request.Poblacion);
                 

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
                        operationResult.IdCiudad = iRptaParam.Value != DBNull.Value ? Convert.ToInt32(iRptaParam.Value) : 0;
                        operationResult.Message = sRptaParam.Value != DBNull.Value ? Convert.ToString(sRptaParam.Value) : string.Empty;
                        operationResult.Success = bRptaParam.Value != DBNull.Value && Convert.ToBoolean(bRptaParam.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                operationResult.IdCiudad = 0;
                operationResult.Message = "Error creating period: " + ex.Message;
                operationResult.Success = false;
            }
            return operationResult;
        }

        public Task<CiudadesDeleteResponse> Delete(CiudadesDeleteRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<CiudadesDeleteResponse> Delete(int id)
        {
            var operationResult = new CiudadesDeleteResponse();
            try
            {
                using (SqlConnection cn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("USP_CiudadDelete", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CiudadID", id);
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

        public async Task<IEnumerable<CiudadesReadResponse>> GetAll(int id)
        {
            List<CiudadesReadResponse> lista = new List<CiudadesReadResponse>();
            try
            {
                using (SqlConnection cn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("USP_CiudadesSelectAll", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Status", id);
                        await cn.OpenAsync();

                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            while (await dr.ReadAsync())
                            {
                                lista.Add(new CiudadesReadResponse
                                {
                                    CiudadID = dr["CiudadID"] is DBNull ? 0 : Convert.ToInt32(dr["CiudadID"]),
                                    Status = dr["Status"] is DBNull ? 0 : Convert.ToInt32(dr["Status"]),
                                    NombreCiudad = dr["NombreCiudad"] is DBNull ? null : dr["NombreCiudad"].ToString(),
                                    Pais = dr["Pais"] is DBNull ? null : dr["Pais"].ToString(),
                                    Poblacion = dr["Poblacion"] is DBNull ? 0 : Convert.ToInt32(dr["Poblacion"]),
                                 
                           

                                });
                            }
                        }
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving all Ciudades for company: " + ex.Message);
            }
        }

        public Task<IEnumerable<CiudadesReadResponse>> GetAll(CiudadesReadRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<CiudadesReadResponse> GetById(int id)
        {
            CiudadesReadResponse result = new CiudadesReadResponse();
            try
            {
                using (SqlConnection cn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("USP_CiudadesById", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CiudadID", id);
                        await cn.OpenAsync();

                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            if (await dr.ReadAsync())
                            {
                                result = new CiudadesReadResponse
                                {
                                    CiudadID = dr["CiudadID"] is DBNull ? 0 : Convert.ToInt32(dr["CiudadID"]),
                                    Status = dr["Status"] is DBNull ? 0 : Convert.ToInt32(dr["Status"]),
                                    NombreCiudad = dr["NombreCiudad"] is DBNull ? null : dr["NombreCiudad"].ToString(),
                                    Pais = dr["Pais"] is DBNull ? null : dr["Pais"].ToString(),
                                    Poblacion = dr["Poblacion"] is DBNull ? 0 : Convert.ToInt32(dr["Poblacion"]),
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

        public Task<CiudadesReadResponse> GetById(CiudadesReadRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<CiudadesUpdateResponse> Update(CiudadesUpdateRequest request)
        {
            var operationResult = new CiudadesUpdateResponse();
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    using (var cmd = new SqlCommand("USP_CiudadesUpdate", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@CiudadID", request.CiudadID);
                        cmd.Parameters.AddWithValue("@NombreCiudad", request.NombreCiudad);
                        cmd.Parameters.AddWithValue("@Pais", request.Pais);                 
                        cmd.Parameters.AddWithValue("@Poblacion", request.Poblacion);
              

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
                operationResult.Message = "Error updating city: " + ex.Message;
                operationResult.Success = false;
            }
            return operationResult;
        }
    }
}
