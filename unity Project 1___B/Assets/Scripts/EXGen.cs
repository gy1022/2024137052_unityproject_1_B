using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXGen : MonoBehaviour
{
    public GameObject item;  //���� ������Ʈ ������ ����
    public float chekTime;   //�ð��� üũ ����

    // Update is called once per frame
    void Update()
    {
        chekTime += Time.deltaTime;  //�� ������ �ð��� ���ؼ� �׾Ƽ�
        if(chekTime > 2.0f)  //2�ʰ� ������ ��
        {
            GameObject Temp = Instantiate(item);  //������Ʈ�� �����ϴ� �Լ� <Instrantiate> ,GameObject�� ���Ƿ� ������ Temp ���� ���� 
            Temp.transform.position += new Vector3(0.0f, Random.Range(0, 4), 0.0f);  //�������� 0.3�� ������ ��ġ�� �ٲ��ش�.
            Destroy(Temp, 20.0f);  //������Ʈ�� 20�� �Ŀ� �ı�
            chekTime = 0.0f;   //2�ʸ��� �������� �����ϱ� ���ؼ� �ʱ�ȭ
        }
        
    }
}
