using OrganizeDotNET6a.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizeDotNET6a.Shared.Entities
{
    public class UrlItem : BaseItem
    {
        public string Url
        {
            get => _url;
            set => SetProperty(ref _url, value);
        }
        private string _url;
    }
}