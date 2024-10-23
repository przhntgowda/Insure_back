using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlShamil.Model.Dto
{
    public  class BaseDto
    {
        public DateTime CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }

        //[DefaultValue(true)]
        public bool IsActive { get; set; }=true;
        //public BaseDto() 
        //{ 
        //   CreatedOn= DateTime.Now;
        //}
    }
}
