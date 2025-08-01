using System;

namespace VnPostLib.Common.Base.Interfaces
{
    /// <summary>
    /// BaseModel
    /// </summary>
    public interface IBaseModel
    {
        /// <summary>
        /// Khóa chính của bảng
        /// </summary>
        long Id { get; set; }
        /// <summary>
        /// Trạng thái dữ liệu
        /// </summary>
        byte? Status { get; set; }
        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        DateTime? Modified { get; set; }
        /// <summary>
        /// Người cập nhật
        /// </summary>
        long? ModifiedBy { get; set; }
    }
}
