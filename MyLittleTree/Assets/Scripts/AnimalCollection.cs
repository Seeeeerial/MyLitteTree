using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalCollection : MonoBehaviour
{
    // 동물 이미지 컴포넌트
    public Image[] image;
    // 동물 정보가 들어가는 오브젝트
    public Text[] impormationText;
    // 동물 획득 여부
    private bool[] haveAnimal;

    // 동물 이미지
    public Sprite[] animalSprite;
    // 동물 정보
    private string[] animalImpormation;
    // 동물이 오픈되지 않았을 때 사용할 ? 이미지
    public Sprite unopenedImage;


    // Start is called before the first frame update
    void Start()
    {
        // 동물 획득 여부 불러오기
    }


    // 동물 도감 창 갱신
    private void UpdateAnimalCollection() {

    }

    // 동물을 얻음
    public void GetAnimal(string animalName) {
        // 동물 추가

        // 갱신
        UpdateAnimalCollection();
    }

    // 동물 index 반환
    private int GetAnimalIdx(string animalName) {
        switch (animalName) {
            case "Dog":
                return 0;

        }

        return -1;
    }
}
