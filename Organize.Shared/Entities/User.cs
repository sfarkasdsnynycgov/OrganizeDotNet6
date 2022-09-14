using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using OrganizeDotNET6a.Shared.Entities;
using OrganizeDotNET6a.Shared.Enums;

namespace OrganizeDotNET6a.Shared.Entities
{
    public class clsUser: BaseEntity
    { //  <DataAnnotationsValidator /> <!--take advantage of user property decorations from System.ComponentModel.DataAnnotations. 
        private string uid = "";


        //public clsUser(NavigationManager? navigationManager)
        //{
        //    myNavigationManager = navigationManager;
        //}

        private NavigationManager myNavigationManager { get; set; }

        private ObservableCollection<BaseItem> _userItems = new ObservableCollection<BaseItem>();

        public string Token { get; set; }


        [Required(ErrorMessage = "system needs a dsnyad user ID")]
        [StringLength(30,ErrorMessage="User ID is unexpectedly long")]
        public string UserName {
            get {
                try {
                    if (myNavigationManager != null && string.IsNullOrEmpty(uid))
                    {
                        var uri = myNavigationManager.ToAbsoluteUri(myNavigationManager.Uri);
                        Microsoft.Extensions.Primitives.StringValues sv;
                        if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("userName", out sv))
                        {
                            uid = sv;
                        }
                        else
                        {
                            uid = "cannot determine UID from browser";
                        }
                    }
           
                }
                catch(Exception ex)
                {
                    uid = ex.Message;
                }

                return uid;
            }
            set {
                uid = value;
            }
        } // = "Ben";

        [Required(ErrorMessage ="system needs a dsnyad password")]
        public string Password { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        
        [Phone]
        public string PhoneNumber { get; set; }

        public ObservableCollection<BaseItem> UserItems
        {
            get => _userItems;
            set => SetProperty(ref _userItems, value);
        }
        public GenderTypeEnum GenderType { get; set; }
        public NavigationManager LclNavigationManager { get; }
        public bool IsUserItemsPropertyLoaded { get; set; } = false;

        public override string ToString()
        {
            var salutation = string.Empty;
            if (GenderType == GenderTypeEnum.Male)
            {
                salutation = "Mr";
            }

            if (GenderType == GenderTypeEnum.Female)
            {
                salutation = "Mrs";
            }

            return $"{salutation}. {FirstName} {LastName}";
        }
    }
}
