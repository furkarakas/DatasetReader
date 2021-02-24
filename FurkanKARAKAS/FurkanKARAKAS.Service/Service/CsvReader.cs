using FurkanKARAKAS.Model;
using FurkanKARAKAS.Service.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FurkanKARAKAS.Service.Service
{
    public class CsvReader : IDatasetReader
    {
        public List<DatasetModel> GetByCityName(string CityName,int OrderType)
        {
            var lst_data = ReadDataset().Where(x=> x.CityName == CityName).ToList();
            if (OrderType == 1) 
                lst_data = lst_data.OrderBy(x => x.CityName).ToList(); 
            else 
                lst_data = lst_data.OrderByDescending(x => x.CityName).ToList(); 


            return lst_data;
        }

        public List<DatasetModel> ReadDataset()
        {

            string BaseDirectory = HttpContext.Current.Server.MapPath("/Dataset/"+ ConfigurationManager.AppSettings["DatasetName"]);
             
            string[] allLines = System.IO.File.ReadAllLines(BaseDirectory, Encoding.GetEncoding("windows-1254"));
             
            var query = from line in allLines
                        let data = line.Split(',') 
                        select new DatasetModel
                        {
                            CityName = data[0],
                            CityCode = data[1],
                            DistrictName = data[2],
                            ZipCode = data[3]
                        };


            return query.ToList();
        }
    }
}
