using System;

namespace Cake.Svn
{
    public class SvnBaseResults
    {
        public string RepositoryPath { get; set; }
        public string StandardOutput { get; set; }
        public string StandardError { get; set; }
        public int Revision { get; set; }
        public int ExitCode { get; set; }
    }

    public class SvnInfoResults : SvnBaseResults
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