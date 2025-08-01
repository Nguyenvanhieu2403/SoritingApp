using Microsoft.Extensions.Configuration;
using SortingApp_Desktop.Common.@base;
using SortingApp_Desktop.DataContext.constants;
using SortingApp_Desktop.DataContext;
using SortingApp_Desktop.Repository.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingApp_Desktop.Repository
{
    public class ConfigRepos : BaseRepos<Config>, IConfigRepos
    {
        private readonly IConfiguration _configuration;

        public ConfigRepos(IConfiguration configuration) : base(configuration, ServiceConstant.DefaultSchema)
        {
            _configuration = configuration;
        }
    }
}
