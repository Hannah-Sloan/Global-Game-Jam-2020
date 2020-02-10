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
    [SerializeField] Color hitColor;
    [SerializeField] int impactLength = 2;
    [SerializeField] float hitBrightnessScaling = 1;

    int health;
    bool isOnFire = false;
    Timer fireTimer;
    int fireTicks = 0;
    NavMeshAgent agent;
    Player player;
    Rigidbody rb;
    ParticleSystem fireFx = null;
    Material myMat;

    bool done = true;
    bool die = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Enemy";

        health = maxHealth;
        fireTimer = Timer.CreateTimer(fireTickTime, false);

        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody>();
        myMat = GetComponent<MeshRenderer>().material;

        die = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            if (done)
                Die();
            else
                die = true;
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
        StartCoroutine(HitAnimation(damage));
    }

    IEnumerator HitAnimation(int damage)
    {
        done = false;
        myMat.SetColor("_EmissionColor", hitColor * damage * hitBrightnessScaling);
        for (int i = 0; i < impactLength; i++)
        {
            yield return null;
        }
        myMat.SetColor("_EmissionColor", hitColor * 0);
        done = true;
        if (die)
            Die();
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
