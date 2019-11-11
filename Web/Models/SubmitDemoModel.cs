using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class SubmitDemoModel
    {
        [LocalizedDisplayName("SubmitDemo", "RELOAD_PAGE")]
        public bool ReloadPage { get; set; }
        
        [LocalizedDisplayName("SubmitDemo", "REDIRECT")]
        public string Redirect { get; set; }
        
        [LocalizedDisplayName("SubmitDemo", "UPDATE_CALLER")]
        public string UpdateCaller { get; set; }
        
        [LocalizedDisplayName("SubmitDemo", "CLOSE_MODAL")]
        public bool CloseModal { get; set; }
        
        [LocalizedDisplayName("SubmitDemo", "PAGE_MESSAGE")]
        public string PageMessage { get; set; }

        [LocalizedDisplayName("SubmitDemo", "TITLE")]
        public string Title { get; set; }

        [LocalizedDisplayName("SubmitDemo", "BODY")]
        public string Body { get; set; }

        [LocalizedDisplayName("SubmitDemo", "MODAL_MESSAGE")]
        public string ModalMessage { get; set; }

        [LocalizedDisplayName("SubmitDemo", "TARGET_CONTAINER")]
        public string TargetContainer { get; set; }
    }
}