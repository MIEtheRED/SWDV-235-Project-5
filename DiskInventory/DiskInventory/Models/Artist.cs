using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DiskInventory.Models
{
    public partial class Artist
    {
        public Artist()
        {
            DiskHasArtists = new HashSet<DiskHasArtist>();
        }

        public int ArtistId { get; set; }
        [Required(ErrorMessage = "Please enter the name of the artist. If the artist does not have a last name, or is a group, enter the entire name into the \"First Name\" field.")]
        public string Fname { get; set; }
        public string Lname { get; set; }
        [Required(ErrorMessage = "Please select an Artist Type")]
        [Display(Name = "Artist Type")]
        public int? ArtistTypeId { get; set; }

        public virtual ArtistType ArtistType { get; set; }
        public virtual ICollection<DiskHasArtist> DiskHasArtists { get; set; }
    }
}
