using System.Collections.Generic;

namespace SortingApp_Desktop.Common.api.Models
{
    public class ApiDiscoveryControllerModel
    {
        public string ControllerName { get; set; }
        public string Description { get; set; }
        public List<ApiDiscoveryMethodModel> DsPermissionModels { get; set; }
    }

    public class ApiDiscoveryMethodModel
    {
        public string PermissionName { get; set; }
        public int PermissionIndex { get; set; }
        public string ControllerName { get; set; }
    }
}
