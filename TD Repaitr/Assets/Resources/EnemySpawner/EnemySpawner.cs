using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Unit[] Units;
    public EnemyPoint[] Path;

    public int EnemiesCount;
    public int DefaultEnemiesCount;
    private int CurrentWave;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            NextWave();
        }
    }

    void NextWave()
    {
        if (CurrentWave <= Units.Length)
        {
            CurrentWave++;
            EnemiesCount = DefaultEnemiesCount;
            SpawnEnemies();
        }
        else
        {
            //Victory
        }
    }

    void SpawnEnemies()
    {
        var unit = Instantiate<Unit>(Units[CurrentWave], transform.position, Units[CurrentWave].gameObject.transform.rotation);
        unit.AssignPath(Path);
        if (EnemiesCount > 0)
        {
            Invoke("SpawnEnemies",0.3f);
            EnemiesCount--;
        }
    }
}
