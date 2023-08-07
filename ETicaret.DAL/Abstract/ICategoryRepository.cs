using ETicaret.DAL.Abstract.DataManagement;
using ETicaret.Entity;

namespace ETicaret.DAL.Abstract;

// ICategoryRepository -->> DerivedClass ***  IRepository --> Base
public interface ICategoryRepository : IRepository<Category>
{
    
}