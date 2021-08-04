using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class VirusFSM : MonoBehaviour
{
    // ���� ���� ���
    enum VirusState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }

    // ���̷��� ���� ����
    VirusState v_state;

    //�÷��̾� ���� ������Ʈ
    GameObject player;

    // �÷��̾� �߰� ����
    public float findDistance = 8.0f;

    // ĳ���� ��Ʈ�ѷ�
    CharacterController cc;

    // �̵��ӵ�
    public float moveSpeed = 5.0f;

    // ���� ���� ����
    public float attackDistance = 2.0f;

    // �����ð�
    float currentTime = 0;

    // ���� ������ �ð�
    float attackDelay = 2.0f;

    // ���̷��� ���ݷ�
    public int attackPower = 3;

    // �ʱ� ��ġ ����� ����
    Vector3 originPos;
    Quaternion originRot;

    // �̵� ���� ����
    public float moveDistance = 20.0f;

    // �ִ� ü��
    public int maxHp = 10;

    //���� ü�� ����
    int currentHp;

    // �����̴� ����
    public Slider hpSlider;

    //�׺���̼� �޽� ������Ʈ
    NavMeshAgent smith;

    // �ִϸ����� ������Ʈ ����
    Animator anim;

    void Start()
    {
        // �ʱ� ���� ���´� idle
        v_state = VirusState.Idle;

        // �÷��̾� �˻�
        player = GameObject.Find("Player");

        // ĳ���� ��Ʈ�ѷ� ��������
        cc = GetComponent<CharacterController>();

        // �ʱ� ��ġ, ȸ�� ����
        originPos = transform.position;
        originRot = transform.rotation;

        //���� ü�� ����
        currentHp = maxHp;

        //�׺�޽� ������Ʈ ������Ʈ ��������
        smith = GetComponent<NavMeshAgent>();
        smith.speed = moveSpeed;
        smith.stoppingDistance = attackDistance;

        // �ڽ� ������Ʈ�� �ִϸ����� ������Ʈ ��������
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ���°� ���� �� ���°� �ƴϸ� ������Ʈ �Լ� ����
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        // ���� ���� üũ >> �ش� ���º��� ������ ��� ����
        switch (v_state)
        {
            case VirusState.Idle:
                Idle();
                break;
            case VirusState.Move:
                Move();
                break;
            case VirusState.Attack:
                Attack();
                break;
            case VirusState.Return:
                Return();
                break;
            case VirusState.Damaged:
                break;
            case VirusState.Die:
                break;

        }

        // hp �����̴� ���� ü�� ���� ���� 
        hpSlider.value = (float)currentHp / (float)maxHp;
    }

    void Idle()
    {
        // �÷��̾���� �Ÿ��� �߰� �����̳� >> move
        if (Vector3.Distance(player.transform.position, transform.position) < findDistance)
        {
            //�̵� ���·� ����
            v_state = VirusState.Move;
            print("������ȯ: Idle -> Move");
            anim.SetTrigger("IdleToMove");
        }
    }

    void Move()
    {

        // ���� ��ġ�� �̵� ���� ������
        if (Vector3.Distance(originPos, transform.position) > moveDistance)
        {
            // ���� ���¸� ���ͷ� ��ȯ
            v_state = VirusState.Return;
            print("���� ��ȯ: Move -> Return");
        }

        // �÷��̾���� �Ÿ��� ���� �������� �� ��� >> �÷��̾ ���� �̵�
        else if (Vector3.Distance(player.transform.position, transform.position) > attackDistance)
        {
            //�׺�޽� ������Ʈ�� �̿��Ͽ� Ÿ�� �������� �̵�
            smith.SetDestination(player.transform.position);
            smith.stoppingDistance = attackDistance;
        }

        // ���� ���� ��
        else
        {
            //���� ���·� ����
            v_state = VirusState.Attack;
            print("���� ��ȯ: Move -> Attack");

            // ���� �ð��� ���� ������ �ð���ŭ �̸� ����
            currentTime = attackDelay;

            //�̵� ���߰�, Ÿ�� �ʱ�ȭ
            smith.isStopped = true;
            smith.ResetPath();
        }
    }

    //����
    void Attack()
    {
        // �÷��̾ ���� ���� ���� ���� >> �÷��̾� ����
        if (Vector3.Distance(player.transform.position, transform.position) <= attackDistance)
        {
            //���� ���ð��� ���� ��� �ð��� �Ѿ�ٸ�
            if (currentTime >= attackDelay)
            {
                currentTime = 0;
                //�÷��̾� ����
                print("����!");
                anim.SetTrigger("StartAttack");
            }

            //// ������ �ð����� �÷��̾� ����
            //currentTime += Time.deltaTime;
            //if (currentTime > attackDelay)
            //{
            //    player.GetComponent<PlayerController>().DamageAction(attackPower);
            //    currentTime = 0;
            //}
            else
            {
                //�ð��� ����
                currentTime += Time.deltaTime;
            }
        }
        else
        {
            //�̵� ���·� ��ȯ
            v_state = VirusState.Move;
            print("���� ��ȯ: Attack -> Move");
        }
    }

    //�÷��̾�� �������� �ִ� �Լ�
    public void HitEvent()
    {
        PlayerMove pm = player.GetComponent<PlayerMove>();
        pm.DamageAction(attackPower);
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

            v_state = VirusState.Idle;
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
        anim.SetTrigger("Damaged");
        // �ǰ� ��Ǹ�ŭ ��ٸ���
        yield return new WaitForSeconds(0.5f);

        // ���� ���¸� �̵� ���·� ��ȯ
        v_state = VirusState.Move;
    }

    void Die()
    {
        // �������� �ǰ� �ڷ�ƾ ����
        StopAllCoroutines();

        // ���� ���� ó�� �ڷ�ƾ ����
        StartCoroutine(DieProcess());

        anim.SetTrigger("Die");

        VaccineManager.instance.DropVaccineToPosition(transform.position, 1);

       

    }

    IEnumerator DieProcess()
    {
        // ĳ���� ��Ʈ�ѷ� ������Ʈ ��Ȱ��ȭ
        cc.enabled = false;

        // 2�ʵ��� ��ٸ� �� �ڱ� �ڽ� ����
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);

        
    }

    // ������ ó�� �Լ�
    public void HitVillain(int value)
    {
        // �̹� �ǰ�, ���, ���� ���¶�� �Լ� ����
        if (v_state == VirusState.Damaged || v_state == VirusState.Die || v_state == VirusState.Return)
        {
            return;
        }

        // �÷��̾��� ���ݷ¸�ŭ ���ʹ� ü�� ����
        currentHp -= value;

        // ���̷��� hp�� 0���� ũ�� 
        if (currentHp > 0)
        {
            //�ǰ� ���·� ��ȯ
            v_state = VirusState.Damaged;
            print("���� ��ȯ: Any state -> Damaged");
            Damaged();
        }
        // �׷��� �ʴٸ�
        else
        {
            //���� ���·� ��ȯ
            v_state = VirusState.Die;
            print("���� ��ȯ: Any state -> Die");
            Die();
        }
    }
}

