using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test_Google_Api.Models.ViewModel
{
    public class SearchModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


    class SearchModelComparer : IEqualityComparer<SearchModel>
    {
        public bool Equals(SearchModel s1, SearchModel s2)
        {
            return s1.Id == s2.Id;
        }

        public int GetHashCode(SearchModel s)
        {
            return s.Id;
        }
    }
}