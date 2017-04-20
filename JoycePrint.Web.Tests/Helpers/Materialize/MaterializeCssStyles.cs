namespace JoycePrint.Web.Tests.Helpers.Materialize
{
    public static class MaterializeCssStyles
    {
        public const string MaterializeTextarea = " materialize-textarea";

        #region Materialize Input Group

        #region FieldCss Initial

        public static string MaterializeInputGroupIconClassesInitial => "material-icons prefix orange-text text-accent-4";

        public static string MaterializeInputGroupInputClassesInitial => "validate";

        public static string MaterializeInputGroupLabelClassesInitial => null;

        public static string MaterializeInputGroupValidationLabelClassesInitial => "val-msg";

        #endregion

        #region FieldCss Touched

        public static string MaterializeInputGroupIconClassesTouched => "material-icons prefix orange-text text-accent-4";

        public static string MaterializeInputGroupInputClassesTouched => "touched";

        public static string MaterializeInputGroupLabelClassesTouched => null;

        public static string MaterializeInputGroupValidationLabelClassesTouched => "val-msg";

        #endregion

        #region FieldCss Valid

        public static string MaterializeInputGroupIconClassesValid => "material-icons prefix text-accent-4 success-text";

        public static string MaterializeInputGroupInputClassesValid => "validate touched valid";

        public static string MaterializeInputGroupLabelClassesValid => "active";

        public static string MaterializeInputGroupValidationLabelClassesValid => "val-msg active";

        #endregion

        #region FieldCss InValid

        public static string MaterializeInputGroupIconClassesInValid => "material-icons prefix text-accent-4 danger-text";

        public static string MaterializeInputGroupInputClassesInValid => "validate touched invalid";

        public static string MaterializeInputGroupLabelClassesInValid => "active";

        public static string MaterializeInputGroupValidationLabelClassesInValid => "val-msg active";

        #endregion

        #region FieldCss Optional

        public static string MaterializeInputGroupIconClassesOptional => "material-icons prefix primary-text";

        public static string MaterializeInputGroupInputClassesOptional => null;

        public static string MaterializeInputGroupLabelClassesOptional => null;

        public static string MaterializeInputGroupValidationLabelClassesOptional => null;

        #endregion

        #region FieldCss Active

        public static string MaterializeInputGroupIconClassesActive => "material-icons prefix orange-text text-accent-4";

        public static string MaterializeInputGroupInputClassesActive => "validate";

        public static string MaterializeInputGroupLabelClassesActive => null;

        public static string MaterializeInputGroupValidationLabelClassesActive => "val-msg";

        #endregion

        #endregion

        #region Materialize Select Group

        #region FieldCss Initial

        public static string MaterializeSelectGroupIconClassesInitial => "material-icons prefix orange-text text-accent-4";

        public static string MaterializeSelectGroupInputClassesInitial => "select-dropdown validate";

        public static string MaterializeSelectGroupLabelClassesInitial => null;

        public static string MaterializeSelectGroupSpanClassesInitial => "caret";

        public static string MaterializeSelectGroupUnOrderedListClassesInitial => "dropdown-content select-dropdown ";
        public static string MaterializeSelectGroupUnOrderedListSelectedItemClassesInitial => null;

        public static string MaterializeSelectGroupSelectListClassesInitial => "validate initialized";
        
        public static string MaterializeSelectGroupValidationLabelClassesInitial => "val-msg";

        #endregion

        #endregion

        #region Materialize Collapse

        #region FieldCss Initial

        public static string MaterializeCollapseHeaderCssInitial => "collapsible-header primary-text valign-wrapper";

        public static string MaterializeCollapseBodyCssInitial => "collapsible-body";

        #endregion

        #region FieldCss Active

        public static string MaterializeCollapseHeaderCssActive => "collapsible-header primary-text valign-wrapper active";

        public static string MaterializeCollapseBodyCssActive => "collapsible-body";

        #endregion

        #endregion
    }
}