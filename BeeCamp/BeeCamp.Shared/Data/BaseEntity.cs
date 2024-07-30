namespace BeeCamp.Shared.Data;

public class BaseEntity : BaseModel
{
    [PrimaryKey("id", false)]
    public long Id { get; set; }
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
    [Column("deleted_at")]
    public DateTime DeletedAt { get; set; }
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
    [Column("created_by")]
    public string CreatedBy { get; set; } = string.Empty;
    [Column("deleted_by")]
    public string? DeletedBy { get; set; }
}