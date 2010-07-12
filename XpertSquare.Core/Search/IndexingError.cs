using System;

using XpertSquare.Core.Model;


namespace XpertSquare.Core.Search
{
    public class IndexingError
    {
        public XsQuestion Question {get;set;}
        public Exception Exception { get; set; } 
    }
}
