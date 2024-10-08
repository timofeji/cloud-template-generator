using System;
using System.Collections.Generic;
using System.Text;

namespace TemplateGenerator
{
    [Serializable]
    public class ProcessResult
    {
        public Object ObjectProcessed;
        public Exception Exception;
        public string Result = "";
    }
}
