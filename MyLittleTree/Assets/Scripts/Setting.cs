using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    // 사용할 오디오 소스 컴포넌트
    public AudioSource audioSource; 
    // 배경음악 ON/OFF 버튼
    public Button bgmOnOffButton;
    // 배경음악 ON/OFF 버튼 텍스트 컴포넌트
    public Text bgmButtonText;
    // 배경음악 ON/OFF 상태 저장
    public bool bgmOn;
    // 효과음 ON/OFF 버튼
    public Button soundEffectOnOffButton;
    // 효과음 ON/OFF 버튼 텍스트 컴포넌트
    public Text soundEffectButtonText;
    // 효과음 ON/OFF 상태 저장
    public bool soundEffectOn;

    // bool 형 변수를 저장할 때 사용
    private const int TRUE = 1;
    private const int FALSE = 0;

    void Start() {
        // 배경음악 ON/OFF 불러오기
        int temp = PlayerPrefs.GetInt(GameManager.instance.id + "BgmOn", TRUE);
        if (temp == FALSE) {
            bgmOn = false;
        }
        else if (temp == TRUE) {
            bgmOn = true;
        }
        // 배경음악 ON/OFF 버튼 텍스트 변경
        bgmButtonText.text = bgmOn == true ? "ON" : "OFF";
        // 배경음악이 ON이면 배경음악 재생
        if (bgmOn == true) {
            audioSource.Play();
        }

        // 효과음 ON/OFF 불러오기
        temp = PlayerPrefs.GetInt(GameManager.instance.id + "SoundEffectOn", TRUE);
        if (temp == FALSE) {
            soundEffectOn = false;
        }
        else if (temp == TRUE) {
            soundEffectOn = true;
        }
        // 효과음 ON/OFF 버튼 텍스트 변경
        soundEffectButtonText.text = soundEffectOn == true ? "ON" : "OFF";
    }

    // 배경음악 ON/OFF 버튼 클릭시 호출
    public void OnBgmOnOffButtonClick() {
        if (bgmOn == true) {
            bgmOn = false;
            // 배경음악 ON/OFF 버튼 텍스트 변경
            bgmButtonText.text = "OFF";
            // 배경음악 정지
            audioSource.Stop();
        }
        else {
            bgmOn = true;
            // 배경음악 ON/OFF 버튼 텍스트 변경
            bgmButtonText.text = "ON";
            // 배경음악 정지
            audioSource.Play();
        }

        // 배경음악 ON/OFF 상태 저장
        PlayerPrefs.SetInt(GameManager.instance.id + "BgmOn", bgmOn == true ? TRUE : FALSE);
    }

    // 효과음 ON/OFF 버튼 클릭시 호출
    public void OnSoundEffectOnOffButtonClick() {
        if (soundEffectOn == true) {
            soundEffectOn = false;
            // 효과음 ON/OFF 버튼 텍스트 변경
            soundEffectButtonText.text = "OFF";
        }
        else {
            soundEffectOn = true;
            // 효과음 ON/OFF 버튼 텍스트 변경
            soundEffectButtonText.text = "ON";
        }

        // 효과음 ON/OFF 상태 저장
        PlayerPrefs.SetInt(GameManager.instance.id + "SoundEffectOn", soundEffectOn == true ? TRUE : FALSE);
    }

    // 게임 초기화 버튼 클릭시 호출
    public void OnResetButtonClick() {
        GameManager.instance.ResetGameData();
    }
}
