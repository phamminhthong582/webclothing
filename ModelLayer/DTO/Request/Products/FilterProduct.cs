namespace ModelLayer.DTO.Request.Products;

public class FilterProduct
{
    public string? ProductName { get; set; }
    public double? Price { get; set; }
    public  string? Size{ get; set; }
    public string? Color { get; set; }
    public string? Category { get; set; }
}