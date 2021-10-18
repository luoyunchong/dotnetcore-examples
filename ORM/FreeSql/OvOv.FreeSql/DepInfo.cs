using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FreeSql.DataAnnotations;

namespace OvOv.FreeSql
{
    public class DepInfo
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int DEPARTMENTID { get; set; }
        public string DepName { get; set; }

        public DepType DepType { get; set; }
    }

    public enum DepType
    {
        TEST,
        TEST2
    }
}
