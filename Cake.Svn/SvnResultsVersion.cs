using System;

namespace Cake.Svn
{
    public class SvnResultsVersion
    {
        public int Revision { get; set; }
        public int LowRevision { get; set; }
        public bool Modifications { get; set; }
        public bool Switched { get; set; }
        public bool Exported { get; set; }
    }
}