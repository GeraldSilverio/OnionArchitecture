using TaskManagement.Core.Domain.Commons;

namespace TaskManagement.Core.Domain.Entities;

public class Tasks : AuditoryProperties
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string IdUser { get; set; }
    public int IdTaskStatus { get; set; }
    public TasksStatus TaskStatus { get; set; }

    public Tasks()
    {

    }

    public Tasks(string name, string idUser, int idTaskStatus, string createdBy)
    {
        Name = name;
        IdUser = idUser;
        IdTaskStatus = idTaskStatus;
        CreatedBy = createdBy;
        IsDeleted = false;
    }
}