using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0;

            DamagePopup.Create(mouseWorldPosition, 
                Random.Range(Statics.MinEnemyDamage, Statics.MaxEnemyDamage), true);
        }
    }
}