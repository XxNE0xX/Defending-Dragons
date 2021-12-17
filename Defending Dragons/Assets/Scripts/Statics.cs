using UnityEngine;

public class Statics
{
    // Base enemies count
    public static readonly int BaseEnemiesCountInPool = 20;
    public static readonly int AddMoreEnemiesToPoolCount = 15;

    public static readonly float DefaultPoolPositionX = -200f;
    
    public static float ScreenEdgeX;
    public static readonly float EnemySpawnOffset = 0.8f;
    public static readonly float PoolVerticalOffset = 0.35f;

    public static readonly float EnemySpeed = 0.7f;

    public static readonly int CastleHealth = 10000;

    public static readonly int MaxEnemyDamage = 125;
    public static readonly int MinEnemyDamage = 50;

    public static readonly float DisappearTimerMax = 3f;
    
    // Minimum and maximum for the red color of the damage popup text
    public static readonly int MaxDamagePopupGreen = 230;
    public static readonly int MinDamagePopupGreen = 120;
    
    // Enemy Colors
    public static readonly Color32 Blue = new Color32(0, 204, 255, 255);
    public static readonly Color32 Red = new Color32(255, 76, 81, 255);
    public static readonly Color32 Yellow = Color.yellow;
    public static readonly Color32 Green = new Color32(102, 255, 76, 255);
    public static readonly Color32 Purple = new Color32(255, 0, 255, 255);
    public static readonly Color32 Orange = new Color32(255, 116, 0, 255);
    public static readonly Color32 Black = new Color32(130, 130, 130, 255);
    public static readonly Color32 DefaultColor = new Color32(255, 255, 255, 255);
    
    // Tiles properties
    public const float GROUND_TILES_SIZE = 1f;
    public const float THRESHOLD_MARGIN_FOR_EXPLOSION = 0.1f;
    

    public static void LogWarningMethodNotImplemented(string name)
    {
        Debug.LogWarning("Method " + name + " Has Not Been Implemented!");
    }
}
