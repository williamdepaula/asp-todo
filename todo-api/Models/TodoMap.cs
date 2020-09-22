using FluentNHibernate.Mapping;

namespace todo_api.Models
{
    public class TodoMap : ClassMap<Todo>
    {
        public TodoMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.isComplete);
            Table("Todos");
        }
    }
}