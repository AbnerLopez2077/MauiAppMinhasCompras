using MauiAppMinhasCompras.Models;
using SQLite;
namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;
        //conexão com o banco
        public SQLiteDatabaseHelper(string path) {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait();
        }
        // metodo de inserção com instancia do tipo Produto
        public Task<int> Insert(Produto p)
        {
            return _conn.InsertAsync(p);
        }
        //metodo update retornando uma lista
        public Task<List<Produto>> Update(Produto p)
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=?";
            return _conn.QueryAsync<Produto>(
                sql, p.Descricao, p.Quantidade, p.Preco, p.Id
                );
        }
        // metodo delete  com parametro Id
        public Task<int> Delete(int id)
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }
        //Metodo para retornar os registros da tabela em uma lista
        public Task<List<Produto>> GetAll()
        {
            return _conn.Table<Produto>().ToListAsync();
        }
        //Metodo para pesquisa com o parametro de string
        public Task<List<Produto>> Search (string q)
        {
            string sql = "SELECT * FROM Produto WHERE Descricao LIKE '%" + q + "%' ";
            return _conn.QueryAsync<Produto>(sql);     
        }
    }
}
