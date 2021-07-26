using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{

    // ȸ�� �ӵ� ����
    public float rotSpeed = 200f;

    // ȸ�� �� ����
    float mx = 0;
    float my = 0;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ������� ���콺 �Է��� �޾� ��ü ȸ��
        // 1. ���콺 �Է�
        float mouse_X = Input.GetAxis("Mouse X");
        float mouse_Y = Input.GetAxis("Mouse Y");

        // 1-1. ȸ�� �� ������ ���콺 �Է� ����ŭ �̸� ����
        mx += mouse_X * rotSpeed * Time.deltaTime;
        my += mouse_Y * rotSpeed * Time.deltaTime;

        // 1-2. ���콺 ���� �̵� ȸ�� ����(my) ���� -90 ~ 90�� ���̷� ����
        my = Mathf.Clamp(my, -90f, 90f);

        // 2. ���콺 �Է� ���� �̿��� ȸ�� ���� ����
        transform.eulerAngles = new Vector3(-my, mx, 0);

        /*
        // 3. ȸ�� �������� ��ü ȸ��
        // r = r0 + vt
        transform.eulerAngles += dir * rotSpeed * Time.deltaTime;

        // 4. x �� ȸ��(���� ȸ��) ���� -90 ~ 90�� ���̷� ����
        Vector3 rot = transform.eulerAngles;
        rot.x = Mathf.Clamp(rot.x, -90f, 90f);
        transform.eulerAngles = rot;
        */


    }
}