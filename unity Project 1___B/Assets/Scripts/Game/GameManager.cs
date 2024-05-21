using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] CircleObject;
    public Transform GenTransform;
    public float TimeCheck;
    public bool isGen;

    public int Point;
    public int BestScore;
    public static event Action<int> OnPointChanged;
    public static event Action<int> OnBestScoreChanged;

    // Start is called before the first frame update
    void Start()
    {
        BestScore = PlayerPrefs.GetInt("BestScore");
        GenObject();
        OnPointChanged?.Invoke(Point);
        OnBestScoreChanged?.Invoke(BestScore);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGen)
        {
            TimeCheck -= Time.deltaTime;
            if(TimeCheck <= 0 )
            {
                int RandNumber = UnityEngine.Random.Range(0, 3);
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

        Point += (int)Mathf.Pow(index, 2) * 10;
        OnPointChanged?.Invoke(Point);
    }

    public void EndGame()
    {
        int BestScore = PlayerPrefs.GetInt("BestScore");

        if(Point > BestScore)
        {
            BestScore = Point;
            PlayerPrefs.SetInt("BestScore", Point);
            OnBestScoreChanged?.Invoke(Point);
        }
    }
}
