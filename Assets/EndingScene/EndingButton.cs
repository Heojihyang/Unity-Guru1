using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingButton : MonoBehaviour
{
    // ���� ��ư�� ������ ���� ����
    public void Quitbutton()
    {
        Application.Quit();
    }

    // ó������ ��ư�� ������ ó�� �޴� ������ �̵�
    public void Restartbutton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
