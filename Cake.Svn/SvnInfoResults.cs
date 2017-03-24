using System;

namespace Cake.Svn
{
    public class SvnInfoResults : SvnResultsBase
    {
        public SvnInfoResults()
        {
            RepositoryPath = string.Empty;
            RepositoryRoot = string.Empty;
            RepositoryUuid = string.Empty;
            NodeKind = string.Empty;
            LastChangedAuthor = string.Empty;
            LastChangedRevision = 0;
            LastChangedDate = DateTime.Now;
            Schedule = string.Empty;
        }

        public string RepositoryRoot { get; set; }
        public string RepositoryUuid { get; set; }
        public string NodeKind { get; set; }
        public string LastChangedAuthor { get; set; }
        public int LastChangedRevision { get; set; }
        public DateTime LastChangedDate { get; set; }
        public string Schedule { get; set; }
    }
}