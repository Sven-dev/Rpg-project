public class Example : Saveable
{
    public override int Value
    {
        get { return Global.Save.PlayerHealth; }

        set { Global.Save.PlayerHealth = value; }
    }
}