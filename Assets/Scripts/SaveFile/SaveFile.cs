using UnityEngine;

public class SaveFile
{
    public int ActiveScene = 1;
    public int ActiveRoom = 0;
    public Vector2 PlayerLocation = Vector2.zero;
    public Direction PlayerDirection = Direction.Up;
    public int PlayerHealth = 100;
    public bool SwordEnabled = false;
    public bool DashEnabled = false;
    public bool ShieldEnabled = false;
    public bool TimeEnabled = false;
}