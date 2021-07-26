using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //�߷� ����
    public float gravity = -20.0f;

    //������
    public float jumpPower = 10.0f;

    //�ִ� ���� Ƚ��
    public int maxJump = 2;

    //���� ���� Ƚ��
    int jumpCount = 0;

    //���� �ӵ� ����
    float yVelocity = 0;

    //�ӷ� ����
    public float moveSpeed = 7.0f;

    //���� ĳ���� ��Ʈ�ѷ�
    CharacterController cc;

    //ü�� ����
    public int hp;

    //�ִ� ü��
    public int maxHp = 10;

    // Start is called before the first frame update
    void Start()
    {
        //ĳ���� ��Ʈ�ѷ� ������Ʈ �ޱ�
        cc = GetComponent<CharacterController>();

        //ü�º��� �ʱ�ȭ
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
