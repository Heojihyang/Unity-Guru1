using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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

    // ���� ���ݷ�
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

    //�ִϸ����� ������Ʈ ����
    Animator anim;

    //�ǰݽ� ����
    AudioSource audio;
    public AudioClip audioDamaged;

    void Start()
    {
        // �ʱ� ���� ���´� idle
        v_state = VillainState.Idle;

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

        //�ڽĿ�����Ʈ�� �ִϸ��̼� ������Ʈ����������
        anim = GetComponentInChildren<Animator>();

        this.audio = GetComponent<AudioSource>();
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
                break;
            case VillainState.Die:
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
            v_state = VillainState.Move;
            print("Villain ������ȯ: Idle -> Move");

            anim.SetTrigger("IdletoMove");
        }
    }

    void Move()
    {

        // ���� ��ġ�� �̵� ���� ������
        if (Vector3.Distance(originPos, transform.position) > moveDistance)
        {
            // ���� ���¸� ���ͷ� ��ȯ
            v_state = VillainState.Return;
            print("Villain ���� ��ȯ: Move -> Return");
        }

        // �÷��̾���� �Ÿ��� ���� �������� �� ��� >> �÷��̾ ���� �̵�
        else if (Vector3.Distance(player.transform.position, transform.position) > attackDistance)
        {
            //�̵����� ���Ѵ�
            Vector3 dir = (player.transform.position - transform.position).normalized;

            //���� ���� ������ �̵������ ��ġ��Ų��
            transform.forward = dir;

            //�׺�޽� ������Ʈ�� �̿��Ͽ� Ÿ�� �������� �̵�
            smith.SetDestination(player.transform.position);
            smith.stoppingDistance = attackDistance;
        }

        // ���� ���� ��
        else
        {
            //���� ���·� ����
            v_state = VillainState.Attack;
            print("Villain ���� ��ȯ: Move -> Attack");

            //�ִϸ��̼� ȣ��
            anim.SetTrigger("MovetoAttackDelay");

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
                print("Villain�� ����!");
                HitEvent();

                anim.SetTrigger("StartAttack");
                audio.clip = audioDamaged;

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
            v_state = VillainState.Move;
            print("Villain ���� ��ȯ: Attack -> Move");

            anim.SetTrigger("AttacktoMove");
        }
    }

    //�÷��̾�� �������� �ִ� �Լ�
    public void HitEvent()
    {
        PlayerController pc = player.GetComponent<PlayerController>();
        pc.DamageAction(attackPower);
    }

    void Return()
    {

        Vector3 dist = originPos - transform.position;
        dist.y = 0;

        // �ʱ� ��ġ������ �Ÿ��� 0.1f �ʰ��� �ʱ� ��ġ ������ �̵�
        //if (Vector3.Distance(transform.position, originPos) > 1.0f)
        if (dist.magnitude > 0.9f)
        {
            //print("return");
            //Vector3 dir = (originPos - transform.position).normalized;
            Vector3 dir = dist.normalized;
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

            //��� ���·� ��ȯ
            v_state = VillainState.Idle;
            print("Villain ���� ��ȯ: Return -> Idle");
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

    // ������ ó�� �Լ�
    public void HitVillain(int value)
    {
        // �̹� �ǰ�, ���, ���� ���¶�� �Լ� ����
        if (v_state == VillainState.Damaged || v_state == VillainState.Die || v_state == VillainState.Return)
        {
            return;
        }

        // �÷��̾��� ���ݷ¸�ŭ ���ʹ� ü�� ����
       currentHp -= value;

        // ���� hp�� 0���� ũ�� 
        if (currentHp > 0)
        {
            //�ǰ� ���·� ��ȯ
            v_state = VillainState.Damaged;
            print("Villain ���� ��ȯ: Any state -> Damaged");
            Damaged();
        }
        // �׷��� �ʴٸ�
        else
        {
            //���� ���·� ��ȯ
            v_state = VillainState.Die;
            print("Villain ���� ��ȯ: Any state -> Die");
            Die();
        }
    }
}
