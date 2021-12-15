using System.Collections.Generic;
using UnityEngine;

public abstract class PoolGenerator
{
    protected GameObject prefab;
    
    /// <summary>
    /// Initializes the generator.
    /// </summary>
    public void Init(){}

    /// <summary>
    /// Fills the enemies object pool for later usage.
    /// </summary>
    /// <param name="idleObjects"> The list that keeps spawned objects.</param>
    /// <param name="objectsPool"> The parent object that is used as the pool.</param>
    /// <param name="objectsCount"> Number of objects that are going to be in the pool.</param>
    /// <param name="objectName"> The name of the spawned objects.</param>
    public void GenerateObjects(List<GameObject> idleObjects, GameObject objectsPool, int objectsCount, string objectName)
    {
        for (int i = 0; i < objectsCount; i++)
        {
            GameObject obj = Object.Instantiate(prefab, 
                new Vector3(Statics.DefaultPoolPositionX, Statics.PoolVerticalOffset, 0), Quaternion.identity);
            obj.name = objectName + i;
            obj.transform.SetParent(objectsPool.transform);
            if (obj.GetComponent<Rigidbody2D>() != null)
            {
                obj.GetComponent<Rigidbody2D>().gravityScale = 0f;
            }
            idleObjects.Add(obj);
        }

    }

}