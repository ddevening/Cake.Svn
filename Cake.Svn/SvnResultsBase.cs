using System;

namespace Cake.Svn
{
    public class SvnResultsBase
    {
        public string RepositoryPath { get; set; }
        public string StandardOutput { get; set; }
        public string StandardError { get; set; }
        public int Revision { get; set; }
        public int ExitCode { get; set; }
    }
}