using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글톤을 할당할 전역 변수

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
    }

}
