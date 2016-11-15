using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace JoycePrint.Web.Extensions
{
    public static class HtmlDropDownExtensions
    {
        #region Unused Helpers

        //[Obsolete]
        //public static MvcHtmlString EnumDropDownList<TEnum>(this HtmlHelper htmlHelper, string name, TEnum selectedValue)
        //{
        //    IEnumerable<TEnum> values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();

        //    IEnumerable<SelectListItem> items =
        //        from value in values
        //        select new SelectListItem
        //        {
        //            Text = value.ToString(),
        //            Value = value.ToString(),
        //            Selected = (value.Equals(selectedValue))
        //        };

        //    return htmlHelper.DropDownList(name, items);
        //}

        //[Obsolete]
        //public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
        //{
        //    ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
        //    Type enumType = GetNonNullableModelType(metadata);
        //    IEnumerable<TEnum> values = Enum.GetValues(enumType).Cast<TEnum>();

        //    TypeConverter converter = TypeDescriptor.GetConverter(enumType);

        //    IEnumerable<SelectListItem> items =
        //        from value in values
        //        select new SelectListItem
        //        {
        //            Text = converter.ConvertToString(value),
        //            Value = value.ToString(),
        //            Selected = value.Equals(metadata.Model)
        //        };

        //    if (metadata.IsNullableValueType)
        //    {
        //        items = SingleEmptyItem.Concat(items);
        //    }

        //    return htmlHelper.DropDownListFor(expression, items);
        //}

        #endregion

        private static readonly SelectListItem[] SingleEmptyItem = new[] { new SelectListItem { Text = "", Value = "" } };

        public static string GetEnumDescription<TEnum>(TEnum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if ((attributes != null) && (attributes.Length > 0))
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static string GetEnumDisplay<TEnum>(TEnum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DisplayNameAttribute[] attributes = (DisplayNameAttribute[])fi.GetCustomAttributes(typeof(DisplayNameAttribute), false);

            if ((attributes != null) && (attributes.Length > 0))
                return attributes[0].DisplayName;
            else
                return value.ToString();
        }

        public static MvcHtmlString MaterializeEnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, string optionLabel)
        {
            return MaterializeEnumDropDownListFor(htmlHelper, expression, optionLabel, null, null);
        }

        public static MvcHtmlString MaterializeEnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, string optionLabel, object htmlAttributes, object optionAttributes = null)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            Type enumType = GetNonNullableModelType(metadata);
            IEnumerable<TEnum> values = Enum.GetValues(enumType).Cast<TEnum>();

            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string id = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName);

            if (string.IsNullOrEmpty(optionLabel))
                optionLabel = "Please select an option";

            var trigger = new TagBuilder("a");
            trigger.AddCssClass("dropdown-button btn");
            trigger.Attributes.Add("href", "#");
            trigger.Attributes.Add("data-activates", id);
            trigger.SetInnerText(optionLabel);

            var ul = new TagBuilder("ul");
            
            foreach (var value in values)
                ul.InnerHtml += AddOption(value);

            ul.AddCssClass("dropdown-content");

            ul.Attributes.Add("id", id);

            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(htmlAttributes))
            {
                // By adding the 'true' as the third parameter, you can overwrite whatever default attribute you have set earlier.
                ul.MergeAttribute(prop.Name.Replace('_', '-'), prop.GetValue(htmlAttributes).ToString(), true);
            }

            var dropdown = new MvcHtmlString(trigger.ToString() + ul.ToString());

            return dropdown;

            #region old
            //IEnumerable<SelectListItem> items = from value in values
            //                                    select new TagBuilder("li")
            //                                    {
            //                                        InnerHtml = new TagBuilder("a").SetInnerText(GetEnumDisplay(value))
            //                                        //Attributes = ,
            //                                        //Value = value.ToString(),
            //                                        //Selected = value.Equals(metadata.Model)
            //                                    };

            // If the enum is nullable, add an 'empty' item to the collection
            //if (metadata.IsNullableValueType)
            //    items = SingleEmptyItem.Concat(items);

            //return htmlHelper.DropDownListFor(expression, items, htmlAttributes);
            #endregion
        }

        private static TagBuilder AddOption<TEnum>(TEnum value)
        {
            return AddOption(GetEnumDisplay(value));
        }

        private static TagBuilder AddOption(string value)
        {
            var li = new TagBuilder("li");
            var a = new TagBuilder("a");

            a.SetInnerText(value);
            a.Attributes.Add("href", "#!");

            li.InnerHtml = a.ToString();

            return li;
        }

        private static Type GetNonNullableModelType(ModelMetadata modelMetadata)
        {
            Type realModelType = modelMetadata.ModelType;

            Type underlyingType = Nullable.GetUnderlyingType(realModelType);
            if (underlyingType != null)
            {
                realModelType = underlyingType;
            }
            return realModelType;
        }
    }
}