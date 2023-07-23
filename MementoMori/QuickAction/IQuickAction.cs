namespace MementoMori.QuickAction;

public interface IQuickAction
{
    Task ExecuteAsync(QuickActionContext context);
}

public class QuickActionContext
{
    
}