using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                                           //UI�� ����ϱ� ���ؼ�
using UnityEngine.SceneManagement;                              //Scene �̵� �ϱ� ���ؼ�

public class ExRayCast : MonoBehaviour
{
    public int Point = 0;                                       //����Ʈ ��� ����
    public GameObject TargetObject;                              //Ÿ�� ������
    public float ChekTime = 0;                                  //Ÿ�� Gen �ð� ����
    public float GameTime = 30.0f;                               //���� �ð� ����

    public Text pointUI;                                         //Unity UI ����
    public Text timeUI;                                         //Unity UI ����

    // Update is called once per frame
    void Update()
    {
        ChekTime += Time.deltaTime;                                                      //�������� �����Ǿ� �ð��� ����ϰ� �Ѵ�
        GameTime -= Time.deltaTime;                                                      //������ �ð��� �����Ͽ� 30�� -> 0�ʷ� �����Ѵ�

        if(GameTime <= 0)                                                   //0�ʰ� �Ǹ�
        {
            PlayerPrefs.SetInt("Point", Point);                             //����Ƽ���� �����ϴ� ���� �Լ�
            SceneManager.LoadScene("MainScene");                            //MainScene���� �̵��Ѵ�
        }

        pointUI.text = "���� : " + Point.ToString();                              //UI���� ǥ��
        timeUI.text = "���� �ð� : " + GameTime.ToString() + "s";                   //UI ���� �ð� ǥ��



        if(ChekTime >= 0.5f)                                                            //0.5�ʸ��� �ൿ�� �Ѵ�.
        {
            
            int RandomX = Random.Range(0, 12);                                          //0~11������ �������� �̾Ƴ���.
            int RandomY = Random.Range(0, 12);                                          //0~11������ �������� �̾Ƴ���.
            GameObject temp = Instantiate(TargetObject);                                //Instantiacte �Լ��� ���ؼ� �������� �����Ѵ�.
            temp.transform.position = new Vector3(-6 + RandomX, -6 + RandomY, 0);
            Destroy(temp, 1.0f);
            ChekTime = 0;
        }
        if(Input.GetMouseButtonDown(1))             //���콺 ������ ��ư�� ������ ���
        {
            Ray cast = Camera.main.ScreenPointToRay(Input.mousePosition);               //ī�޶� ȭ�� �������� ���콺 �����ǿ��� Ray�� ����

            RaycastHit hit;                                                             //�� Ray�� �޾ƿ��� ����

            if(Physics.Raycast(cast, out hit))                                          //Ray�� ����� ���� ������
            {
                Debug.Log(hit.collider.gameObject.name);                                //����� ������Ʈ �̸��� ������ش�.
                Debug.DrawLine(cast.origin, hit.point, Color.red, 2.0f);                //Ray�� �������� ǥ�����ִ� �Լ�

                if(hit.collider.gameObject.tag == "Target")                             //����� ������Ʈ�� Tag�� Target�� ���
                {
                    Point += 1;                                                         //�ı����� 1���� �ش�
                    Destroy(hit.collider.gameObject);                                   //�ش� ���� ������Ʈ�� �ı��Ѵ�
                }

            }
        }
        
    }
}