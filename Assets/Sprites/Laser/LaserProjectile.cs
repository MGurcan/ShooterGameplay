using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    private float damage;
    private void Start()
    {
        Destroy(gameObject, 4f);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Vurdu: " + damage);
            EnemyMovement enemyMovement = collision.gameObject.GetComponent<EnemyMovement>();
            if (enemyMovement != null)
            {
                Debug.Log("Hani vurduydu " + damage);
                enemyMovement.TakeDamage(damage);
            }
    
            
        }
        Destroy(gameObject);
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

}
