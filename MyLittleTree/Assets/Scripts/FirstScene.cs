using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstScene : MonoBehaviour
{
    // MyLittleTree 식별자
    public string id = "MyLittleTree";

    private bool CheckGameData() {
        if(PlayerPrefs.HasKey(id + "Money")) {
            Debug.Log("재화 존재");
            return true;
        }
        else {
            // 모든 키 값을 제거({계절 변화 주기}, {축복 생성 주기}, {2번째 열매 남은 시간} 데이터 삭제가 안되는 버그 때문에 여기에 추가로 사용)
            PlayerPrefs.DeleteAll();
        }
        if(PlayerPrefs.HasKey(id + "LastGenerateBlessingTime")) {
            Debug.Log("축복 주기 존재");
            return true;
        }
        if(PlayerPrefs.HasKey(id + "NextSeasonRemainingTime")) {
            Debug.Log("계절 주기 존재");
            return true;
        }
        for (int i = 0; i < 10; i++) {
            if(PlayerPrefs.HasKey(id + "FruitName" + i)) {
                Debug.Log("열매 이름 " + i + "존재");
                return true;
            }
            if(PlayerPrefs.HasKey(id + "BlessingCount" + i)) {
                Debug.Log("열매 축복 횟수 " + i + "존재");
                return true;
            }
            if(PlayerPrefs.HasKey(id + "SellingPrice" + i)) {
                Debug.Log("열매 판매가 " + i + "존재");
                return true;
            }
            if(PlayerPrefs.HasKey(id + "RemainingTime" + i)) {
                Debug.Log("열매 성장 남은 시간 " + i + "존재");
                return true;
            }
            if(PlayerPrefs.HasKey(id + "Harvestable" + i)) {
                Debug.Log("열매 수확 가능 " + i + "존재");
                return true;
            }
        }
        if(PlayerPrefs.HasKey(id + "Blessing")) {
            Debug.Log("축복 갯수 존재");
            return true;
        }
        if(PlayerPrefs.HasKey(id + "SeasonIndex")) {
            Debug.Log("계절 Index 존재");
            return true;
        }
        if(PlayerPrefs.HasKey(id + "FruitHarvestCount")) {
            Debug.Log("미션 열매 수확 횟수 존재");
            return true;
        }
        for (int i = 0; i < 10; i++) {
            if(PlayerPrefs.HasKey(id + "FruitAchievement" + i)) {
                Debug.Log("열매 미션 성공 여부 존재 " + i);
                return true;
            }
            if(PlayerPrefs.HasKey(id + "TreeAchievement" + i)) {
                Debug.Log("나무 미션 성공 여부 존재 " + i);
                return true;
            }
            if(PlayerPrefs.HasKey(id + "GetReward" + i)) {
                Debug.Log("보상 받기 여부 존재" + i);
                return true;
            }
        }

        if(PlayerPrefs.HasKey(id + "BgmOn")) {
            Debug.Log("배경음악 ON/OFF 존재");
            return true;
        }
        if(PlayerPrefs.HasKey(id + "SoundEffectOn")) {
            Debug.Log("효과음 ON/OFF 존재");
            return true;
        }
        if(PlayerPrefs.HasKey(id + "TreeGrade")) {
            Debug.Log("나무 등급 존재");
            return true;
        }

        return false;
    }

    // 게임 화면 클릭 시 호출
    public void OnScreenClick() {
        if (CheckGameData()) {
            // 메인 씬 이동
        }
        else {
            // 튜토리얼 씬 이동
        }

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
