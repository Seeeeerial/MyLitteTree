using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstScene : MonoBehaviour
{
    // 게임 식별자
    private string id = "MyLittleTree";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 게임 화면 클릭 시 호출
    public void OnScreenClick() {
        // 나중에 게임 데이터가 없으면 튜토리얼 씬 이동으로 변경

        LoadMainScene();
    }

    // 튜토리얼 씬 이동
    public void LoadTutorialScene() {
        SceneManager.LoadScene("TutorialScene");
    }

    // 메인 씬 이동
    public void LoadMainScene() {
        SceneManager.LoadScene("MainScene");
    }
}
