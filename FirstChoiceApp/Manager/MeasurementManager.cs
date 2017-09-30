using FirstChoiceApp.Gateway;
using FirstChoiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstChoiceApp.Manager
{
    public class MeasurementManager
    {
        MeasurementGateway objMeasurementGateway = new MeasurementGateway();

        internal List<Measurement> GetAllMeasurement()
        {
            return objMeasurementGateway.GetAllMeasurement();
        }

        internal bool CreateMeasurement(Measurement objMeasurement)
        {
            if (objMeasurementGateway.IsExist(objMeasurement))
            {
                throw new Exception("Measurement -" + objMeasurement.MeasurementName + "- Already Exist");
            }
            return objMeasurementGateway.CreateMeasurement(objMeasurement) > 0;
        }

        internal bool UpdateMeasurement(Measurement objMeasurement)
        {
            return objMeasurementGateway.UpdateMeasurement(objMeasurement) > 0;
        }
    }
}