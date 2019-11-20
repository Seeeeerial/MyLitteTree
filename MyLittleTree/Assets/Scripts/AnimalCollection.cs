using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalCollection : MonoBehaviour
{
    // 동물 이미지 컴포넌트
    public Image[] animalImage = new Image[5];
    // 동물 정보가 들어가는 오브젝트
    public Text[] animalInpormationText = new Text[5];

    // 동물 스프라이트
    public Sprite[] animalSprite = new Sprite[5];
    // 동물 정보
    private string[] animalInpormation = {"이름 : 개\n특징 : 개 특징", 
    "이름 : 고양이\n특징 : 고양이 특징", 
    "이름 : 돼지\n특징 : 돼지 특징", 
    "이름 : 거위\n특징 : 거위 특징", 
    "이름 : 기니피그\n특징 : 기니피그 특징"};
    // 동물이 오픈되지 않았을 때 사용할 ? 이미지
    public Sprite unopenedSprite;


    // 동물 도감 창 갱신
    // UIManager의 OnUIButtonClick에서 동물도감 패널을 활성화할 때 호출
    public void UpdateAnimalCollection(bool[] haveAnimal) {
        for (int i = 0; i < haveAnimal.Length; i++) {
            if (haveAnimal[i] == true) {
                animalImage[i].sprite = animalSprite[i];
                animalInpormationText[i].text = animalInpormation[i];
            }
            else {
                animalImage[i].sprite = unopenedSprite;
                animalInpormationText[i].text = "소유하지 않음";
            }
        }
    }
}
