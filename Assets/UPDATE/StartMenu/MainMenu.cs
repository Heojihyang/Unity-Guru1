using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // ���� ��ư�� ������ ���� ����
    public void Quitbutton()
    {
        Application.Quit();
    }

    // ���� ��ư�� ������ ���� ������ �̵�
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
