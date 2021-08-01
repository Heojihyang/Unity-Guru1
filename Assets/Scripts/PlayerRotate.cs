using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    // ȸ�� �ӵ� ����
    public float rotSpeed = 200f;

    // ȸ�� �� ����
    float mx = 0;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // ���� ���°� ���� �� ���°� �ƴϸ� ������Ʈ �Լ� ����
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        // ������� ���콺 �Է��� �޾� ��ü ȸ��
        // 1. ���콺 �Է�
        float mouse_X = Input.GetAxis("Mouse X");

        // 1-1. ȸ�� �� ������ ���콺 �Է� ����ŭ �̸� ����
        mx += mouse_X * rotSpeed * Time.deltaTime;

        // 2. ���콺 �Է� ���� �̿��� ȸ�� ���� ����
        transform.eulerAngles= new Vector3(0, mx, 0);

    }
}
