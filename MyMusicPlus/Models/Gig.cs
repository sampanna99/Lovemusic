using System;
using System.ComponentModel.DataAnnotations;

namespace MyMusicPlus.Models
{
    public class Gig
    {
        public int Id { get; set; }

        [Required]
        public ApplicationUser Artist { get; set; }


        public DateTime Datetime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        [Required]
        public Genre Genre { get; set; }



    }
}