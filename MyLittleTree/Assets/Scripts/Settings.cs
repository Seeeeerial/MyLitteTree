using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    // 사용할 오디오 소스 컴포넌트
    public AudioSource audioSource; 
    // 배경음악 슬라이더
    public Slider bgmSlider;

    // 가장 마지막 배경음악 크기
    public float bgmVolume;

    void Start() {
        // 배경음악 음량 불러오기
        bgmVolume = PlayerPrefs.GetFloat(GameManager.instance.id + "BgmVolume", 1f);
        // 배경음악 음량 적용
        audioSource.volume = bgmVolume;
        // 배경음악 음량 슬라이더에 적용
        bgmSlider.value = bgmVolume;
    }

    // 배경음악 음량 변경시 호출
    public void BgmValueChanged() {
        // 음량 조절 적용
        bgmVolume = bgmSlider.value;
        audioSource.volume = bgmVolume;
        // 배경음악 음량 저장
        PlayerPrefs.SetFloat(GameManager.instance.id + "BgmVolume", bgmVolume);
    }

    // 게임 초기화 버튼 클릭시 호출
    public void OnResetButtonClick() {
        GameManager.instance.ResetGameData();
    }
}
