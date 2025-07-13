
using Shared.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities;

public abstract class BaseEntity : IEntity, ISoftDeletable, ITrackable, IFilterable
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public required string CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }
    public required DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset? ModifiedOn { get; set; }
}

