using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    동물 오브젝트에 각자 컴포넌트로 추가
*/
public class Animal : MonoBehaviour
{
    // 동물 클릭 시 실행할 애니메이터
    private Animator animalAnimator;
    // 동물 울음소리 오디오 소스
    private AudioSource animalAudio;



    // Start is called before the first frame update
    void Start()
    {
        
        animalAudio = GetComponent<AudioSource>();
    }


    // 동물을 클릭했으면
    public void OnAnimalClick() {
        // 애니메이션 재생

        // 효과음 재생
    }
}
