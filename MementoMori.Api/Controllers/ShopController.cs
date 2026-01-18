using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;

namespace MementoMori.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShopController : ControllerBase
{
    /// <summary>
    /// Get shop items
    /// </summary>
    [HttpGet("items")]
    public ActionResult<List<ShopItem>> GetShopItems()
    {
        // TODO: Get shop items from game data
        return Ok(new List<ShopItem>
        {
            new ShopItem
            {
                Id = 1,
                Name = "Gold Pack",
                Price = 100,
                Currency = ShopCurrency.Diamond,
                Stock = 10,
                MaxStock = 10,
                Discount = 0
            }
        });
    }

    /// <summary>
    /// Buy shop item
    /// </summary>
    [HttpPost("buy")]
    public ActionResult<BuyItemResponse> BuyItem([FromBody] BuyItemRequest request)
    {
        // TODO: Implement buy logic
        return Ok(new BuyItemResponse
        {
            Success = true,
            Message = "Purchase successful",
            RemainingStock = 9
        });
    }

    /// <summary>
    /// Get auto-buy configuration
    /// </summary>
    [HttpGet("auto-buy-config")]
    public ActionResult<AutoBuyConfig> GetAutoBuyConfig()
    {
        // TODO: Load from user settings
        return Ok(new AutoBuyConfig
        {
            Enabled = false,
            Items = new List<AutoBuyItem>()
        });
    }

    /// <summary>
    /// Update auto-buy configuration
    /// </summary>
    [HttpPut("auto-buy-config")]
    public ActionResult UpdateAutoBuyConfig([FromBody] AutoBuyConfig config)
    {
        // TODO: Save to user settings
        return Ok();
    }
}

[ExportTsClass]
public class ShopItem
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Price { get; set; }
    public ShopCurrency Currency { get; set; }
    public int Stock { get; set; }
    public int MaxStock { get; set; }
    public int Discount { get; set; }
}

[ExportTsEnum]
public enum ShopCurrency
{
    Gold,
    Diamond,
    Ticket
}

[ExportTsClass]
public class BuyItemRequest
{
    public int ItemId { get; set; }
    public int Quantity { get; set; }
}

[ExportTsClass]
public class BuyItemResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public int RemainingStock { get; set; }
}

[ExportTsClass]
public class AutoBuyConfig
{
    public bool Enabled { get; set; }
    public List<AutoBuyItem> Items { get; set; } = new();
}

[ExportTsClass]
public class AutoBuyItem
{
    public int ItemId { get; set; }
    public int MaxDiscount { get; set; }
    public bool UseConsumables { get; set; }
}
