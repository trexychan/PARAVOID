using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeList : MonoBehaviour
{
    public ArrayList nodeList = new ArrayList();
    public GameObject nodeGroup;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject node in nodeGroup.transform)
        {
            nodeList.Add(node);
        }
        foreach (GameObject node in nodeList)
        {
            Debug.Log(node.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
