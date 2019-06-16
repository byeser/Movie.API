using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Movie.ServiceHost.API.Models;

namespace Movie.ServiceHost.API.Data
{
    public class FilmRepository : IFilmRepository
    {
        private readonly string _connectionStr;
        private IDbConnection _connection { get { return new SqlConnection(_connectionStr); } }
        public FilmRepository()
        {
            _connectionStr = "Data Source=94.73.147.7;Initial Catalog=u8719880_mobtask;User ID=u8719880_taskuse;Password=GAla47D3";
        }
        public async Task AddAsync(Film film)
        {
            using (IDbConnection dbconnection = _connection)
            {
                string query = @"INSERT INTO [dbo].[movies] (
                                [imdbID],
                                [title],
                                [year],
                                [type],
                                [poster]) VALUES (
                                @imdbID,
                                @title,
                                @year,
                                @type,
                                @poster)";

                await dbconnection.ExecuteAsync(query, film);
            }
        }

        public async Task<IEnumerable<Film>> GetAllAsync()
        {
            using (IDbConnection dbconnection = _connection)
            {
                string query = @"SELECT [imdbID]
                                ,[title]
                                ,[year]
                                ,[type]
                                ,[poster]
                                FROM [dbo].[movies]";
                var film = await dbconnection.QueryAsync<Film>(query);
                return film;
            }
        }

        public async Task<IEnumerable<Film>> GetAsync(string title)
        {
            using (IDbConnection dbconnection = _connection)
            {
                string query = @"SELECT [imdbID]
                                ,[title]
                                ,[year]
                                ,[type]
                                ,[poster]
                                FROM [dbo].[movies]  
                                 WHERE  title LIKE '%" + title + "%' ";
                var film = await dbconnection.QueryAsync<Film>(query);
                return film;
            }
        }

        public async Task UpdateAsync(Film film)
        {
            using (IDbConnection dbconnection = _connection)
            {
                string query = @"UPDATE [dbo].[movies] SET 
                                imdbID=@imdbID,
                                title=@title,
                                year=@year,
                                type=@type,
                                poster=@poster WHERE imdbID='" + film.imdbID+"'";

                await dbconnection.ExecuteAsync(query, film);
            }
        }
    }
}
