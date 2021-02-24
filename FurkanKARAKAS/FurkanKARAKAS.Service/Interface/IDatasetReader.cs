using FurkanKARAKAS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurkanKARAKAS.Service.Interface
{
    public interface IDatasetReader
    {
        List<DatasetModel> ReadDataset();
        List<DatasetModel> GetByCityName(string CityName,int OrderType);
    }
}
