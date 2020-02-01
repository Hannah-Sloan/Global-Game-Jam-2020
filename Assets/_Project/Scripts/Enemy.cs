using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] float fireTickTime;
    [SerializeField] int fireDamage;
    [SerializeField] int maxFireTicks;

    int health;
    bool isOnFire = false;
    Timer fireTimer;
    int fireTicks = 0;
    NavMeshAgent agent;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Enemy";

        health = maxHealth;
        fireTimer = Timer.CreateTimer(fireTickTime, false);

        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>();
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
        }

        agent.SetDestination(player.transform.position);
        transform.position = agent.nextPosition;

    }

    public void TakeDamage(int damage)
    {
        health = Mathf.Max(health - damage, 0);
    }

    public void Ignite()
    {
        fireTimer.TimerStart();
        isOnFire = true;
        fireTicks = 0;
    }

    void Die()
    {
        Destroy(fireTimer);
        Destroy(gameObject);
    }
    
}
