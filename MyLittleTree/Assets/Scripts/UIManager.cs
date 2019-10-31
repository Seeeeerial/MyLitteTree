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
    public GameObject fruitPanel;
    public Text moneyText;

    public Button[] fruitButton;
    public Text[] fruitButtonText;
    // 열매 버튼 밑의 남은 시간 텍스트
    public Text[] remainingTimeText;

    private int index;

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

    // 재화 갱신
    public void moneyUpdate(int money) {
        moneyText.text = "재화 : " + money;
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
        // fruitPanel이 활성화 되어 있으면
        if (fruitPanel.activeSelf == true) {
            fruitPanel.SetActive(false);
        }
    }

    // 열매를 심음
    // 매개변수로 열매의 이름을 받음
    public void OnFruitPlantingButtonClick(string fruitName) {
        Fruit fruit = fruitButton[index].GetComponent<Fruit>();

        // 열매 종류에 맞게 fruit 스크립트 필드 초기 설정
        fruit.Set(fruitName);

        // 열매 패널 닫기
        fruitPanel.SetActive(false);
    }

    // 과일 버튼을 누르면 호출
    // 매개변수로 열매 버튼의 id(식별자를 받음 -> index)
    public void OnFruitButtonClick(int idx) {
        index = idx;

        Fruit fruit = fruitButton[index].GetComponent<Fruit>();

        // 열매가 심어져 있지 않은 경우 -> 열매 선택 창 오픈
        if (fruit.fruitName == "") {
            if (fruitPanel.activeSelf == false) {
                PanelDeactivation();
                fruitPanel.SetActive(true);
            }
        }
        // 열매가 심어져 있는 경우 -> 수확
        else if (fruit.harvestable == true) {
            // 재화 증가
            GameManager.instance.AddMoney((int)fruit.sellingPrice);

            // 필드 초기화
            fruit.Reset();

        }
    }

    // 열매 창의 X 버튼을 누르면 호출
    public void OnFruitCloseButtonClick() {
        fruitPanel.SetActive(false);
    }




}
