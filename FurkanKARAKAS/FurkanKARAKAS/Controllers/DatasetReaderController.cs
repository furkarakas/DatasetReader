using FurkanKARAKAS.Model;
using FurkanKARAKAS.Service.Interface;
using FurkanKARAKAS.Service.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace FurkanKARAKAS.Controllers
{
    public class DatasetReaderController : ApiController
    {
        IDatasetReader datasetReader;
        public DatasetReaderController()
        {
            if (ConfigurationManager.AppSettings["DatasetName"] == "sample_data.csv")
                datasetReader = new CsvReader();
            else if (ConfigurationManager.AppSettings["DatasetName"] == "sample_data.xml")
                datasetReader = new XmlReader();
        }

        public List<DatasetModel> Get()
        {
            var lst_Data = datasetReader.ReadDataset();

            return lst_Data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CityName"></param>
        /// <param name="OrderType">1 - Asc 2 - Desc</param>
        /// <returns></returns>
        public List<DatasetModel> GetByCityName(string CityName, int OrderType)
        {
            var lst_Data = datasetReader.GetByCityName(CityName,OrderType);

            return lst_Data;
        }


        public bool ChangeDataset(string DatasetName)
        {
            try
            {
                ConfigurationManager.AppSettings["DatasetName"] = DatasetName;

                if (ConfigurationManager.AppSettings["DatasetName"] == "sample_data.csv")
                    datasetReader = new CsvReader();
                else if (ConfigurationManager.AppSettings["DatasetName"] == "sample_data.xml")
                    datasetReader = new XmlReader();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
