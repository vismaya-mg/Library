namespace LibraryManagement.Model.Common;

/// <summary>
/// Entity for all other entity to inherit common fields
/// </summary>
public class BaseDomainEntity
{
    //Gets and sets created on 
    public DateTime CreatedOn { get; set; }

    //Gets and sets updated on
    public DateTime UpdatedOn { get; set; }
}
