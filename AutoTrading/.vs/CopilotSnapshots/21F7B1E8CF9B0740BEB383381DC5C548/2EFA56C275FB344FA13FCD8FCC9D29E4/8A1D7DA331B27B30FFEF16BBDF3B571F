using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTrading.Presentation.Models.Market
{
    public class StockBaseInfo
    {
        private string _itemCode;
        private string _itemName;

        public StockBaseInfo(string itemCode, string itemName)
        {
            _itemCode = itemCode;
            _itemName = itemName;
        }

        public string ItemCode
        {
            get => _itemCode;
            set
            {
                _itemCode = value;
                //OnPropertyChanged(nameof(ItemCode));
            }
        }

        public string ItemName
        {
            get => _itemName;
            set
            {
                _itemName = value;
                //OnPropertyChanged(nameof(ItemName));
            }
        }
    }
}
