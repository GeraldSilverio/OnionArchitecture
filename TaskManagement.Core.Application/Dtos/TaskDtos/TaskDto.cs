namespace TaskManagement.Core.Application.Dtos.TaskDtos
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string IdUser { get; set; }
        public int IdTaskStatus { get; set; }
    }
}
