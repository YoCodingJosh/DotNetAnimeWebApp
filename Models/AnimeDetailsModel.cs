using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeWebApp.Models
{
    public class AnimeDetailsModel
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string EnglishTitle { get; set; }
        public string Studios { get; set; }
        public string Synopsis { get; set; }
        public string ImageURL { get; set; }
        public string Genres { get; set; }
        public bool Airing { get; set; }
        public string Premiered { get; set; }
        public int? Episodes { get; set; }
        public int? Score { get; set; }
        public string? ListStatus { get; set; }
    }
}
