using System;

namespace VnPostLib.Common.Api.Models
{
    /// <summary>
    /// Dùng trong trường hợp trả về hàm void khi bình thường (không cần data trả về)
    /// </summary>
    public interface IMethodResult
    {
        /// <summary>
        /// Trạng thái thành công hay không
        /// </summary>
        bool Success { get; set; }
        /// <summary>
        /// Mã lỗi (nếu có)
        /// </summary>
        string Error { get; set; }
        /// <summary>
        /// Diễn giải cho lỗi (nếu có)
        /// </summary>
        string Message { get; set; }
        /// <summary>
        /// Mã lỗi trả về (trong trường hợp trả về qua http thì đây là http status code)
        /// </summary>
        int? Status { get; set; }

    }

    /// <summary>
    /// Dùng trong trường hợp trả về một dữ liệu nào đó khác void khi bình thường
    /// </summary>
    public class MethodResult : IMethodResult
    {
        public bool Success { get; set; } = true;
        public string Error { get; set; } = "";
        public string Message { get; set; } = "";
        public int? Status { get; set; } = 200;

        public static MethodResult ResultWithError(string error, string message = "", int? status = null) => new MethodResult()
        {
            Success = false,
            Message = message,
            Error = error,
            Status = status,
        };

        public static MethodResult ResultWithSuccess(string message = "") => new MethodResult()
        {
            Success = true,
            Message = message,
        };

        public static MethodResult ResultWithAccessDenined()
        {
            return ResultWithError("ERR_FORBIDDEN", "Bạn không đủ quyền để lấy dữ liệu đã yêu cầu", 403);
        }

        public static MethodResult ResultWithNotFound()
        {
            return ResultWithError("ERR_NOT_FOUND", "Không tìm thấy dữ liệu đã yêu cầu", 400);
        }
    }

    /// <summary>
    /// Mọi kết quả trả về của Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMethodResult<T>
    {
        /// <summary>
        /// Trạng thái thành công hay không
        /// </summary>
        bool Success { get; set; }
        /// <summary>
        /// Output trả về nếu thành công
        /// </summary>
        T Data { get; set; }
        /// <summary>
        /// Mã lỗi (nếu có)
        /// </summary>
        string Error { get; set; }
        /// <summary>
        /// Diễn giải cho lỗi (nếu có)
        /// </summary>
        string Message { get; set; }
        /// <summary>
        /// Mã lỗi trả về (trong trường hợp trả về qua http thì đây là http status code)
        /// </summary>
        int? Status { get; set; }
        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        int TotalRecord { get; set; }
    }

    [Serializable]
    public class MethodResult<T> : IMethodResult<T>
    {
        public bool Success { get; set; } = true;
        public T Data { get; set; } = default(T);
        public string Error { get; set; } = "";
        public string Message { get; set; } = "";
        public int? Status { get; set; }
        public int TotalRecord { get; set; }

        public static MethodResult<T> ResultWithData(T data, string message = "", int totalRecord = 0) => new MethodResult<T>()
        {
            Data = data,
            Message = message,
            TotalRecord = totalRecord,
            Status = 200,
        };

        public static MethodResult<T> ResultWithError(string error, int? status = null, string message = "", T data = default(T)) => new MethodResult<T>()
        {
            Success = false,
            Error = error,
            Message = message,
            Status = status,
            Data = data,
        };

        public static MethodResult<T> ResultWithAccessDenined()
        {
            return ResultWithError("ERR_FORBIDDEN", 403, "Bạn không đủ quyền để lấy dữ liệu đã yêu cầu");
        }

        public static MethodResult<T> ResultWithNotFound()
        {
            return ResultWithError("ERR_NOT_FOUND", 400, "Không tìm thấy dữ liệu đã yêu cầu");
        }
    }
}
