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
    public class VehiculosRepository : IVehiculosRepository
    {
        private readonly string _connectionString;
        public VehiculosRepository(IOptions<E_ConnectionStrings> connectionString)
        {
            _connectionString = connectionString.Value.Sql;
        }

        public async Task<VehiculosCreateResponse> Create(VehiculosCreateRequest request)
        {
            var operationResult = new VehiculosCreateResponse();
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    using (var cmd = new SqlCommand("USP_VehiculoInsert", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros de entrada
                        cmd.Parameters.AddWithValue("@TipoVehiculo", request.TipoVehiculo);
                        cmd.Parameters.AddWithValue("@Patente", request.Patente);
                        cmd.Parameters.AddWithValue("@Marca", request.Marca);
                        cmd.Parameters.AddWithValue("@Modelo", request.Modelo);
                        cmd.Parameters.AddWithValue("@Color", request.Color);
               

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
                        operationResult.VehiculoID = iRptaParam.Value != DBNull.Value ? Convert.ToInt32(iRptaParam.Value) : 0;
                        operationResult.Message = sRptaParam.Value != DBNull.Value ? Convert.ToString(sRptaParam.Value) : string.Empty;
                        operationResult.Success = bRptaParam.Value != DBNull.Value && Convert.ToBoolean(bRptaParam.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                operationResult.VehiculoID = 0;
                operationResult.Message = "Error creating period: " + ex.Message;
                operationResult.Success = false;
            }
            return operationResult;
        }

        public Task<VehiculosDeleteResponse> Delete(VehiculosDeleteRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<VehiculosDeleteResponse> Delete(int id)
        {
            var operationResult = new VehiculosDeleteResponse();
            try
            {
                using (SqlConnection cn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("USP_VehiculoDelete", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@VehiculoID", id);
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

        public async Task<IEnumerable<VehiculosReadResponse>> GetAll(int id)
        {
            List<VehiculosReadResponse> lista = new List<VehiculosReadResponse>();
            try
            {
                using (SqlConnection cn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("USP_VehiculoSelectAll", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Status", id);
                        await cn.OpenAsync();

                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            while (await dr.ReadAsync())
                            {
                                lista.Add(new VehiculosReadResponse
                                {
                                    VehiculoID = dr["VehiculoID"] is DBNull ? 0 : Convert.ToInt32(dr["VehiculoID"]),
                                    Status = dr["Status"] is DBNull ? 0 : Convert.ToInt32(dr["Status"]),
                                    TipoVehiculo = dr["TipoVehiculo"] is DBNull ? null : dr["TipoVehiculo"].ToString(),
                                    Patente = dr["Patente"] is DBNull ? null : dr["Patente"].ToString(),
                                    Color = dr["Color"] is DBNull ? null : dr["Color"].ToString(),
                                    Marca = dr["Marca"] is DBNull ? null : dr["Marca"].ToString(),
                                    Modelo = dr["Modelo"] is DBNull ? null : dr["Modelo"].ToString(),
                       
                                });
                            }
                        }
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving all Vehiculo for company: " + ex.Message);
            }
        }

        public Task<IEnumerable<VehiculosReadResponse>> GetAll(VehiculosReadRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<VehiculosReadResponse> GetById(int id)
        {
            VehiculosReadResponse result = new VehiculosReadResponse();
            try
            {
                using (SqlConnection cn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("USP_VehiculoById", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@VehiculoID", id);
                        await cn.OpenAsync();

                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            if (await dr.ReadAsync())
                            {
                                result = new VehiculosReadResponse
                                {
                                    VehiculoID = dr["VehiculoID"] is DBNull ? 0 : Convert.ToInt32(dr["VehiculoID"]),
                                    Status = dr["Status"] is DBNull ? 0 : Convert.ToInt32(dr["Status"]),
                                    TipoVehiculo = dr["TipoVehiculo"] is DBNull ? null : dr["TipoVehiculo"].ToString(),
                                    Marca = dr["Marca"] is DBNull ? null : dr["Marca"].ToString(),
                                    Modelo = dr["Modelo"] is DBNull ? null : dr["Modelo"].ToString(),
                                    Color = dr["Color"] is DBNull ? null : dr["Color"].ToString(),
                                    Patente = dr["Patente"] is DBNull ? null : dr["Patente"].ToString(),
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

        public Task<VehiculosReadResponse> GetById(VehiculosReadRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<VehiculosUpdateResponse> Update(VehiculosUpdateRequest request)
        {
            var operationResult = new VehiculosUpdateResponse();
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    using (var cmd = new SqlCommand("USP_VehiculoUpdate", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@VehiculoID", request.VehiculoID);
                        cmd.Parameters.AddWithValue("@TipoVehiculo", request.TipoVehiculo);
                        cmd.Parameters.AddWithValue("@Marca", request.Marca);
                        cmd.Parameters.AddWithValue("@Patente", request.Patente);
                        cmd.Parameters.AddWithValue("@Modelo", request.Modelo);
                        cmd.Parameters.AddWithValue("@Color", request.Color);

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
