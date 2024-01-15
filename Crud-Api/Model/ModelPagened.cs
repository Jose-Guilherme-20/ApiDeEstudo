using System.Collections.Generic;

namespace Crud_Api.Model
{
    public class ModelPagened<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalResults { get; set; }
        public string SortFields { get; set; }
        public string SortDirections { get; set; }

        public Dictionary<string, object> Filters { get; set; }

        public List<T> List { get; set; }

        public ModelPagened()
        {

        }
        public ModelPagened(int currentPage, int pageSize, string sortFields, string sortDirections)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            SortFields = sortFields;
            SortDirections = sortDirections;
        }
        public ModelPagened(int currentPage, int pageSize, string sortFields, string sortDirections, Dictionary<string, object> filters)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            SortFields = sortFields;
            SortDirections = sortDirections;
            Filters = filters;
        }
        public ModelPagened(int currentPage, string sortFields, string sortDirections) : this(currentPage, 10, sortFields, sortDirections)
        {
            CurrentPage = currentPage;
            SortFields = sortFields;
            SortDirections = sortDirections;
        }
        public int GetCurrentPage()
        {
            return CurrentPage == 0 ? 2 : CurrentPage;
        }
        public int GetPageSize()
        {
            return PageSize == 0 ? 10 : PageSize;
        }
    }
}