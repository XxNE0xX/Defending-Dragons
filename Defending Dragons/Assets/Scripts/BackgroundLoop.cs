using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    [SerializeField] private Transform backgroundContainer;
    [SerializeField] private GameObject backgroundLayer;
    [SerializeField] private float movementSpeed;
    
    private float _objectWidth;

    private void Awake()
    {
        LoadChildObjects(backgroundLayer);
    }

    /// <summary>
    /// Creates another clone of the moving background and adds it as a child to the container.
    /// </summary>
    /// <param name="obj"> The layer that needs to be duplicated</param>
    private void LoadChildObjects(GameObject obj)
    {
        _objectWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x;
        
        GameObject clone = Instantiate(obj, backgroundContainer, true);
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
        for (int i = 1; i >= 0; i--) // Move all the layers to the left based on the given speed, starting from the right one
        {
            Transform layer = backgroundContainer.GetChild(i);
            layer.position += Vector3.left * (Time.deltaTime * movementSpeed);
        }
    }

    private void LateUpdate()
    {
        RepositionChildren(backgroundContainer);
    }
}
