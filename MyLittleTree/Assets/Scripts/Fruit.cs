using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fruit : MonoBehaviour
{
    // 미션 컴포넌트
    public Mission mission;

    // 열매 버튼
    public Button fruitButton;
    // 남은 시간 텍스트 컴포넌트
    public Text remainingTimeText;
    // 열매 버튼 이미지 컴포넌트
    private Image fruitButtonImage;
    // 열매 이미지
    public Sprite[] fruitSprite;
    // 열매 기본 이미지
    private Sprite basicSprite;


    // 심어져 있는 과일 이름
    public string fruitName;
    // 축복을 받았는지 여부
    public bool getBlessing;
    // 판매 가격
    public float sellingPrice;
    // 성장까지 남은 시간
    public float remainingTime;
    // 수확 가능 여부
    public bool harvestable;

    // 열매 버튼 식별자
    // 저장/불러오기 식별자 구분
    // 1부터 시작
    public int fruitId;

    void Start() {
        fruitButtonImage = fruitButton.GetComponent<Image>();

        basicSprite = fruitButtonImage.sprite;

        // 열매 이름 불러오기
        fruitName =  PlayerPrefs.GetString(GameManager.instance.id + "FruitName" + fruitId, "");
        // 축복을 받았는지 여부 불러오기
        getBlessing = PlayerPrefs.GetInt(GameManager.instance.id + "GetBlessing" + fruitId, 0) == 1 ? true : false;
        // 열매 판개 가격 불러오기
        sellingPrice = PlayerPrefs.GetFloat(GameManager.instance.id + "SellingPrice" + fruitId, 0);
        // 열매 성장 남은 시간 불러오기
        remainingTime = PlayerPrefs.GetFloat(GameManager.instance.id + "RemainingTime" + fruitId, 0);
        // 열매 수확 가능 정보 불러오기
        harvestable = PlayerPrefs.GetInt(GameManager.instance.id + "Harvestable" + fruitId, 0) == 1 ? true : false;

        // 열매 이미지 스프라이트 변경
        if (fruitName != "") {
            if (fruitName == "Apple") {
                fruitButtonImage.sprite = fruitSprite[0];
            }
            else if (fruitName == "Pear") {
                fruitButtonImage.sprite = fruitSprite[1];
            }
            else if (fruitName == "Persimmon") {
                fruitButtonImage.sprite = fruitSprite[2];
            }
            else if (fruitName == "Grape") {
                fruitButtonImage.sprite = fruitSprite[3];
            }
            else if (fruitName == "Hallabong") {
                fruitButtonImage.sprite = fruitSprite[4];
            }
        }

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

        // 열매 성장 남은 시간 저장
        PlayerPrefs.SetFloat(GameManager.instance.id + "RemainingTime" + fruitId, remainingTime);

        // 열매의 성장이 완료되면
        if (harvestable == false && remainingTime <= 0f) {
            harvestable = true;
            //fruitButton.interactable = true;

            // 열매 수확 가능 정보 저장
            PlayerPrefs.SetInt(GameManager.instance.id + "Harvestable" + fruitId, harvestable == true ? 1 : 0);
            
            // 남은 시간 텍스트 초기화
            remainingTimeText.text = "";
            // 열매 색상 초기화
            // GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }

        // 성장 중이면
        if (remainingTime > 0f) {
            // 남은 시간 표시
            remainingTimeText.text = (int)remainingTime + "초";
        }
    }

    // 필수 설정 함수
    // UIManager.cs에서 호출
    public void Set(string fName) {
        int idx = -1;

        fruitName = fName;
        getBlessing = false;

        if (fName == "Apple") {
            idx = 0;
        }
        else if (fName == "Pear") {
            idx = 1;
        }
        else if (fName == "Persimmon") {
            idx = 2;
        }
        else if (fName == "Grape") {
            idx = 3;
        }
        else if (fName == "Hallabong") {
            idx = 4;
        }
        else {
            Debug.Log("열매 심기 실패");
            return;
        }

        sellingPrice = GameManager.instance.fruitSellingPrice[idx];
        remainingTime = GameManager.instance.fruitRemainingTime[idx];
        // 구매 가격만큼 소지금 감소
        GameManager.instance.SubMoney(Mathf.RoundToInt(GameManager.instance.fruitPurchasePrice[idx]));
        // 열매 이미지 스프라이트 변경
        fruitButtonImage.sprite = fruitSprite[idx];

        // 열매 이름 저장
        PlayerPrefs.SetString(GameManager.instance.id + "FruitName" + fruitId, fruitName);
        // 축복을 받았는지 여부 저장
        PlayerPrefs.SetInt(GameManager.instance.id + "GetBlessing" + fruitId, getBlessing == true ? 1 : 0);
        // 열매 판매 가격 저장
        PlayerPrefs.SetFloat(GameManager.instance.id + "SellingPrice" + fruitId, sellingPrice);
        // 열매 성장 남은 시간 저장
        PlayerPrefs.SetFloat(GameManager.instance.id + "RemainingTime" + fruitId, remainingTime);
        // 열매 수확 가능 정보 저장
        PlayerPrefs.SetInt(GameManager.instance.id + "Harvestable" + fruitId, harvestable == true ? 1 : 0);
    }
 
    // 필드 초기화 함수
    public void Reset() {
        // 필드 초기화
        fruitName = "";
        getBlessing = false;
        sellingPrice = 0f;
        remainingTime = 0f;
        harvestable = false;
        //fruitButton.interactable = true;

        // 열매 이름 저장
        PlayerPrefs.SetString(GameManager.instance.id + "FruitName" + fruitId, fruitName);
        // 축복을 받았는지 여부 저장
        PlayerPrefs.SetInt(GameManager.instance.id + "GetBlessing" + fruitId, getBlessing == true ? 1 : 0);
        // 열매 판매 가격 저장
        PlayerPrefs.SetFloat(GameManager.instance.id + "SellingPrice" + fruitId, sellingPrice);
        // 열매 성장 남은 시간 저장
        PlayerPrefs.SetFloat(GameManager.instance.id + "RemainingTime" + fruitId, remainingTime);
        // 열매 수확 가능 정보 저장
        PlayerPrefs.SetInt(GameManager.instance.id + "Harvestable" + fruitId, harvestable == true ? 1 : 0);

        fruitButtonImage.sprite = basicSprite;
    }
    
    // 요정의 축복 주기(한 번에 1개만 사용 가능)
    public void UseBlessing() {
        if (getBlessing) {
            return;
        }

        getBlessing = true;

        // 축복을 받았는지 여부 저장
        PlayerPrefs.SetInt(GameManager.instance.id + "GetBlessing" + fruitId, getBlessing == true ? 1 : 0);

        // 가격 증가
        sellingPrice *= 2f;

        // 열매 판매 가격 저장
        PlayerPrefs.SetFloat(GameManager.instance.id + "SellingPrice" + fruitId, sellingPrice);

        // 축복 감소
        GameManager.instance.SubBlessing();
    }

    // 열매가 축복을 받을 수 있는지 반환
    // 심여져 있는 열매가 존재 && 축복을 받지 않음 -> true 반환
    // 열매가 심어지지 않음 || 축복을 이미 받음 -> false 반환
    public bool GiveBlessing() {
        if (fruitName != "" && getBlessing == false) {
            return true;
        }

        return false;
    }
}
