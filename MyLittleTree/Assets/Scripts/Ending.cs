using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    // 요정 이미지 컴포넌트
    public Image fairyImage;
    // 요정 스프라이트
    public Sprite fairy;
    // 요정 말풍선
    public Text dialogueText;
    // 요정 대사, 개수 미 확정
    private string[] dialogue = new string[10];
    // 대사 인덱스 -> 화면 클릭 시 +1 해서 다음 대사로 넘어감
    private int dialogueIndex = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // 엔딩 실행 메서드
    private void PlayEnding() {

    }

    // 엔딩 화면 클릭시 호출
    public void OnGameScreenClick() {
        
    }
}
