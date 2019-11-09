using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeManager : MonoBehaviour
{
    // 미션 컴포넌트
    public Mission mission;
    // 메인 화면의 나무 RectTransform 컴포넌트
    private RectTransform mainTreeRT;
    // 메인 화면의 나무 이미지 컴포넌트
    private Image mainTreeImage;
    // 나무 정보 패널
    public GameObject treePanel;
    // 나무 패널 안의 나무 이미지 컴포넌트
    public Image treeImage;
    // 나무 정보 텍스트 컴포넌트
    public Text treeText;
    // 나무 단계별 스프라이트
    public Sprite[] treeSprite;
    // 나무 업그레이드 버튼(자료형을 GameObject를 사용한 이유는 Button을 사용하게 되면 SetActive 메서드를 호출할 수 없기 때문)
    public GameObject treeUpgradeButton;
    // 나무 최대 등급
    private const int maxTreeGrade = 5;
    // 나무 등급(= 레벨)
    // 기본값 1
    // 범위 1~5
    public int treeGrade;
    /* 
        최대 열매 심기 가능 갯수
        업그레이드 시 심을 수 있는 열매 개수 1개씩 증가
        treeGrade = 1 -> maxFruitCount = 3
        treeGrade = 2 -> maxFruitCount = 4
        treeGrade = 3 -> maxFruitCount = 5
        treeGrade = 4 -> maxFruitCount = 6
        treeGrade = 5 -> maxFruitCount = 7
        즉, maxFruitCount = treeGrade + 2
    */
    public int maxFruitCount;

    // 나무 업그레이드 비용
    private int[] treeUpgradeCost = {0, 0, 0, 0, 0};

    void Start()
    {
        mainTreeRT = GetComponent<RectTransform>();
        mainTreeImage = GetComponent<Image>();


        // 나무 등급 불러오기
        // 저장된 데이터가 없으면 기본값 1
        treeGrade = PlayerPrefs.GetInt(GameManager.instance.id + "TreeGrade", 1);
        // 최대 열매 심기 가능 갯수 설정
        maxFruitCount = treeGrade + 2;

        TreePanelUpdate();
        TreeRelocation();
        FruitRelocation();     
    }

    void Update()
    {
        //MenuAnimation();
    }
/*
    private void MenuAnimation()        //메뉴가 커졌다 작아졌다 하면서 나오는 연출을 위한 함수
    {
        if (menuOpen && treeMenu.transform.localScale.x < 1)
            treeMenu.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        if (!(menuOpen) && treeMenu.transform.localScale.x > 0.0f)
            treeMenu.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
        if (treeMenu.transform.localScale.x <= 0 && isOn)
		{
			treeMenu.SetActive(false);
			isOn = false;
		}
            
    }
*/

    // 나무 이미지, 정보 변경
    private void TreePanelUpdate() {
        // 현재 나무 등급에 맞게 나무 패널 나무 이미지 변경
        treeImage.sprite = treeSprite[treeGrade - 1];
        // 현재 나무 등급에 맞게 나무 패널 나무 정보 변경
        string str = "나무 등급 : " + treeGrade + "\n심을 수 있는 열매 수 : " + maxFruitCount;
        if (treeGrade < 5) {
            str += "\n업그레이드 비용 : " + treeUpgradeCost[treeGrade - 1];
        }
        treeText.text = str;

        // 현재 나무 등급이 최대 등급인 경우
        if (treeGrade == maxTreeGrade) {
            treeUpgradeButton.SetActive(false);
        }
    }

    /*
        나무 재배치
        메인 화면의 나무 이미지 변경
        메인 화면의 나무 크기 변경
        메인 화면의 나무 위치 변경
    */
    private void TreeRelocation() {
        // 메인 화면의 나무 이미지 변경
        mainTreeImage.sprite = treeSprite[treeGrade - 1];

        if (treeGrade == 1) {
            // 메인 화면의 나무 크기 변경
            mainTreeRT.sizeDelta = new Vector2(200, 300);
            // 메인 화면의 나무 위치 변경
            mainTreeRT.anchoredPosition = new Vector2(0, -150);
        }
        else if (treeGrade == 2) {
            // 메인 화면의 나무 크기 변경
            mainTreeRT.sizeDelta = new Vector2(250, 375);
            // 메인 화면의 나무 위치 변경
            mainTreeRT.anchoredPosition = new Vector2(0, -112.5f);
        }
        else if (treeGrade == 3) {
            // 메인 화면의 나무 크기 변경
            mainTreeRT.sizeDelta = new Vector2(300, 450);
            // 메인 화면의 나무 위치 변경
            mainTreeRT.anchoredPosition = new Vector2(0, -75);
        }
        else if (treeGrade == 4) {
            // 메인 화면의 나무 크기 변경
            mainTreeRT.sizeDelta = new Vector2(350, 525);
            // 메인 화면의 나무 위치 변경
            mainTreeRT.anchoredPosition = new Vector2(0, -37.5f);
        }
        else if (treeGrade == 5) {
            // 메인 화면의 나무 크기 변경
            mainTreeRT.sizeDelta = new Vector2(400, 600);
            // 메인 화면의 나무 위치 변경
            mainTreeRT.anchoredPosition = new Vector2(0, 0);
        }
    }

    /*
        열매 재배치
        maxFruitCount만큼 열매 버튼 활성화
        열매 버튼 재배치
    */
    private void FruitRelocation() {
        // maxFruitCount만큼 열매 버튼 활성화(index 반대로?)
        for (int i = 0; i < maxFruitCount; i++) {
            UIManager.instance.fruitButton[i].SetActive(true);
        }

        if (treeGrade == 1) {
            // 열매 버튼 3개 재배치
            UIManager.instance.fruitButton[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -59);
            UIManager.instance.fruitButton[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(-37.5f, -134);
            UIManager.instance.fruitButton[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(37.5f, -134);
        }
        else if (treeGrade == 2) {
            // 열매 버튼 4개 재배치
            UIManager.instance.fruitButton[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(-37.5f, 0);
            UIManager.instance.fruitButton[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(52, 0);
            UIManager.instance.fruitButton[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-52, -84);
            UIManager.instance.fruitButton[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(62.5f, -84);
        }
        else if (treeGrade == 3) {
            // 열매 버튼 5개 재배치
            UIManager.instance.fruitButton[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(-52, 65);
            UIManager.instance.fruitButton[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(52, 65);
            UIManager.instance.fruitButton[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-87.5f, -38);
            UIManager.instance.fruitButton[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -21);
            UIManager.instance.fruitButton[4].GetComponent<RectTransform>().anchoredPosition = new Vector2(87.5f, -38);
        }
        else if (treeGrade == 4) {
            // 열매 버튼 6개 재배치
            UIManager.instance.fruitButton[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(-87.5f, 112.5f);
            UIManager.instance.fruitButton[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 160);
            UIManager.instance.fruitButton[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(87.5f, 112.5f);
            UIManager.instance.fruitButton[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(-112.5f, 9);
            UIManager.instance.fruitButton[4].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 47);
            UIManager.instance.fruitButton[5].GetComponent<RectTransform>().anchoredPosition = new Vector2(112.5f, 9);
        }
        else if (treeGrade == 5) {
            // 열매 버튼 7개 재배치
            UIManager.instance.fruitButton[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(-62.5f, 200);
            UIManager.instance.fruitButton[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(62.5f, 200);
            UIManager.instance.fruitButton[2].GetComponent<RectTransform>().anchoredPosition = new Vector2(-105, 105);
            UIManager.instance.fruitButton[3].GetComponent<RectTransform>().anchoredPosition = new Vector2(105, 105);
            UIManager.instance.fruitButton[4].GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, -10);
            UIManager.instance.fruitButton[5].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 50);
            UIManager.instance.fruitButton[6].GetComponent<RectTransform>().anchoredPosition = new Vector2(100, -10);
        }
    }

    // 나무 업그레이드 메서드
    private void TreeUpgrade() {
        // 업그레이드 비용만큼 재화 감소
        GameManager.instance.SubMoney(treeUpgradeCost[treeGrade - 1]);
        // 나무 등급 상승
        treeGrade++;
        // 나무 등급 저장
        PlayerPrefs.SetInt(GameManager.instance.id + "TreeGrade", treeGrade);
        // 활성화 열매 버튼 개수 변경
        maxFruitCount = treeGrade + 2;
    }

    // 나무 업그레이드 버튼을 클릭할 경우
    public void OnUpgradeButtonClick() {
        // 업그레이드 비용이 부족한 경우(업그레이드 비용이 부족해도 버튼을 비활성화하지 않을 때 사용)
        // || 나무 등급이 최고 등급인 경우
        if (!(GameManager.instance.money >= treeUpgradeCost[treeGrade - 1]) || treeGrade == maxTreeGrade) {
            return;
        }

        TreeUpgrade();
        
        TreePanelUpdate();
        TreeRelocation();
        FruitRelocation();
    }
}
