using OrganizeDotNET6a.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizeDotNET6a.Shared.Entities
{
    public class ParentItem:BaseItem
    {
        public ObservableCollection<ChildItem> ChildItems { get; set; }
        // observatble allows program to listen for changes to the collection
    }
}
