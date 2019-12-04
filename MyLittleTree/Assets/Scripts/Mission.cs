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
    public int fruitHarvestCount;

    /*
        미션 목표
        max : 열매 수확 미션 수
    */
    //private int[] fruitHarvestObjective = {10, 20, 30, 50, 100};
    private int[] fruitHarvestObjective = {1, 2, 3, 5, 10};
    /*
        열매 미션 달성 여부
        max : 열매 수확 미션 수
    */
    public bool[] fruitAchievement = new bool[5];
    /*
        나무 업그레이드 미션 달성 여부
        max : 나무 업그레이드 미션 수
    */
    public bool[] treeAchievement = new bool[5];
    // 수확 미션 보상
    private int[] fruitHarvestReward = {100, 300, 1000, 2000, 4000};
    // 나무 업그레이드 미션 보상
    private string[] treeUpgradeReward = {"Dog", "Cat", "Pig", "Goose", "GuineaPig"};
    private string[] treeUpgradeRewardKor = {"개", "고양이", "돼지", "거위", "기니피그"};

    /*
        보상 수령 여부
        max : 모든 미션 수
    */
    public bool[] getReward = new bool[10];

    // 미션 목록 패널 오브젝트
    public GameObject[] missionListPanel = new GameObject[10];
    // 미션 목록 패널의 트랜스폼 컴포넌트
    private RectTransform[] missionListPanelRectTransform = new RectTransform[10];
    // 미션 설명 텍스트 컴포넌트
    public Text[] missionText = new Text[10];
    // 미션 보상 텍스트 컴포넌트
    public Text[] missionRewardText = new Text[10];
    // 미션 성공 보상 받기 버튼
    public Button[] rewardButton = new Button[10];
    
    // 미션 달성 수 표시 텍스트 컴포넌트
    public Text achievementCountText;

    // 미션 목록 패널의 트랜스폼 컴포넌트의 localPosition
    private Vector3[] missionListPanelLocalPos = new Vector3[10];

    void Awake() {
        // 미션 목록 패널 트랜스폼 저장
        for (int i = 0; i <  missionListPanel.Length; i++) {
            missionListPanelRectTransform[i] = missionListPanel[i].GetComponent<RectTransform>();
            if (missionListPanelRectTransform[i] == null)
                Debug.Log("널 : " + i);
        }

        for (int i = 0; i < missionListPanelLocalPos.Length; i++) {
            missionListPanelLocalPos[i] = missionListPanelRectTransform[i].localPosition;
            //Debug.Log("missionListPanelLocalPos[" + i + "] = " + missionListPanelLocalPos[i]);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

/*
        for (int i = 0; i < missionListPanel.Length; i++) {
            missionListPanelTransform[i] = missionListPanel[i].GetComponent<Transform>();
        }
*/
/*
        for (int i = 0; i < 10; i++) {
            Debug.Log("Transform[" + i + "]이 같음 = " +  missionListPanel[i].GetComponent<RectTransform>() == missionListPanelTransform[i]);
        }
*/

        try {
            // 열매 수확 횟수 불러오기
            fruitHarvestCount = PlayerPrefs.GetInt(GameManager.instance.id + "FruitHarvestCount", 0);

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
            }
        } 
        catch (System.Exception e) {
            Debug.Log(e);
            UIManager.instance.ErrorMessage("파일 자동 불러오기 실패");
            GameManager.instance.QuitGame();
        }

        // 미션 창 갱신
        UpdateMission();

        UpdateMissionRewardText();
    }


    // 열매 수확시 진행도 적용
    public void HarvestMission() {
        fruitHarvestCount++;

        try {
            // 열매 수확 횟수 저장
            PlayerPrefs.SetInt(GameManager.instance.id + "FruitHarvestCount", fruitHarvestCount);
        } 
        catch (System.Exception e) {
            Debug.Log(e);
            UIManager.instance.ErrorMessage("파일 자동 저장 실패");
        }
        
        FruitMissionAchievementCheck();
    }

    // 열매 수확 미션 달성 여부 체크
    private void FruitMissionAchievementCheck() {
        for (int i = 0; i < fruitAchievement.Length; i++) {
            // 완료하지 못한 미션 중 미션 조건이 충족 됨
            if (fruitAchievement[i] == false && fruitHarvestCount >= fruitHarvestObjective[i]) {
                fruitAchievement[i] = true;

                try {
                    // 열매 미션 달성 여부 저장
                    PlayerPrefs.SetInt(GameManager.instance.id + "FruitAchievement" + i, fruitAchievement[i] == true ? 1 : 0);
                } 
                catch (System.Exception e) {
                    Debug.Log(e);
                    UIManager.instance.ErrorMessage("파일 자동 저장 실패");
                }

                // 미션 창 갱신
                UpdateMission();

                break;
            }
        }
    }

    /*
        나무 업그레이드 미션
        반드시 순차적으로 완료 가능
        Ex) 1단계 업그레이드 -> 2단계 업그레이드
    */
    public void TreeUpgradeMission(int treeGrade) {
        if (!(treeGrade >= 1 && treeGrade <= 5)) {
            Debug.Log("TreeUpgradeMission(int treeGrade)의 treeGrade값 오류");
            GameManager.instance.QuitGame();
        }

        if (treeAchievement[treeGrade - 1] == true) {
            return;
        }

        treeAchievement[treeGrade - 1] = true;

        try {
            // 나무 업그레이드 미션 달성 여부 저장
            PlayerPrefs.SetInt(GameManager.instance.id + "TreeAchievement" + (treeGrade - 1), treeAchievement[treeGrade - 1] == true ? 1 : 0);
        } 
        catch (System.Exception e) {
            Debug.Log(e);
            UIManager.instance.ErrorMessage("파일 자동 저장 실패");
        }

        // 미션 창 갱신
        UpdateMission();
    }
        
    // 열매 미션 성공 보상
    // 보상 받기 버튼 클릭시 호출
    public void FruitMissionReward(int index) {
        if (!(index >= 0 && index <= 4)) {
            Debug.Log("Mission.FruitMissionReward(int index)의 index값 오류");
            GameManager.instance.QuitGame();
        }

        try {
            // 보상 지급
            GameManager.instance.AddMoney(fruitHarvestReward[index]);
        } 
        catch (System.Exception e) {
            Debug.Log(e);
            UIManager.instance.ErrorMessage("도전 과제 보상 받기 실패");
        }
        

        // 보상 지급 여부 저장
        getReward[index] = true;

        CheckMissionAllClear();

        try {
            // 보상 수령 여부 저장
            PlayerPrefs.SetInt(GameManager.instance.id + "GetReward" + index,  getReward[index] == true ? 1 : 0);
        } 
        catch (System.Exception e) {
            Debug.Log(e);
            UIManager.instance.ErrorMessage("파일 자동 저장 실패");
        }

        Debug.Log("보상 수령 여부 저장 " + index);

        UpdateMission();

        // 버튼 클릭 소리 재생
        UIManager.instance.PlayButtonClickSound();
    }

    // 나무 미션 성공 보상
    // 보상 받기 버튼 클릭시 호출
    public void TreeMissionReward(int index) {
        if (!(index >= 0 && index <= 4)) {
            Debug.Log("Mission.TreeMissionReward(int index)의 index값 오류");
            GameManager.instance.QuitGame();
        }

        try {
            // 보상 지급
            UIManager.instance.SaveAnimalActive(index);
        } 
        catch (System.Exception e) {
            Debug.Log(e);
            UIManager.instance.ErrorMessage("도전 과제 보상 받기 실패");
        }

        
        
        // 보상 지급 여부 저장
        // 5 : 열매 미션 수
        getReward[index + 5] = true;

        CheckMissionAllClear();

        try {
            // 보상 수령 여부 저장
            PlayerPrefs.SetInt(GameManager.instance.id + "GetReward" + (index + 5),  getReward[index + 5] == true ? 1 : 0);
        } 
        catch (System.Exception e) {
            Debug.Log(e);
            UIManager.instance.ErrorMessage("파일 자동 저장 실패");
        }

        Debug.Log("보상 수령 여부 저장 " + (index + 5));

        UpdateMission();

        // 버튼 클릭 소리 재생
        UIManager.instance.PlayButtonClickSound();
    }

    // 미션 보상 텍스트 변경
    private void UpdateMissionRewardText() {
        for (int i = 0; i < missionRewardText.Length; i++) {
            if (i < 5) {
                //missionAchievementText[i].text = (fruitAchievement[i] == true ? "달성" : "미달성");
                missionRewardText[i].text = "보상\n요정의 빛 " + fruitHarvestReward[i];
            }
            else {
                //missionAchievementText[i].text = (treeAchievement[i - 5] == true ? "달성" : "미달성");
                missionRewardText[i].text = "보상\n" + treeUpgradeRewardKor[i - 5];
            }
        }
    }

    // 미션 창 갱신 && 미션 달성 수 표시 텍스트 갱신
    private void UpdateMission() {
        try {
            UpdateMissionInformationText();
            UpdateMissionRewardButton();
            SortMission();

            // 미션 달성 수 표시 텍스트 갱신
            UpdateAchievementCountText();
        }
        catch (System.Exception e) {
            Debug.Log(e);
            UIManager.instance.ErrorMessage("도전과제 달성 표시 오류");
        }
    }

    // 미션 설명 텍스트 갱신
    private void UpdateMissionInformationText() {
        for (int i = 0; i < missionText.Length; i++) {
            if (i < 5) {
                // 현재 수확 미션 진행도를 현재 열매 수확 횟수가 목표 수확 횟수보다 많으면 목표 수확 횟수로 출력
                missionText[i].text = "열매 수확 횟수\n";
                if (fruitHarvestCount <= fruitHarvestObjective[i]) {
                    missionText[i].text += fruitHarvestCount;
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


    // 미션 보상 버튼 활성화 여부 변경과 버튼 텍스트 갱신
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
                    rewardButton[i].GetComponentInChildren<Text>().text = "보상 받기";
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
                    rewardButton[i].GetComponentInChildren<Text>().text = "보상 받기";
                }
                else {
                    Debug.Log("나무 업그레이드 미션 버튼 예외");
                }
            }
        }
    }


    /*
        미션 목록을 우선 순위로 정렬
        기존 방식으로 나열 후 재 정렬(패널의 위치를 바꾸는 형식)
        우선 순위(위쪽)
        1. 미션을 달성 && 보상받지 않음
        2. 미션을 미달성
        3. 보상을 받음(조금 어둡게)
        4. 열매 수확 도전과제 중 수확량이 작은 순
        5. 나무 업그레이드 도전과제 중 나무 업그레이드 등급이 낮은 순
    */
    
    private void SortMission() {
        int[] panelNum = new int[10];

        int panelIndex = 0;

        try {
            // 미션 달성 && 보상 받지 않은 미션 패널 트랜스폼 저장
            for (int i = 0; i < 10; i++) {
                if (i < fruitAchievement.Length) {
                    if (fruitAchievement[i] == true && getReward[i] == false) {
                        panelNum[panelIndex++] = i;
                    }
                }
                else {
                    if (treeAchievement[i - fruitAchievement.Length] == true && getReward[i] == false) {
                        panelNum[panelIndex++] = i;
                    }
                }
            }

            // 미션을 미달성한 미션 패널 트랜스폼 저장
            for (int i = 0; i < 10; i++) {
                if (i < fruitAchievement.Length) {
                    if (fruitAchievement[i] == false) {
                        panelNum[panelIndex++] = i;
                    }
                }
                else {
                    if (treeAchievement[i - fruitAchievement.Length] == false) {
                        panelNum[panelIndex++] = i;
                    }
                }
            }

            // 보상을 받은 미션 패널 트랜스폼 저장
            for (int i = 0; i < 10; i++) {
                if (getReward[i] == true) {
                    panelNum[panelIndex++] = i;

                    // 패널의 색상 어둡게
                    missionListPanel[i].GetComponent<Image>().color = new Color(0f, 0f, 0f, 100/255f);
                }
            }

            // 우선순위에 따라 도전과제 정렬
            for (int i = 0; i < panelNum.Length; i++) {
                missionListPanelRectTransform[panelNum[i]].localPosition = missionListPanelLocalPos[i];
            } 
        }
        catch (System.Exception e) {
            Debug.Log(e);
            UIManager.instance.ErrorMessage("도전과제 정렬 실패");
            
            // 도전과제 정렬이 되지 않은 상태로 되돌림
            for (int i = 0; i < missionListPanelRectTransform.Length; i++) {
                missionListPanelRectTransform[i].localPosition = missionListPanelLocalPos[i];
            }
        } 
    }

    // 미션 달성 수 표시 텍스트 컴포넌트 갱신
    private void UpdateAchievementCountText() {
        int achievementCount = 0;

        for (int i = 0; i < fruitAchievement.Length + treeAchievement.Length; i++) {
            if (i < fruitAchievement.Length) {
                // 열매 미션을 달성하고 보상을 받지 않음
                if (fruitAchievement[i] == true && getReward[i] == false) {
                    achievementCount++;
                }
            }
            else {
                if (treeAchievement[i - fruitAchievement.Length] == true && getReward[i] == false) {
                    achievementCount++;
                }
            }
        }

        // 미션 달성 수 표시 텍스트 갱신
        achievementCountText.text = "" + achievementCount;
    }


    // 모든 미션 보상을 받으면 엔딩 씬 호출
    private void CheckMissionAllClear() {
        for (int i = 0; i < getReward.Length; i++) {
            if (getReward[i] == false) {
                return;
            }
        }
        // 게임 데이터 초기화
        GameManager.instance.ResetGameData();
        // 엔딩 씬 호출
        GameManager.instance.LoadEndingScene();
    }
}
