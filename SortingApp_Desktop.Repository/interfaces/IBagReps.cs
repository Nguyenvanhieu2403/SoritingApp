using SortingApp_Desktop.Common.api.Models;
using SortingApp_Desktop.Common.@base.Interfaces;
using SortingApp_Desktop.DataContext;
using SortingApp_Desktop.DataContext.dtos;
using SortingApp_Desktop.DataContext.models;
using SortingApp_Net.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApp_Desktop.Repository.interfaces
{
    public interface IBagReps : IBaseRepos<Bag>
    {
        Task<MethodResult<Bag>> Create(BagInput bagInput);
        Task<MethodResult<List<Bag>>> SearchAsync(int? ProcessId);
        Task<MethodResult<PrintBD8>> CloseBag(CloseBagInput closeBagInput);
        Task<MethodResult<PrintBD8>> PrintBD8(string MailNumber, string BagNumber, string DestinationPosCode);
    }
}
