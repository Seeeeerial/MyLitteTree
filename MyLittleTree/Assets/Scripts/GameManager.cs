using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글톤을 할당할 전역 변수

    // 트리 매니저 컴포넌트
    public TreeManager treeManager;

    // 계절 이미지 컴포넌트
    public Image seasonImage;
    // 계절 텍스트 컴포넌트(나중에 삭제)
    public Text seasonText;
    // 계절 이미지
    public Sprite[] seasonSprite;

    // MyLittleTree 식별자
    public string id = "MyLittleTree";

    // 재화
    public int money;
    // 요정의 축복 소지량
    public int blessing;
    // 마지막으로 생성된 축복 시간
    public float lastGeneratedBlessingTime;
    // 축복 생성 주기
    private float ganerateBlessingTime = 300f;

    // 다음 계절 까지 남은 시간
    public float nextSeasonRemainingTime;

    // 한 계절 당 주기
    private float seasonalChangeTime = 10f;
    // 계절 이름(나중에 삭제)
    private string[] seasonName = {"봄", "여름", "가을", "겨울"};

    private int seasonIndex;

    // 최대 소지금
    private int maxMoney = 99999;

    /*
        과일 구매가, 판매가, 성장 시간 정보
        [0] : 사과
        [1] : 배
        [2] : 감
        [3] : 포도
        [4] : 한라봉
    */
    public float[] fruitPurchasePrice = {0f, 2f, 5f, 10f, 20f};
    public float[] fruitSellingPrice = {1f, 5f, 25f, 150f, 500f};
    public float[] fruitRemainingTime = {10f, 30f, 60f, 300f, 600f};
/*
    public float[] fruitPurchasePrice = {0f, 3f, 5f, 10f, 30f};
    public float[] fruitSellingPrice = {3f, 10f, 30f, 70f, 300f};
    public float[] fruitRemainingTime = {10f, 30f, 60f, 300f, 600f};
*/

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
        // 재화를 불러오기
        money = PlayerPrefs.GetInt(id + "Money", 0);
        // 계절 index 불러오기
        seasonIndex =  PlayerPrefs.GetInt(id + "SeasonIndex", 0);
        // 계절 변화 남은 시간 불러오기
        nextSeasonRemainingTime = PlayerPrefs.GetFloat(id + "NextSeasonRemainingTime", 0);
    }

    void Start() {
        // 해상도 고정
        Screen.SetResolution(1280, 720, true);

        // 처음 실행될 때 저장되었던 재화를 불러와서 게임 창에 갱신
        UIManager.instance.UpdateMoney(money);

        seasonText.text = seasonName[seasonIndex];
        // 배경 이미지 변경
        seasonImage.sprite = seasonSprite[seasonIndex];
    }

    // Update is called once per frame
    void Update()
    { 
        if (nextSeasonRemainingTime >= seasonalChangeTime) {
            seasonIndex = (seasonIndex + 1) % 4;
            seasonText.text = seasonName[seasonIndex];
            nextSeasonRemainingTime = 0f;
            // 배경 이미지 변경
            seasonImage.sprite = seasonSprite[seasonIndex];
            // 계절 index 저장
            PlayerPrefs.SetInt(id + "SeasonIndex", seasonIndex);
        }
        else {
            nextSeasonRemainingTime += Time.deltaTime;
        }

        // 계절 변화 남은 시간 저장(한프레임마다 저장 -> 비효율적 -> 일정 주기로 저장되도록 변경(ex N초 마다 저장))
        PlayerPrefs.SetFloat(id + "NextSeasonRemainingTime", nextSeasonRemainingTime);
    }

    // 재화를 얻음
    public void AddMoney(int addMoney) {
        SetMoney(money + addMoney);
    }

    // 재화를 지불
    public void SubMoney(int subMoney) {
        SetMoney(money - subMoney);
    }

    private void SetMoney(int setMoney) {
        money = setMoney;
        // 소지금이 최대 소지금보다 많아지면 소지금을 최대 소지금으로 변경
        if (money > maxMoney) {
            money = maxMoney;
        }
        UIManager.instance.UpdateMoney(money);
        // 재화를 저장
        PlayerPrefs.SetInt(id + "Money", money);
    }

    public void SubBlessing() {
        blessing--;
    }

    public int GetTreeGrade() {
        return treeManager.treeGrade;
    }

/*
    // 돈, 축복 데이터 저장
    public void SaveItemData(int money, int blessing) {
        // 재화를 저장
        PlayerPrefs.SetInt(id + "Money", money);
        // 축복 저장
        PlayerPrefs.SetInt(id + "Blessing", blessing);
        Debug.Log("SaveItemData");
    }

    // 돈, 축복 데이터 불러오기
    public void LoadItemData() {
        // 재화를 불러오기
        money = PlayerPrefs.GetInt(id + "Money");
        // 축복 갯수 불러오기
        blessing = PlayerPrefs.GetInt(id + "Blessing");
        Debug.Log("SaveItemData");
    }

    // 미션 진행도 데이터 저장
    // Missions.cs 에서 사용
    public void SaveMissionData(Missions mission) {
        
    }

    // 미션 진행도 데이터 불러오기
    // Missions.cs 에서 사용 -> Start() 
    public void LoadMissionData(Missions mission) {

    }

    // 동물 도감 데이터 저장
    public void SaveAnimalCollectionData() {

    }

    // 동물 도감  데이터 불러오기
    public void LoadAnimalCollectionData() {

    }

    // 설정 데이터 저장
    public void SaveSettingData(Settings setting) {
        PlayerPrefs.SetInt(id + "BgmVolume", bgmVolume);
    }

    // 설정 데이터 불러오기
    public void LoadSettingData(Settings setting) {
        setting.bgmVolume = PlayerPrefs.GetInt(id + "BgmVolume");
    }

    // 동물 잠금해제 여부 저장


*/
    // 게임 정보 초기화
    public void ResetGameData() {
        // 모든 키 값을 제거
        PlayerPrefs.DeleteAll();
        Debug.Log("게임 초기화");   // 나중에 지우기
        money = 0;  // 나중에 지우기
        UIManager.instance.UpdateMoney(money);  // 나중에 지우기

        // 처음 씬 불러오기
        LoadFirstScene();
    }

    // 처음 씬 불러오기
    public void LoadFirstScene() {
        SceneManager.LoadScene("FirstScene");
    }

    // 엔딩 씬 불러오기
    public void LoadEndingScene() {
        
    }
}
