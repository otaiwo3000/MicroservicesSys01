using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Helpdesk.Service.DtoModels
{
    public abstract class SupportChannelsAbstract
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }


    public class SupportChannelsModel : SupportChannelsAbstract
    {
        public int SupportChannelId { get; set; }
    }

    public class SupportChannelsCreateModel : SupportChannelsAbstract
    {

    }

    public class SupportChannelsUpdateModel : SupportChannelsAbstract
    {

    }
}
