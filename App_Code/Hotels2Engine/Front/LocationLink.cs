using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LocationLink
/// </summary>
/// 

namespace Hotels2thailand.Front
{
    public class LocationLink
    {
        public short DestinationID { get; set; }
        public string DestinationTitle { get; set; }
        public string DestinationFileName { get; set; }
        public short LocationID { get; set; }
        public string LocationTitle { get; set; }
        public string LocationFileName { get; set; }
        public string FolderDestination { get; set; }
        public string FolderLocation { get; set; }

        public LocationLink()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}