using JoycePrint.Web.Tests.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace JoycePrint.Web.Tests.Helpers
{
    static class Extensions
    {
        public static string Active = "active";
        public static string ValidText = "success-text";
        public static string InvalidText = "danger-text";
        public static string RequiredText = "orange-text";

        /// <summary>
        /// Update the css to the FieldCss type for the string this method is called on
        /// </summary>
        /// <param name="value">The value this method is called on</param>
        /// <param name="getFor">The css class to add</param>
        /// <returns></returns>
        public static string UpdateCssTo(this IMaterializeGroup value, string css, FieldCss getFor)
        {
            switch (getFor)
            {
                case FieldCss.Initial:
                    return css;
                case FieldCss.Active:
                    return css.Contains(Active) ? css : $"{css} {Active}";
                case FieldCss.Valid:
                    // We need to remove the orange-text class
                    css = css.Contains(RequiredText) ? css.RemoveCss(RequiredText) : css;
                    css = $"{css} {ValidText}";
                    return css;
                case FieldCss.Invalid:
                    // We need to remove the orange-text class
                    css = css.Contains(RequiredText) ? $"{css.RemoveCss(RequiredText)}" : css;
                    css = css.Contains(ValidText) ? $"{css.RemoveCss(ValidText)}" : css;
                    css = $"{css} {InvalidText}";
                    return css;
                default:
                    return css;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="getFor"></param>
        /// <param name="isTextarea"></param>
        /// <returns></returns>
        public static string UpdateCssTo(this IMaterializeGroup value, string css, FieldCss getFor, bool isTextarea)
        {
            if (isTextarea) css += MaterializeCssStyles.MaterializeTextarea;
            return value.UpdateCssTo(css, getFor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="getFor"></param>
        /// <param name="abortFor"></param>
        /// <returns></returns>
        public static string UpdateCssTo(this IMaterializeGroup value, string css, FieldCss getFor, FieldCss abortFor)
        {
            if (getFor == abortFor) return css;
            return value.UpdateCssTo(css, getFor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputGroup"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UpdateTextTo(this MaterializeInputGroup inputGroup, string value)
        {
            inputGroup.InputText = value;
            return inputGroup.InputText;
        }

        /// <summary>
        /// Remove the css class from the string this method is called on
        /// </summary>
        /// <param name="value">The string that may contain the css class to remove</param>
        /// <param name="css">The css class to remove</param>
        /// <returns></returns>
        public static string RemoveCss(this string value, string css)
        {
            var startIndex = value.IndexOf(css);

            // The css class is not present
            if (startIndex == -1) return value;

            // Remove the class and the space if the css class is not the first in the list
            startIndex = startIndex == 0 ? startIndex : startIndex - 1;
            var endIndex = startIndex == 0 ? css.Length : css.Length + 1;

            return value.Remove(startIndex, endIndex);
        }

        /// <summary>
        /// Assert Extension to replace the AssertHelper.AssertAreEqual method
        /// </summary>
        /// <param name="actual">The object the method is called on</param>
        /// <param name="expected">The expected value</param>
        /// <param name="field">The field being asserted on</param>
        public static void MatchesActual(this string expected, string actual, string field)
        {
            Assert.AreEqual(expected, actual, $"The expected {field} [{expected}] differs from the actual {field} [{actual}]");
        }

        /// <summary>
        /// Assert Extension to assert that css classes are the same
        /// This will handle a list of single whitespace seperated css class names
        /// </summary>
        /// <param name="actual">The object the method is called on</param>
        /// <param name="expected">The expected value</param>
        /// <param name="field">The field being asserted on</param>
        public static void MatchesActualCss(this string expected, string actual, string field)
        {
            char SPACE = ' ';

            var expectedCss = expected.Split(SPACE);
            var actualCss = actual.Split(SPACE);

            // Loop through all the classes and check 1 by 1 here
            foreach (var css in expectedCss)
            {
                if (!actualCss.Contains(css))
                    Assert.AreEqual(expected, actual, $"The expected {field} [{expected}] differs from the actual {field} [{actual}]");
            }
        }
    }
}