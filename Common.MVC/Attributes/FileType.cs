using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Common.MVC.Attributes
{
    public class FileTypesAttribute : ValidationAttribute
    {
        private readonly List<string> _types;
        private readonly bool _disallow;

        public FileTypesAttribute(string types, bool disallow = false)
        {
            _types = types.Split(',').ToList();
            _disallow = disallow;
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;

            var httpPostedFileBase = value as HttpPostedFileBase;

            var extension = System.IO.Path.GetExtension(httpPostedFileBase?.FileName);
            if (extension == null) return true;

            var fileExt = extension.Substring(1);

            return _disallow
                ? !_types.Contains(fileExt, StringComparer.OrdinalIgnoreCase)
                : _types.Contains(fileExt, StringComparer.OrdinalIgnoreCase);
        }

        public override string FormatErrorMessage(string name)
        {
            return $"Invalid file type. Only the following types {string.Join(", ", _types)} are supported.";
        }
    }
}