public class BackMenu : MenuItem
{
    public KeyboardControllerMenu Controller;
    public MenuIndexer Target;

    private void Start()
    {
        MenuBar[] Settings = transform.parent.GetComponentsInChildren<MenuBar>();
        Settings[0].Value = Prefs.Settings.Volume;
        Settings[1].Value = Prefs.Settings.Effects;
    }

    public override void SelectItem()
    {
        Controller.ActiveMenu.ToggleMenu();
        Controller.ActiveMenu = Target;
        Target.ToggleMenu();

        MenuBar[] Settings = transform.parent.GetComponentsInChildren<MenuBar>();
        Prefs.Settings.Volume = Settings[0].Value;
        Prefs.Settings.Effects = Settings[1].Value;
    }
}