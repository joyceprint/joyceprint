using System;

namespace JoycePrint.Web.Tests.Enums
{
    [Flags]
    public enum FieldCss
    {
        None = (1 << 0),
        Initial = (1 << 1),
        Touched = (1 << 2),
        Valid = (1 << 3),
        Invalid = (1 << 4),
        Optional = (1 << 5),
        Active = (1 << 6),
    }
}