using InventoryCore.Models;
using InventoryCore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryCore.Repository
{
    public interface IRepoItem
    {
        

        Task<List<ItemMaster>> GetItems();

        Task<ItemViewModel> GetItem(int? postId);

        Task<int> AddItem(ItemViewModel p);

        Task<int> DeleteItem(int? Item_ID);

        Task UpdateItem(ItemViewModel p);
    }
}
