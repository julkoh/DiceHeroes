public enum DiceFaceColor {
     WATER, EARTH, FIRE, NEUTRAL, LAVA, ROCK, ICE, PHYSICAL, POISON, RADIATION
}

public class DiceFaceColorCombine {
    public DiceFaceColor[,] matrix = new DiceFaceColor[,]{
        {
            DiceFaceColor.ICE, DiceFaceColor.RADIATION, DiceFaceColor.POISON
        },
        {
            DiceFaceColor.RADIATION, DiceFaceColor.ROCK, DiceFaceColor.PHYSICAL
        },
        {
            DiceFaceColor.POISON, DiceFaceColor.PHYSICAL, DiceFaceColor.LAVA
        }
    };
}
