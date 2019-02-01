using UnityEngine;

public class SaveFile
{
    public string ActiveScene = "Debug";
    public Vector2 PlayerLocation = Vector2.zero;
    public Direction PlayerDirection = Direction.Up;
    public int PlayerHealth = 100;
    public bool PlayerSword = false;
}