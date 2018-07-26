using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace MultiThreadingWebCoreDemo.Models
{
    [DataContract]
    public class MultiprocessModel
    {
        [DataMember(Name = "Id")]
        public int Id { get; set; }

        [DataMember(Name = "UserId")]
        public string UserId { get; set; }

        [DataMember(Name = "Module")]
        public string Module { get; set; }

        [DataMember(Name = "TotalRecords")]
        public int TotalRecords { get; set; }

        [DataMember(Name = "FailedRecords")]
        public int FailedRecords { get; set; }

        [DataMember(Name = "SuccessRecords")]
        public int SuccessRecords { get; set; }

        [DataMember(Name = "Percentage")]
        public decimal Percentage { get; set; }

        [DataMember(Name = "IsCompleted")]
        public bool IsCompleted { get; set; }
    }
}
