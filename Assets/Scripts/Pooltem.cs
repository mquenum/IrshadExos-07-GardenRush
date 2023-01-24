using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooltem : MonoBehaviour
{
    public Spawner spawner;

    // Update is called once per frame
    void OnDisable()
    {
        if (spawner!= null)
        {
            spawner.AddToPool(this);
        }
        
    }
}
