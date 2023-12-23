using Colegios.Api.Interfaces;
using Colegios.Api.Utilities;
using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;

namespace Colegios.Api.Services
{
    /// <summary>
    /// Servicios Genericos para ejecutar procedimientos alamcenados
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Repositorio<T> : IRepositorio<T>
    {
        public async Task<bool> UpdateAsync(string cnx, string spName, T model)
        {
            return await ManagerDapper.ExecuteStoredProcedure(cnx, spName, AddParameters(model,true)) > 0;
        }
        public async Task<bool> InsertAsync(string cnx, string spName, T model)
        {
            return await ManagerDapper.ExecuteStoredProcedure(cnx, spName, AddParameters(model)) > 0;
        }
        public async Task<bool> DeleteAsync<P>(string cnx, string spName, P parameters)
        {
            return await ManagerDapper.ExecuteStoredProcedure(cnx, spName, AddParameters(parameters)) > 0;
        }
        public async Task<IEnumerable<T>> FindAsync(string cnx,string spName)
        {
            return await ManagerDapper.ExecuteStoredProcedureReader<T>(cnx, spName, parameters: null);
        }
        public async Task<IEnumerable<T>> FindAsync<P>(string cnx, string spName, P parameters)
        {
            return await ManagerDapper.ExecuteStoredProcedureReader<T>(cnx, spName,AddParameters(parameters));
        }
        private static DynamicParameters AddParameters(object obj, bool fromUpdate = false, Dictionary<string, Type> outPutParameters = null)
        {
            DynamicParameters parameters = new DynamicParameters();

            if (obj != null)
            {
                PropertyInfo[] properties = obj.GetType().GetProperties();
                if (fromUpdate)
                {
                    parameters = new DynamicParameters(obj);
                    goto OUTPUT;
                }
                parameters = new DynamicParameters();

                foreach (var prop in properties)
                {
                    if (prop.GetCustomAttribute(typeof(KeyAttribute)) == null)
                    {
                        parameters.Add(prop.Name, prop.GetValue(obj));
                    }

                }
            }
            OUTPUT:
            if (outPutParameters != null)
            {
                foreach (KeyValuePair<string, Type> outPut in outPutParameters)
                {
                    parameters.Add(outPut.Key, dbType: GetDbType(outPut.Value), direction: ParameterDirection.Output);
                }

            }

            return parameters;

        }
        private static DbType GetDbType(Type type)
        {
            return typeMap[type];
        }

        private static readonly Dictionary<Type, DbType> typeMap = new Dictionary<Type, DbType>()
        {
            { typeof(byte?), DbType.Byte },
            { typeof(sbyte?),DbType.SByte },
            { typeof(short?), DbType.Int16 },
            { typeof(ushort?), DbType.UInt16 },
            { typeof(int?), DbType.Int32 },
            { typeof(uint?), DbType.UInt32 },
            { typeof(long?), DbType.Int64 },
            { typeof(ulong?), DbType.UInt64 },
            { typeof(float?), DbType.Single },
            { typeof(double?), DbType.Double },
            { typeof(decimal?), DbType.Decimal },
            { typeof(DateTime?), DbType.DateTime },
            { typeof(bool?), DbType.Boolean },
            { typeof(char?), DbType.StringFixedLength },
            { typeof(byte), DbType.Byte },
            { typeof(sbyte),DbType.SByte },
            { typeof(short), DbType.Int16 },
            { typeof(ushort), DbType.UInt16 },
            { typeof(int), DbType.Int32 },
            { typeof(uint), DbType.UInt32 },
            { typeof(long), DbType.Int64 },
            { typeof(ulong), DbType.UInt64 },
            { typeof(float), DbType.Single },
            { typeof(double), DbType.Double },
            { typeof(decimal), DbType.Decimal },
            { typeof(bool), DbType.Boolean },
            { typeof(char), DbType.StringFixedLength },
            { typeof(Guid), DbType.Guid },
            { typeof(DateTime), DbType.DateTime },
            { typeof(DateTimeOffset), DbType.DateTimeOffset },
            { typeof(string), DbType.String },
            { typeof(byte[]), DbType.Binary }
        };
    }

}
