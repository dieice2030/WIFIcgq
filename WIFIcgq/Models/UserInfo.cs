using System;
using System.Collections.Generic;

namespace WIFIcgq.Models
{
    public partial class UserInfo
    {
        public UserInfo()
        {
            DeviceInfo = new HashSet<DeviceInfo>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string DeviceModel { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string Coporation { get; set; }
        public string Department { get; set; }
        public string Sex { get; set; }

        public ICollection<DeviceInfo> DeviceInfo { get; set; }
    }
}
