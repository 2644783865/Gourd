using Gourd.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gourd.Application.ViewModel.Default
{
    public class GetUserListRequest
    {
        public string name { get; set; }

        public string pwd { get; set; }

        public int? pageIndex { get; set; }

        public int? pageSize { get; set; }

        public List<SortOrder> sortOrder { get; set; }
    }
}
