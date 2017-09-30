using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FirstChoiceApp.Models;
using System.Data.SqlClient;
using System.Data;

namespace FirstChoiceApp.Gateway
{
    public class MeasurementGateway
    {
        DbConnection objConnection = new DbConnection();

        internal List<Measurement> GetAllMeasurement()
        {
            List<Measurement> objMeasurementList = new List<Measurement>();

            string query = "SELECT * FROM Measurement";

            SqlCommand command = new SqlCommand(query, objConnection.Connection());
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Measurement objMeasurement = new Measurement()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        MeasurementName = reader["MeasurementName"].ToString()
                    };
                    objMeasurementList.Add(objMeasurement);
                }
            }
            return objMeasurementList;
        }

        internal int UpdateMeasurement(Measurement objMeasurement)
        {
            int countAffectedRow = 0;

            SqlCommand command = new SqlCommand("uspUpdateMeasurement", objConnection.Connection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("Id", objMeasurement.Id);
            command.Parameters.AddWithValue("MeasurementName", objMeasurement.MeasurementName);

            countAffectedRow = command.ExecuteNonQuery();

            return countAffectedRow;
        }

        internal bool IsExist(Measurement objMeasurement)
        {
            bool isExist = GetAllMeasurement().Exists(x=> x.MeasurementName == objMeasurement.MeasurementName);

            return isExist;
        }

        internal int CreateMeasurement(Measurement objMeasurement)
        {
            int countAffectedRow = 0;

            SqlCommand command = new SqlCommand("uspCreateMeasurement", objConnection.Connection());
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("MeasurementName", objMeasurement.MeasurementName);

            countAffectedRow = command.ExecuteNonQuery();

            return countAffectedRow;
        }
    }
}