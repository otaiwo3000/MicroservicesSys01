
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UserMgt.Shared.Common.Contracts;
using UserMgt.Shared.Common.Utilities;

namespace UserMgt.Shared.Entities
{
    public class PendingEmail : EntityBase, IIdentifiableEntity
    {
        public int Id { get; set; }
        public string RecepientEmails { get; set; }
        public string MailSubject { get; set; }
        public string MailContent { get; set; }
        public bool IsSent { get; set; }
        public int RetryCount { get; set; }
        public string Source { get; set; }

        public int EntityId
        {
            get
            {
                return Id;
            }
        }

    }
}
