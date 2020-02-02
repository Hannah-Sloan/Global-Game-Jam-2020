using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : Singleton<Inventory>
{
    private void Start()
    {
        UIEnableDisable.Instance.UIOn += OnUI;
    }

    [System.Serializable]
    public class tokenList
    {
        public string name;
        public List<GameObject> tokens;
    }

    [System.Serializable]
    public class later
    {
        public later()
        {
        }
        public later(GunComponent newMod, int indexToInsertInto)
        {
            this.newMod = newMod;
            this.indexToInsertInto = indexToInsertInto;
        }
        public GunComponent newMod;
        public int indexToInsertInto;
    }

    [SerializeField] public List<tokenList> modTokens = new List<tokenList>();

    public List<later> toAddLater = new List<later>(5);

    void Awake()
    {
        toAddLater = new List<later>(5) {null, null, null, null, null };
    }

    public void AddComponent(GunComponent newMod, int indexToInsertInto)
    {
        toAddLater[indexToInsertInto] = (new later(newMod, indexToInsertInto));
        //var nextOpen = toAddLater.FindIndex(x => x == null);
        //toAddLater[nextOpen] = new later(newMod, nextOpen);
        //return;
    }

    public void SyncToAddLater(List<GunComponent> currentInv)
    {
        for (int i = 0; i < 5; i++)
        {
            toAddLater[i] = new later(currentInv[i], i);
        }
    }

    private void RealAddComponent(GunComponent newMod, int indexToInsertInto)
    {
        GameObject toSpawn = null;

        //Grabs icon from file.
        if (newMod is MagasineComponent)
        {
            if (newMod is AreaOfEffect)
                toSpawn = modTokens[0].tokens[0];
            if (newMod is Penetrative)
                toSpawn = modTokens[0].tokens[1];
            if (newMod is Knockback)
                toSpawn = modTokens[0].tokens[2];
            if (newMod is Lightning)
                toSpawn = modTokens[0].tokens[3];
            if (newMod is Fire)
                toSpawn = modTokens[0].tokens[4];
            if (newMod is Damage)
                toSpawn = modTokens[0].tokens[5];
        }
        else if (newMod is MuzzleComponent)
        {
            if (newMod is ProjectileNumber)
                toSpawn = modTokens[1].tokens[0];
            if (newMod is Kickback)
                toSpawn = modTokens[1].tokens[1];
            if (newMod is Heavy)
                toSpawn = modTokens[1].tokens[2];
            if (newMod is Accuracy)
                toSpawn = modTokens[1].tokens[3];
        }
        else //Type  of TriggerComponent
        {
            if (newMod is Capacity)
                toSpawn = modTokens[2].tokens[0];
            if (newMod is FireRate)
                toSpawn = modTokens[2].tokens[1];
            if (newMod is ReloadTime)
                toSpawn = modTokens[2].tokens[2];
        }

        Transform parent = GameObject.FindGameObjectWithTag("Inventory").transform.GetChild(indexToInsertInto);
        GameObject token = Instantiate(toSpawn, parent);
        var modMover = token.GetComponent<UIModMover>();
        modMover.home = parent.GetComponentInChildren<Blank>();
        newMod.gameObject.transform.parent = token.transform;
        newMod.gameObject.SetActive(true);
        modMover.AddToInv(indexToInsertInto);
    }

    public void OnUI()
    {
        for (int i = 0; i < toAddLater.Count; i++)
        {
            if (toAddLater[i] == null || toAddLater[i].newMod == null) continue;
            RealAddComponent(toAddLater[i].newMod, toAddLater[i].indexToInsertInto);
            toAddLater[i] = null;
        }
        //toAddLater = FindObjectOfType<InventoryHolder>().mods.Select(x => x == null ? null : new later()).ToList();
        //toAddLater = new List<later>() { null, null, null, null, null };
    }
}
