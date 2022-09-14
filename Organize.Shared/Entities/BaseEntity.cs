using System;
using System.Collections.Generic;
using System.Text;

namespace OrganizeDotNET6a.Shared.Entities
{
    public class BaseEntity : NotifyingObject
    {
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id == value)
                {
                    return;
                }

                _id = value;
                NotifyPropertyChanged();
            }
        }

        private int _id;
    }
}
