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
    public class ViajeRepository : IViajeRepository
    {
        private readonly string _connectionString;
        public ViajeRepository(IOptions<E_ConnectionStrings> connectionString)
        {
            _connectionString = connectionString.Value.Sql;
        }

        public async Task<ViajeCreateResponse> Create(ViajeCreateRequest request)
        {
            var operationResult = new ViajeCreateResponse();
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    using (var cmd = new SqlCommand("USP_ViajeInsert", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetros de entrada
                        cmd.Parameters.AddWithValue("@FechaSalida", request.FechaSalida);
                        cmd.Parameters.AddWithValue("@FechaLlegada", request.FechaLlegada);
                        cmd.Parameters.AddWithValue("@VehiculoID", request.VehiculoID);
                        cmd.Parameters.AddWithValue("@CiudadOrigenID", request.CiudadOrigenID);
                        cmd.Parameters.AddWithValue("@CiudadDestinoID", request.CiudadDestinoID);

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
                        operationResult.IdViaje = iRptaParam.Value != DBNull.Value ? Convert.ToInt32(iRptaParam.Value) : 0;
                        operationResult.Message = sRptaParam.Value != DBNull.Value ? Convert.ToString(sRptaParam.Value) : string.Empty;
                        operationResult.Success = bRptaParam.Value != DBNull.Value && Convert.ToBoolean(bRptaParam.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                operationResult.IdViaje = 0;
                operationResult.Message = "Error creating period: " + ex.Message;
                operationResult.Success = false;
            }
            return operationResult;
        }

        public Task<ViajeDeleteResponse> Delete(ViajeDeleteRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<ViajeDeleteResponse> Delete(int id)
        {
            var operationResult = new ViajeDeleteResponse();
            try
            {
                using (SqlConnection cn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("USP_ViajeDelete", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ViajeID", id);
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
                operationResult.Message = "Error deleting viaje: " + ex.Message;
                operationResult.Success = false;
            }
            return operationResult;
        }

        public async Task<IEnumerable<ViajeReadResponse>> GetAll(int id)
        {
            List<ViajeReadResponse> lista = new List<ViajeReadResponse>();
            try
            {
                using (SqlConnection cn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("USP_ViajesSelectAll", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Status", id);
                        await cn.OpenAsync();

                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            while (await dr.ReadAsync())
                            {
                                lista.Add(new ViajeReadResponse
                                {
                                    

                                    FechaSalida = dr["FechaSalida"] is DBNull ? null : (DateTime?)dr["FechaSalida"],
                                    ViajeID = dr["ViajeID"] is DBNull ? 0 : Convert.ToInt32(dr["ViajeID"]),
                                    FechaLlegada = dr["FechaLlegada"] is DBNull ? null : Convert.ToDateTime(dr["FechaLlegada"]),
                                   
                                    VehiculoID = dr["VehiculoID"] is DBNull ? 0 : Convert.ToInt32(dr["VehiculoID"]),

                                    CiudadDestino = dr["CiudadDestino"] is DBNull ? null : dr["CiudadDestino"].ToString(),
                                    CiudadOrigen = dr["CiudadOrigen"] is DBNull ? null : dr["CiudadOrigen"].ToString(),
                                    CiudadOrigenID = dr["CiudadIDOrigen"] is DBNull ? 0 : Convert.ToInt32(dr["CiudadIDOrigen"]),
                                    CiudadDestinoID = dr["CiudadIDDestino"] is DBNull ? 0 : Convert.ToInt32(dr["CiudadIDDestino"]),
                                    Status = dr["Status"] is DBNull ? 0 : Convert.ToInt32(dr["Status"]),

                                    TipoVehiculo = dr["TipoVehiculo"] is DBNull ? null : dr["TipoVehiculo"].ToString(),
                                    Patente = dr["Patente"] is DBNull ? null : dr["Patente"].ToString(),
                                    Marca = dr["Marca"] is DBNull ? null : dr["Marca"].ToString(),
                                    Modelo = dr["Modelo"] is DBNull ? null : dr["Modelo"].ToString(),
                                    Color = dr["Color"] is DBNull ? null : dr["Color"].ToString(),

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

        public Task<IEnumerable<ViajeReadResponse>> GetAll(ViajeReadRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<ViajeReadResponse> GetById(int id)
        {
            ViajeReadResponse result = new ViajeReadResponse();
            try
            {
                using (SqlConnection cn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("USP_ViajeById", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Viaje", id);
                        await cn.OpenAsync();

                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            if (await dr.ReadAsync())
                            {
                                result = new ViajeReadResponse
                                {
                                    ViajeID = dr["ViajeID"] is DBNull ? 0 : Convert.ToInt32(dr["ViajeID"]),
                                    FechaLlegada = dr["FechaLlegada"] is DBNull ? null : Convert.ToDateTime(dr["FechaLlegada"]),
                                    FechaSalida = dr["FechaSalida"] is DBNull ? null : Convert.ToDateTime(dr["FechaSalida"]),
                                    VehiculoID = dr["VehiculoID"] is DBNull ? 0 : Convert.ToInt32(dr["VehiculoID"]),

                                    CiudadDestino = dr["CiudadDestino"] is DBNull ? null : dr["CiudadDestino"].ToString(),
                                    CiudadOrigen = dr["CiudadOrigen"] is DBNull ? null : dr["CiudadOrigen"].ToString(),
                                    CiudadOrigenID = dr["CiudadIDOrigen"] is DBNull ? 0 : Convert.ToInt32(dr["CiudadIDOrigen"]),
                                    CiudadDestinoID = dr["CiudadIDDestino"] is DBNull ? 0 : Convert.ToInt32(dr["CiudadIDDestino"]),
                                    Status = dr["Status"] is DBNull ? 0 : Convert.ToInt32(dr["Status"]),

                                    TipoVehiculo = dr["TipoVehiculo"] is DBNull ? null : dr["TipoVehiculo"].ToString(),
                                    Patente = dr["Patente"] is DBNull ? null : dr["Patente"].ToString(),
                                    Marca = dr["Marca"] is DBNull ? null : dr["Marca"].ToString(),
                                    Modelo = dr["Modelo"] is DBNull ? null : dr["Modelo"].ToString(),
                                    Color = dr["Color"] is DBNull ? null : dr["Color"].ToString(),

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

        public Task<ViajeReadResponse> GetById(ViajeReadRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<ViajeUpdateResponse> Update(ViajeUpdateRequest request)
        {
            var operationResult = new ViajeUpdateResponse();
            try
            {
                using (var cn = new SqlConnection(_connectionString))
                {
                    using (var cmd = new SqlCommand("USP_ViajeUpdate", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@ViajeID", request.ViajeID);
                        cmd.Parameters.AddWithValue("@FechaSalida", request.FechaSalida);
                        cmd.Parameters.AddWithValue("@FechaLlegada", request.FechaLlegada);
                        cmd.Parameters.AddWithValue("@VehiculoID", request.VehiculoID);
                        cmd.Parameters.AddWithValue("@CiudadOrigenID", request.CiudadOrigenID);
                        cmd.Parameters.AddWithValue("@CiudadDestinoID", request.CiudadDestinoID);

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
                operationResult.Message = "Error updating Viaje: " + ex.Message;
                operationResult.Success = false;
            }
            return operationResult;
        }
    }
}
