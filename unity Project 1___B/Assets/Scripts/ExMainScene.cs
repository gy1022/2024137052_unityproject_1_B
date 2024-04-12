using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                                       //UI ����ϱ� ���� �߰�
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
