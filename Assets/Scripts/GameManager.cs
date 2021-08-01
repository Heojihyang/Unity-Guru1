using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //���� ���� ����
    public enum GameState
    {
        Ready,
        Run,
        Pause,
        GameOver
    }

    //���� ���� ����
    public GameState gState;

    // �ؽ�Ʈ ����
    public Text stateLable;

    //�÷��̾� ���� ������Ʈ ����

    //GameObject player;

    //�÷��̾� ���� ������Ʈ ����
    //PlayerController playerC;

    //�̱���
    public static GameManager gm;

    private void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // ���� �ʱ� ���� = �غ���� 
        gState = GameState.Ready;

        //������ ���¸� ���� ���·� ����
        //gState = GameState.Run;

        // ���� ���� �ڷ�ƾ �Լ� ����
        StartCoroutine(GameStart());

        //�÷��̾� ������Ʈ�� �˻�
        //player = GameObject.Find("Player");

        //playerC = player.GetComponent<PlayerController>();
    }

    IEnumerator GameStart()
    {
        // Ready ���� ǥ��
        stateLable.text = "Ready";

        // Ready ���� ���� : ��Ȳ��
        stateLable.color = new Color32(234, 182, 13, 255);

        // 2�� ���
        yield return new WaitForSeconds(2.0f);

        // Go ������ ����
        stateLable.text = "Go!";

        // 0.5�ʰ� ���
        yield return new WaitForSeconds(0.5f);

        // Go ���� ����
        stateLable.text = "";

        // ���� ���� ��ȯ : �غ� -> ���� 
        gState = GameState.Run;
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
