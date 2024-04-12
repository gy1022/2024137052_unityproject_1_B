using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                                       //UI 사용하기 위해 추가
using UnityEngine.SceneManagement;

public class ExMainScene : MonoBehaviour
{
    public Text PointUI;

    void Start()
    {
        PointUI.text = PlayerPrefs.GetInt("Point").ToString();
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("GameScene_Shoot");
    }

}
