using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //�÷��̾� ���� ������Ʈ ����
    GameObject player;

    //�÷��̾� ���� ������Ʈ ����
    PlayerMove playerM;

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
        //������ ���¸� ���� ���·� ����
        gState = GameState.Run;

        //�÷��̾� ������Ʈ�� �˻�
        player = GameObject.Find("Player");

        playerM = player.GetComponent<PlayerMove>();
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
