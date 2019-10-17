using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{   
    public static UIManager instance; // 싱글톤을 할당할 전역 변수
    public GameObject settingPanel;
    public GameObject missionPanel;
    public GameObject animalCollectionPanel;

    // 게임 시작과 동시에 싱글톤을 구성
    void Awake() {
        // 싱글톤 변수 instance가 비어있는가?
        if (instance == null)
        {
            // instance가 비어있다면(null) 그곳에 자기 자신을 할당
            instance = this;
        }
        else
        {
            // instance에 이미 다른 GameManager 오브젝트가 할당되어 있는 경우

            // 씬에 두개 이상의 GameManager 오브젝트가 존재한다는 의미.
            // 싱글톤 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            Debug.LogWarning("씬에 두개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
        }
    }

    void Update() {
        // 날씨
        // 시간
    }

    // 미션, 설정 등의 Panel이 활성화되어 있으면 비활성화
    // Panel을 활성화시킬때 실행
    public void PanelDeactivation() {
        // settingPanel이 활성화되어 있으면
        if (settingPanel.activeSelf == true) {
            settingPanel.SetActive(false);
        }
        // missionPanel이 활성화되어 있으면
        if (missionPanel.activeSelf == true) {
            missionPanel.SetActive(false);
        }
        // animalCollectionPanel이 활성화되어 있으면
        if (animalCollectionPanel.activeSelf == true) {
            animalCollectionPanel.SetActive(false);
        }
    }
}
