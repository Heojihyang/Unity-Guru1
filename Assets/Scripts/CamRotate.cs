using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    /*
    // ȸ�� �ӵ� ����
    public float rotSpeed = 200f;

    // ȸ�� �� ����
    float mx = 0;
    float my = 0;

    public float low_angleLimit = 30.0f;
    public float high_angleLimit = 30.0f;

    Transform player;
    */
    
    void Start()
    {
        //player=GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        // ���� ���°� ���� �� ���°� �ƴϸ� ������Ʈ �Լ� ����
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }
        */

        /*
        // ������� ���콺 �Է��� �޾� ��ü ȸ��
        // 1. ���콺 �Է�
        float mouse_X = Input.GetAxis("Mouse X");
        float mouse_Y = Input.GetAxis("Mouse Y");

        // 1-1. ȸ�� �� ������ ���콺 �Է� ����ŭ �̸� ����
        mx += mouse_X * rotSpeed * Time.deltaTime;
        my += mouse_Y * rotSpeed * Time.deltaTime;

        // 1-2. ���콺 ���� �̵� ȸ�� ����(my) �� ����
        my = Mathf.Clamp(my, -low_angleLimit, high_angleLimit);

        // 2. ���콺 �Է� ���� �̿��� ȸ�� ���� ����
        transform.eulerAngles = new Vector3(-my, mx, 0);
        */
        
        /*
        Vector3 pl = (player.transform.position - transform.position).normalized;
        transform.forward = pl;

        */
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
