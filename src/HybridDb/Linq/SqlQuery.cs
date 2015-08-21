using System.Collections.Generic;

namespace HybridDb.Linq
{
    public class SqlQuery
    {
        public string Select { get; set; }
        public string Where { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public string OrderBy { get; set; }
        public IDictionary<string, object> Parameters { get; set; }
        public ExecutionSemantics ExecutionMethod { get; set; }

        public enum ExecutionSemantics
        {
            Single,
            SingleOrDefault,
            First,
            FirstOrDefault,
            Enumeration
        }
    }
}