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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
