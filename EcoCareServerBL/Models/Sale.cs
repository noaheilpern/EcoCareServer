﻿using System;
using System.Collections.Generic;

#nullable disable

namespace EcoCareServerBL.Models
{
    public partial class Sale
    {
        public string BuyerUserName { get; set; }
        public int ProductId { get; set; }
        public int DateBought { get; set; }
        public int PriceBought { get; set; }
        public int SaleId { get; set; }

        public virtual RegularUser BuyerUserNameNavigation { get; set; }
        public virtual Product Product { get; set; }
    }
}
