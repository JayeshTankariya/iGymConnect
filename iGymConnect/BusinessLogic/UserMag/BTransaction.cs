using BusinessLogic.ObjectModel;
using DataLogic.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.UserMag
{
    public class BTransaction
    {
        public static List<OMTransaction> GetAllTransaction()
        {
            var TranList = new List<OMTransaction>();
            using (var context = new iGymConnectEntities())
            {
                TranList = context.TransactionMasters
                    .Select(x => new OMTransaction
                    {
                        Id = x.Id,
                        MemberId = x.MemberId.HasValue ? x.MemberId.Value : 0,
                        TransactionDateTime = x.TransactionDateTime ?? DateTime.Now,
                        TransactionMode = x.TransactionMode,
                        Remarks = x.Remarks,

                    }).ToList();
            }
            return TranList;
        }

        public static List<OMTransactionChild> GetTrasactionChild(int parentId)
        {
            var TranList = new List<OMTransactionChild>();
            using (var context = new iGymConnectEntities())
            {
                TranList = context.TransactionChilds.Where(x => x.TransactionMasterId == parentId)
                    .Select(x => new OMTransactionChild
                    {
                        Id = x.Id,
                        TransactionMasterId = x.TransactionMasterId.HasValue ? x.TransactionMasterId.Value : 0,
                        ItemId = x.ItemId.HasValue ? x.ItemId.Value : 0,
                        ItemTotal = x.ItemTotal.HasValue ? x.ItemTotal.Value : 0,
                    }).ToList();
            }
            return TranList;
        }
        
        public static List<OMTransaction> GetDateWiseTransaction(Nullable<DateTime> dt)
        {
            var dtList = new List<OMTransaction>();
            using (var c = new iGymConnectEntities())
            {
                dtList = c.TransactionMasters.Where(x => x.TransactionDateTime > dt)
                .Select(x => new OMTransaction
                {
                    Id = x.Id,
                    MemberId = x.MemberId.HasValue ? x.MemberId.Value: 0,
                    TransactionDateTime = x.TransactionDateTime ?? DateTime.Now,
                    TransactionMode = x.TransactionMode,
                    Remarks = x.Remarks
                }).ToList();
            }
            return dtList;
        }
        public static List<OMTransaction> GetTranRange(Nullable<DateTime> dt, Nullable<DateTime> dt1)
        {
            var dtList = new List<OMTransaction>();
            using (var c = new iGymConnectEntities())
            {
                dtList = c.TransactionMasters.Where(x => x.TransactionDateTime > dt && x.TransactionDateTime < dt1)
                .Select(x => new OMTransaction
                {
                    Id = x.Id,
                    MemberId = x.MemberId.HasValue ? x.MemberId.Value : 0,
                    TransactionDateTime = x.TransactionDateTime ?? DateTime.Now,
                    TransactionMode = x.TransactionMode,
                    Remarks = x.Remarks
                }).ToList();
            }
            return dtList;
        }
        public static List<OMPOSCardMaster> GetCardItems(int id)
        {
            var TranList = new List<OMPOSCardMaster>();
            using (var context = new iGymConnectEntities())
            {
                if (id > 0)
                {
                    TranList = context.POSCardItems.Where(x => x.TransactionMasterId == id)
                    .Select(x => new OMPOSCardMaster
                    {
                        Id = x.Id,
                        TransactionMasterId = x.TransactionMasterId.HasValue ? x.TransactionMasterId.Value : 0,
                        CardId = x.CardId.HasValue ? x.CardId.Value : 0
                    }).ToList();
                }
                else
                {
                    TranList = context.POSCardItems
                    .Select(x => new OMPOSCardMaster
                    {
                        Id = x.Id,
                        TransactionMasterId = x.TransactionMasterId.HasValue ? x.TransactionMasterId.Value : 0,
                        CardId = x.CardId.HasValue ? x.CardId.Value : 0
                    }).ToList();
                }
            }
            return TranList;
        }

        public static List<OMTransaction> Save(OMTransaction tran)
        {
            var tranlist = new List<OMTransaction>();
            TransactionMaster tranMas = new TransactionMaster();
            if (tran.Id > 0)
            {
                using (var t = new iGymConnectEntities())
                {
                    tranMas = t.TransactionMasters.FirstOrDefault(x => x.Id == tranMas.Id);
                    //tranMas.Id = tran.Id;
                    tranMas.MemberId = tran.MemberId;
                    tranMas.TransactionDateTime = tran.TransactionDateTime;
                    tranMas.TransactionMode = tran.TransactionMode;
                    tranMas.Remarks = tran.Remarks;

                    for (var _items = 0; _items < tran.Items.Count; _items++)
                    {
                        TransactionChild tranChild = new TransactionChild();
                        tranChild.Id = tran.Id;
                        tranChild.TransactionMasterId = tran.Items[_items].TransactionMasterId;
                        tranChild.ItemId = tran.Items[_items].ItemId;
                        tranChild.ItemTotal = tran.Items[_items].ItemTotal;
                        t.TransactionChilds.Add(tranChild);
                    }

                    for (var _cards = 0; _cards < tran.Cards.Count; _cards++)
                    {
                        POSCardItem tranChild = new POSCardItem();
                        tranChild.Id = tran.Id;
                        tranChild.TransactionMasterId = tran.Items[_cards].TransactionMasterId;
                        t.POSCardItems.Add(tranChild);
                    }
                    t.TransactionMasters.Add(tranMas);
                    t.SaveChanges();
                    //tranlist = GetAllTransaction();

                }
            }
            else
            {
                tranMas.MemberId = tran.MemberId;
                tranMas.TransactionDateTime = DateTime.Now;
                tranMas.TransactionMode = tran.TransactionMode;
                tranMas.Remarks = tran.Remarks;
                using (var t = new iGymConnectEntities())
                {
                    t.TransactionMasters.Add(tranMas);
                    t.SaveChanges();
                }
            }
            tranlist = GetAllTransaction();
            var lastInserted = tranlist.OrderByDescending(x => x.Id).FirstOrDefault();
            using (var t = new iGymConnectEntities())
            {
                for (var _items = 0; _items < tran.Items.Count; _items++)
                {
                    TransactionChild tranChild = new TransactionChild();
                    //List<OMTransactionChild> tc = tran.Items;
                    tranChild.Id = tran.Id;
                    tranChild.TransactionMasterId = lastInserted.Id;
                    tranChild.ItemId = tran.Items[_items].ItemId;
                    tranChild.ItemTotal = tran.Items[_items].ItemTotal;
                    t.TransactionChilds.Add(tranChild);
                    t.SaveChanges();
                }
                for (var _cards = 0; _cards < tran.Cards.Count; _cards++)
                {
                    POSCardItem tranChild = new POSCardItem();
                    tranChild.Id = tran.Id;
                    tranChild.CardId = tran.Cards[_cards].CardId;
                    tranChild.TransactionMasterId = lastInserted.Id;
                    tranChild.DateCreated = DateTime.Now;
                    tranChild.DateUpdated = DateTime.Now;
                    tranChild.CreatedBy = 1;
                    tranChild.UpdatedBy = 1;
                    tranChild.Deleted = false;
                    t.POSCardItems.Add(tranChild);
                    t.SaveChanges();
                }
            }
            return tranlist;
        }
    }
}
