using OrganizeDotNET6a.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizeDotNET6a.Shared.Entities
{
    public class TextItem : BaseItem
    {
        public string SubTitle
        {
            get => _subTitle;
            set => SetProperty(ref _subTitle, value);

        }
        private string _subTitle;

        public string Detail
        {
            get => _detail;
            set => SetProperty(ref _detail, value);
        }
        private string _detail;
    }
}