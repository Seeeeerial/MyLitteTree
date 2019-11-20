using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
	public GameObject lightObject;		//빛뿜어지기연출위한 빛
	public GameObject Tree;		//나무
    public GameObject fairy;    // 요정
	private bool isFairyAppear = false;

	public GameObject ballon;	//말풍선
	public Text dialogueText;   // 요정 대사 텍스트
	private string[] dialogue;  // 요정 대사
	private int dialogueIndex = 0;  // 대사 인덱스
	enum chatState { chatting = 0, wait, next };//대사의 상태 chatting은 대사나오는중, wait는 터치 대기
	int nowChatting = (int)chatState.next;      //대사의 상태


	void Awake()
	{
		dialogue = GameObject.Find("EndingManager").GetComponent<TalkList>().getEndChat();
	}
	void Start()
    {
        
    }
	void Update()
	{
		
	}
	
	private void fairyAppear()			//요정 등장용
	{
		if (fairy.transform.localPosition.x > 500)
			fairy.transform.localPosition -= new Vector3(5f, 0f, 0f);
		else
		{
			if (fairy.transform.GetChild(0).gameObject.transform.localScale.x > 0)
				fairy.transform.GetChild(0).gameObject.transform.localScale -= new Vector3(0.2f, 0.2f, 0f);
			else
			{
				fairy.transform.GetChild(0).gameObject.SetActive(false);
				isFairyAppear = true;
			}
		}
	}

	private void LightSpread()		//빛이 퍼져나가는 효과
	{
		if (lightObject.transform.localScale.x < 10)
		{
			lightObject.transform.localScale += new Vector3(0.1f, 0.1f, 0f);
		}
	}
	private IEnumerator LightFadeOut()	//빛이 점점 사라지는 효과
	{
		Color color = lightObject.GetComponent<Image>().color;
		yield return new WaitForSeconds(1f);
		while (color.a > 0)             //빛이 희미해짐
		{
			color.a -= 0.02f;
			lightObject.GetComponent<Image>().color = color;
			yield return null;
		}

		lightObject.SetActive(false);               //원상복귀
		lightObject.transform.localScale = new Vector3(0f, 0f, 0f);
		color.a = 1;
		lightObject.GetComponent<Image>().color = color;
	}

	public void OnTouchScreen()
	{
		if (nowChatting == (int)chatState.wait)
		{
			nowChatting = (int)chatState.next;
			dialogueIndex++;
		}
	}

	// 엔딩 화면 클릭시 호출
	public void OnCreditClick() {
		SceneManager.LoadScene("MainScene");
	}

	private IEnumerator Speeking(string narration)    //대사가 하나씩 나오기 위함
	{
		string writeText = "";

		for (int a = 0; a < narration.Length; a++)
		{
			writeText += narration[a];
			dialogueText.text = writeText;

			yield return new WaitForSeconds(0.02f);
		}
		nowChatting = (int)chatState.wait;
	}


	public void OnSkipClick()       //스킵 눌렀을 때
	{
		SceneManager.LoadScene("MainScene");
	}
}
