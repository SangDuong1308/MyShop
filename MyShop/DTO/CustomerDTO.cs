using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DTO
{
    public class CustomerDTO : INotifyPropertyChanged
    {
        public int CusID { get; set; }
        public string? CusName { get; set; }
        public string? CusTel { get; set; }
        public string? CusAddress { get; set; }

        public string? CusEmail { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
