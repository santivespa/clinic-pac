using ClinicPac.Models;

using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPac.LocalData
{
    public class UserDataBase
    {
        readonly SQLiteAsyncConnection _database;

        public UserDataBase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
            _database.CreateTableAsync<Consultation>().Wait();
        }

        public Task<List<User>> GetUsersAsync()
        {
            return _database.Table<User>().ToListAsync();

        }

        public Task<User> GetUserAsync()
        {
            return _database.Table<User>().FirstOrDefaultAsync();
        }



        public Task<int> SaveUserAsync(User user)
        {
            _database.DropTableAsync<User>().Wait();
            _database.CreateTableAsync<User>().Wait();

            DeleteAllCons();
            user.Consultations.ForEach(x => { SaveConsAsync(x); });
            return _database.InsertAsync(user);

        }

        public Task<int> OnlySaveUserAsync(User user)
        {
            _database.DropTableAsync<User>().Wait();
            _database.CreateTableAsync<User>().Wait();
            return _database.InsertAsync(user);
        }


        public Task<int> DeleteUserAsync(User user)
        {
            return _database.DeleteAsync(user);
        }


        public async Task<List<Consultation>> GetConsListAsync()
        {
            return await _database.Table<Consultation>().OrderByDescending(x => x.Date).ToListAsync();
        }

        public async void SaveConsAsync(Consultation cons)
        {
            await _database.InsertAsync(cons);
        }


        public async void SaveConsState(Consultation cons)
        {
            await _database.UpdateAsync(cons);
        }



        public Task<int> DeleteConsAsync(Consultation cons)
        {
            return _database.DeleteAsync(cons);
        }

        public void DeleteAllCons()
        {
            _database.DropTableAsync<Consultation>().Wait();
            _database.CreateTableAsync<Consultation>().Wait();

        }
        public void ClearData()
        {
            _database.DropTableAsync<User>().Wait();
            _database.CreateTableAsync<User>().Wait();
            _database.DropTableAsync<Consultation>().Wait();
            _database.CreateTableAsync<Consultation>().Wait();
        }



        public async Task<Consultation> GetCons(int id)
        {
            return await _database.Table<Consultation>()
                 .Where(i => i.ID == id)
                 .OrderByDescending(x => x.Date)
                 .FirstOrDefaultAsync();
        }

        public async Task<List<Consultation>> GetTicketCons()
        {
            return await _database.Table<Consultation>()
                 .Where(x => x.State == Consultation.States.Ticket)
                 .OrderByDescending(x => x.Date)
                 .ToListAsync();
        }
        public async Task<List<Consultation>> GetNoTicketCons()
        {
            return await _database.Table<Consultation>()
                 .Where(x => x.State == Consultation.States.NoTicket)
                 .OrderByDescending(x => x.Date)
                 .ToListAsync();
        }
        public async Task<List<Consultation>> GetPaidCons()
        {
            return await _database.Table<Consultation>()
                 .Where(x => x.State == Consultation.States.Paid)
                 .OrderByDescending(x => x.Date)
                 .ToListAsync();
        }
    }
}
