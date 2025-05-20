public class ElectricGuitarDto : GuitarDto
{
    public int PickupCount { get; set; }
    public VibratoSystem VibratoSystem { get; set; }


}
public enum VibratoSystem { None, FloatingBridge, LockedBridge, Bigsby }