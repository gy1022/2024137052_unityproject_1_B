using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleObject : MonoBehaviour
{
    public bool isDrag;                 //드래그 중인지 판단하는 bool
    public bool isUsed;                 //사용완료 판단하는 bool
    Rigidbody2D rigidbody2D;            //2D 강체를 불러온다

    public int index;                   //과일 번호를 만든다

    public float EndTime = 0.0f;
    public SpriteRenderer SpriteRenderer;

    public GameManager gameManager;

    void Awake()
    {
        isUsed = false;                                 //사용 완료가 되지 않음9처음 사용)
        rigidbody2D = GetComponent<Rigidbody2D>();      //강체를 가져온다
        rigidbody2D.simulated = false;                  //생성될 때는 시뮬레이팅 되지 않는다.
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
        if (isUsed) return;                 //사용완료된 물체를 더이상 업데이트 하지 않기 위해서 return으로 돌려준다

        if(isDrag)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float leftBorder = -4.5f + transform.localScale.x / 2f;
            float rightBorder = 4.5f - transform.localScale.x / 2f;

            if (mousePos.x < leftBorder) mousePos.x = leftBorder;
            if (mousePos.x > rightBorder) mousePos.x = rightBorder;

            mousePos.y = 8;
            mousePos.z = 0;

            transform.position = Vector3.Lerp(transform.position, mousePos, 0.2f); //이 오브젝트의 위치는 마우스 위치로 이동된다. 0.2f 속도로 이동된다
        }

        if (Input.GetMouseButtonDown(0)) Drag();                //마우스 버튼이 눌렸을 때 Drag 함수 호출
        if (Input.GetMouseButtonUp(0)) Drop();                  //마우스 버튼이 눌렸을 때 Drop 함수 호출
    }
    void Drag()
    {
        isDrag = true;                  //드래그 시작
        rigidbody2D.simulated = false;  //드래그 중에는 물리 현상이 일어나는 것을 막기 위해서
    }
    void Drop()
    {
        isDrag = false;                 //드래그가 종료
        isUsed = true;                  //사용이 완료
        rigidbody2D.simulated = true;   //물리 현상 시작

        GameObject Temp = GameObject.FindWithTag("GameManager");
        if(Temp != null )
        {
            Temp.gameObject.GetComponent<GameManager>().GenObject();
        }
    }

    public void Used()
    {
        isDrag = false;             //드래그가 종료
        isUsed = true;              //사용이 완료
        rigidbody2D.simulated = true;       //물리 현상 시작
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
        if (index >= 7)                 //준비된 과일이 최대 7개
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
