using QueryBuilderP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QueryBuilderP
{
    internal class BannedGame : IClassModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Series { get; set; }
        public string Country { get; set; }
        public string Details { get; set; }
        private static int lastId = 1; //used for the incrementing 

        public BannedGame()
        {

        }

        public BannedGame(string title, string series, string country, string details)
        {
            Id = lastId++; //used for the incrementing 
            Title = title;
            Series = series;
            Country = country;
            Details = details;
        }

        public override string ToString()
        {
            return $"ID: {Id} TITLE: {Title} SERIES: {Series} ";
        }
    }
}
