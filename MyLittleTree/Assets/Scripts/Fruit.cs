﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fruit : MonoBehaviour
{
    // 열매 버튼
    public Button fruitButton;
    // 남은 시간 텍스트 컴포넌트
    public Text remainingTimeText;
    // 열매 버튼 이미지 컴포넌트
    private Image fruitImage;
    // 열매 이미지
    public Sprite[] fruitSprite;
    private Sprite basicImage;


    // 심어져 있는 과일 이름
    public string fruitName;
    // 축복받은 횟수
    public int blessingCount;
    // 판매 가격
    public float sellingPrice;
    // 성장까지 남은 시간
    public float remainingTime;
    // 수확 가능 여부
    public bool harvestable;

    // 사과 정보
    public const float applePurchasePrice = 0f;
    public const float appleSellingPrice = 300f;
    public const float appleRemainingTime = 10f;

    // 감 정보
    public const float persimmon = 0f;

    // 배 정보
    public const float pear = 0f;

    // 포도 정보
    public const float grape = 0f;

    // 한라봉 정보
    public const float hallabong = 0f;

    void Start() {
        fruitImage = fruitButton.GetComponent<Image>();

        basicImage = fruitImage.sprite;

        fruitName = "";
        blessingCount = 0;
        sellingPrice = 0f;
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
        if (harvestable == false && remainingTime <= 0f) {
            harvestable = true;
            //fruitButton.interactable = true;
            
            // 남은 시간 텍스트 초기화
            remainingTimeText.text = "";
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
        if (fName == "Apple") {
            fruitName = fName;
            blessingCount = 0;
            sellingPrice = appleSellingPrice;
            remainingTime = appleRemainingTime;
            // 구매가격만큼 소지금 감소
            // GameManager.instance.SubMoney(applePurchasePrice); -> 0원 이므로 실행하지 않음

            fruitImage.sprite = fruitSprite[0];
        }
        else if (fName == "Orange") {

        }

        // 열매 버튼 비활성화
        //fruitButton.interactable = false;
    }
 
    // 필드 초기화 함수
    public void Reset() {
        // 필드 초기화
        fruitName = "";
        blessingCount = 0;
        sellingPrice = 0f;
        remainingTime = 0f;
        harvestable = false;
        //fruitButton.interactable = true;


        fruitImage.sprite = basicImage;
    }
    
    // 요정의 축복 주기
    public void UseBlessing(int num) {
        blessingCount += num;

        // 가격 증가
        sellingPrice *= 1.3f;

        // 축복 감소
        GameManager.instance.SubBlessing();
    }

}
