public class Challenge_1 : SpawnLogic
{
    private bool[,] Array = new bool[,]
    {
        { true, false, false },
        { false, false, true },
        { true, false, true },
    };

    public override bool[,] SpawnArray()
    {
        return Array;
    }
}
