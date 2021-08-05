using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    int hp;


    //�ִ� ü��
    public int maxHp = 10;

    // �����̴� UI
    public Slider hpSlider;

    //ȸ�� ����
    public float rotSpeed = 200.0f;

    // ����Ʈ UI ������Ʈ
    public GameObject hitEffect;


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
        // ���� ���°� ���� �� ���°� �ƴϸ� ������Ʈ �Լ� ����
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }


        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //�̵����� ����
        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();

        //�̵� ����(���� ��ǥ)�� ī�޶��� ���� �������� (���� ��ǥ) ��ȯ
        //dir = Camera.main.transform.TransformDirection(dir);

        if(!(h==0&&v==0))
        {
            //�̵��������� �÷��̾� �̵�
            //P = P0 + VT
            //transform.position += dir * moveSpeed * Time.deltaTime;
            cc.Move(dir * moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotSpeed);
            

        }

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


        // �����̴� value�� ü�� ������ ����
        hpSlider.value = (float)hp / (float)maxHp;

        //�̵��������� �÷��̾� �̵�
        //P = P0 + VT
        //transform.position += dir * moveSpeed * Time.deltaTime;
        //cc.Move(dir * moveSpeed * Time.deltaTime);

    }



    // �÷��̾� �ǰ� �Լ�
    public void DamageAction(int damage)
    {
        // ���ʹ��� ���ݷ¸�ŭ �÷��̾��� ü�� ����
        hp -= damage;
        if(hp < 0)
        {
            hp = 0;
        }
        // hp�� 0���� ū ��쿡�� ȭ���� �Ӿ����� ����Ʈ �ڷ�ƾ�� ����
        else
        {
            StartCoroutine(HitEffect());
        }
    }

    IEnumerator HitEffect()
    {
        // 1. ����Ʈ�� �Ҵ�(Ȱ��ȭ ��Ų��)
        hitEffect.SetActive(true);

        // 2. 0.3�� ��ٸ���
        yield return new WaitForSeconds(0.3f);

        // 3. ����Ʈ�� ����(��Ȱ��ȭ)
        hitEffect.SetActive(false);
    }
}
