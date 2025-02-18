namespace TodoAppEntityFramework.Models
{
    public class Todo
    {
        public int Id { get; set; }

        public string JobName { get; set; }

        public bool IsApproved { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now; // .  . 
    }
}
