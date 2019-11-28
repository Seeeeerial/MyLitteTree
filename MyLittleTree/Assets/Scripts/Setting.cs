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

    void Start() {
        try {
            // 배경음악 ON/OFF 불러오기
            bgmOn = PlayerPrefs.GetInt(GameManager.instance.id + "BgmOn", 1) == 1 ? true : false;

            // 효과음 ON/OFF 불러오기
            soundEffectOn = PlayerPrefs.GetInt(GameManager.instance.id + "SoundEffectOn", 1) == 1 ? true : false;
        } 
        catch (System.Exception e) {
            Debug.Log(e);
            UIManager.instance.ErrorMessage("파일 자동 불러오기 실패");
            GameManager.instance.QuitGame();
        }

        // 배경음악 ON/OFF 버튼 텍스트 변경
        bgmButtonText.text = bgmOn == true ? "ON" : "OFF";
        // 배경음악이 ON이면 배경음악 재생
        if (bgmOn == true) {
            audioSource.Play();
        }

        // 효과음 ON/OFF 버튼 텍스트 변경
        soundEffectButtonText.text = soundEffectOn == true ? "ON" : "OFF";
    }

    // 배경음악 ON/OFF 버튼 클릭시 호출
    public void OnBgmOnOffButtonClick() {
        try {
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
        }
        catch (System.Exception e) {
            Debug.Log(e);

            // bgmOn을 Default 값인 true로 변경
            bgmOn = true;
            // 배경음악 ON/OFF 버튼 텍스트 변경
            bgmButtonText.text = "ON";
            // 배경음악 정지
            audioSource.Play();
        }



        try {
            // 배경음악 ON/OFF 상태 저장
            PlayerPrefs.SetInt(GameManager.instance.id + "BgmOn", bgmOn == true ? 1 : 0);
        } 
        catch (System.Exception e) {
            Debug.Log(e);
            UIManager.instance.ErrorMessage("파일 자동 저장 실패");
        }

        // 버튼 클릭 소리 재생
        UIManager.instance.PlayButtonClickSound();
    }

    // 효과음 ON/OFF 버튼 클릭시 호출
    public void OnSoundEffectOnOffButtonClick() {
        try {
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
        }
        catch (System.Exception e) {
            Debug.Log(e);
            UIManager.instance.ErrorMessage("사운드 조절 실패");

            // soundEffectOn을 Default 값인 true로 변경
            soundEffectOn = true;
            // 효과음 ON/OFF 버튼 텍스트 변경
            soundEffectButtonText.text = "ON";
        }

        try {
            // 효과음 ON/OFF 상태 저장
            PlayerPrefs.SetInt(GameManager.instance.id + "SoundEffectOn", soundEffectOn == true ? 1 : 0);
        } 
        catch (System.Exception e) {
            Debug.Log(e);
            UIManager.instance.ErrorMessage("파일 자동 저장 실패");
        }

        // 버튼 클릭 소리 재생
        UIManager.instance.PlayButtonClickSound();
    }

    // 게임 초기화 버튼 클릭시 호출
    public void OnResetButtonClick() {
        GameManager.instance.ResetGameData();

        // 버튼 클릭 소리 재생
        UIManager.instance.PlayButtonClickSound();
    }
}
