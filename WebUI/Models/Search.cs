using System.Collections.Generic;

namespace WebUI.Models
{
     public class Search
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string imdbID { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }
    }

    public class Root1
    {
        public List<Search> Search { get; set; }
        public string totalResults { get; set; }
        public string Response { get; set; }
    }

}