using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hotels2thailand.LinqProvider.Staff;

/// <summary>
/// Summary description for StaffActivity
/// </summary>
/// 
namespace Hotels2thailand.Staffs
{
    public enum StaffLogActionType : byte
    {
        Insert = 1,
        Update = 2,
        Delete = 3,
        Authorization = 4
    }
}
