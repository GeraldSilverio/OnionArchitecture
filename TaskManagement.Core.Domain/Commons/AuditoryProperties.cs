namespace TaskManagement.Core.Domain.Commons;

/// <summary>
/// Class representing audit properties.
/// </summary>
public class AuditoryProperties
{
    /// <summary>
    /// Person who created the record.
    /// </summary>
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Person who modified the record.
    /// </summary>
    public string? ModifiedBy { get; set; }

    /// <summary>
    /// Date when the record was created.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Date when the record was last modified.
    /// </summary>
    public DateTime ModifiedDate { get; set; }

    /// <summary>
    /// Indicates whether the record is active or not, to implement SoftDelete.
    /// </summary>
    public bool IsDeleted { get; set; }
}