using UnityEngine;
using System.Collections.Generic;

public sealed class ObjectPooler : MonoBehaviour
{
    [SerializeField] private Transform prefab;
    private Queue<Transform> objectsQueue = new Queue<Transform>();

    [SerializeField] private int quantity;

    public void CreateObjects(Transform parent = null)
    {
        for (int i = 0; i < quantity; i++)
        {
            Transform obj = Instantiate(prefab, parent);
            objectsQueue.Enqueue(obj);
            obj.gameObject.SetActive(false);
        }
    }

    public Transform ReleaseObject()
    {
        Transform pooledObj = objectsQueue.Dequeue();
        objectsQueue.Enqueue(pooledObj);
        return pooledObj;

    }
}