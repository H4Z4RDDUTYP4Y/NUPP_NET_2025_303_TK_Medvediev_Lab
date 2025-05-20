public class AcousticGuitarDto : GuitarDto
{
    public bool HasPiezo { get; set; }
    public StringType StringType { get; set; }
}

public enum StringType { Steel, Nylon }