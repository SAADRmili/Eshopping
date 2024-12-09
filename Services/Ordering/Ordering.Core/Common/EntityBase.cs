namespace Ordering.Core.Common;
public abstract class EntityBase
{
    //set is made to use in the dervied class
    public int Id { get; protected set; }

    //audit properties 
    public string? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
}
