namespace todo_api.Models
{
    public class Todo
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual bool isComplete { get; set; }
    }
}