using System.Collections.Generic;
using System.Data;

namespace AutoLotDAL_ADO.BulkImport
{
    public interface IMyDataReader<T> : IDataReader
    {
        List<T> Records { get; set; }
    }
}
