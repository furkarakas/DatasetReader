using FurkanKARAKAS.Model;
using FurkanKARAKAS.Service.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace FurkanKARAKAS.Service.Service
{
    public class XmlReader : IDatasetReader
    {
        public List<DatasetModel> GetByCityName(string CityName, int OrderType)
        {
            var lst_data = ReadDataset().Where(x => x.CityName == CityName).ToList();
            if (OrderType == 1) 
                lst_data = lst_data.OrderBy(x => x.CityName).ToList(); 
            else 
                lst_data = lst_data.OrderByDescending(x => x.CityName).ToList(); 


            return lst_data;
        }

        public List<DatasetModel> ReadDataset()
        {

            string BaseDirectory = HttpContext.Current.Server.MapPath("/Dataset/" + ConfigurationManager.AppSettings["DatasetName"]);


            string xmlDosyasi = BaseDirectory;
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlDosyasi);

            List<DatasetModel> lst_data = new List<DatasetModel>();

            foreach (XmlNode City in xmlDocument.DocumentElement.ChildNodes)
            {
                foreach (XmlNode district in City.ChildNodes)
                {
                    foreach (XmlNode zipcode in district.ChildNodes)
                    {
                        DatasetModel datasetModel = new DatasetModel();
                        datasetModel.CityCode = City.Attributes["code"].Value;
                        datasetModel.CityName = City.Attributes["name"].Value;
                        datasetModel.DistrictName = district.Attributes["name"].Value;
                        datasetModel.ZipCode = zipcode.Attributes["code"].Value;

                        lst_data.Add(datasetModel);
                    }
                }
            }

            return lst_data;
        }
    }
}
