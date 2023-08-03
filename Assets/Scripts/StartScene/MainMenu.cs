using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;


public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

public enum Character
{
    Boss,
    Rosales,
    AJ
}

public enum Weapon
{
    Quas,
    Wex,
    Exort
}

public enum Laser
{
    laser_1,
    laser_2,
    laser_3
}

public class MainMenu : MonoBehaviour
{
    public GameData gameData;


    public string selectedDifficulty = "Easy";  //Easy, Medium, Hard
    public TextMeshProUGUI selectedDifText;

    public string selectedCharacter = "Boss";   //Boss, Rosales, AJ

    public TMP_InputField nicknameField;
    public string nickname = "Player";

    public string selectedWeapon = "Quas";   //Quas, Wex, Exort

    public string selectedLaser = "laser_1";   //laser_1, laser_2, laser_3

    public string selectedRadar = "radar_60";

    public GameObject[] characterPrefabs;
    public Color[] laserSprites;
    public Sprite[] weaponSprites;

    public GameObject MainMenuBackGround;
    // Düz string olarak karakter, silah, lazer ve zorluk seçenekleri
    public string[] characterOptions = { "Boss", "Rosales", "AJ" };
    public string[] weaponOptions = { "Quas", "Wex", "Exort" };
    public string[] laserOptions = { "laser_1", "laser_2", "laser_3" };
    public string[] difficultyOptions = { "Easy", "Medium", "Hard" };

    // Diðer deðiþkenler ve fonksiyonlar...


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
    public void SaveGameData()
    {
        // Save the data to the ScriptableObject
        gameData.playerName = nickname;
        gameData.selectedCharacter = selectedCharacter;
        gameData.selectedWeapon = selectedWeapon;
        gameData.selectedLaser = selectedLaser;
        gameData.selectedDifficulty = selectedDifficulty;
        gameData.selectedRadar = selectedRadar;
    }

    public void LoadGameData()
    {
        // Load the data from the ScriptableObject and apply it to the game
        nicknameField.text = gameData.playerName;
        selectedCharacter = gameData.selectedCharacter;
        selectedWeapon = gameData.selectedWeapon;
        selectedLaser = gameData.selectedLaser;
        selectedDifficulty = gameData.selectedDifficulty;

        ChangeDifficulty(selectedDifficulty);
        ChangeCharacter(selectedCharacter);
        ChangeLaser(selectedLaser);
        ChangeWeapon(selectedWeapon);
        ChangeRadar(selectedRadar);
        SaveNickname();
    }


    public void PlayGame()
    {
        SaveGameData();
        SceneManager.LoadScene("Sanctuary");
    }
    public void QuitGame()
    {
        SaveGameData();
        Application.Quit();
    }

    public void ChangeDifficulty(string difficulty)
    {
        selectedDifficulty = difficulty;
        selectedDifText.text = "Selected Difficulty: " + selectedDifficulty;
    }
    public void ChangeCharacter(string character)
    {
        selectedCharacter = character;

        Transform inspectTransform = MainMenuBackGround.transform.Find("Inspect");
        GameObject characterPrefab = characterPrefabs[0];
            if(character == "Boss")
                characterPrefab = characterPrefabs[0];
            else if (character == "Rosales")
                characterPrefab = characterPrefabs[1];
            else if (character == "AJ")
                characterPrefab = characterPrefabs[2];

        if (inspectTransform != null)
        {
            GameObject inspectGameObject = inspectTransform.gameObject;
            foreach (Transform child in inspectGameObject.transform)
            {
                Destroy(child.gameObject);
            }

            if (characterPrefab != null)
            {
                GameObject newInspectObject = Instantiate(characterPrefab, inspectGameObject.transform.position, inspectGameObject.transform.rotation);

                newInspectObject.transform.SetParent(inspectGameObject.transform);

            }
            else
            {
                Debug.LogError("New prefab is not assigned!");
            }
        }
    }
    public void ChangeWeapon(string weapon)
    {
        selectedWeapon = weapon;

        Transform canvasTransform = MainMenuBackGround.transform.Find("Canvas");
        Sprite weaponPrefab = weaponSprites[0];
        if (weapon == "Quas")
            weaponPrefab = weaponSprites[0];
        else if (weapon == "Wex")
            weaponPrefab = weaponSprites[1];
        else if (weapon == "Exort")
            weaponPrefab = weaponSprites[2];

        if (canvasTransform != null)
        {
            Image[] images = canvasTransform.GetComponentsInChildren<Image>();
            foreach (Image image in images)
            {
                if (image.name == "Weapon")
                {
                    image.sprite = weaponPrefab;
                }
            }
        }
    }

    public void ChangeLaser(string laser)
    {
        selectedLaser = laser;
    }

    public void ChangeRadar(string radar)
    {
        selectedRadar = radar;
    }
    public void SaveNickname()
    {
        nickname = nicknameField.text;
        Debug.Log(nickname + " nickname");
    }
}
