using PM2T24Video.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM2T24Video.Controllers
{
    public class VideoController
    {
        readonly SQLiteAsyncConnection _connection;
        public VideoController()
        {
            SQLiteOpenFlags extensiones = SQLiteOpenFlags.ReadWrite |
               //SQLiteOpenFlags.ReadOnly |
               SQLiteOpenFlags.Create |
               SQLiteOpenFlags.SharedCache;

            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "DBVideo.db3"), extensiones);
            _connection.CreateTableAsync<Videos>();
        }

        public async Task<int> AddVideos(Videos video)
        {
            if (video.id == 0)
                return await _connection.InsertAsync(video);
            else
                return await _connection.UpdateAsync(video);
        }

        public async Task<List<Videos>> GetListVideos()
        {
            return await _connection.Table<Videos>().ToListAsync();
        }
    }
}
