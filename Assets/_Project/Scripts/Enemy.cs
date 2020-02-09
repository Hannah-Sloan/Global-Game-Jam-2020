using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] float fireTickTime;
    [SerializeField] int fireDamage;
    [SerializeField] int maxFireTicks;
    [SerializeField] ParticleSystem fireFxPrefab;
    [SerializeField] ParticleSystem blood; 

    int health;
    bool isOnFire = false;
    Timer fireTimer;
    int fireTicks = 0;
    NavMeshAgent agent;
    Player player;
    Rigidbody rb;
    ParticleSystem fireFx = null;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Enemy";

        health = maxHealth;
        fireTimer = Timer.CreateTimer(fireTickTime, false);

        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Die();
        }

        if (isOnFire && fireTimer.CheckAndReset())
        {
            TakeDamage(fireDamage);
            fireTicks++;
        }

        if (fireTicks > maxFireTicks)
        {
            isOnFire = false;
            fireTimer.TimerStop();
            fireTimer.ResetTimer();
            if (fireFx != null) Destroy(fireFx.gameObject);
        }

        //Don't worry if this errors, it's just running on the frame the ai dies.
        agent.SetDestination(player.transform.position);
    }

    public void TakeDamage(int damage)
    {
        health = Mathf.Max(health - damage, 0);
    }

    public void Ignite()
    {
        fireFx = Instantiate(fireFxPrefab, transform);
        fireTimer.TimerStart();
        isOnFire = true;
        fireTicks = 0;
    }

    void Die()
    {
        Destroy(fireTimer.gameObject);
        Instantiate(blood, transform.position, Quaternion.identity);
        EnemySpawner.Instance.EnemyKilled();
        Destroy(gameObject);
    }
    
}
