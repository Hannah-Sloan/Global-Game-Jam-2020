using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    [System.Serializable]
    public class tokenList
    {
        public string name;
        public List<GameObject> tokens;
    }

    //[SerializeField] protected List<GunComponent> inventory = new List<GunComponent>(5);

    [SerializeField] public List<tokenList> modTokens = new List<tokenList>();

    public void AddComponent(GunComponent newMod, int indexToInsertInto)
    {
        GameObject toSpawn = null;

        if (newMod.GetType() == typeof(MagasineComponent))
        {
            if (newMod.GetType() == typeof(AreaOfEffect))
                toSpawn = modTokens[0].tokens[0];
            if (newMod.GetType() == typeof(Penetrative))
                toSpawn = modTokens[0].tokens[1];
            if (newMod.GetType() == typeof(Knockback))
                toSpawn = modTokens[0].tokens[2];
            if (newMod.GetType() == typeof(Lightning))
                toSpawn = modTokens[0].tokens[3];
            if (newMod.GetType() == typeof(Fire))
                toSpawn = modTokens[0].tokens[4];
            if (newMod.GetType() == typeof(Bullet))
                toSpawn = modTokens[0].tokens[5];
        }
        else if (newMod.GetType() == typeof(MuzzleComponent))
        {
            if (newMod.GetType() == typeof(ProjectileNumber))
                toSpawn = modTokens[1].tokens[0];
            if (newMod.GetType() == typeof(Kickback))
                toSpawn = modTokens[1].tokens[1];
            if (newMod.GetType() == typeof(Heavy))
                toSpawn = modTokens[1].tokens[2];
            if (newMod.GetType() == typeof(Accuracy))
                toSpawn = modTokens[1].tokens[3];
        }
        else //Type  of TriggerComponent
        {
            if (newMod.GetType() == typeof(Capacity))
                toSpawn = modTokens[2].tokens[0];
            if (newMod.GetType() == typeof(FireRate))
                toSpawn = modTokens[2].tokens[1];
            if (newMod.GetType() == typeof(ReloadTime))
                toSpawn = modTokens[2].tokens[2];
        }

        Transform parent = transform.GetChild(indexToInsertInto);
        GameObject token = Instantiate(toSpawn, parent);
        var modMover = token.GetComponent<UIModMover>();
        modMover.home = parent.GetComponentInChildren<Blank>();
        modMover.Dropped();
    }
}
