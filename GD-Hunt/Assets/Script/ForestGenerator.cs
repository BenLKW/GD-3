using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ForestGenerator : MonoBehaviour
{
    
    public GameObject treeObject1;

    public GameObject treeObject2;
    
    public GameObject treeInWorldObject;

    public int treeAmount;

    List<GameObject> treeList = new List<GameObject>();

    GameObject[] treeArray;

    private void Start()
    {
        //GameObject gameObject = treeObject[i];
        for (int i=0; i <= treeAmount; i++)
        {
            treeList.Add(Instantiate(treeObject1));
            treeList.Add(Instantiate(treeObject2));
            treeArray = treeList.ToArray();
            treeArray[i].transform.position = new Vector3(Random.Range(this.transform.position.x-50, this.transform.position.x + 50), 0, Random.Range(this.transform.position.z - 50, this.transform.position.z + 50));
            treeArray[i].transform.parent = treeInWorldObject.transform;

        }
    }

}