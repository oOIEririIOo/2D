using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPool : MonoBehaviour
{
    public static ArrowPool instance;

    public GameObject gameObj;
    public int objCount;

    private Queue<GameObject> availableObjects = new Queue<GameObject>();
    private void Awake()
    {
        instance = this;

        //��ʼ�������
        FillPool();
    }

    public void FillPool()
    {
        for(int i = 0;i<10;i++)
        {
            var newObj = Instantiate(gameObj);
            newObj.transform.SetParent(transform);

            //ȡ�����ã����ض����
            ReturnPool(newObj);
        }
    }

    public void ReturnPool(GameObject gameObject)
    {
        gameObject.SetActive(false);
        //���䵽�����еȴ�ʹ��
        availableObjects.Enqueue(gameObject);
    }

    public GameObject GetFromPool()
    {
        if(availableObjects.Count == 0)
        {
            FillPool();
        }
        var outObj = availableObjects.Dequeue();
        outObj.SetActive(true);
        return outObj;
    }
}
