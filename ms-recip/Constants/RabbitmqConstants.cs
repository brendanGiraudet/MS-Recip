namespace ms_recip.Constants;

public static class RabbitmqConstants
{
    public const string CreateRecipRoutingKey = "CreateRecip";
    public const string CreateRecipResultRoutingKey = "CreateRecipResult";
    
    public const string UpdateRecipRoutingKey = "UpdateRecip";
    public const string UpdateRecipResultRoutingKey = "UpdateRecipResult";
    
    public const string DeleteRecipRoutingKey = "DeleteRecip";
    public const string DeleteRecipResultRoutingKey = "DeleteRecipResult";

    public const string RecipExchangeName = "recip";
    
    public const string ApplicationName = "ms-recip";


}
