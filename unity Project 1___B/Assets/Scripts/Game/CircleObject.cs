using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleObject : MonoBehaviour
{
    public bool isDrag;                 //�巡�� ������ �Ǵ��ϴ� bool
    public bool isUsed;                 //���Ϸ� �Ǵ��ϴ� bool
    Rigidbody2D rigidbody2D;            //2D ��ü�� �ҷ��´�

    public int index;                   //���� ��ȣ�� �����

    public float EndTime = 0.0f;
    public SpriteRenderer SpriteRenderer;

    public GameManager gameManager;

    void Awake()
    {
        isUsed = false;                                 //��� �Ϸᰡ ���� ����9ó�� ���)
        rigidbody2D = GetComponent<Rigidbody2D>();      //��ü�� �����´�
        rigidbody2D.simulated = false;                  //������ ���� �ùķ����� ���� �ʴ´�.
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isUsed) return;                 //���Ϸ�� ��ü�� ���̻� ������Ʈ ���� �ʱ� ���ؼ� return���� �����ش�

        if(isDrag)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float leftBorder = -4.5f + transform.localScale.x / 2f;
            float rightBorder = 4.5f - transform.localScale.x / 2f;

            if (mousePos.x < leftBorder) mousePos.x = leftBorder;
            if (mousePos.x > rightBorder) mousePos.x = rightBorder;

            mousePos.y = 8;
            mousePos.z = 0;

            transform.position = Vector3.Lerp(transform.position, mousePos, 0.2f); //�� ������Ʈ�� ��ġ�� ���콺 ��ġ�� �̵��ȴ�. 0.2f �ӵ��� �̵��ȴ�
        }

        if (Input.GetMouseButtonDown(0)) Drag();                //���콺 ��ư�� ������ �� Drag �Լ� ȣ��
        if (Input.GetMouseButtonUp(0)) Drop();                  //���콺 ��ư�� ������ �� Drop �Լ� ȣ��
    }
    void Drag()
    {
        isDrag = true;                  //�巡�� ����
        rigidbody2D.simulated = false;  //�巡�� �߿��� ���� ������ �Ͼ�� ���� ���� ���ؼ�
    }
    void Drop()
    {
        isDrag = false;                 //�巡�װ� ����
        isUsed = true;                  //����� �Ϸ�
        rigidbody2D.simulated = true;   //���� ���� ����

        GameObject Temp = GameObject.FindWithTag("GameManager");
        if(Temp != null )
        {
            Temp.gameObject.GetComponent<GameManager>().GenObject();
        }
    }

    public void Used()
    {
        isDrag = false;             //�巡�װ� ����
        isUsed = true;              //����� �Ϸ�
        rigidbody2D.simulated = true;       //���� ���� ����
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "End_Line")
        {
            EndTime += Time.deltaTime;

            if(EndTime > 1)
            {
                SpriteRenderer.color = new Color(0.9f, 0.2f, 0.2f);
            }
            if(EndTime > 3)
            {
                gameManager.EndGame();
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "End_Line")
        {
            EndTime = 0.0f;
            SpriteRenderer.color = Color.white;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (index >= 7)                 //�غ�� ������ �ִ� 7��
            return;

        if (collision.gameObject.tag == "Fruit")
        { 
            CircleObject temp = collision.gameObject.GetComponent<CircleObject>();

            if(temp.index == index)
            {
                if(gameObject.GetInstanceID() > collision.gameObject.GetInstanceID())
                {
                    GameObject Temp = GameObject.FindWithTag("GameManager");
                    if (Temp != null)
                    {
                        Temp.gameObject.GetComponent<GameManager>().MergeObject(index , gameObject.transform.position);
                    }

                    Destroy(temp.gameObject);
                    Destroy(gameObject);                    
                }
            }
        }
    }
}
