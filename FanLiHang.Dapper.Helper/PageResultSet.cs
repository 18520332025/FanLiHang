using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FanLiHang.Dapper.Helper
{
    public class PagerResultSet<T>
    {

        public PagerInfor Pager
        {
            get; set;
        }

        public List<T> Rows
        {
            get;
            set;
        }

        public PagerResultSet(IList<T> list, int total, PagerParameter pagerParameter)
        {
            this.Rows = new List<T>(list);
            this.Pager = new PagerInfor(total, pagerParameter);
        }
        public PagerResultSet(IList<T> list, PagerInfor pager)
        {
            this.Rows = new List<T>(list);
            this.Pager = pager;
        }
    }

    

    public class PagerInfor
    {
        public int Total
        {
            get;
            set;
        }
        public PagerParameter PagerParameter
        {
            get;
            set;
        }

        public PagerInfor(int total, PagerParameter pagerParameter)
        {
            Total = total;
            PagerParameter = pagerParameter;
            PageCount = (int)Math.Ceiling(((double)Total / pagerParameter.PageSize));
            HasNext = PageCount > PagerParameter.PageIndex;
            HasPrevious = PagerParameter.PageIndex > 1;
            StartRow = PagerParameter.PageSize * (PagerParameter.PageIndex - 1);
            EndRow = PagerParameter.PageIndex * PagerParameter.PageSize;
            if (EndRow > Total)
            {
                EndRow = Total;
            }
            bool canShowPreviousPageIndexs = PageCount - PagerParameter.PageIndex > ViewPageCount / 2;
            bool canShowNextPageIndexs = PagerParameter.PageIndex > ViewPageCount / 2;
            if (canShowNextPageIndexs && canShowPreviousPageIndexs)
            {
                ViewStartPageIndex = (PagerParameter.PageIndex) - ViewPageCount / 2;
                ViewEndPageIndex = (PagerParameter.PageIndex) + ViewPageCount / 2;
            }
            else if (canShowPreviousPageIndexs)
            {
                ViewStartPageIndex = 1;
                ViewEndPageIndex = ViewPageCount > PageCount ? PageCount : ViewPageCount;
            }
            else if (canShowNextPageIndexs)
            {
                ViewEndPageIndex = PageCount;
                ViewStartPageIndex = PageCount - ViewPageCount > 1 ? PageCount - ViewPageCount + 1 : 1;
            }
            else
            {
                ViewStartPageIndex = 1;
                ViewEndPageIndex = PageCount;
            }
        }
        private int ViewPageCount = 11;
        private int _pageCount;

        public int PageCount
        {
            get;
        }

        public bool HasNext
        {
            get;
        }

        public bool HasPrevious
        {
            get;
        }

        public int StartRow
        {
            get; set;
        }

        public int EndRow
        {
            get; set;
        }

        public int ViewStartPageIndex
        {
            get; set;
        }

        public int ViewEndPageIndex
        {
            get; set;
        }
    }
}
