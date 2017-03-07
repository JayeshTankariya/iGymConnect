using BusinessLogic.ObjectModel;
using DataLogic.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.UserMag
{
    public class BPOSCardMaster
    {
     
        public static List<OMPOSCardMaster> GetPOSCardItems(int Id)
        {
            var cardList = new List<OMPOSCardMaster>();
            using (var context = new iGymConnectEntities())
            {
                cardList = context.POSCardItems.Where(x => x.TransactionMasterId == Id)
                    .Select(x => new OMPOSCardMaster
                    {
                        Id = x.Id,
                        TransactionMasterId = x.TransactionMasterId.HasValue ? x.TransactionMasterId.Value : 0,
                        CardId = x.CardId.HasValue ? x.CardId.Value : 0,
                    }).ToList();
            }
            return cardList;
        }

        
        public static List<OMCardMaster> GetCardByMember(int Id)
        {
            var cardList = new List<OMCardMaster>();
            using (var context = new iGymConnectEntities())
            {
                cardList = context.CardMasters
                    .Where(x => x.MemberId == Id)
                    .Select(x => new OMCardMaster
                    {
                        Id = x.Id,
                        Number = x.Number.HasValue ? x.Number.Value : 0,
                        Amount = x.Amount.HasValue ? x.Amount.Value : 0,
                        ExpiryDate = x.ExpiryDate ?? DateTime.Now,
                        MemberId = x.MemberId.HasValue ? x.MemberId.Value : 0,
                        IsUsed = x.IsUsed.HasValue ? x.IsUsed.Value : false,
                    }).ToList();
            }

            return cardList;
        }

        public static List<OMCardMaster> ShowCardDetails(int Id)         
        {
            var details = new List<OMCardMaster>();
            using (var context = new iGymConnectEntities())
            {
                details = context.CardMasters
                    .Where(x => x.Id == Id)
                    .Select(x => new OMCardMaster
                    {
                        Id = x.Id,
                        Number = x.Number.HasValue ? x.Number.Value : 0,
                        Amount = x.Amount.HasValue ? x.Number.Value : 0,
                        ExpiryDate = x.ExpiryDate ?? DateTime.Now,
                        MemberId = x.MemberId.HasValue ? x.MemberId.Value : 0,
                        IsUsed = x.IsUsed.HasValue ? x.IsUsed.Value : false,
                    }).ToList();
            }

            return details;
        }
    }
}
