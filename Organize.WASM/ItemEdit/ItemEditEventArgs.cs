using OrganizeDotNET6a.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizeDotNET6a.WASM.ItemEdit
{
    public class ItemEditEventArgs : EventArgs
    {
        public BaseItem Item { get; set; }
    }
}
