using SortingApp_Desktop.Common.api.Models;
using SortingApp_Desktop.Common.@base.Interfaces;
using SortingApp_Desktop.DataContext;
using SortingApp_Desktop.DataContext.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApp_Desktop.Repository.interfaces
{
    public interface IConfigRepos : IBaseRepos<Config>
    {
        //Task<MethodResult<Bag>> Create(BagInput bagInput);
    }
}
