using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helpdesk.Service.Misc
{
    public class TreeStructureModel
    {

    }

    public class Menu_SubObjectListInfo_2
    {
        public int OrganizationDocumentId { get; set; }
        public string GroupName { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string FilePathANDFileName { get; set; }
        public string Alias { get; set; }
        ////public string UIsrefState { get; set; }
        //public string ParameterKey { get; set; }
        public string FileType { get; set; }
        public int? Position { get; set; }

    }

    public class Menu_SubObjectListInfo_2B
    {
        public int OrganizationDocumentId { get; set; }
        public string FileName { get; set; }
        public string Alias { get; set; }
        ////public string UIsrefState { get; set; }
        //public string ParameterKey { get; set; }
        public string FilePath { get; set; }
        public string FilePathANDFileName { get; set; }
        public string FileType { get; set; }
        public int? Position { get; set; }
    }

    public class UrlMenuListInfo
    {
        public string UrlMenu { get; set; }
        public List<MenuList> MenuMenuList { get; set; }
    }

    public class MenuList
    {
        public string MenulistColumn { get; set; }
        public List<Menu_SubObjectListInfo_2B> OtherMenus { get; set; }
    }

    public class FilePathWithFileName
    {
        public string filepathANDname { get; set; }
        public string filename { get; set; }
    }

}
