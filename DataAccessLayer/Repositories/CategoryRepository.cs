using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class CategoryRepository : ICategoryDal
    {

        public void AddCategory(Category category)
        {
            using (Context context = new Context())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }

        public void Delete(Category t)
        {
            throw new NotImplementedException();
        }

        public void DeleteCategory(Category category)
        {
            using (Context context = new Context())
            {
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }

        public Category GetById(int id)
        {
            using (Context context = new Context())
            {
                return context.Categories.Find(id);
            }
        }

        public Category GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetListAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(Category t)
        {
            throw new NotImplementedException();
        }

        public List<Category> ListAllCategory()
        {
            using (Context context = new Context())
            {
              return context.Categories.ToList();
            }
        }

        public void Update(Category t)
        {
            throw new NotImplementedException();
        }

        public void UpdateCategory(Category category)
        {
            using (Context context = new Context())
            {
                context.Categories.Update(category);
                context.SaveChanges();
            }
        }
    }
}
