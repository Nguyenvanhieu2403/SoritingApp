using SortingApp_Net.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApp_Desktop.Repository.interfaces
{
    public interface IQRCodeRespo
    {
        byte[] generateQRCode(string data, int width, int height);
    }
}
