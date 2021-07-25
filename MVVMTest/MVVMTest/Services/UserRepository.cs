using MVVMTest.ClsPublic;
using MVVMTest.Entitys;
using MVVMTest.Interfaces;
using MVVMTest.Models;
using PCLStorage;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTest.Services
{
   public class UserRepository: IRepository<User> 
    {
        readonly List<User> items;
        private SQLiteAsyncConnection db;
        public UserRepository()
        {
            db = GetConnection();
            db.CreateTableAsync<User>();
        }

        public SQLite.SQLiteAsyncConnection GetConnection()
        {
            SQLiteAsyncConnection sqlitConnection;
            var sqliteFilename = "SampleDB.db3";
            IFolder folder = FileSystem.Current.LocalStorage;
            string path = PortablePath.Combine(folder.Path.ToString(), sqliteFilename);
            sqlitConnection = new SQLite.SQLiteAsyncConnection(path);
            return sqlitConnection;
        }

        public async Task<bool> AddItemAsync(User item)
        {
            item.UserId = IdentityGenerator.NewSequentialGuid();
            await db.InsertAsync(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(User item)
        {
            //await db.Table<User>().DeleteAsync(x => x.UserId == item.UserId);
            await db.UpdateAsync(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            await db.Table<User>().DeleteAsync(x => x.UserId == id);
            return await Task.FromResult(true);
        }

        public async Task<User> GetItemAsync(Guid id)
        {
            //return await Task.FromResult(items.FirstOrDefault(s => s.UserId == id));
            return await db.Table<User>().FirstOrDefaultAsync(f => f.UserId == id);
        }

        public async Task<List<User>> GetItemsAsync(bool forceRefresh = false)
        {
            return await db.Table<User>().ToListAsync();
        }
    }
}
