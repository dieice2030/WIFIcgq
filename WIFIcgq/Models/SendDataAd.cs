using System;
using System.Collections.Generic;

namespace WIFIcgq.Models
{
    public partial class SendDataAd
    {
        public int Id { get; set; }
        public string Data { get; set; }
        public DateTime? Time { get; set; }
        public string Backup1 { get; set; }
        public string Backup2 { get; set; }
    }
}
