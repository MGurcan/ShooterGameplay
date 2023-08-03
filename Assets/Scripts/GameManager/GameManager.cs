using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameData gameData;

    public Camera mainCamera;

    public GameObject[] characters;
    public GameObject[] weaponsBoss;
    public GameObject[] weaponsRosales;
    public GameObject[] weaponsAJ;
    public GameObject[] radars;

    public TextMeshProUGUI nickname;

    private float nearLRF = 11; // 4 br from camera to hero 7 + 4
    private float farLRF = 34;  // 4 br from camera to hero 30 + 4

    private string currentLRFType = "near";

    public Material projectilePrefab;

    public int totalEnemy;
    public int currentEnemy = 0;

    private void Start()
    {
        mainCamera.farClipPlane = nearLRF;
    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            if(currentLRFType== "near")
            {
                currentLRFType = "far";
                mainCamera.farClipPlane = farLRF;
                mainCamera.fieldOfView = 30;
            }
            else if (currentLRFType == "far")
            {
                currentLRFType = "near";
                mainCamera.farClipPlane = nearLRF;
                mainCamera.fieldOfView = 60;
            }  
        }
    }
    private void Awake()
    {
        // Assign GameData reference if it's not already assigned
        if (gameData == null)
        {
            gameData = Resources.Load<GameData>("GameData"); // Change "GameData" to the correct path of your ScriptableObject
        }

        // Load player data from the ScriptableObject
        LoadGameData();
    }

    public void EnemyCreated()
    {
        currentEnemy++;
    }
    public void LoadGameData()
    {
        nickname.text = gameData.playerName;

        if(gameData.selectedDifficulty == "Easy")
        {
            totalEnemy = 1;
        }
        else if (gameData.selectedDifficulty == "Medium")
        {
            totalEnemy = 2;
        }
        else if (gameData.selectedDifficulty == "Hard")
        {
            totalEnemy = 3;
        }

        if (gameData.selectedCharacter == "Boss")
        {
            characters[0].SetActive(true);
            characters[1].SetActive(false);
            characters[2].SetActive(false);
        }
        if (gameData.selectedCharacter == "Rosales")
        {
            characters[0].SetActive(false);
            characters[1].SetActive(true);
            characters[2].SetActive(false);
        }
        if (gameData.selectedCharacter == "AJ")
        {
            characters[0].SetActive(false);
            characters[1].SetActive(false);
            characters[2].SetActive(true);
        }
        LoadWeapons();
        LoadRadar();
    }

    public void LoadWeapons()
    {
        for (int i = 0; i < weaponsBoss.Length; i++)
        {
            weaponsBoss[i].SetActive(gameData.selectedWeapon == "Quas" && i == 0 ||
                                     gameData.selectedWeapon == "Wex" && i == 1 ||
                                     gameData.selectedWeapon == "Exort" && i == 2);
            
            weaponsRosales[i].SetActive(gameData.selectedWeapon == "Quas" && i == 0 ||
                                        gameData.selectedWeapon == "Wex" && i == 1 ||
                                        gameData.selectedWeapon == "Exort" && i == 2);
            
            weaponsAJ[i].SetActive(gameData.selectedWeapon == "Quas" && i == 0 ||
                                   gameData.selectedWeapon == "Wex" && i == 1 ||
                                   gameData.selectedWeapon == "Exort" && i == 2);
        }

        if (gameData.selectedLaser == "laser_1")
        {
            projectilePrefab.color = HexToColor("#005DFF");
        }
        else if (gameData.selectedLaser == "laser_2")
        {
            projectilePrefab.color = HexToColor("#FF0062");
        }
        else if (gameData.selectedLaser == "laser_3")
        {
            projectilePrefab.color = HexToColor("#FFFFFF");
        }
    }

    private void LoadRadar()
    {
        for (int i = 0; i < radars.Length; i++)
        {
            radars[i].SetActive(gameData.selectedRadar == "radar_60" && i == 0 ||
                                    gameData.selectedRadar == "radar_120" && i == 1 ||
                                     gameData.selectedRadar == "radar_180" && i == 2);

        }
    }
    Color HexToColor(string hex)
    {
        Color color = new Color();
        UnityEngine.ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }
}
