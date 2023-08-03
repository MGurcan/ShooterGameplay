using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Custom/GameData", order = 1)]
public class GameData : ScriptableObject
{
    public string playerName;
    public string selectedCharacter;
    public string selectedWeapon;
    public string selectedLaser;
    public string selectedDifficulty;
    public string selectedRadar;
}