using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoopMultiple : MonoBehaviour
{
    [SerializeField] private Transform[] backgroundContainers;
    [SerializeField] private GameObject[] backgroundLayers;
    [SerializeField] private float[] movementSpeeds;
    
    private float _objectWidth;

    private void Awake()
    {

        for (int i = 0; i < backgroundLayers.Length; i++)
        {
            LoadChildObjects(backgroundLayers[i], i);
        }

    }

    /// <summary>
    /// Creates another clone of the moving background and adds it as a child to the container.
    /// </summary>
    /// <param name="obj"> The layer that needs to be duplicated</param>
    /// <param name="index"> Index in the background container</param>
    private void LoadChildObjects(GameObject obj, int index)
    {
        _objectWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x;
        
        GameObject clone = Instantiate(obj, backgroundContainers[index], true);
        Vector3 objPosition = obj.transform.position;
        clone.transform.position = new Vector3(_objectWidth, objPosition.y, objPosition.z);
        clone.name = obj.name + "_clone";
        
    }

    /// <summary>
    /// When the left layer totally is out of sight is moved to the right,
    /// and it's replaced as the last sibling
    /// </summary>
    /// <param name="container"> The container of background layers</param>
    void RepositionChildren(Transform container)
    {
        Transform firstChild = container.GetChild(0);
        Vector3 firstChildPosition = firstChild.position;
        if (firstChildPosition.x < -_objectWidth)
        {
            firstChild.position = new Vector3(_objectWidth, firstChildPosition.y, firstChildPosition.z);
            firstChild.SetAsLastSibling();
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < backgroundContainers.Length; i++)
        {
            for (int j = 1; j >= 0; j--) // Move all the layers to the left based on the given speed, starting from the right one
            {
                Transform layer = backgroundContainers[i].GetChild(j);
                layer.position += Vector3.left * (Time.deltaTime * movementSpeeds[i]);
            }
        }
    }

    private void LateUpdate()
    {
        for (int i = 0; i < backgroundContainers.Length; i++)
        {
            RepositionChildren(backgroundContainers[i]);
        }
    }
}
