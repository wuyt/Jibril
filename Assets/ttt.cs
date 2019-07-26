using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;

public class ttt : MonoBehaviour
{
    private Vector2[] randomPlace;

    void Start()
    {
        Debug.Log(randomPlace);

        for(int i = 0; i < 5; i++)
        {
            Debug.Log(Random.insideUnitCircle * 5);
        }




    }

    void Ined()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("aa");


            int layerMask = 1 << 8;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Physics.Raycast(ray, out RaycastHit hitInfo, 1000f, 8);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000f, layerMask))
            {
                Debug.Log(hitInfo.transform.name);
                //raycastHit.point 为点击的物体的具体位置
            }
        }
    }
}
