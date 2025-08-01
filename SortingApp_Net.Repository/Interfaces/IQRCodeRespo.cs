using SortingApp_Net.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VnPostLib.Common.Base.Interfaces;

namespace SortingApp_Net.Repository.Interfaces
{
    public interface IQRCodeRespo
    {
        byte[] generateQRCode(String data, int width, int height);
    }
}
