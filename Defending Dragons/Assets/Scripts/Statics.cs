using UnityEngine;

public class Statics
{
    // Base enemies count
    public static readonly int BaseEnemiesCountInPool = 20;

    public static readonly float DefaultPoolPositionX = -200f;

    public static readonly int AddMoreEnemiesToPoolCount = 15;

    public static float ScreenEdgeX;
    public static readonly float EnemySpawnOffset = 0.8f;
    public static readonly float EnemyVerticalOffset = 0.35f;

    public static readonly float EnemySpeed = 0.7f;

    public static readonly int CastleHealth = 10000;

    public static readonly int MaxEnemyDamage = 125;
    public static readonly int MinEnemyDamage = 50;

    public static readonly float DisappearTimerMax = 3f;
    
    // Minimum and maximum for the red color of the damage popup text
    public static readonly int MaxDamagePopupGreen = 230;
    public static readonly int MinDamagePopupGreen = 120;
}
