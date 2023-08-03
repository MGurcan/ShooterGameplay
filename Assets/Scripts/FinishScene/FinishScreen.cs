using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class FinishScreen : MonoBehaviour
{
    public FinishData finishData;

    public float remainHealth;
    public int totalShot;
    public int succesfullShot;
    public float totalDamage;
    public int totalKilledEnemy;
    public int[] enemyKillRanges;

    public TextMeshProUGUI remainHealthText;
    public TextMeshProUGUI totalShotText;
    public TextMeshProUGUI totalDurationText;
    public TextMeshProUGUI succesfullShotText;
    public TextMeshProUGUI totalDamageText;
    public TextMeshProUGUI totalKilledEnemyText;

    public TextMeshProUGUI enemyKillRangesText;
    private void Awake()
    {
        // Assign GameData reference if it's not already assigned
        if (finishData == null)
        {
            finishData = Resources.Load<FinishData>("FinishData"); // Change "GameData" to the correct path of your ScriptableObject
        }

        // Load player data from the ScriptableObject
        LoadFinishData();
        Debug.Log("loaded data");

        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadFinishData()
    {
        // Load the data from the ScriptableObject and apply it to the game
        remainHealthText.text = "Remain Health: " + finishData.remainHealth.ToString();
        totalShotText.text = "Total Shot: " + finishData.totalShot.ToString();
        totalDurationText.text = "Total Duration: " + finishData.totalDuration.ToString();
        succesfullShotText.text = "Succesfull Shot: " + finishData.succesfullShot.ToString(); ;
        totalDamageText.text = "Total Damage: " + finishData.totalDamage.ToString(); ;
        totalKilledEnemyText.text = "Total Killed Enemy: " + finishData.totalKilledEnemy.ToString();

        if (enemyKillRangesText != null && finishData.enemyKillRanges!= null)
        {
            string floatTextContent = "";
            int count = 1;
            foreach (float item in finishData.enemyKillRanges)
            {
                floatTextContent += "Enemy " + count + " : " + item.ToString("F2") + "\n"; // Float deðeri stringe çevir ve bir alt satýra geç
                count++;
            }
            enemyKillRangesText.text = floatTextContent;
        }
    }

    public void GoStartScreen()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void QuitGame()
    {   // records will be handled on gamescene
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Sanctuary");
    }
}
