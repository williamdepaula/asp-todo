using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using todo_api.Models;

namespace todo_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private NHibernateHelper _repository;

        [HttpGet]
        public ActionResult<IEnumerable<Todo>> Get()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var todos =  session.Query<Todo>().ToList();
                
                return todos;
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Todo> Get(int id) {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var todo =  session.Get<Todo>(id);
                
                return todo;
            }
        }

        [HttpPost]
        public void Post(Todo todo)
        {
            try {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(todo);
                        transaction.Commit();
                    }
                }

            } catch {
                
            }
        }

        [HttpPut("{id}")]
        public void Put(int id, Todo todo)
        {
            try {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    var todoAlterado = session.Get<Todo>(id);

                    todoAlterado.Name = todo.Name;
                    todoAlterado.isComplete = todo.isComplete;

                    using(ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(todoAlterado);
                        transaction.Commit();
                    }
                }
            } catch {

            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try {
                using(ISession session = NHibernateHelper.OpenSession())
                {
                    var todo = session.Get<Todo>(id);
                    using (ITransaction transaction =  session.BeginTransaction())
                    {
                        
                        session.Delete(todo);
                        transaction.Commit();
                    }

                }
            } catch {}
        }
        
    }
}