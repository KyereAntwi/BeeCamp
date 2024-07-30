using Supabase.Postgrest.Attributes;

namespace BeeCamp.WorkSpaces.Entities;

[Table("workspaces")]
public class Workspace : BaseEntity
{
    [Column("title")]
    public string Title { get; set; } = string.Empty;
    [Column("description")]
    public string Description { get; set; } = string.Empty;
}