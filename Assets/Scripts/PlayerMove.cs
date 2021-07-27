using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //�߷� ����
    public float gravity = -20.0f;

    //������
    public float jumpPower = 4.0f;

    //�ִ� ���� Ƚ��
    public int maxJump = 6;

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
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //�̵����� ����
        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();

        //�̵� ����(���� ��ǥ)�� ī�޶��� ���� �������� (���� ��ǥ) ��ȯ
        dir = Camera.main.transform.TransformDirection(dir);

        //�÷��̾ ���� ������ ���� ���� Ƚ���� 0���� �ʱ�ȭ
        //���� �ӵ� ��(�߷�)�� �ٽ� 0���� �ʱ�ȭ
        if (cc.collisionFlags == CollisionFlags.Below)
        {
            jumpCount = 0;
            yVelocity = 0;
        }

        //���� Ű�� ���� ��, �������� ���� �ӵ��� ����
        //��, ���� ���� Ƚ���� �ִ� ���� Ƚ���� �Ѿ�� �ʾƾ� ��
        if (Input.GetButtonDown("Jump") && jumpCount < maxJump)
        {
            jumpCount++;
            yVelocity = jumpPower;
        }        

        //ĳ������ �����ӵ�(�߷�)�� ����
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        //�̵��������� �÷��̾� �̵�
        //P = P0 + VT
        //transform.position += dir * moveSpeed * Time.deltaTime;
        cc.Move(dir * moveSpeed * Time.deltaTime);
    }
}
