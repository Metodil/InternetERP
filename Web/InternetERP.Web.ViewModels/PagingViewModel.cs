﻿namespace InternetERP.Web.ViewModels
{
    using System;

    public class PagingViewModel
    {
        public string AspAction { get; set; }

        public int PageNumber { get; set; }

        public int PagesCount => (int)Math.Ceiling((double)this.ItemsCount / this.ItemsPerPage);

        public bool HasPreviousPage => this.PageNumber > 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int PreviousPageNumber => this.PageNumber - 1;

        public int NextPageNumber => this.PageNumber + 1;

        public int ItemsCount { get; set; }

        public int ItemsPerPage { get; set; }

        public string FilterBy { get; set; }
    }
}
