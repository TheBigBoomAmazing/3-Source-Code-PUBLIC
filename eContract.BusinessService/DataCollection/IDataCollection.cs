using System.Data;
using System.Drawing.Text;

namespace TopDATA.BusinessService.DataCollection
{
    public interface IDataCollection
    {
        DataTable InventoryData(string strBuff);

        DataTable SalesData(string strBuff);

        DataTable OrderData(string strBuff);
    }
}