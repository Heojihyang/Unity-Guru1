using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFire : MonoBehaviour
{
    // �ǰ� ����Ʈ ������Ʈ
    public GameObject bulletEffect;

    // �ǰ� ����Ʈ ��ƼŬ �ý���
    ParticleSystem ps;

    // �ҵ��� �߻� ȿ�� ����Ʈ 
    public GameObject sanitizerEffect;

    // �߻� ���� ���ݷ�
    public int weaponPower = 5;




    void Start()
    {
        // �ǰ� ����Ʈ ������Ʈ���� ��ƼŬ �ý��� ������Ʈ ��������
        ps = bulletEffect.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // ���콺 ���� ��ư�� ������ �ü��� �ٶ󺸴� �������� ����
        // ���콺 ���� ��ư �Է�
        if (Input.GetMouseButton(0))
        {
            // ���� ���� �� �߻�� ��ġ, ���� ���� ����
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            // ���̰� �ε��� ����� ������ ������ ���� ����
            RaycastHit hitInfo = new RaycastHit();

            // ���� �߻� �� ���� �ε��� ��ü�� ������ �ǰ� ����Ʈ ǥ��
            if (Physics.Raycast(ray, out hitInfo))
            {
                // ���̿� �ε��� ����� ���̾ 'Enemy'�� ��� ������ �Լ� ����
                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    VillainFSM vFSM = hitInfo.transform.GetComponent<VillainFSM>();
                    vFSM.HitVillain(weaponPower);
                }

                // �ǰ� ����Ʈ�� ��ġ�� ���̰� �ε��� �������� �̵�
                bulletEffect.transform.position = hitInfo.point;

                // �ǰ� ����Ʈ�� forward  ������ ���̰� �ε��� ������ ���� ���Ϳ� ��ġ
                bulletEffect.transform.forward = hitInfo.normal;

                // �ǰ� ����Ʈ�� �÷���
                ps.Play();
 
            }
            // �ҵ��� ����Ʈ �ǽ�
            StartCoroutine(San_EffectOn(0.05f));


        }

        // �ҵ��� ����Ʈ �ڷ�ƾ �Լ�
        IEnumerator San_EffectOn(float duration)
        {
            //����Ʈ ������Ʈ Ȱ��ȭ
            sanitizerEffect.SetActive(true);

            // ���� �ð���ŭ ��ٸ���
            yield return new WaitForSeconds(duration);

            // ����Ʈ ������Ʈ ��Ȱ��ȭ
            sanitizerEffect.SetActive(false); 
        }
    }
}
