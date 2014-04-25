using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PageNavigator
/// </summary>
/// 
namespace Hotels2thailand.Front
{
    public class PageNavigator
    {
        private byte _display = 1;
        private int _totalRecord;
        private int _pageSize;
        private int _pageCurrent;
        private string _url;

        public byte DisplayStyle{
            set{_display=value;}
        }
      
        public PageNavigator()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public PageNavigator(int totalRecord, int pageSize, int pageCurrent, string Url)
        {
            _totalRecord = totalRecord;
            _pageSize = pageSize;
            _pageCurrent = pageCurrent;
            _url = Url;
        }

        public string DisplayPage()
        {        
            string result = string.Empty;
            int totalPage = (int)Math.Ceiling((double)_totalRecord / _pageSize);           
            for (int intPage = 1; intPage <= totalPage; intPage++)
            {
                //result = result + "<a href=\"" +_url + "?page=" + intPage + "\">" + intPage + "</a> ";
                result = result + "<a href=\"" + _url + "\">" + intPage + "</a> ";
            }
            return result;
        }
    }
}