using SortingApp_Desktop.Common.utils;

namespace SortingApp_Desktop.Common.@base
{
    public class BaseSearchModel
    {
        private string _keyword;
        public string Keyword
        {
            get => StringUtil.RemoveSqlInjectionChar(_keyword);
            set => _keyword = value;
        }
        /// <summary>
        /// Trạng thái dữ liệu
        /// </summary>
        public byte Status { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string OrderCol { get; set; }
        public bool IsDesc { get; set; }
        public int TotalRecord { get; set; }
    }
}
