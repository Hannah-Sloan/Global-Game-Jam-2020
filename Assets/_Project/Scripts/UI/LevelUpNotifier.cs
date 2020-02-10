using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpNotifier : MonoBehaviour
{
    [SerializeField] ComponentSpawner spawner;
    Animator animator;

    public delegate void LevelUp();

    public LevelUp levelUP;

    int level;

    private void Start()
    {
        level = 1;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (EnemySpawner.Instance.totalKills + 1 >= Mathf.Pow(spawner.tierScaleFactor, level))
        {
            level++;
            levelUP?.Invoke();
            animator.SetTrigger("LevelUp");
        }
    }
}
