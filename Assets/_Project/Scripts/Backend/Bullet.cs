using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] int MAX_JUMPS;
    [SerializeField] float LIGHTNING_TIMEOUT;
    [SerializeField] float lightning_radius;

    [System.Flags]
    public enum Flags
    {
        nothing = 0,
        supressDestroy = 1 >> 1,
    }
    public delegate void EventCB(Vector3 bulletPos, ref List<Enemy> enemy, ref Flags flags);

    [HideInInspector] public EventCB collisionEvent;
    [HideInInspector] public EventCB aoeEvent;
    [HideInInspector] public bool withLightning = false;

    public float timeToLive;
    Timer deathTimer;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        deathTimer = Timer.CreateTimer(timeToLive);
        player = FindObjectOfType<Player>();
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
        if (aoeEvent != null)
            aoeEvent(pos, ref enemies, ref flags);
        if (collisionEvent != null)
            collisionEvent(pos, ref enemies, ref flags);
    }

    Enemy GetNearest(Enemy enemy, List<Enemy> blacklist = null)
    {
        if (blacklist == null) blacklist = new List<Enemy>();
        var pos = enemy.transform.position;
        var nearest = Physics
            .SphereCastAll(pos, lightning_radius, Vector3.up)
            .Select(thing => thing.collider.GetComponent<Enemy>())
            .Where(e => e != null && e != enemy && !blacklist.Contains(e))
            .OrderBy(e => (pos - e.transform.position).sqrMagnitude)
            .FirstOrDefault();

        Debug.Log($"nearest: {nearest}");

        return nearest;
    }

    IEnumerator DoLightning(Enemy enemy, Vector3 offset)
    {
        Debug.Log($"enemy: {enemy}");
        List<Enemy> blacklist = new List<Enemy>();
        // jump to nearest near-enough
        var prev_enemy = enemy;
        var nearest = GetNearest(enemy);
        // apply the actions to them
        for (int i = 0; i < MAX_JUMPS; i++)
        {
            if (nearest == null) break;

            blacklist.Add(prev_enemy);

            // pause
            Debug.Log($"pausing for {LIGHTNING_TIMEOUT} seconds");
            yield return new WaitForSeconds(LIGHTNING_TIMEOUT);
            //yield return null;
            Debug.Log($"unpaused");

            Flags flags = Flags.nothing;

            var from = prev_enemy.transform.position;
            var to = nearest.transform.position;
            Debug.Log($"to: {to}, " +
                $"from: {from}, " +
                $"offset: {offset}, " +
                $"newPoint: {(to - from) + offset}, " +
                $"nearest: {nearest}");

            DoTheThings((to-from)+offset, nearest, ref flags);


            // jump to next nearest near-enough
            prev_enemy = nearest;
            nearest = GetNearest(nearest, blacklist);
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
                player.StartCoroutine(DoLightning(enemy, offset));
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet")) return;

        Flags flags = Flags.nothing;

        if (other.gameObject.CompareTag("Enemy"))
        {
            var enemy = other.gameObject.GetComponent<Enemy>();
            DoTheThings(transform.position, enemy, ref flags);
            var offset = transform.position - enemy.transform.position;
            if (withLightning)
            {
                player.StartCoroutine(DoLightning(enemy, offset));
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
