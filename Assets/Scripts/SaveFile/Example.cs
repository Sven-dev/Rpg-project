public class Example : Saveable
{
    public override int Value
    {
        get { return GlobalVariables.Save.PlayerHealth; }

        set { GlobalVariables.Save.PlayerHealth = value; }
    }
}