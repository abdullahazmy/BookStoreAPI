using BookStoreAPI.Models;

namespace BookStoreAPI.Repository
{
    public class UnitOfWorks
    {
        BookStoreContext db;
        GenericRepository<Book> bookRepository;
        GenericRepository<Order> orderRepository;
        GenericRepository<OrderDetails> orderDetailsRepository;
        public UnitOfWorks(BookStoreContext db, GenericRepository<Book> bookRepository, GenericRepository<Order> orderRepository, GenericRepository<OrderDetails> orderDetailsRepository)
        {
            this.db = db;
        }
        public GenericRepository<Book> BookRepository
        {
            get
            {
                if (bookRepository == null)
                {
                    bookRepository = new GenericRepository<Book>(db);
                }
                return bookRepository;
            }
        }
        public GenericRepository<Order> OrderRepository
        {
            get
            {
                if (orderRepository == null)
                {
                    orderRepository = new GenericRepository<Order>(db);
                }
                return orderRepository;
            }
        }
        public GenericRepository<OrderDetails> OrderDetailsRepository
        {
            get
            {
                if (orderDetailsRepository == null)
                {
                    orderDetailsRepository = new GenericRepository<OrderDetails>(db);
                }
                return orderDetailsRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }


    }
}