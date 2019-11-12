using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    미션 패널에 컴포넌트로 추가
    미션 종류
    1. 과일 종류 별로 N개 수확
    2. 나무 업그레이드
*/
public class Mission : MonoBehaviour
{
    public AnimalCollection animalCollection;

    /* 
        미션 진행도
        max : 열매 수확 미션 수
        [0] : Apple 수확 개수
        [1] : 
        [2] : 
        [3] : 
        [4] : 
    private int[] progress = new int[5];
*/
    //열매 수확 미션 진행도
    public int fruitHarvest;

    /*
        미션 목표
        max : 열매 수확 미션 수
    */
    private int[] fruitHarvestObjective = {10, 20, 30, 50, 100};
    /*
        열매 미션 달성 여부
        max : 열매 수확 미션 수
    */
    private bool[] fruitAchievement = new bool[5];
    /*
        나무 업그레이드 미션 달성 여부
        max : 나무 업그레이드 미션 수
    */
    private bool[] treeAchievement = new bool[5];
    // 수확 미션 보상
    public int[] fruitHarvestReward = {100, 300, 1000, 2000, 4000};
    // 나무 업그레이드 미션 보상
    public string[] treeUpgradeReward = {"Dog", "Cat", "GuineaPig", "Raccoon", "Fox"};
    public string[] treeUpgradeRewardKor = {"개", "고양이", "기니피그", "너구리", "여우"};

    /*
        보상 수령 여부
        max : 모든 미션 수
    */
    public bool[] getReward = new bool[10];

    // 미션 설명 텍스트 컴포넌트
    public Text[] missionText = new Text[10];
    // 미션 성공 여부 텍스트 컴포넌트
    public Text[] missionAchievementText = new Text[10];
    // 미션 성공 보상 받기 버튼
    public Button[] rewardButton = new Button[10];


    // Start is called before the first frame update
    void Start()
    {
        // 열매 수확 횟수 불러오기
        fruitHarvest = PlayerPrefs.GetInt(GameManager.instance.id + "FruitHarvest", 0);

        // 열매 미션 달성 여부 불러오기
        for (int i = 0; i < fruitAchievement.Length; i++) {
            fruitAchievement[i] = PlayerPrefs.GetInt(GameManager.instance.id + "FruitAchievement" + i, 0) == 1 ? true : false;
        }

        // 나무 업그레이드 미션 달성 여부 불러오기
        for (int i = 0; i < treeAchievement.Length; i++) {
            treeAchievement[i] = PlayerPrefs.GetInt(GameManager.instance.id + "TreeAchievement" + i, 0) == 1 ? true : false;
        }

        // 보상 수령 여부 불러오기
        for (int i = 0; i < getReward.Length; i++) {
            getReward[i] = PlayerPrefs.GetInt(GameManager.instance.id + "GetReward" + i, 0) == 1 ? true : false;
            Debug.Log("getReward[" + i + "] = " + getReward[i]);
        }

        // 미션 창 갱신
        UpdateMission();
    }


    // 열매 수확시 진행도 적용
    public void HarvestMission() {
        fruitHarvest++;

        // 열매 수확 횟수 저장
        PlayerPrefs.SetInt(GameManager.instance.id + "FruitHarvest", fruitHarvest);

        FruitMissionAchievementCheck();
    }

    // 미션 달성 여부 체크
    private void FruitMissionAchievementCheck() {
        for (int i = 0; i < fruitAchievement.Length; i++) {
            // 완료하지 못한 미션 중 미션 조건이 충족 됨
            if (fruitAchievement[i] == false && fruitHarvest >= fruitHarvestObjective[i]) {
                fruitAchievement[i] = true;

                // 열매 미션 달성 여부 저장
                PlayerPrefs.SetInt(GameManager.instance.id + "FruitAchievement" + i, fruitAchievement[i] == true ? 1 : 0);

                break;
            }
        }

        // 미션 창 갱신
        UpdateMission();
    }

    /*
        나무 업그레이드 미션
        반드시 순차적으로 완료 가능
        Ex) 1단계 업그레이드 -> 2단계 업그레이드
    */
    public void TreeUpgradeMission(int treeGrade) {
        treeAchievement[treeGrade - 1] = true;

        // 나무 업그레이드 미션 달성 여부 저장
        PlayerPrefs.SetInt(GameManager.instance.id + "TreeAchievement" + (treeGrade - 1), treeAchievement[treeGrade - 1] == true ? 1 : 0);

        Debug.Log("나무 업그레이드 미션 달성 여부 저장" + (treeGrade - 1));

        // 미션 창 갱신
        UpdateMission();
    }

    // 미션 창 갱신
    private void UpdateMission() {
        UpdateMissionInformationText();
        updateMissionAchievementText();
        UpdateMissionRewardButton();
    }

    // 미션 설명 텍스트 갱신
    private void UpdateMissionInformationText() {
        for (int i = 0; i < missionText.Length; i++) {
            if (i < 5) {
                // 현재 수확 미션 진행도를 현재 열매 수확 횟수가 목표 수확 횟수보다 많으면 목표 수확 횟수로 출력
                missionText[i].text = "열매 수확 횟수\n";
                if (fruitHarvest <= fruitHarvestObjective[i]) {
                    missionText[i].text += fruitHarvest;
                    Debug.Log("열매 미션 갱신");
                }
                else {
                    missionText[i].text += fruitHarvestObjective[i];
                }
                missionText[i].text += "/" + fruitHarvestObjective[i];
            }
            else {
                missionText[i].text = "나무 " + (i - 4) + "등급 달성";
            }
        }
    }

    // 미션 달성 여부 텍스트 갱신
    private void updateMissionAchievementText() {
        for (int i = 0; i < missionAchievementText.Length; i++) {
            if (i < 5) {
                //missionAchievementText[i].text = (fruitAchievement[i] == true ? "달성" : "미달성");
                missionAchievementText[i].text = "보상\n요정의 빛 " + fruitHarvestReward[i];
            }
            else {
                //missionAchievementText[i].text = (treeAchievement[i - 5] == true ? "달성" : "미달성");
                missionAchievementText[i].text = "보상\n" + treeUpgradeRewardKor[i - 5];
            }
        }
    }

    // 미션 보상 버튼과 버튼 텍스트 갱신
    private void UpdateMissionRewardButton() {
        /*
            미션 미달성 || 보상 수령 함 : 보상 버튼 비활성화
            미션 달성 && 보상 수령 안함 : 보상 버튼 활성화
        */
        for (int i = 0; i < rewardButton.Length; i++) {
            // 열매 수확 미션
            if (i < 5) {
                if (fruitAchievement[i] == false) {
                    rewardButton[i].interactable = false;
                    rewardButton[i].GetComponentInChildren<Text>().text = "미달성";
                }
                else if (getReward[i] == true) {
                    rewardButton[i].interactable = false;
                    rewardButton[i].GetComponentInChildren<Text>().text = "수령 완료";
                }
                else if (fruitAchievement[i] == true && getReward[i] == false) {
                    rewardButton[i].interactable = true;
                    rewardButton[i].GetComponentInChildren<Text>().text = "보상 수령";
                }
                else {
                    Debug.Log("열매 수확 미션 버튼 예외");
                }
            }
            else {
                if (treeAchievement[i - 5] == false) {
                    rewardButton[i].interactable = false;
                    rewardButton[i].GetComponentInChildren<Text>().text = "미달성";
                }
                else if (getReward[i] == true) {
                    rewardButton[i].interactable = false;
                    rewardButton[i].GetComponentInChildren<Text>().text = "수령 완료";
                }
                else if (treeAchievement[i - 5] == true && getReward[i] == false) {
                    rewardButton[i].interactable = true;
                    rewardButton[i].GetComponentInChildren<Text>().text = "보상 수령";
                }
                else {
                    Debug.Log("나무 업그레이드 미션 버튼 예외");
                }
            }
        }
    }
    
    // 열매 미션 성공 보상
    // 보상 받기 버튼 클릭시 호출
    public void FruitMissionReward(int index) {
        // 보상 지급
        GameManager.instance.AddMoney(fruitHarvestReward[index]);

        // 보상 지급 여부 저장
        getReward[index] = true;

        // 보상 수령 여부 저장
        PlayerPrefs.SetInt(GameManager.instance.id + "GetReward" + index,  getReward[index] == true ? 1 : 0);

        Debug.Log("보상 수령 여부 저장 " + index);

        UpdateMission();
    }

    // 나무 미션 성공 보상
    // 보상 받기 버튼 클릭시 호출
    public void TreeMissionReward(int index) {
        // 보상 지급
        UIManager.instance.SaveAnimalActive(index);
        
        // 보상 지급 여부 저장
        // 5 : 열매 미션 수
        getReward[index + 5] = true;

        // 보상 수령 여부 저장
        PlayerPrefs.SetInt(GameManager.instance.id + "GetReward" + (index + 5),  getReward[index + 5] == true ? 1 : 0);

        Debug.Log("보상 수령 여부 저장 " + (index + 5));

        UpdateMission();
    }

    // 모든 미션을 클리어하고 모든 보상을 받으면 엔딩 씬 호출
    private void CheckMissionAllClear() {
        // 엔딩 씬 호출
    }
}
