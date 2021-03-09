using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPool : MonoBehaviour
{
    public GameObject prefab;   // The prefab to spawn
    public int size;            // Size of the array
    public int min;             // The min number of spawns
    public string tagName;      // Used to locate the spawns for this prefab
    public string objName;      // Reset the name of the obj so it can work with pickup
    public int x;               // X rotation of object
    public int y;               // Y rotation of object
    public int z;               // Z rotation of object
    
    Dictionary<GameObject, bool> pool = new Dictionary<GameObject, bool>();     // Stores a list of the game objects
    GameObject selectedObj;     // The object selected to move to spawn
    Vector3 spawnPos;           // The chosen spawn position
    Transform tempPos;            // The temporary position
    GameObject[] spawns;        // An array to hold the spawn positions
    int number;                 // The number of spawns - random
    int spawnNum;               // The spawn point chosen - random
    List<int> called = new List<int>();     // List of called spawn points
    //Transform test;             // The parent for the prefabs

    // Start is called before the first frame update
    void Start()
    {
        //// ASSIGNS VARIABLES ////
        tempPos = GameObject.Find("Pool").transform;
        spawns = GameObject.FindGameObjectsWithTag(tagName);
        number = Random.Range(min, (spawns.Length + 1));
        //test = GameObject.Find("Test").transform;
        //Debug.Log(number);

        //// SPAWNS THE OBJECTS AT TEMP POS ////
        for (int i = 0; i < size; i++)
        {
            //Debug.Log("adding objs");
            GameObject obj = (GameObject)Instantiate(prefab, tempPos.position, Quaternion.identity, tempPos);
            obj.transform.rotation = Quaternion.Euler(x, y, z);
            obj.name = objName;
            pool.Add(obj, true);
            //Debug.Log(prefab);
            //Debug.Log(obj);
            //obj.GetComponent<Rigidbody>().isKinematic = true;
        }
        
        //// SELECTS WHICH SPAWNS TO USE AND MOVES OBJS TO THEM ////
        for (int i = 0; i < number; i++)
        {
            spawnNum = Random.Range(0, spawns.Length);

            while (called.Contains(spawnNum))
                spawnNum = Random.Range(0, spawns.Length);
            
            called.Add(spawnNum);
            spawnPos = spawns[spawnNum].transform.position;
            spawn();
            //Debug.Log(spawnNum);
            //Debug.Log(spawns[spawnNum].name);
        }
    }

    //// MOVES THE OBJ TO CHOSEN SPAWN POS ////
    void spawn()
    {
        foreach(KeyValuePair<GameObject, bool> obj in pool)
        {
            if(obj.Value == true)
            {
                selectedObj = obj.Key;
                break;
            }
        }

        pool[selectedObj] = false;
        //selectedObj.GetComponent<Rigidbody>().isKinematic = false;
        selectedObj.transform.position = spawnPos;
    }
}
