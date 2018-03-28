using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericCrudOperations
{
    class EmployeeDTO
    {
        public int EmpNo { get; set; }
        public String EName { get; set; }
        public string job { get; set; }
        public int mgr { get; set; }
        public DateTime HireDate { get; set; }
        public String Sal { get; set; }
        public int comm { get; set; }
      
        int deptno { get; set; }

    }
}
