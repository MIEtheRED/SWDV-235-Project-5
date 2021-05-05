using System;
using System.Collections.Generic;

#nullable disable

namespace DiskInventory.Models
{
    public partial class DiskHasArtist
    {
        public int DiskHasArtistId { get; set; }
        public int DiskId { get; set; }
        public int ArtistId { get; set; }

        public virtual Artist Artist { get; set; }
        public virtual Disk Disk { get; set; }
    }
}
