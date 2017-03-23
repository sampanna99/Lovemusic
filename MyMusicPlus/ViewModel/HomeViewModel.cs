using MyMusicPlus.Models;
using System.Collections.Generic;

namespace MyMusicPlus.ViewModel
{
    public class HomeViewModel
    {
        public bool ShowActions { get; set; }
        public IEnumerable<Gig> UpcomingGigs { get; set; }
        public string Heading { get; set; }
    }
}