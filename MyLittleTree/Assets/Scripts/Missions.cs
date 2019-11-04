using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    미션 패널에 컴포넌트로 추가
    미션 종류
    1. 과일 종류 별로 N개 수확
    2. 나무 업그레이드
*/
public class Missions : MonoBehaviour
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
    */
    private int[] progress = new int[5];
    /*
        미션 목표
        max : 열매 수확 미션 수
    */
    private int[] objective = new int[5];
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



    // Start is called before the first frame update
    void Start()
    {
        // 미션 진행도, 달성 여부 불러오기
    }


    // 열매 수확시 진행도 적용
    public void HarvestMission(string fruitName) {
        if (fruitAchievement[0] == false && fruitName == "Apple") {
            progress[0] += 1;
            AchievementCheck(0);
        }
        else if (fruitName == "") {

        }
    }

    // 미션 달성 여부 체크
    private void AchievementCheck(int index) {
        if (progress[index] >= objective[index]) {
            fruitAchievement[index] = true;

            // 미션 창 갱신

            // 보상
        }
    }

    /*
        나무 업그레이드 미션
        반드시 순차적으로 완료 가능
        Ex) 1단계 업그레이드 -> 2단계 업그레이드
    */
    public void TreeUpgradeMission() {
        for (int i = 0; i < treeAchievement.Length; i++) {
            if (treeAchievement[i] == false) {
                treeAchievement[i] = true;
                // 미션 창 갱신

                // 보상

                break;
            }
        }
    }

    // 미션 성공 보상
    public void Reward() {

    }


    // 미션 창 갱신
    private void UpdateMission() {

    }
}
