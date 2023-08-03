using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShootingManager : MonoBehaviour
{
    public GameData gameData;
    public FinishData finishData;

    [Header("Animation")]
    public Animator anim;

    [Header("Laser Bullet")]
    public GameObject laserProjectile;
    private float projectileSpeed = 200f;
    public Transform projectileStartPointTransform;

    [Header("Sliders")]
    public Slider chargeDurationSlider;
    public Text weaponTemparatureText;

    [Header("Weapon Fire System")]
    private float loadedEnergy = 0f;
    private float shootLoadingDuration = 0f;
    private float shootLoadingStart = 0f;
    private float maxshootLoadingDuration = 5f;

    [Header("Weapon Battery System")]
    private float totalBattery = 5000f;
    public Text totalBatteryText;


    [Header("Weapon Shooting System")]
    private float weaponHitRange = 10f;
    private int totalShotTime = 0;  // -> finishdata.totalshot
    private int totalSuccessfullHit = 0;  // -> finishdata.totalsuccesfullshot
    private float totalDamage;  // -> finishdata.totalDamage
    private float totalDuration = 0f;  // -> finishdata.totalDuration
    private int totalKill = 0;
    private float[] enemyKillRanges;
    [Header("Weapon Cooling System")]
    private float firstWeaponTemparature = 20f;
    private float weaponTemparature = 20f;
    private float coolingDuration = 0f;
    private float currentCoolingDuration = 0f;

    private bool triggeredLeftClick = false;

    [SerializeField] private GameObject radarTriangle;
    public float apexAngleDegrees = 60f; // Tepe açýsý (derece cinsinden)
    public float edgeLength = 1f; // Kenar uzunluðu

    void Start()
    {
        anim = GetComponent<Animator>();
        
        if(gameData.selectedWeapon == "Quas")
        {
            totalBattery = 5000f;
            weaponHitRange = 10f;
        }
        else if (gameData.selectedWeapon == "Wex")
        {
            totalBattery = 10000f;
            weaponHitRange = 15f;
        }
        else if (gameData.selectedWeapon == "Exort")
        {
            totalBattery = 15000f;
            weaponHitRange = 20f;
        }
        totalBatteryText.text = totalBattery.ToString("F1");
    }

    void Update()
    {
        totalDuration += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            triggeredLeftClick = true;
            shootLoadingStart = Time.time;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            shootLoadingDuration = Time.time - shootLoadingStart;
            if (shootLoadingDuration > maxshootLoadingDuration)
                shootLoadingDuration = maxshootLoadingDuration;
            triggeredLeftClick = false;
            chargeDurationSlider.value = 0f;
            Shoot(shootLoadingDuration);

        }

        if (triggeredLeftClick && weaponTemparature <= 20f)
        {
            float value = ((Time.time - shootLoadingStart) / maxshootLoadingDuration) * 100;
            if (value > 100)
                value = 100f;
            chargeDurationSlider.value = value;
        }
        
        //Weapon Temprature Calculation
        currentCoolingDuration += Time.deltaTime;
        float t = currentCoolingDuration / coolingDuration;
        weaponTemparature = Mathf.Lerp(firstWeaponTemparature, 20f, t);

        if(weaponTemparature < 20f)
        {
            weaponTemparature = 20f;
        }
        weaponTemparatureText.text = weaponTemparature.ToString("F1") + "°C";
    }

    void Shoot(float shootLoadingDuration)
    {
        if (weaponTemparature <= 20f)
        {
            totalShotTime++;
            anim.SetTrigger("Shoot");

            // shootLoadingDuration -> 0 - 5 sn maxshootloadingDuration -> 5
            firstWeaponTemparature = 20 + (shootLoadingDuration / maxshootLoadingDuration) * 50;
            weaponTemparature = firstWeaponTemparature;

            coolingDuration = Mathf.Pow(2, (firstWeaponTemparature / 10)) / Mathf.Pow(2, (firstWeaponTemparature / 20)); //total coolingDuration
            currentCoolingDuration = 0;

            loadedEnergy = Mathf.Pow(2f, shootLoadingDuration) * 100;

            totalBattery -= loadedEnergy;

            if(loadedEnergy > totalBattery)
            {
                loadedEnergy = totalBattery;
            }
            if(totalBattery < 0)
            {
                totalBattery = 0;
            }
            totalBatteryText.text = totalBattery.ToString("F1") + "";

            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            GameObject newLaserProjectile = Instantiate(laserProjectile, projectileStartPointTransform.position, projectileStartPointTransform.rotation);
            newLaserProjectile.GetComponent<LaserProjectile>().SetDamage(0);

            if (Physics.Raycast(ray, out hit))
            {
                if(hit.transform != null)
                {
                    float damage = CalculateDamage(hit.transform, loadedEnergy);
                    newLaserProjectile.GetComponent<LaserProjectile>().SetDamage(damage);

                    if (hit.transform.CompareTag("Enemy"))
                    {
                        totalSuccessfullHit++;
                        totalDamage += damage;
                    }
                }
                Debug.Log("Ateþ Edildi! Vurulan Nesne: " + hit.transform.name);

                Vector3 targetPosition = hit.point;

                Vector3 shootDirection = (targetPosition - projectileStartPointTransform.position).normalized;

                newLaserProjectile.GetComponent<Rigidbody>().velocity = shootDirection * projectileSpeed;
            }
        }
        
    }

    public void CountKilledEnemy()
    {
        totalKill++;
    }

    public void AddEnemyKillRanges(float range)
    {
        if (enemyKillRanges == null)
        {
            float[] newArray = new float[1];
            newArray[0] = range;
            enemyKillRanges = newArray;
        }
        else
        {
            int index = enemyKillRanges.Length;
            float[] newArray = new float[index + 1];
            Array.Copy(enemyKillRanges, newArray, index);
            newArray[newArray.Length - 1] = range;
            enemyKillRanges = newArray;
        }

    }
    public float CalculateDamage(Transform hitTransform, float loadedEnergy)
    {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Shooter").transform.position;

        Vector3 hitObjectPosition = hitTransform.position;

        // Oyuncu ile vurulan nesne arasýndaki mesafeyi ölç
        float range = Vector3.Distance(playerPosition, hitObjectPosition);

        // Damage = LE - UER
        // UER = LE * log20(Range)
        float UER = loadedEnergy * Mathf.Log(range, 20f);
        float damage = loadedEnergy - UER;

        // Mesafeyi kullanarak gerekli iþlemleri yapabilirsiniz
        Debug.Log("Oyuncu ile vurulan nesne arasýndaki mesafe: " + range);
        Debug.Log("Vurulan Damage: " + damage);
        if (range > weaponHitRange)
            return 0;
        return damage;
    }

    public void SaveFinishDataDeath()
    {
        finishData.totalDuration = totalDuration;
        finishData.totalDamage = totalDamage;
        finishData.enemyKillRanges = enemyKillRanges;
        finishData.totalShot = totalShotTime;
        finishData.totalKilledEnemy = totalKill;
        finishData.succesfullShot = totalSuccessfullHit;
        finishData.remainHealth = 0f;
    }
}
