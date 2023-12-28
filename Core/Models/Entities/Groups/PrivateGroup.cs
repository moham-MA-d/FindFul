using static DTO.Enumerations.PaymentEnums;

namespace Core.Models.Entities.Groups;

public class PrivateGroup : Group
{
    public string PaymentInfo  { get; set; }
    public Payment Payment { get; set; }
}


