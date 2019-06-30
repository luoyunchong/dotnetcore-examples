namespace Webapi.FreeSql.Web
{
    /// <summary>
    /// 分页信息
    /// </summary>
    public class PageDto
    {
        /// <summary>
        /// 一页多少个数据
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// 第几页，从1开始。
        /// </summary>
        public int PageNumber { get; set; } = 1;

    }
}
