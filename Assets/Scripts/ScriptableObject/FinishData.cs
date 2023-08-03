using UnityEngine;

[CreateAssetMenu(fileName = "FinishData", menuName = "Custom/FinishData", order = 1)]
public class FinishData : ScriptableObject
{
    public float remainHealth;
    public int totalShot;
    public float totalDuration;
    public int succesfullShot;
    public float totalDamage;
    public int totalKilledEnemy;
    public float[] enemyKillRanges;
}