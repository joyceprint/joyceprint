using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoycePrint.Web.Tests.Helpers
{
    public enum MaterializeTag
    {
        None,
        Icon,
        Input,
        Textarea,
        Label,
        ValidationLabel,
        Span,
        UnOrderedList,
        ListItem,
        Select,
        Option
    }

    interface IMaterializeGroup
    {
        //MaterializeTag Tag { get; set; }
    }
}
