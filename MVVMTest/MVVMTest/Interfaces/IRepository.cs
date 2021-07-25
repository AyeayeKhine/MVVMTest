using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTest.Interfaces
{
  public  interface IRepository<T> 
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(Guid id);
        Task<T> GetItemAsync(Guid id);
        Task<List<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
