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

    public static readonly int MaxEnemy1Damage = 125;
    public static readonly int MinEnemy1Damage = 50;
    public static readonly int MaxEnemy2Damage = 180;
    public static readonly int MinEnemy2Damage = 75;

    public static readonly float DisappearTimerMax = 1.0f;
    
    // Minimum and maximum for the red color of the damage popup text
    public static readonly int MaxDamagePopupGreen = 230;
    public static readonly int MinDamagePopupGreen = 120;
    
    // Enemy Colors
    public static readonly Color32 Blue = new Color32(9, 123, 228, 255);
    public static readonly Color32 Red = new Color32(225, 50, 33, 255);
    public static readonly Color32 Yellow = new Color32(243, 220, 52, 255);
    public static readonly Color32 Green = new Color32(28, 200, 15, 255);
    public static readonly Color32 Purple = new Color32(116, 32, 187, 255);
    public static readonly Color32 Orange = new Color32(255, 116, 0, 255);
    public static readonly Color32 Black = new Color32(130, 130, 130, 255);
    public static readonly Color32 DefaultColor = new Color32(255, 255, 255, 255);
    
    // Tiles properties
    public const float GROUND_TILES_SIZE = 1f;
    public const float THRESHOLD_MARGIN_FOR_EXPLOSION = 0.1f;
    
    // Cannon properties
    public const float RELOAD_TEXT_BLINKING_TIME = 0.8f;

    public static void LogWarningMethodNotImplemented(string name)
    {
        Debug.LogWarning("Method " + name + " Has Not Been Implemented!");
    }

    public static Color32 GetColorFromEnemyColor(EnemyColor enemyColor)
    {
        Color32 color = enemyColor switch
        {
            EnemyColor.Blue => Statics.Blue,
            EnemyColor.Red => Statics.Red,
            EnemyColor.Yellow => Statics.Yellow,
            EnemyColor.Green => Statics.Green,
            EnemyColor.Purple => Statics.Purple,
            EnemyColor.Orange => Statics.Orange,
            EnemyColor.Black => Statics.Black,
            _ => Statics.DefaultColor
        };
        return color;
    }
    
    public static bool IsGamePaused = false;
}
