using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemies : MonoBehaviour
{
    public Transform player; // Karakterin transformu

    private GameObject[] enemies;

    public float detectionRadius = 5f; // Düþmanlarý algýlayacak mesafe
    public float detectionAngle = 60f; // Düþmanlarý algýlayacak açý

    public LayerMask radarLayer;

    public GameData gameData;
    private void Start()
    {
        if (gameData.selectedRadar == "laser_60")
            detectionAngle = 60f;
        else if (gameData.selectedRadar == "laser_120")
            detectionAngle = 120f;
        else if (gameData.selectedRadar == "laser_180")
            detectionAngle = 180f;
    }
    void Update()
    {

        HandleEnemyVisible();
    }
    private void HandleEnemyVisible()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for(int i = 0; i < enemies.Length; i++)
        {
            bool enemyVisible = Vector3.Angle(player.forward, enemies[i].transform.position - player.position) <= detectionAngle;
            //enemies[i].SetActive(Vector3.Angle(player.forward, enemies[i].transform.position - player.position) <= detectionAngle)
            enemies[i].gameObject.GetComponent<EnemyMovement>().enemyVisibleOnMinimap = enemyVisible;
        }
            
    }



}
