using BookStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Repository
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        BookStoreContext db;
        public GenericRepository(BookStoreContext db)
        {
            this.db = db;
        }
        public List<TEntity> SelectAll()
        {
            return db.Set<TEntity>().ToList();
        }
        public TEntity SelectById(object id)
        {
            return db.Set<TEntity>().Find(id);
        }
        public void Add(TEntity entity)
        {
            db.Set<TEntity>().Add(entity);
            db.SaveChanges();
        }
        public void Update(TEntity entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(object id)
        {
            TEntity entity = db.Set<TEntity>().Find(id);
            db.Set<TEntity>().Remove(entity);
            db.SaveChanges();
        }
        //public void Save()
        //{
        //    db.SaveChanges();
        //}


    }
}
