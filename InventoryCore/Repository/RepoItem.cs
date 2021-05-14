using InventoryCore.Models;
using InventoryCore.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryCore.Repository
{
    public class RepoItem: IRepoItem
    {
        ApplicationContext db;
        public RepoItem(ApplicationContext _db)
        {
            db = _db;
        }

        public async Task<List<ItemMaster>> GetItems()
        {
            if (db != null)
            {
                return await db.ItemMasters.ToListAsync();
            }

            return null;
        }
      

        public async Task<ItemViewModel> GetItem(int? Item_ID)
        {
            if (db != null)
            {
                return await (from p in db.ItemMasters                             
                              where p.Item_ID == Item_ID
                              select new ItemViewModel
                              {
                                  Item_ID = p.Item_ID,
                                  Item_Name = p.Item_Name,
                                  Price = p.Price,
                                  Description = p.Description                                
                              }).FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<int> AddItem(ItemViewModel p)
        {
            if (db != null)
            {
                ItemMaster IM = new ItemMaster()
                {
                    Item_ID = p.Item_ID,
                    Item_Name = p.Item_Name,
                    Price = p.Price,
                    Description = p.Description,
                    Add_By = 1,
                    Add_Date = DateTime.Now,
                    Status = true
                };
                await db.ItemMasters.AddAsync(IM);
                await db.SaveChangesAsync();

                return IM.Item_ID;
            }

            return 0;
        }

        public async Task<int> DeleteItem(int? Item_ID)
        {
            int result = 0;

            if (db != null)
            {

                var item = await db.ItemMasters.FirstOrDefaultAsync(x => x.Item_ID == Item_ID);

                if (item != null)
                {

                    db.ItemMasters.Remove(item);

                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }


        public async Task UpdateItem(ItemViewModel p)
        {
            if (db != null)
            {
                ItemMaster IM = new ItemMaster()
                {
                    Item_ID = p.Item_ID,
                    Item_Name = p.Item_Name,
                    Price = p.Price,
                    Description = p.Description,
                    Edit_By = 1,
                    Edit_Date = DateTime.Now
                    
                };

                db.ItemMasters.Update(IM);


                await db.SaveChangesAsync();
            }
        }
    
}
}
