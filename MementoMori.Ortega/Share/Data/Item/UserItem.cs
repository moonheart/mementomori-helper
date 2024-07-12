using MementoMori.Ortega.Share.Data.Interface;
using MementoMori.Ortega.Share.Enums;
using MessagePack;

namespace MementoMori.Ortega.Share.Data.Item;

[MessagePackObject(true)]
public class UserItem : IUserItem, IDeepCopy<UserItem>
{
    public UserItem DeepCopy()
    {
        var userItem = new UserItem();
        userItem.ItemCount = ItemCount;
        userItem.ItemType = ItemType;
        return userItem;
    }

    public long ItemCount { get; set; }

    public long ItemId { get; set; }

    public ItemType ItemType { get; set; }

    public bool IsCurrency()
    {
        return ItemType == ItemType.CurrencyFree || ItemType == ItemType.CurrencyPaid;
    }

    public bool IsEqual(IUserItem userItem)
    {
        if (userItem == null) return false;
        return ItemType == userItem.ItemType && ItemId == userItem.ItemId && ItemCount == userItem.ItemCount;
    }

    public bool IsEqual(ItemType itemType, long itemId)
    {
        return ItemType == itemType && ItemId == itemId;
    }

    public bool IsEqual(ExchangePlaceItemType exchangePlaceItemType)
    {
        return false;
        /*
An exception occurred when decompiling this method (06003CEC)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Boolean Ortega.Share.Data.Item.UserItem::IsEqual(Ortega.Share.Enums.ExchangePlaceItemType)

---> System.Exception: Basic block has to end with unconditional control flow.
{
Block_0:
stloc:ItemType(var_0_06, ldfld:ItemType(UserItem::<ItemType>k__BackingField, ldloc:UserItem(this)))
}

at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.FlattenBasicBlocks(ILNode node) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 1813
at ICSharpCode.Decompiler.ILAst.ILAstOptimizer.Optimize(DecompilerContext context, ILBlock method, AutoPropertyProvider autoPropertyProvider, StateMachineKind& stateMachineKind, MethodDef& inlinedMethod, AsyncMethodDebugInfo& asyncInfo, ILAstOptimizationStep abortBeforeStep) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstOptimizer.cs:line 347
at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 123
at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
--- End of inner exception stack trace ---
at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1627
*/
        ;
    }
}