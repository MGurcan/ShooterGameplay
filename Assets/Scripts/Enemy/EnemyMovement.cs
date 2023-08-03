using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator anim;

    private float currentHealth = 1000f;
    public Slider sleider;

    public bool enemyVisibleOnMinimap = false;

    public GameObject minimapObject;

    private ShootingManager shootingManager;

    private bool die = false;

    private void Start()
    {
        shootingManager = FindObjectOfType<ShootingManager>();
    }
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed /= 4f;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, navMeshAgent.destination);

        if(distanceToPlayer < 1f)
        {
            shootingManager.SaveFinishDataDeath();
            SceneManager.LoadScene("FinishScene");
        }
        sleider.value = currentHealth;
        if(currentHealth <= 0 && !die)
        {
            Debug.Log("hei");
            die = true;
            shootingManager.CountKilledEnemy();
            shootingManager.AddEnemyKillRanges(distanceToPlayer);
            PlayDeathAnimation();
        }

        if (distanceToPlayer < 5f)
        {
            anim.SetBool("CatWalking", false);
            anim.SetTrigger("CanJumpAttack");
        }
        else
        {
            anim.SetBool("CatWalking", true);
        }
        minimapObject.SetActive(enemyVisibleOnMinimap);
    }

    public void PlayDeathAnimation()
    {
        anim.SetTrigger("FallingBackDie");
        navMeshAgent.speed = 0f;
    }

    public void OnDeathAnimationFinished()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    public void SetHealth(float health)
    {
        currentHealth = health;
    }

}
