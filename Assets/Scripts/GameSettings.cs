using UnityEngine;

[CreateAssetMenu]
public class GameSettings : ScriptableObject
{
    [Header("Animations")]
    public float harvestedBlockFlyTime;
    public float soldBlockFlyTime;
    public float coinFlyTime;
    public float delayBetweenSells;
    public int coinCounterShakeStrength;

    [Header("General")]
    public int inventoryMax;
    public int grassBlockPrice;
    public float moveSpeed;
    public float grassGrowTime;
    public float grassCutRange;
    public Color grassToCutColor;
}