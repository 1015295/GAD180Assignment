using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    public GameObject keyPrefab;
    public Transform[] keySpawnLocations;
    public List<GameObject> keysList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (var keyLoc in keySpawnLocations)
        {
            GameObject newKey = Instantiate(keyPrefab, keyLoc.position, keyLoc.rotation); //For every KeySpawnLoc, a key is placed at its location.
            keysList.Add(newKey); //All instantiated keys are added to a list.
        }
    }
}
