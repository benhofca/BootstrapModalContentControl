namespace Web.Models
{
    /// <summary>
    /// Typically created in the View, the StaticModalDisplay is used to package and pass the Static Modal data into partial "_Modal_StaticContent"
    /// </summary>
    public class StaticModalDisplay
    {
        public string Title { get; set; }

        public string Action { get; set; }

        public string Dialog { get; set; }

        public BootstrapContext ButtonStyle { get; set; }

        public string ButtonText { get; set; }

        public string PartialView { get; set; } = null;

        public object PartialModel { get; set; } = null;
    }
    /// <summary>
    /// Typically created in the Control method OptionActionPost to pass into a partial view with is 
    /// rendered to string and passed back to the client for display in "#modDynamicModal .modal-body" 
    /// </summary>
    public class DynamicModalDisplay
    {
        public string Title { get; set; } = null;

        public string Body { get; set; } = null;

        public bool Wide { get; set; } = false;

        public bool CloseModal { get; set; } = false;

        public bool ReloadPage { get; set; } = false;

        public string PageMessage { get; set; } = null;

        public string ModalMessage { get; set; } = null;

        public string UpdateCaller { get; set; } = null;

        public string Redirect { get; set; } = null;
        public string TargetContainer { get; internal set; }
    }
    public enum BootstrapContext
    {
        Primary,
        Secondary,
        Success,
        Info,
        Warning,
        Danger,
        Light,
        Dark
    }
    public enum ButtonType
    {
        Button,
        LinkButton,
        LinkBadge
    }
}