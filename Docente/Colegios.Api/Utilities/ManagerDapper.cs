using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Colegios.Api.Utilities
{
    public class ManagerDapper
    {
        /// <summary>
        /// Ejecuta procedimiento de forma asyncrona almacenado con dapper usado para eliminar- actualizar - insertar
        /// </summary>
        /// <param name="cnx"></param>
        /// <param name="spName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static async Task<int> ExecuteStoredProcedure(string cnx, string spName,DynamicParameters parameters = null)
        {
            using (IDbConnection conn = new SqlConnection(cnx))
            {
                return await conn.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure, commandTimeout: 0);
            }
        }
        /// <summary>
        /// Ejecuta procedimiento almacenado y retorna una lista generica
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cnx"></param>
        /// <param name="spName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> ExecuteStoredProcedureReader<T>(string cnx, string spName, DynamicParameters parameters = null)
        {
            using (IDbConnection conn = new SqlConnection(cnx))
            {
                return await conn.QueryAsync<T>(spName, parameters, commandType: CommandType.StoredProcedure, commandTimeout: 0);
            }
        }
    }
}
