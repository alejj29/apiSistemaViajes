using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control14.NetCore.Domain
{
    public class Response<T>
    {
        //public int Rows { get; set; }
        //public bool Success { get; set; }
        //public string Message { get; set; }
        public T Data { get; set; }
    }
    public class OperationResult
    {
        public int iRpta { get; set; }
        public string sRpta { get; set; }
        public bool bRpta { get; set; }        
        
    }
    public class OperationResultBase
    {
        public bool Success { get; set; }
        public int Rows { get; set; }
        public string Message { get; set; }

    }
}
