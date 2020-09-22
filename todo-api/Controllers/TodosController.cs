using System.Linq;
using Microsoft.AspNetCore.Http;
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
        public IActionResult Get()
        {
            using (NHibernate.ISession session = NHibernateHelper.OpenSession())
            {
                var todos =  session.Query<Todo>().ToList();
                
                return Ok(todos);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id) {
            using (NHibernate.ISession session = NHibernateHelper.OpenSession())
            {
                var todo =  session.Get<Todo>(id);
                
                return Ok(todo);
            }
        }

        [HttpPost]
        public IActionResult Post(Todo todo)
        {
            try {
                using (NHibernate.ISession session = NHibernateHelper.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        var idTodo = session.Save(todo);
                        transaction.Commit();

                        return Ok(idTodo);
                    }
                }

            } catch {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao salvar");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Todo todo)
        {
            try {
                using (NHibernate.ISession session = NHibernateHelper.OpenSession())
                {
                    var todoAlterado = session.Get<Todo>(id);

                    todoAlterado.Name = todo.Name;
                    todoAlterado.isComplete = todo.isComplete;

                    using(ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(todoAlterado);
                        transaction.Commit();

                        return Ok();
                    }
                }
            } catch {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar banco de dados");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try {
                using(NHibernate.ISession session = NHibernateHelper.OpenSession())
                {
                    var todo = session.Get<Todo>(id);
                    using (ITransaction transaction =  session.BeginTransaction())
                    {
                        
                        session.Delete(todo);
                        transaction.Commit();
                        return Ok();
                    }

                }
            } catch {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar o banco");
            }
        }
        
    }
}