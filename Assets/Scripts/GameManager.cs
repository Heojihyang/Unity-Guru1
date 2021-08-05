using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    // �ɼ� �޴� UI ������Ʈ
    public GameObject optionUI;

    // ���� ���� ���
    public GameObject gameOver;

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
        if (GameObject.Find("Player").GetComponent<PlayerController>().hp <= 0) // ��𿣵��� ����
        {
            gameOver.SetActive(true);
            GameObject.Find("BadEndingIntro").GetComponent<FadeController>().Fade();
        }


        if (GetComponent<ScoringSystem>().score == 10) // ���ǿ����� ����
        {
            GameObject.Find("HappyEndingIntro").GetComponent<FadeController>().Fade();

        }
    }

    // �ɼ� �޴� �ѱ�
    public void OpenOptionWindow()
    {
        // ���� ���¸� pause�� ����
        gState = GameState.Pause;

        // �ð� ����
        Time.timeScale = 0;

        // �ɼ� �޴� â Ȱ��ȭ
        optionUI.SetActive(true);
    }

    // �ɼ� �޴� ����(����ϱ�)
    public void CloseOptionWindow()
    {
        // ���� ���¸� run ���·� ����
        gState = GameState.Run;

        // �ð��� 1��� �ǵ���
        Time.timeScale = 1.0f;

        // �ɼ� �޴� â ��Ȱ��ȭ
        optionUI.SetActive(false);
    }

    // ���� �����(���� �� �ٽ� �ε�)
    public void GameRestart()
    {
        // �ð��� 1��� �ǵ���
        Time.timeScale = 1.0f;

        // ���� ���� �ٽ� �ε�
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // ���� ����
    public void GameQuit()
    {
        // ���ø����̼� ����
        Application.Quit();
    }
}
