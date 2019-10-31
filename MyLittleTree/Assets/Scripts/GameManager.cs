using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글톤을 할당할 전역 변수

    // MyLittleTree 식별자
    private string id = "MyLittleTree";

    // 재화
    public int money;
    // 요정의 축복 소지량
    public int blessing;


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

        // 게임 데이터 불러오기
        LoadGameData();

        
    }

    void Start() {
        // 처음 실행될 때 저장되었던 재화를 불러와서 게임 창에 갱신
        UIManager.instance.moneyUpdate(money);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 재화를 얻음
    public void AddMoney(int addMoney) {
        SetMoney(money + addMoney);
    }

    // 재화를 지불
    public void SubMoney(int subMoney) {
        SetMoney(money - subMoney);
    }

    private void SetMoney(int money) {
        this.money = money;
        UIManager.instance.moneyUpdate(money);
        SaveGameData();
    }

    // 게임 정보 저장
    public void SaveGameData() {
        // 재화를 저장
        PlayerPrefs.SetInt(id + "Money", money);
        Debug.Log("재화 저장");
    }

    // 게임 정보 불러오기
    public void LoadGameData() {
        // 재화를 불러오기
        money = PlayerPrefs.GetInt(id + "Money");
        Debug.Log("재화 불러오기");
    }

    // 게임 정보 초기화
    public void ResetGameData() {
        // 모든 키 값을 제거
        PlayerPrefs.DeleteAll();
        Debug.Log("게임 초기화");
        money = 0;
        UIManager.instance.moneyUpdate(money);
    }
}
