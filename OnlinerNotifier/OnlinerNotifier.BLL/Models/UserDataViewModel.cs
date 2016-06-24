﻿using System.Collections.Generic;

namespace OnlinerNotifier.BLL.Models
{
    public class UserDataViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AvatarUri { get; set; }

        public string Email { get; set; }

        public List<ProductViewModel> Products { get; set; }
    }
}
