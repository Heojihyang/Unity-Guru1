using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    //�߻��� ��ġ
    public Transform fireTransform;

    // �ǰ� ����Ʈ ���� ������Ʈ
    public GameObject bulletEffect;

    // ��ƼŬ �ý��� ����
    ParticleSystem ps;

    // �ҵ��� �߻� ȿ�� ����Ʈ 
    public ParticleSystem sanitizerEffect;

    // ���� ���ݷ�
    public int weaponPower = 5;

    //�����Ÿ�
    private float fireDistance = 50f;

    public interface IDamageable
    {
        void TakeHit(float damage, RaycastHit hit);
    }

    void Start()
    {
        // ��ƼŬ �ý��� ������Ʈ ��������
        ps = bulletEffect.GetComponent<ParticleSystem>();
    }

    public void Shot()
    {
        //�浹 ���� ����
        RaycastHit hit;

        //�Ѿ��� ���� �� ����
        Vector3 hitPosition = Vector3.zero;

        //����ĳ��Ʈ
        if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hit, fireDistance))
        {
            //���̰� �浹�� ���

            //�浹�� �������κ��� IDamageable ������Ʈ �������� �õ�
            IDamageable target = hit.collider.GetComponent<IDamageable>();

            //�� ���� ����
            if (target != null)
            {
                //HitVillain �Լ� ����
                target.TakeHit(weaponPower, hit);
            }

            //���� �浹 ��ġ ����
            hitPosition = hit.point;
        }
        else
        {
            //�浹���� �ʾҴٸ�
            //�ִ� �����Ÿ����� ���� ���� ��ġ�� �浹 ��ġ�� ���
            hitPosition = fireTransform.position + fireTransform.forward * fireDistance;
        }

        //�߻� ����Ʈ ���        
        sanitizerEffect.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //// ���콺 ���� ��ư�� ������ �ü��� �ٶ󺸴� �������� ����
        //// ���콺 ���� ��ư �Է�
        //if (Input.GetMouseButton(0))
        //{
        //    // ���� ���� �� �߻�� ��ġ, ���� ���� ����
        //    Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        //    // ���̰� �ε��� ����� ������ ������ ���� ����
        //    RaycastHit hitInfo = new RaycastHit();

        //    // ���� �߻� �� ���� �ε��� ��ü�� ������ �ǰ� ����Ʈ ǥ��
        //    if (Physics.Raycast(ray, out hitInfo))
        //    {
        //        // ���̿� �ε��� ����� ���̾ 'Enemy'�� ��� ������ �Լ� ����
        //        if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        //        {
        //            VillainFSM vFSM = hitInfo.transform.GetComponent<VillainFSM>();
        //            vFSM.HitVillain(weaponPower);
        //        }

        //        // �ǰ� ����Ʈ�� ��ġ�� ���̰� �ε��� �������� �̵�
        //        bulletEffect.transform.position = hitInfo.point;

        //        // �ǰ� ����Ʈ�� forward  ������ ���̰� �ε��� ������ ��� ���Ϳ� ��ġ
        //        bulletEffect.transform.forward = hitInfo.normal;

        //        // �ǰ� ����Ʈ�� �÷���
        //        ps.Play();

        //    }
        //    // �ҵ��� ����Ʈ �ڷ�ƾ �Լ� ����
        //    StartCoroutine(San_EffectOn(0.5f));

        Shot();
    }

    //// �ҵ��� ����Ʈ �ڷ�ƾ �Լ�
    //IEnumerator San_EffectOn(float duration)
    //{
    //    //����Ʈ ������Ʈ Ȱ��ȭ
    //    sanitizerEffect.SetActive(true);

    //    // ���� �ð���ŭ ��ٸ���
    //    yield return new WaitForSeconds(duration);

    //    // ����Ʈ ������Ʈ ��Ȱ��ȭ
    //    sanitizerEffect.SetActive(false); 
    //}
}
