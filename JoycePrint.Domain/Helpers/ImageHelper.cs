using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JoycePrint.Domain.Helpers
{
    public static class ImageHelper
    {
        private static readonly string[] CacheBustFor = { "src", "srcset" };

        /// <summary>        
        /// Creates an image tag that stamps each image, to prevent issues with browsers caching old images
        /// This method uses the LastWriteTime to generate the time stamp
        /// <para />
        /// Add srcset attributes, using a ':' to seperate the image path from corresponding image width and
        /// using a ',' to seperate different image path and image width combinations
        /// <para />        
        /// Example:        
        /// /Content/images/homepage-21.jpg:1367w,/Content/images/homepage-21.jpg:1367w        
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>        
        public static MvcHtmlString Image(this HtmlHelper helper, object htmlAttributes)
        {
            var img = new TagBuilder("img");

            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(htmlAttributes))
            {
                var cacheBusterValue = prop.GetValue(htmlAttributes)?.ToString();

                if (CacheBustFor.Contains(prop.Name))
                    cacheBusterValue = GenerateCacheBuster(prop.Name, prop.GetValue(htmlAttributes)?.ToString());

                // By adding the 'true' as the third parameter, you can overwrite whatever default attribute you have set earlier.
                img.MergeAttribute(prop.Name.Replace('_', '-'), cacheBusterValue, true);
            }

            return MvcHtmlString.Create(img.ToString(TagRenderMode.SelfClosing));
        }

        /// <summary>
        /// Generate the cache buster image path
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string GenerateCacheBuster(string prop, string value)
        {
            string cacheBusterValue;

            cacheBusterValue = prop.Equals("srcset", StringComparison.OrdinalIgnoreCase) ? HandleSrcSet(value) : GetPath(value);

            return cacheBusterValue;
        }

        /// <summary>
        /// Handles the srcset attribute of the image tag
        /// 
        /// srcset expects a value in the form - { path height, path height etc }
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>
        /// Example:   
        /// /Content/images/homepage-21.jpg:1367w, /Content/images/homepage-21.jpg:1367w
        /// </remarks>
        private static string HandleSrcSet(string value)
        {
            const char itemSeperator = ':';
            const char infoSeperator = ',';

            var srcset = string.Empty;

            var imgsInfo = value.Split(infoSeperator);

            foreach (var info in imgsInfo)
            {
                var imgInfo = info.Split(itemSeperator);

                var path = GetPath(imgInfo[0]);

                srcset += $"{path} {imgInfo[1]}, ";
            }

            srcset = srcset.Remove(srcset.Length - 2);

            return srcset;
        }

        /// <summary>
        /// Gets the path and appends the Last Write Time to it
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string GetPath(string value)
        {
            var path = HttpContext.Current.Server.MapPath(value);

            if (path == null) return string.Empty;

            return value + "?v=" + string.Format(File.GetLastWriteTime(path).ToString("MMddyyhhmmss"));
        }
    }
}