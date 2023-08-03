using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    public Transform player;
    public Transform minimapOverlay;
    public float angle;

    private GameObject[] enemies;

    void Start()
    {
        player = GameObject.FindWithTag("Shooter").transform;
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        transform.position = player.position + Vector3.up * 5f;
        RotateOverlay();

    }

    private void RotateOverlay()
    {
        minimapOverlay.localRotation = Quaternion.Euler(0,0, -player.eulerAngles.y - angle);
    }


}
