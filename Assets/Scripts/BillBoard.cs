using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    // ���� ī�޶��� �������, ���� ������� ��ġ 


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ī�޶��� �������, ���� ������� ��ġ 
        transform.forward = Camera.main.transform.forward;
    }
}
