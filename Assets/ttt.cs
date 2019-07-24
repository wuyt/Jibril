using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;

public class ttt : MonoBehaviour
{
    [SerializeField]
    AbstractMap _map;
    // Start is called before the first frame update
    void Start()
    {
        _map.OnInitialized += Ined;
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
