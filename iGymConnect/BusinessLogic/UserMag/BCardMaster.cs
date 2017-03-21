using BusinessLogic.ObjectModel;
using DataLogic.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.UserMag
{
    public class BCardMaster
    {
        public static List<OMCardMaster> GetAllCard()
        {
            var cardList = new List<OMCardMaster>();
            using (var context = new iGymConnectEntities())
            {
                cardList = context.CardMasters
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
        public static List<OMCardMaster> GetCard()
        {
            var cardList = new List<OMCardMaster>();
            using (var context = new iGymConnectEntities())
            {
                cardList = context.CardMasters
                    .Where(x => x.MemberId == null && x.IsUsed == null)
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
        //public static List<OMCardMaster> SaveCardItems(OMCardMaster card)
        //{

        //    var cardlist = new List<OMCardMaster>();
        //    CardMaster cardMas = new CardMaster();
        //    if (card.Id > 0)
        //    {
        //        using (var t = new iGymConnectEntities())
        //        {
        //            cardMas = t.CardMasters.FirstOrDefault(x => x.Id == cardMas.Id);
        //            //cardMas.Id = card.Id;
        //            cardMas.Number = card.Number;
        //            cardMas.Amount = card.Amount;
        //            cardMas.ExpiryDate = card.ExpiryDate;
        //            cardMas.MemberId = card.MemberId;

        //            for (var _cards = 0; _cards < card.Cards.Count; _cards++)
        //            {
        //                POSCardItem POSCardItem = new POSCardItem();
        //                //List<OMPOSCardItems> c = card.cards;
        //                POSCardItem.Id = card.Id;
        //                POSCardItem.TransactionMasterId = card.Cards[_cards].TransactionMasterId;
        //                POSCardItem.CardId = card.Cards[_cards].CardId;
        //                POSCardItem.Amount = card.Cards[_cards].Amount;
        //                t.POSCardItems.Add(POSCardItem);
        //            }
        //            for (var _items = 0; _items < card.Items.Count; _items++)
        //            {
        //                TransactionChild tranChild = new TransactionChild();
        //                //List<OMTransactionChild> tc = tran.Items;
        //                tranChild.Id = card.Id;
        //                tranChild.TransactionMasterId = card.Items[_items].TransactionMasterId;
        //                tranChild.ItemId = card.Items[_items].ItemId;
        //                tranChild.ItemTotal = card.Items[_items].ItemTotal;
        //                t.TransactionChilds.Add(tranChild);
        //            }
        //            t.CardMasters.Add(cardMas);
        //            t.SaveChanges();
        //        }
        //    }
        //    else
        //    {
        //        cardMas.Id = card.Id;
        //        cardMas.Number = card.Number;
        //        cardMas.Amount = card.Amount;
        //        cardMas.ExpiryDate = card.ExpiryDate;
        //        cardMas.MemberId = card.MemberId;
        //        using (var t = new iGymConnectEntities())
        //        {
        //            t.CardMasters.Add(cardMas);
        //            t.SaveChanges();
        //        }
        //    }
        //    cardlist = GetAllCard();
        //    var lastInserted = cardlist.OrderByDescending(x => x.Id).FirstOrDefault();
        //    using (var t = new iGymConnectEntities())
        //    {
        //        for (var _cards = 0; _cards < card.Cards.Count; _cards++)
        //        {
        //            POSCardItem POSCardItem = new POSCardItem();
        //            //List<OMPOSCardItem> c = card.Cards;
        //            POSCardItem.Id = card.Id;
        //            POSCardItem.TransactionMasterId = lastInserted.Id;
        //            POSCardItem.CardId = card.Cards[_cards].CardId;
        //            POSCardItem.Amount = card.Cards[_cards].Amount;
        //            t.POSCardItems.Add(POSCardItem);
        //            t.SaveChanges();
        //        }
        //        for (var _items = 0; _items < card.Items.Count; _items++)
        //        {
        //            TransactionChild tranChild = new TransactionChild();
        //            //List<OMTransactionChild> tc = tran.Items;
        //            tranChild.Id = card.Id;
        //            tranChild.TransactionMasterId = lastInserted.Id;
        //            tranChild.ItemId = card.Items[_items].ItemId;
        //            tranChild.ItemTotal = card.Items[_items].ItemTotal;
        //            t.TransactionChilds.Add(tranChild);
        //            t.SaveChanges();
        //        }
        //    }
        //    return cardlist;
        //}
        public static int Save(int from, int to, decimal amount, DateTime expiryDate)
        {
            using (var context = new iGymConnectEntities())
            {
                CardMaster card = new CardMaster();
                for (var i = from; i <= to; i++)
                {
                    card = context.CardMasters.FirstOrDefault(x => x.Number == i);
                    if (card != null && card.Number.HasValue && card.Number.Value > 0)
                    {
                        return 1;
                    }
                }
                card = new CardMaster();
                for (var i = from; i <= to; i++)
                {
                    card.Number = i;
                    card.Amount = amount;
                    card.ExpiryDate = expiryDate;
                    card.Deleted = false;
                    card.UpdatedBy = 1;
                    card.DateUpdated = DateTime.Now;
                    card.CreatedBy = 1;
                    card.DateCreated = DateTime.Now;
                    context.CardMasters.Add(card);
                    context.SaveChanges();
                }
                return 0;
            }
        }
        public static List<OMCardMaster> GetMemberAndCard(int MemberId, int Number)
        {
            var cardList = new List<OMCardMaster>();
            CardMaster cm = new CardMaster();
            using (var c = new iGymConnectEntities())
            {
                if (Number > 0)
                {
                    cm = c.CardMasters.FirstOrDefault(x => x.Id == Number);
                    cm.MemberId = MemberId;
                    cm.IsUsed = true;

                    c.SaveChanges();
                }
            }
            return cardList;
        }
        
    }
}
