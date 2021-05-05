using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DiskInventory.Models
{
    public partial class Disk
    {
        public Disk()
        {
            DiskHasArtists = new HashSet<DiskHasArtist>();
            DiskHasBorrowers = new HashSet<DiskHasBorrower>();
        }

        public int DiskId { get; set; }
        [Required(ErrorMessage = "Please enter a disk name.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Please enter more than 3 chars.")]
        public string DiskName { get; set; }
        [Required(ErrorMessage = "Please enter a date.")]
        public DateTime ReleaseDate { get; set; }
        [Required(ErrorMessage ="Please enter a genre.")]
        public int? GenreId { get; set; }
        [Required(ErrorMessage = "Please enter a disk status.")]
        public int? StatusId { get; set; }
        [Required(ErrorMessage = "Please enter a disk type.")]
        public int? DiskTypeId { get; set; }

        public virtual DiskType DiskType { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<DiskHasArtist> DiskHasArtists { get; set; }
        public virtual ICollection<DiskHasBorrower> DiskHasBorrowers { get; set; }
    }
}
