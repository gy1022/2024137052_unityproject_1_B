using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] CircleObject;
    public Transform GenTransform;
    public float TimeCheck;
    public bool isGen;

    // Start is called before the first frame update
    void Start()
    {
        GenObject();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGen)
        {
            TimeCheck -= Time.deltaTime;
            if(TimeCheck <= 0 )
            {
                int RandNumber = Random.Range(0, 3);
                GameObject Temp = Instantiate(CircleObject[RandNumber]);
                Temp.transform.position = GenTransform.position;
                isGen = true;
            }
        }
    }

    public void GenObject()
    {
        isGen = false;
        TimeCheck = 1.0f;
    }

    public void MergeObject(int index, Vector3 position)
    {
        GameObject Temp = Instantiate(CircleObject[index]);
        Temp.transform.position = position;
        Temp.GetComponent<CircleObject>().Used();
    }
}
