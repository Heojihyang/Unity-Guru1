

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillainFSM : MonoBehaviour
{
    // ���� ���� ���
    enum VillainState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }

    // ���� ���� ����
    VillainState v_state;

    // �÷��̾� �߰� ����
    public float findDistance = 8f;

    // �÷��̾� Ʈ������
    Transform player;

    // ���� ���� ����
    public float attackDistance = 2f;

    // �̵��ӵ�
    public float moveSpeed = 5f;

    // ĳ���� ��Ʈ�ѷ� ������Ʈ
    CharacterController cc;

    // �����ð�
    float currentTime = 0;

    // ���� ������ �ð�
    float attackDelay = 2f;

    // ���� ���ݷ�
    public int attackPower = 3;

    // �ʱ� ��ġ ����� ����
    Vector3 originPos;
    Quaternion originRot;

    // �̵� ���� ����
    public float moveDistance = 20f;

    // ���� ü��
    public int hp = 15;



    void Start()
    {
        // ���� ���� ���´� idle
        v_state = VillainState.Idle;

        // �÷��̾� Ʈ������ ������Ʈ �޾ƿ���
        player = GameObject.Find("Player").transform;

        // ĳ���� �÷��̾� ������Ʈ ��������
        cc = GetComponent<CharacterController>();

        // �ʱ� ��ġ, ȸ�� �� ����
        originPos = transform.position;
        originRot = transform.rotation;

    }

    // Update is called once per frame
    void Update()
    {
        // ���� ���� üũ >> �ش� ���º��� ������ ��� ����
        switch (v_state)
        {
            case VillainState.Idle:
                Idle();
                break;
            case VillainState.Move:
                Move();
                break;
            case VillainState.Attack:
                Attack();
                break;
            case VillainState.Return:
                Return();
                break;
            case VillainState.Damaged:
                //Damaged();
                break;
            case VillainState.Die:
                //Die();
                break;
        }
    }

    void Idle()
    {
        // �÷��̾���� �Ÿ��� �߰� �������� ������� ��� >> move
        if (Vector3.Distance(transform.position, player.position) < findDistance)
        {
            v_state = VillainState.Move;
        }
    }

    void Move()
    {

        // ���� ��ġ�� �ʱ� ��ġ���� �̵� ���� ������ �Ѿ ���
        if (Vector3.Distance(transform.position, originPos) > moveDistance)
        {
            // ���� ���¸� ���ͷ� ��ȯ
            v_state = VillainState.Return;

        }

        // �÷��̾���� �Ÿ��� ���� �������� �� ��� >> �÷��̾ ���� �̵�
        else if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            // �̵� ���� ����
            Vector3 dir = (player.position - transform.position).normalized;

            // ĳ���� ��Ʈ�ѷ��� �̿��� �̵�
            cc.Move(dir * moveSpeed * Time.deltaTime);

            // �÷��̾ ���� ���� ��ȯ
            transform.forward = dir;
        }

        // �׷��� �ʴٸ� ���ݻ��·� ��ȯ
        else
        {
            v_state = VillainState.Attack;

            // ���� �ð��� ���� ������ �ð���ŭ �̸� ����
            currentTime = attackDelay;
        }
    }

    void Attack()
    {
        // �÷��̾ ���� ���� ���� ���� >> �÷��̾� ����
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            // ������ �ð����� �÷��̾� ����
            currentTime += Time.deltaTime;
            if (currentTime > attackDelay)
            {
                player.GetComponent<PlayerMove>().DamageAction(attackPower);
                currentTime = 0;
            }
        }
        // �׷��� �ʴٸ� �̵����·� ��ȯ
        else
        {
            v_state = VillainState.Move;
            currentTime = 0;
        }
    }

    void Return()
    {
        // �ʱ� ��ġ������ �Ÿ��� 0.1f �̻��̸� �ʱ� ��ġ ������ �̵�
        if (Vector3.Distance(transform.position, originPos) > 0.1f)
        {
            Vector3 dir = (originPos - transform.position).normalized;
            cc.Move(dir * moveSpeed * Time.deltaTime);

            // ������ ���� �������� ��ȯ
            transform.forward = dir;
        }
        // �׷��� �ʴٸ� �ڽ��� ��ġ�� �ʱ� ��ġ�� ���� >> ��� ���� ��ȯ
        else
        {
            // ��ġ, ȸ�� ���� �ʱ� ���·� ��ȯ
            transform.position = originPos;
            transform.rotation = originRot;

            // hp �ٽ� ȸ��

            v_state = VillainState.Idle;
        }
    }

    // ������ ���� �Լ�
    public void HitVillain(int hitPower)
    {
        // �̹� �ǰ�, ���, ���� ���¶�� �Լ� ����
        if (v_state == VillainState.Damaged || v_state == VillainState.Die || v_state == VillainState.Return)
        {
            return;
        }
        
        // �÷��̾��� ���ݷ¸�ŭ ���ʹ� ü�� ����
        hp -= hitPower;

        // ���� ü���� 0���� ũ�� �ǰ� ���·� ��ȯ
        if (hp > 0)
        {
            v_state = VillainState.Damaged;
            Damaged();
        }
        // �׷��� �ʴٸ� ���� ���·� ��ȯ
        else
        {
            v_state = VillainState.Die;
            Die();
        }

    }

    void Damaged()
    {
        // �ǰ� ���� ó�� �ڷ�ƾ ����
        StartCoroutine(DamageProcess());
    }

    // ������ ó���� �ڷ�ƾ �Լ�
    IEnumerator DamageProcess()
    {
        // �ǰ� ��Ǹ�ŭ ��ٸ���
        yield return new WaitForSeconds(0.5f);

        // ���� ���¸� �̵� ���·� ��ȯ
        v_state = VillainState.Move;
    }

    void Die()
    {
        // �������� �ǰ� �ڷ�ƾ ����
        StopAllCoroutines();

        // ���� ���� ó�� �ڷ�ƾ ����
        StartCoroutine(DieProcess());
    }

    IEnumerator DieProcess()
    {
        // ĳ���� ��Ʈ�ѷ� ������Ʈ ��Ȱ��ȭ
        cc.enabled = false;

        // 2�ʵ��� ��ٸ� �� �ڱ� �ڽ� ����
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
