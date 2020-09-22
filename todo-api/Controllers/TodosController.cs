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
            return new Todo[] {
                new Todo() {
                    Id = 1,
                    Name = "Comprar Leite",
                    isComplete = false
                },
                 new Todo() {
                    Id = 2,
                    Name = "Trabalho de SD",
                    isComplete = true
                },
                 new Todo() {
                    Id = 3,
                    Name = "Pagar Conta de Luz",
                    isComplete = false
                }
            }
            .FirstOrDefault(x => x.Id == id);
        }
        
    }
}