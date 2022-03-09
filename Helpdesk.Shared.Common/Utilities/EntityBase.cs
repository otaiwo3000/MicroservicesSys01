using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Shared.Common.Utilities
{
    //[DataContract]
    public abstract class EntityBase //: IExtensibleDataObject
    {
        public EntityBase()
        {
            ////IsVisible = true;
            ////IsDeleted = false;
            CreatedOn = DateTime.Now;
            UpdatedOn = DateTime.Now;
        }

        //[DataMember]
        //public bool IsVisible { get; set; }
        //[DataMember]
        //public bool IsDeleted { get; set; }
        //[DataMember]
        public DateTime CreatedOn { get; set; }
        //[DataMember]
        public DateTime UpdatedOn { get; set; }
        //[DataMember]
        public string CreatedBy { get; set; } 
        //[DataMember]
        public string UpdatedBy { get; set; }
    }
}
