using JoycePrint.Web.Tests.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using JoycePrint.Web.Tests.Helpers.Materialize;

namespace JoycePrint.Web.Tests.Helpers
{
    public static class Extensions
    {
        public static string Active = "active";
        public static string ValidText = "success-text";
        public static string InvalidText = "danger-text";
        public static string RequiredText = "orange-text";

        ///// <summary>
        ///// Update the css to the FieldCss type for the string this method is called on
        ///// </summary>
        ///// <param name="value">The value this method is called on</param>
        ///// <param name="getFor">The css class to add</param>
        ///// <returns></returns>
        //public static string UpdateCssTo(this IMaterializeGroup value, string css, FieldCss getFor)
        //{
        //    switch (getFor)
        //    {
        //        // TODO: we need to switch the css here based on
        //        // 1 - the state of the input - getFor
        //        // 2 - the type of tag in the input group
        //        // this entire function could also change
        //        case FieldCss.Initial:
        //            return css;
        //        case FieldCss.Active:
        //            return css.Contains(Active) ? css : $"{css} {Active}";
        //        case FieldCss.Valid:
        //            // We need to remove the orange-text class
        //            css = css.Contains(RequiredText) ? css.RemoveCss(RequiredText) : css;
        //            css = $"{css} {ValidText}";
        //            return css;
        //        case FieldCss.Invalid:
        //            // We need to remove the orange-text class
        //            css = css.Contains(RequiredText) ? $"{css.RemoveCss(RequiredText)}" : css;
        //            css = css.Contains(ValidText) ? $"{css.RemoveCss(ValidText)}" : css;
        //            css = $"{css} {InvalidText}";
        //            return css;
        //        default:
        //            return css;
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="css"></param>
        /// <param name="getFor"></param>
        /// <param name="cssClasses"></param>
        /// <returns></returns>
        public static string UpdateCssTo(this IMaterializeGroup value, string css, FieldCss getFor, Dictionary<FieldCss, string> cssClasses)
        {
            switch (getFor)
            {
                // TODO: we need to switch the css here based on
                // 1 - the state of the input - getFor
                // 2 - the type of tag in the input group
                // this entire function could also change
                case FieldCss.Initial:
                    return cssClasses[FieldCss.Initial];
                case FieldCss.Touched:
                    return cssClasses[FieldCss.Touched];
                case FieldCss.Valid:
                    return cssClasses[FieldCss.Valid];
                case FieldCss.Invalid:
                    return cssClasses[FieldCss.Invalid];
                case FieldCss.Optional:
                    return cssClasses[FieldCss.Optional];
                case FieldCss.Active:
                    return cssClasses[FieldCss.Active];
                default:
                    return css;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="css"></param>
        /// <param name="getFor"></param>
        /// <returns></returns>
        public static string UpdateCssForCollapse(this IMaterializeGroup value, string css, FieldCss getFor)
        {
            var cssClasses = new Dictionary<FieldCss, string>
            {
                {FieldCss.Initial, MaterializeCssStyles.MaterializeCollapseHeaderCssInitial},
                {FieldCss.Touched, null},
                {FieldCss.Valid, null},
                {FieldCss.Invalid, null},
                {FieldCss.Optional, null},
                {FieldCss.Active, MaterializeCssStyles.MaterializeCollapseHeaderCssActive}
            };

            return UpdateCssTo(value, css, getFor, cssClasses);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="css"></param>
        /// <param name="getFor"></param>
        /// <returns></returns>
        public static string UpdateCssForIcon(this IMaterializeGroup value, string css, FieldCss getFor)
        {
            var cssClasses = new Dictionary<FieldCss, string>();

            cssClasses.Add(FieldCss.Initial, MaterializeCssStyles.MaterializeInputGroupIconClassesInitial);
            cssClasses.Add(FieldCss.Touched, MaterializeCssStyles.MaterializeInputGroupIconClassesTouched);
            cssClasses.Add(FieldCss.Valid, MaterializeCssStyles.MaterializeInputGroupIconClassesValid);
            cssClasses.Add(FieldCss.Invalid, MaterializeCssStyles.MaterializeInputGroupIconClassesInValid);
            cssClasses.Add(FieldCss.Optional, MaterializeCssStyles.MaterializeInputGroupIconClassesOptional);
            cssClasses.Add(FieldCss.Active, MaterializeCssStyles.MaterializeInputGroupIconClassesActive);

            return UpdateCssTo(value, css, getFor, cssClasses);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="css"></param>
        /// <param name="getFor"></param>
        /// <returns></returns>
        public static string UpdateCssForLabel(this IMaterializeGroup value, string css, FieldCss getFor)
        {
            var cssClasses = new Dictionary<FieldCss, string>();

            cssClasses.Add(FieldCss.Initial, MaterializeCssStyles.MaterializeInputGroupLabelClassesInitial);
            cssClasses.Add(FieldCss.Touched, MaterializeCssStyles.MaterializeInputGroupLabelClassesTouched);
            cssClasses.Add(FieldCss.Valid, MaterializeCssStyles.MaterializeInputGroupLabelClassesValid);
            cssClasses.Add(FieldCss.Invalid, MaterializeCssStyles.MaterializeInputGroupLabelClassesInValid);
            cssClasses.Add(FieldCss.Optional, MaterializeCssStyles.MaterializeInputGroupLabelClassesOptional);
            cssClasses.Add(FieldCss.Active, MaterializeCssStyles.MaterializeInputGroupLabelClassesActive);

            return UpdateCssTo(value, css, getFor, cssClasses);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="css"></param>
        /// <param name="getFor"></param>
        /// <returns></returns>
        public static string UpdateCssForInput(this IMaterializeGroup value, string css, FieldCss getFor)
        {
            var cssClasses = new Dictionary<FieldCss, string>();

            cssClasses.Add(FieldCss.Initial, MaterializeCssStyles.MaterializeInputGroupInputClassesInitial);
            cssClasses.Add(FieldCss.Touched, MaterializeCssStyles.MaterializeInputGroupInputClassesTouched);
            cssClasses.Add(FieldCss.Valid, MaterializeCssStyles.MaterializeInputGroupInputClassesValid);
            cssClasses.Add(FieldCss.Invalid, MaterializeCssStyles.MaterializeInputGroupInputClassesInValid);
            cssClasses.Add(FieldCss.Optional, MaterializeCssStyles.MaterializeInputGroupInputClassesOptional);
            cssClasses.Add(FieldCss.Active, MaterializeCssStyles.MaterializeInputGroupInputClassesActive);

            return UpdateCssTo(value, css, getFor, cssClasses);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="css"></param>
        /// <param name="getFor"></param>
        /// <returns></returns>
        public static string UpdateCssForValidationLabel(this IMaterializeGroup value, string css, FieldCss getFor)
        {
            var cssClasses = new Dictionary<FieldCss, string>();

            cssClasses.Add(FieldCss.Initial, MaterializeCssStyles.MaterializeInputGroupValidationLabelClassesInitial);
            cssClasses.Add(FieldCss.Touched, MaterializeCssStyles.MaterializeInputGroupValidationLabelClassesTouched);
            cssClasses.Add(FieldCss.Valid, MaterializeCssStyles.MaterializeInputGroupValidationLabelClassesValid);
            cssClasses.Add(FieldCss.Invalid, MaterializeCssStyles.MaterializeInputGroupValidationLabelClassesInValid);
            cssClasses.Add(FieldCss.Optional, MaterializeCssStyles.MaterializeInputGroupValidationLabelClassesOptional);
            cssClasses.Add(FieldCss.Active, MaterializeCssStyles.MaterializeInputGroupValidationLabelClassesActive);

            return UpdateCssTo(value, css, getFor, cssClasses);
        }

        /// <summary>
        ///        
        /// </summary>
        /// <param name="value"></param>
        /// <param name="css"></param>
        /// <param name="getFor"></param>
        /// <returns></returns>
        /// <remarks>
        /// Automatically addes the materialize-textarea class
        /// </remarks>
        public static string UpdateCssForTextarea(this IMaterializeGroup value, string css, FieldCss getFor)
        {
            var cssClasses = new Dictionary<FieldCss, string>();

            cssClasses.Add(FieldCss.Initial, MaterializeCssStyles.MaterializeInputGroupInputClassesInitial + MaterializeCssStyles.MaterializeTextarea);
            cssClasses.Add(FieldCss.Touched, MaterializeCssStyles.MaterializeInputGroupInputClassesTouched + MaterializeCssStyles.MaterializeTextarea);
            cssClasses.Add(FieldCss.Valid, MaterializeCssStyles.MaterializeInputGroupInputClassesValid + MaterializeCssStyles.MaterializeTextarea);
            cssClasses.Add(FieldCss.Invalid, MaterializeCssStyles.MaterializeInputGroupInputClassesInValid + MaterializeCssStyles.MaterializeTextarea);
            cssClasses.Add(FieldCss.Optional, MaterializeCssStyles.MaterializeInputGroupInputClassesOptional + MaterializeCssStyles.MaterializeTextarea);
            cssClasses.Add(FieldCss.Active, MaterializeCssStyles.MaterializeInputGroupInputClassesActive + MaterializeCssStyles.MaterializeTextarea);

            return UpdateCssTo(value, css, getFor, cssClasses);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="css"></param>
        /// <param name="getFor"></param>
        /// <returns></returns>
        public static string UpdateCssForSpan(this IMaterializeGroup value, string css, FieldCss getFor)
        {
            var cssClasses = new Dictionary<FieldCss, string>();

            cssClasses.Add(FieldCss.Initial, MaterializeCssStyles.MaterializeSelectGroupSpanClassesInitial);
            cssClasses.Add(FieldCss.Touched, null);
            cssClasses.Add(FieldCss.Valid, null);
            cssClasses.Add(FieldCss.Invalid, null);
            cssClasses.Add(FieldCss.Optional, null);
            cssClasses.Add(FieldCss.Active, null);

            return UpdateCssTo(value, css, getFor, cssClasses);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="css"></param>
        /// <param name="getFor"></param>
        /// <returns></returns>
        public static string UpdateCssForUnOrderedList(this IMaterializeGroup value, string css, FieldCss getFor)
        {
            var cssClasses = new Dictionary<FieldCss, string>();

            cssClasses.Add(FieldCss.Initial, MaterializeCssStyles.MaterializeSelectGroupUnOrderedListClassesInitial);
            cssClasses.Add(FieldCss.Touched, null);
            cssClasses.Add(FieldCss.Valid, null);
            cssClasses.Add(FieldCss.Invalid, null);
            cssClasses.Add(FieldCss.Optional, null);
            cssClasses.Add(FieldCss.Active, null);

            return UpdateCssTo(value, css, getFor, cssClasses);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="css"></param>
        /// <param name="getFor"></param>
        /// <returns></returns>
        public static string UpdateCssForUnOrderedListSelectedItem(this IMaterializeGroup value, string css, FieldCss getFor)
        {
            var cssClasses = new Dictionary<FieldCss, string>();

            cssClasses.Add(FieldCss.Initial, MaterializeCssStyles.MaterializeSelectGroupUnOrderedListSelectedItemClassesInitial);
            cssClasses.Add(FieldCss.Touched, null);
            cssClasses.Add(FieldCss.Valid, null);
            cssClasses.Add(FieldCss.Invalid, null);
            cssClasses.Add(FieldCss.Optional, null);
            cssClasses.Add(FieldCss.Active, null);

            return UpdateCssTo(value, css, getFor, cssClasses);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="css"></param>
        /// <param name="getFor"></param>
        /// <returns></returns>
        public static string UpdateCssForSelectList(this IMaterializeGroup value, string css, FieldCss getFor)
        {
            var cssClasses = new Dictionary<FieldCss, string>();

            cssClasses.Add(FieldCss.Initial, MaterializeCssStyles.MaterializeSelectGroupSelectListClassesInitial);
            cssClasses.Add(FieldCss.Touched, null);
            cssClasses.Add(FieldCss.Valid, null);
            cssClasses.Add(FieldCss.Invalid, null);
            cssClasses.Add(FieldCss.Optional, null);
            cssClasses.Add(FieldCss.Active, null);

            return UpdateCssTo(value, css, getFor, cssClasses);
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="value"></param>
        ///// <param name="getFor"></param>
        ///// <param name="isTextarea"></param>
        ///// <returns></returns>
        //public static string UpdateCssTo(this IMaterializeGroup value, string css, FieldCss getFor, bool isTextarea)
        //{
        //    if (isTextarea) css += MaterializeCssStyles.MaterializeTextarea;
        //    return value.UpdateCssTo(css, getFor);
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="value"></param>
        ///// <param name="getFor"></param>
        ///// <param name="abortFor"></param>
        ///// <returns></returns>
        //public static string UpdateCssTo(this IMaterializeGroup value, string css, FieldCss getFor, FieldCss abortFor)
        //{
        //    if (getFor == abortFor) return css;
        //    return value.UpdateCssTo(css, getFor);
        //}

        /// <summary>
        /// Updates the value of the input element [InputText] for the input group to the value passed in
        /// </summary>
        /// <param name="inputGroup">The MaterializeInputGroup containing the input element</param>
        /// <param name="value">The new value for the input element</param>
        /// <returns>The value of the input element or the input group</returns>
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
            const char space = ' ';

            var expectedCss = expected.Split(space);
            var actualCss = actual.Split(space);

            //Assert.AreEqual(expected.Length, actualCss.Length, $"The expected number of css classes [{expected}] for the field {field} differs from the actual [{actual}]");

            // Loop through all the classes and check 1 by 1 here
            foreach (var css in expectedCss)
            {
                if (!actualCss.Contains(css))
                    Assert.AreEqual(expected, actual, $"The expected {field} [{expected}] differs from the actual {field} [{actual}]");
            }
        }
    }
}