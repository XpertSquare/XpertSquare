using System;
using System.Collections.Generic;

using XpertSquare.Core.Model;

namespace XpertSquare.Web.Models
{
    public class QuestionViewModel
    {
        public XsQuestion Question { get; set; }
        public IList<XsQuestion> RelatedQuestions { get; set; }        
    }
}
