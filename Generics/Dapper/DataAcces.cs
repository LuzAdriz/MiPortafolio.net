using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace Generics.Dapper
{
    public static class DataAcces
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strConx"></param>
        /// <param name="spName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static async Task<int> ExecuteStoredProcedure(string strConx, string spName, DynamicParameters parameters = null)
        {
            using (IDbConnection conn = new SqlConnection(strConx))
            {
                return await conn.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure, commandTimeout: 0);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strConx"></param>
        /// <param name="spName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> ExecuteStoredProcedureReader<T>(string strConx, string spName, DynamicParameters parameters = null)
        {
            using (IDbConnection conn = new SqlConnection(strConx))
            {
                return await conn.QueryAsync<T>(spName, parameters, commandType: CommandType.StoredProcedure, commandTimeout: 0);
            }
        }

    }
}
