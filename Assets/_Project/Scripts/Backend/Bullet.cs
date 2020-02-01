using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] int MAX_JUMPS;
    [SerializeField] float LIGHTNING_TIMEOUT;

    [System.Flags]
    public enum Flags
    {
        nothing = 0,
        supressDestroy = 1 >> 1,
    }
    public delegate void EventCB(Vector3 bulletPos, ref List<Enemy> enemy, ref Flags flags);

    [HideInInspector] public EventCB collisionEvent;
    [HideInInspector] public EventCB aoeEvent;
    [HideInInspector ]public bool withLightning = false;


    public float timeToLive;
    Timer deathTimer;

    // Start is called before the first frame update
    void Start()
    {
        deathTimer = Timer.CreateTimer(timeToLive);
    }

    // Update is called once per frame
    void Update()
    {
        if (deathTimer.CheckComplete()) Destroy(gameObject);
    }

    void DoTheThings(Vector3 pos, Enemy enemy, ref Flags flags)
    {
        List<Enemy> enemies = new List<Enemy>();
        enemies.Add(enemy);
        aoeEvent(pos, ref enemies, ref flags);
        collisionEvent(pos, ref enemies, ref flags);
    }

    Enemy GetNearest(Enemy enemy)
    {
        return enemy;
    }

    IEnumerator DoLightning(Enemy enemy, Vector3 offset)
    {
        // jump to nearest near-enough
        var prev_enemy = enemy;
        var nearest = GetNearest(enemy);
        // apply the actions to them
        for (int i = 0; i < MAX_JUMPS; i++)
        {
            Flags flags = Flags.nothing;
            var from = prev_enemy.transform.position;
            var to = nearest.transform.position;
            DoTheThings((to-from)+offset, nearest, ref flags);
            // pause
            yield return new WaitForSeconds(LIGHTNING_TIMEOUT);
            // jump to next nearest near-enough
            prev_enemy = nearest;
            nearest = GetNearest(nearest);
        }

        yield return null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) return;

        Flags flags = Flags.nothing;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            var enemy = collision.gameObject.GetComponent<Enemy>();
            DoTheThings(transform.position, enemy, ref flags);
            var offset = transform.position - enemy.transform.position;
            if (withLightning)
            {
                StartCoroutine(DoLightning(enemy, offset));
            }
            //List<Enemy> enemies = new List<Enemy>();
            //enemies.Add(enemy);
            //aoeEvent(transform, ref enemies, ref flags);
            //collisionEvent(transform, ref enemies, ref flags);
        }

        if (!ContainsFlag(flags, Flags.supressDestroy))
        {
            Destroy(gameObject);
        }
    }

    bool ContainsFlag(Flags flags, Flags test)
    {
        return (int)(flags & test) == 1;
    }
}
