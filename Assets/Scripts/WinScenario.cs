using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScenario : MonoBehaviour
{
    public int numberOfEnemies;
    public int numberOfScene;

    void Update()
    {
        if (numberOfEnemies == 0)
        {
            FindObjectOfType<GameManager>().NextLevel(numberOfScene);
        }
    }

    public void ReduceNumberOfEnemies()
    {
        numberOfEnemies -= 1;
    }
}
