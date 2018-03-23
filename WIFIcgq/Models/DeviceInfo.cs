using System;
using System.Collections.Generic;

namespace WIFIcgq.Models
{
    public partial class DeviceInfo
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string DeviceId { get; set; }

        public UserInfo UserNameNavigation { get; set; }
    }
}
