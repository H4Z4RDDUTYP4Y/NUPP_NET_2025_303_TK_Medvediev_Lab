public abstract class GuitarDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int StringCount { get; set; }
    public int ScaleLength { get; set; }
    public float Price { get; set; }
    public Guid PlayerId { get; set; }
}