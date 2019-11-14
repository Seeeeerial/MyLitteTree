﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{   
    public static UIManager instance; // 싱글톤을 할당할 전역 변수

    // 미션 컴포넌트
    public Mission mission;
    // 동물도감 컴포넌트
    public AnimalCollection animalCollection;

    // 환경설정 패널
    public GameObject settingPanel;
    // 도전과제 패널
    public GameObject missionPanel;
    // 동물도감 패널
    public GameObject animalCollectionPanel;
    // 열매 패널
    public GameObject fruitPanel;
    // 게임 종료 패널(뒤로가기 버튼 클릭 시 사용)
    public GameObject gameEndPanel;
    // 나무 패널
    public GameObject treePanel;
    // 게임 초기화 패널
    public GameObject resetPanel;
    // 요정의 축복 주기 버튼(Button 컴포넌트와 Image 컴포넌트 둘다 접근하기 위해 GameObject로 사용)
    public GameObject blessingButton;

    // 재화 텍스트 컴포넌트
    public Text moneyText;
    // 열매 버튼(자료형을 GameObject를 사용한 이유는 Button을 사용하게 되면 SetActive 메서드를 호출할 수 없기 때문)
    public GameObject[] fruitButton;
    // 열매 패널을 오픈한 열매 버튼 index -> 열매를 심으면 현재 index에 해당하는 열매 버튼에만 열매가 심어짐
    private int index;

    // 열매 패널의 심기 버튼
    public Button[] fruitPlantingButton;
    // 열매 패널의 가격 텍스트
    public Text[] fruitInformationText;

    // 동물 오브젝트
    public GameObject[] animal = new GameObject[5];
    // 동물 활성화 여부
    public bool[] animalActive = new bool[5];

    // 축복 버튼 클릭 여부
    private bool blessingButtonClick = false;
    // fruitButtonColor Index가 더하기로 증가
    private bool fruitColorIndexPlus = true;
    // 축복 버튼을 클릭할 때 변화되는 열매 버튼 Color 저장
    private Color[] fruitButtonColor = {new Color(1f, 1f, 1f, 0.98f), new Color(1f, 1f, 1f, 0.96f), new Color(1f, 1f, 1f, 0.94f), new Color(1f, 1f, 1f, 0.92f), new Color(1f, 1f, 1f, 0.90f),
        new Color(1f, 1f, 1f, 0.88f), new Color(1f, 1f, 1f, 0.86f), new Color(1f, 1f, 1f, 0.84f), new Color(1f, 1f, 1f, 0.82f), new Color(1f, 1f, 1f, 0.80f), 
        new Color(1f, 1f, 1f, 0.78f), new Color(1f, 1f, 1f, 0.76f), new Color(1f, 1f, 1f, 0.74f), new Color(1f, 1f, 1f, 0.72f), new Color(1f, 1f, 1f, 0.70f), 
        new Color(1f, 1f, 1f, 0.68f), new Color(1f, 1f, 1f, 0.66f), new Color(1f, 1f, 1f, 0.64f), new Color(1f, 1f, 1f, 0.62f), new Color(1f, 1f, 1f, 0.60f), 
        new Color(1f, 1f, 1f, 0.58f), new Color(1f, 1f, 1f, 0.56f), new Color(1f, 1f, 1f, 0.54f), new Color(1f, 1f, 1f, 0.52f), new Color(1f, 1f, 1f, 0.50f), 
        new Color(1f, 1f, 1f, 0.48f), new Color(1f, 1f, 1f, 0.46f), new Color(1f, 1f, 1f, 0.44f), new Color(1f, 1f, 1f, 0.42f), new Color(1f, 1f, 1f, 0.40f), 
        new Color(1f, 1f, 1f, 0.38f), new Color(1f, 1f, 1f, 0.36f), new Color(1f, 1f, 1f, 0.34f), new Color(1f, 1f, 1f, 0.32f), new Color(1f, 1f, 1f, 0.30f)};
    // 열매 버튼 Color Index
    private int fruitButonColorIndex = 0;


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

    void Start() {
        // 열매 정보 텍스트 갱신
        for (int i = 0; i < fruitInformationText.Length; i++) {
            fruitInformationText[i].text = "구매 가격 : " + GameManager.instance.fruitPurchasePrice[i] + 
            "\n판매 가격 : " + GameManager.instance.fruitSellingPrice[i] + 
            "\n성장 시간 : " + GameManager.instance.fruitRemainingTime[i];
        }

        FruitPlantingButtonUpdate();

        LoadAnimalActive();
    }

    void Update() {
        if (Input.GetKey(KeyCode.Escape)) {
            PanelDeactivation();

            gameEndPanel.SetActive(true);
        }

        if (blessingButtonClick) {
            if (fruitColorIndexPlus) {
                fruitButonColorIndex++;
                if (fruitButonColorIndex == fruitButtonColor.Length - 1) {
                    fruitColorIndexPlus = false;
                }
            }
            else {
                fruitButonColorIndex--;
                if (fruitButonColorIndex == 0) {
                    fruitColorIndexPlus = true;
                }
            }

            for (int i = 0; i < fruitButton.Length; i++) {
                // 열매 버튼이 비활성화 상태이면(현재 나무 등급에서는 사용하지 않은 열매의 버튼이면 반복 중지)
                if (fruitButton[i].activeSelf == false) {
                    break;
                }

                if (fruitButton[i].GetComponent<Fruit>().GrowingFruit() == true) 
                    fruitButton[i].GetComponent<Image>().color = fruitButtonColor[fruitButonColorIndex];
            }
        }
    }

    // 재화 갱신
    public void UpdateMoney(int money) {
        moneyText.text = "" + money;
    }

    // 게임 종료 패널에서 YES 버튼 클릭시 호출
    public void OnGameEndYesButtonClick() {
        Application.Quit();
        return;
    }

    // 열매 심기 버튼 갱신
    private void FruitPlantingButtonUpdate() {
        // 소지금에따라 열매 심기 버튼 활성화/비활성화, 텍스트 변경
        for (int i = 0; i < fruitPlantingButton.Length; i++) {
            // 소지금이 열매 구매 가격보다 많으면
            if (GameManager.instance.money >= GameManager.instance.fruitPurchasePrice[i]) {
                // 버튼 활성화
                fruitPlantingButton[i].interactable = true;
                // 버튼 텍스트 변경
                fruitPlantingButton[i].GetComponentInChildren<Text>().text = "심기";
            }
            // 소지금이 열매 구매 가격보다 적으면
            else {
                // 버튼 비활성화
                fruitPlantingButton[i].interactable = false;
                // 버튼 텍스트 변경
                fruitPlantingButton[i].GetComponentInChildren<Text>().text = "재화 부족";
            }
        }
    }

    // 미션, 설정 등의 Panel이 활성화되어 있으면 모두 비활성화
    // Panel을 활성화시킬때 먼저 실행
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
        // treePanel이 활성화 되어 있으면
        if (treePanel.activeSelf == true) {
            treePanel.SetActive(false);
        }
        // gameEndPanel이 활성화 되어 있으면
        if (gameEndPanel.activeSelf == true) {
            gameEndPanel.SetActive(false);
        }
        // resetPanel이 활성화 되어 있으면
        if (resetPanel.activeSelf == true) {
            resetPanel.SetActive(false);
        }

        blessingButtonClick = false;
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
                
                OnUIButtonClick("Fruit");

                FruitPlantingButtonUpdate();
            }
        }
        // 열매가 심어져 있는 경우 -> 수확
        else if (fruit.harvestable == true) {
            // 재화 증가
            GameManager.instance.AddMoney((int)fruit.sellingPrice);

            // 필드 초기화
            fruit.Reset();

            // 수확 미션 진행도 갱신
            mission.HarvestMission();
        }
        // 열매가 성장중이고 축복 버튼이 눌려진 상태
        else if (blessingButtonClick) {
            // 축복 사용
            fruit.UseBlessing();
            // 축복 버튼을 누르지 않은 상태로 변경(구현의 간단함을 위해 축복 버튼을 클릭할 때 호출하는 메서드 사용)
            OnBlessingButtonClick();
        }
    }

    // 미션 버튼을 누르면 호출
    public void OnUIButtonClick(string panelName) {
        GameObject panel = null;

        switch (panelName) {
            case "Fruit":
                panel = fruitPanel;
                break;
            case "Mission":
                panel = missionPanel;
                break;
            case "AnimalCollection":
                panel = animalCollectionPanel;

                // 동물도감 갱신
                animalCollection.UpdateAnimalCollection(animalActive);

                break;
            case "Setting":
                panel = settingPanel;
                break;
            case "Tree":
                panel = treePanel;
                break;
            case "Reset":
                panel = resetPanel;
                break;
            default:
                // 잘못된 panelName이 온 경우
                break;
        }

        if (panel.activeSelf == false) {
            PanelDeactivation();
            panel.SetActive(true);
        }

        Scrollbar scrollbar = panel.GetComponentInChildren<Scrollbar>();

        // 패널들의 스크롤바를 초기값(좌우 -> 좌, 상하 -> 상) 으로 설정
        if (scrollbar != null) {
            // 열매 패널 스크롤바만 좌우 이동
            if (panel == fruitPanel) {
                scrollbar.value = 0;
            }
            else {
                scrollbar.value = 1;
            }
        }
    }

    // 열매 창의 X 버튼을 누르면 호출
    public void OnCloseButtonClick(string panelName) {
        switch (panelName) {
            case "Fruit":
                fruitPanel.SetActive(false);
                break;
            case "Mission":
                missionPanel.SetActive(false);
                break;
            case "AnimalCollection":
                animalCollectionPanel.SetActive(false);
                break;
            case "Setting":
                settingPanel.SetActive(false);
                break;
            case "Tree":
                treePanel.SetActive(false);
                break;
            case "GameEnd":
                gameEndPanel.SetActive(false);
                break;
            case "Reset":
                resetPanel.SetActive(false);
                break;
            default:
                // 잘못된 panelName이 온 경우
                break;
        }
    }

    // 동물 활성화 여부 불러오기
    public void LoadAnimalActive() {
        for (int i = 0; i < animalActive.Length; i++) {
            animalActive[i] = PlayerPrefs.GetInt(GameManager.instance.id + "AnimalActive" + i, 0) == 1 ? true : false;
            animal[i].SetActive(animalActive[i]);
        }
    }

    // 동물 활성화 및 저장
    // Mission의 TreeMissionReward에서 호출
    public void SaveAnimalActive(int index) {
        animalActive[index] = true;

        animal[index].SetActive(animalActive[index]);
        
        PlayerPrefs.SetInt(GameManager.instance.id + "AnimalActive" + index, animalActive[index] == true ? 1 : 0);
    }

    // 축복 버튼을 누를 때 호출
    // 축복 버튼을 누른 상태(blessingButtonClick == true)로 성장 중인 열매를 클릭할 때 호출
    public void OnBlessingButtonClick() {
        // 축복 버튼을 누름
        if (!blessingButtonClick) {
            // 소지중인 축복이 1개도 없으면
            if (GameManager.instance.blessing == 0) {
                return;
            }

            blessingButtonClick = true;

            // 축복 버튼 Color 변경
            blessingButton.GetComponent<Image>().color = new Color(0f, 1f, 1f, 1f);
        }
        // 축복 버튼을 누른 상태에서 축복 버튼을 다시 누름 || 축복을 열매에 사용
        else {
            blessingButtonClick = false;

            // 축복 버튼 Color 변경
            blessingButton.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);

            // 열매 버튼 Color 초기화
            ResetFruitButtonColor();
        }
    }

    // 열매 버튼 Color 초기화
    private void ResetFruitButtonColor() {
        for (int i = 0; i < fruitButton.Length; i++) {
            fruitButton[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
