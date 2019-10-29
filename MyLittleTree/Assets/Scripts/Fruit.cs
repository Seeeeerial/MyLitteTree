using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fruit : MonoBehaviour
{
    public GameObject fruitPanel;

    public Button fruitButton;
    public Text fruitButtonText;
    // 열매 버튼 밑의 남은 시간 텍스트
    public Text remainingTimeText;


    // 심어져 있는 과일 이름
    public string fruitName;
    // 축복받은 횟수
    public int blessingCount;
    // 판매 가격
    public float price;
    // 성장까지 남은 시간
    public float remainingTime;
    // 수확 가능 여부
    public bool harvestable;

    public float applePrice = 300f;
    public float appleRemainingTime = 10f;
    
    void Start() {
        fruitName = "";
        blessingCount = 0;
        price = 0f;
        remainingTime = 0f;
        harvestable = false;
        fruitButton.interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        // 심어진 열매가 없는 경우 || 열매가 성장이 다 된 경우
        if (fruitName == "" || harvestable == true) {
            return;
        }


        // 시간이 흐름
        remainingTime -= Time.deltaTime;

        // 열매의 성장이 완료되면
        if (remainingTime <= 0f) {
            harvestable = true;
            fruitButton.interactable = true;
            
            // remainingTimeText의 텍스트 초기화
            remainingTimeText.text = "";
        }

        // 성장 중이면
        if (remainingTime > 0f) {
            // 남은 시간 표시
            remainingTimeText.text = (int)remainingTime + "초";
        }
        
    }
/* 
    // 필드 초기화 함수
    private void Reset() {
        // 필드 초기화
        fruitName = "";
        blessingCount = 0;
        price = 0f;
        remainingTime = 0f;
        harvestable = false;
        fruitButton.interactable = true;
    }
 */   
    // 요정의 축복 주기
    public void AddBlessing(int num) {
        blessingCount += num;

        // 가격 증가
        price *= 1.3f;
    }

    // 열매를 심음
    // 매개변수로 열매의 이름을 전달
    public void OnFruitPlantingButtonClick(string name) {
        fruitName = name;
        // 과일 버튼 비활성화
        fruitButton.interactable = false;
        // 열매 패널 닫기
        fruitPanel.SetActive(false);

        FruitClassification();
    }

    // 열매 이름으로 열매 관련 변수 설정
    private void FruitClassification() {
        if (fruitName == "Apple") {
            price = applePrice;
            remainingTime = appleRemainingTime;
            fruitButtonText.text = "Apple";
        }
    }

    // 과일 버튼을 누르면 호출
    public void OnFruitButtonClick() {
        // 열매가 심어져 있지 않은 경우 -> 열매 선택 창 오픈
        if (fruitName == "") {
            if (fruitPanel.activeSelf == false) {
                UIManager.instance.PanelDeactivation();
                fruitPanel.SetActive(true);
            }
        }
        // 열매가 심어져 있는 경우 -> 수확
        else if (harvestable == true) {
            // 재화 증가
            GameManager.instance.AddMoney((int)price);

            // 필드 초기화
            fruitName = "";
            blessingCount = 0;
            price = 0f;
            remainingTime = 0f;
            harvestable = false;
            fruitButton.interactable = true;

            // 열매 텍스트 갱신
            fruitButtonText.text = fruitName;
        }
    }

    // X 버튼을 누르면 호출
    public void OnCloseButtonClick() {
        fruitPanel.SetActive(false);
    }
}
