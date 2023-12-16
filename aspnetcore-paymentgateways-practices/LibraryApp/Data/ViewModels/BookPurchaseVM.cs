using LibraryApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Data.ViewModels
{
    public class BookPurchaseVM : Book
    {
        public string Nonce { get; set; }
    }
}
