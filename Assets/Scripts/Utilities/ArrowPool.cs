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

        //初始化对象池
        FillPool();
    }

    public void FillPool()
    {
        for(int i = 0;i<10;i++)
        {
            var newObj = Instantiate(gameObj);
            newObj.transform.SetParent(transform);

            //取消启用，返回对象池
            ReturnPool(newObj);
        }
    }

    public void ReturnPool(GameObject gameObject)
    {
        gameObject.SetActive(false);
        //发配到队列中等待使用
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
