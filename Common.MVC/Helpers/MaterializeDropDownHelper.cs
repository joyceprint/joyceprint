using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;

namespace Common.MVC.Helpers
{
    public static class MaterializeDropDownHelper
    {
        /// <summary>
        /// 
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private static readonly SelectListItem[] SingleEmptyItem = { new SelectListItem { Text = "", Value = "" } };        

        /// <summary>
        /// Create a materialize drop down using an enum
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="optionLabel"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="optionAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString MaterializeEnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, string optionLabel, object htmlAttributes = null, object optionAttributes = null)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var enumType = GetNonNullableModelType(metadata);
            var values = Enum.GetValues(enumType).Cast<TEnum>();

            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            var id = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName);

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
                if (htmlAttributes != null)
                    ul.MergeAttribute(prop.Name.Replace('_', '-'), prop.GetValue(htmlAttributes)?.ToString(), true);
            }

            var dropdown = new MvcHtmlString(trigger + ul.ToString());

            return dropdown;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string GetEnumDisplay<TEnum>(TEnum value)
        {
            var fi = value.GetType().GetField(value.ToString());

            var attributes = (DisplayNameAttribute[])fi.GetCustomAttributes(typeof(DisplayNameAttribute), false);

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if ((attributes != null) && (attributes.Length > 0))
                return attributes[0].DisplayName;

            return value.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        private static TagBuilder AddOption<TEnum>(TEnum value)
        {
            return AddOption(GetEnumDisplay(value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static TagBuilder AddOption(string value)
        {
            var li = new TagBuilder("li");
            var a = new TagBuilder("a");

            a.SetInnerText(value);
            a.Attributes.Add("href", "#!");

            li.InnerHtml = a.ToString();

            return li;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelMetadata"></param>
        /// <returns></returns>
        private static Type GetNonNullableModelType(ModelMetadata modelMetadata)
        {
            var realModelType = modelMetadata.ModelType;

            var underlyingType = Nullable.GetUnderlyingType(realModelType);

            if (underlyingType != null)
            {
                realModelType = underlyingType;
            }

            return realModelType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private static string GetEnumDescription<TEnum>(TEnum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if ((attributes != null) && (attributes.Length > 0))
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}