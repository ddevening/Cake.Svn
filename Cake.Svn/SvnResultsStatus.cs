﻿using System;
using System.Collections.Generic;

namespace Cake.Svn
{
    public class SvnResultsStatus : SvnResultsBase
    {
        public SvnResultsStatus()
        {
            ItemSpec = "";
            MetadataNames = new List<string>();
            //MetadataCount = 0;
        }
        public string ItemSpec { get; set; }
        public List<string> MetadataNames { get; set; }
        //public int MetadataCount { get; set; }
        

        public int MetadataCount
        {
            get { return MetadataNames.Count; }
        }
    }
}