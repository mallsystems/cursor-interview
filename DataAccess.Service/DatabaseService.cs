using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;


namespace DataAccess.Service
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("InterviewDB");
        }

        public void ExecuteQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public DataTable ExecuteStoredProcedure(string storedProcedureName, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                return dataTable;
            }
        }
        public async Task<int> ExecuteStoredProcedureAsync(string procedureName, List<SqlParameter> parameters)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = procedureName;
                    command.CommandType = CommandType.StoredProcedure;

                    foreach (var parameter in parameters)
                    {
                        var clonedParameter = new SqlParameter(parameter.ParameterName, parameter.Value)
                        {
                            SqlDbType = parameter.SqlDbType,
                            Direction = parameter.Direction,
                            Size = parameter.Size
                        };
                        command.Parameters.Add(clonedParameter);
                    }

                    var outputParameter = parameters.FirstOrDefault(p => p.Direction == ParameterDirection.Output);

                    await command.ExecuteNonQueryAsync();

                    if (outputParameter != null)
                    {
                        return (int)command.Parameters[outputParameter.ParameterName].Value;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }
        /// <summary>
        /// TO DO : CONTINUE WORKING ON THIS METHOD TO GET RETRIEVE THE 
        /// PROPERTIES OF THE OBJECT, CREATE THE PARAMETERS BASE ON THE NAME OF THE OBJECTS
        /// AND EXECUTE THE QUERY OR SP
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryStatement"></param>
        /// <param name="commandType"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> ExecuteAsync<T>(string queryStatement, CommandType commandType, T entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = queryStatement;
                    command.CommandType = commandType;


                    var properties = typeof(T).GetProperties();

                    foreach (var property in properties)
                    {

                        var value = property.GetValue(entity);
                        if (value == null || !IsPrimitiveType(property.PropertyType))
                        {
                            continue;
                        }
                        var parameter = new SqlParameter("@" + property.Name, property.GetValue(entity))
                        {
                            SqlDbType = GetSqlDbType(property.PropertyType),
                            Direction = ParameterDirection.Input
                        };
                        command.Parameters.Add(parameter);
                    }

                    await command.ExecuteNonQueryAsync();


                    var outputParameter = command.Parameters["@OutputId"];
                    if (outputParameter != null)
                    {
                        return (int)outputParameter.Value;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }

        private SqlDbType GetSqlDbType(Type type)
        {
            if (type == typeof(int) || (type == typeof(int?)))
            {
                return SqlDbType.Int;
            }
            else if (type == typeof(string))
            {
                return SqlDbType.NVarChar;
            }
            else if (type == typeof(DateTime) || (type == typeof(DateTime?)))
            {
                return SqlDbType.DateTime;
            }
            else if (type == typeof(bool))
            {
                return SqlDbType.Bit;
            }
            else if (type == typeof(float))
            {
                return SqlDbType.Float;
            }
            else if (type == typeof(double))
            {
                return SqlDbType.Float;
            }
            else if (type == typeof(decimal))
            {
                return SqlDbType.Decimal;
            }
            else if (type == typeof(byte))
            {
                return SqlDbType.TinyInt;
            }
            else if (type == typeof(short))
            {
                return SqlDbType.SmallInt;
            }
            else if (type == typeof(long))
            {
                return SqlDbType.BigInt;
            }

            else
            {

                throw new ArgumentException("Unsupported data type: " + type.Name);
            }
        }

        private bool IsPrimitiveType(Type type)
        {
            return type.IsPrimitive || type.IsValueType || type == typeof(string);
        }


    }
}
