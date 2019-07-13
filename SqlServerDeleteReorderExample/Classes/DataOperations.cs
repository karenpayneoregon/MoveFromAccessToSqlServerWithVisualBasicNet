using System;
using System.Data;
using System.Data.SqlClient;

namespace SqlServerDeleteReorderExample.Classes
{
    /// <summary>
    /// Zero exception handling to keep things clear
    /// </summary>
    public class DataOperations
    {
        /*
         * Must change the server name to match your server
         */
        private string ConnectionString = "Data Source=KARENS-PC;" + 
                                          "Initial Catalog=ForumExample;" + 
                                          "Integrated Security=True";

        private readonly string _keyPositionFieldName = "RowPosition";

        /// <summary>
        /// Read all person records into a DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable ReadPeople()
        {
            var dt = new DataTable();

            const string selectStatement = 
                "SELECT Id,RowPosition  ,FirstName + ' ' + LastName AS FullName FROM dbo.Person";

            using (var cn = new SqlConnection() { ConnectionString = ConnectionString })
            {
                using (var cmd = new SqlCommand() { Connection = cn, CommandText = selectStatement })
                {

                    cn.Open();
                    dt.Load(cmd.ExecuteReader());

                    dt.Columns["id"].ColumnMapping = MappingType.Hidden;

                }
            }

            return dt;
        }
        /// <summary>
        /// Remove row which is called from the "Erase data" button in the form
        /// </summary>
        /// <param name="id">Person primary key</param>
        public void RemoveRow(int id)
        {
            const string deleteStatement = "DELETE FROM dbo.Person WHERE id = @id";
            using (var cn = new SqlConnection() { ConnectionString = ConnectionString })
            {
                using (var cmd = new SqlCommand() { Connection = cn, CommandText = deleteStatement })
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cn.Open();
                    cmd.ExecuteNonQuery();                    
                }
            }
        }

        public void ResetIdentifiers()
        {
            const string deleteStatement = "UPDATE ForumExample.dbo.Person " + 
                                           "SET @MaxSurrogateKey = RowPosition = @MaxSurrogateKey + 1 ";

            using (var cn = new SqlConnection() {ConnectionString = ConnectionString})
            {
                using (var cmd = new SqlCommand() {Connection = cn, CommandText = deleteStatement})
                {
                    cmd.Parameters.AddWithValue("@MaxSurrogateKey", 0);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

        }

        /// <summary>
        /// Reorder all keys identified with _keyPositionFieldName
        /// </summary>
        /// <param name="pDataTable"></param>
        public void UpdateAllRowsPosition(DataTable pDataTable)
        {

            var updateStatement = $"UPDATE dbo.Person SET {_keyPositionFieldName} =" +
                                    $" @{_keyPositionFieldName} WHERE id = @PersonId";


            using (var cn = new SqlConnection() { ConnectionString = ConnectionString })
            {
                using (var cmd = new SqlCommand() { Connection = cn })
                {

                    cmd.CommandText = updateStatement;
                    cmd.Parameters.Add(new SqlParameter()
                    {
                        ParameterName = $"@{_keyPositionFieldName}",
                        SqlDbType = SqlDbType.Int
                    });

                    cmd.Parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@PersonId",
                        SqlDbType = SqlDbType.Int
                    });


                    cn.Open();

                    int newPosition = 1;

                    for (var rowIndex = 0; rowIndex < pDataTable.Rows.Count; rowIndex++)
                    {
                        // set new row position
                        cmd.Parameters[$"@{_keyPositionFieldName}"].Value = newPosition;

                        cmd.Parameters["@PersonId"].Value =
                            pDataTable.Rows[rowIndex].Field<int>("Id");

                        cmd.ExecuteNonQuery();
                        newPosition += 1;
                    }

                }
            }
        }
    }
}

