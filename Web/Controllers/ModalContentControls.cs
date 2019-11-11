using Web.Models;
using Web.Extends;

namespace System.Web.Mvc.Html
{
    public static class ModalContentControls
    {
        public static MvcHtmlString StaticModalButton(this HtmlHelper htmlHelper, string id, string action, string label, ButtonType buttonType, BootstrapContext? context, string targetContainer = null, string buttonClass = null)
        {
            TagBuilder button;
            string baseCssClass;
            switch (buttonType)
            {
                case ButtonType.LinkButton:
                    button = new TagBuilder("a");
                    baseCssClass = "btn";
                    break;
                case ButtonType.LinkBadge:
                    button = new TagBuilder("a");
                    baseCssClass = "badge";
                    break;
                case ButtonType.Button:
                default:
                    button = new TagBuilder("button");
                    baseCssClass = "btn";
                    break;
            }
            button.GenerateId(string.Format("btn{0}{1}", action, id));
            button.AddCssClass(baseCssClass);
            if (context != null) button.AddCssClass(string.Format(new CustomStringFormat(), "{0}-{1:L}", baseCssClass, context));
            if (!string.IsNullOrEmpty(buttonClass)) button.AddCssClass(buttonClass);
            button.InnerHtml = label;
            button.MergeAttribute("data-id", id);
            button.MergeAttribute("data-toggle", "modal");
            button.MergeAttribute("data-target", string.Format("#modStaticModal_{0}", action));
            button.MergeAttribute("data-trgcon", targetContainer);
            return MvcHtmlString.Create(button.ToString(TagRenderMode.Normal));
        }
        public static MvcHtmlString DynamicModalButton(this HtmlHelper htmlHelper, string id, string action, string label, ButtonType buttonType, BootstrapContext? context, string targetContainer = null, string buttonClass = null)
        {
            TagBuilder button;
            string baseCssClass;
            switch (buttonType)
            {
                case ButtonType.LinkButton:
                    button = new TagBuilder("a");
                    baseCssClass = "btn";
                    break;
                case ButtonType.LinkBadge:
                    button = new TagBuilder("a");
                    baseCssClass = "badge";
                    break;
                case ButtonType.Button:
                default:
                    button = new TagBuilder("button");
                    baseCssClass = "btn";
                    break;
            }
            button.AddCssClass(baseCssClass);
            if (context != null) button.AddCssClass(string.Format(new CustomStringFormat(), "{0}-{1:L}", baseCssClass, context));
            if (!string.IsNullOrEmpty(buttonClass)) button.AddCssClass(buttonClass);
            button.InnerHtml = label;
            button.MergeAttribute("data-id", id);
            button.MergeAttribute("data-action", action);
            button.MergeAttribute("data-toggle", "modal");
            button.MergeAttribute("data-target", "#modDynamicModal");
            button.MergeAttribute("data-trgcon", targetContainer);
            return MvcHtmlString.Create(button.ToString(TagRenderMode.Normal));
        }
    }
}