using BusinessLogic.UserMag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ObjectModel
{
    public class OMTransaction
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public string TransactionMode { get; set; }
        public string Remarks { get; set; }

        private List<OMTransactionChild> _items;
        public List<OMTransactionChild> Items
        {
            get
            {
                if (_items == null || _items.Count == 0)
                {
                    _items = BTransaction.GetTrasactionChild(Id);
                }
                return _items;
            }
            set
            {
                _items = value;
            }
        }

        public OMMember Member
        {
            get
            {
                if (MemberId > 0)
                {
                    return BMember.GetAllByMember().FirstOrDefault(x => x.MemberId == MemberId);
                }
                else
                {
                    return null;
                }
            }
        }

        private List<OMPOSCardMaster> _cards;
        public List<OMPOSCardMaster> Cards
        {
            get
            {
                if (_cards == null || _cards.Count == 0)
                {
                    _cards = BTransaction.GetCardItems(Id);
                }
                return _cards;
            }
            set
            {
                _cards = value;
            }
        }
    }
}
