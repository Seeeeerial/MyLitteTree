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
	private bool isFairyAppear = false;     //요정이 나타났는가
	public GameObject endingCredit;			//엔딩 크레딧 이미지
	enum lightState { notLight = 0, isLighting, lightingEnd }	//빛 효과의 상태 notLight는 빛이 나온적없음, isLighting은 빛이 나오는중, lightingEnd는 빛나오기 끝남
	private int lightingState = (int)lightState.notLight;		//빛 효과의 현재 상태

	public GameObject ballon;	//말풍선
	public Text dialogueText;   // 요정 대사 텍스트
	private string[] dialogue;  // 요정 대사
	private int dialogueIndex = 0;  // 대사 인덱스
	enum chatState { chatting = 0, wait, next };//대사의 상태 chatting은 대사나오는중, wait는 터치 대기, next는 다음대사로
	int nowChatting = (int)chatState.next;      //대사의 상태

	private AudioSource bgm;                //브금깔기용
	public AudioClip backgroundSound;	//브금
	public AudioClip endingMusic;		//엔딩용 브금

	void Awake()
	{
		dialogue = GameObject.Find("EndingManager").GetComponent<TalkList>().getEndChat();
		bgm = this.gameObject.AddComponent<AudioSource>();
		bgm.clip = this.backgroundSound;
		bgm.loop = true;
	}
	void Start()
    {
        
    }
	void Update()
	{
		if(!(bgm.isPlaying)) bgm.Play();
		if (!(isFairyAppear))		//요정 움직이기
			fairyAppear();
		else
		{
			if (ballon.transform.localScale.y < 0.5f)       //말풍선 나타나기
			{
				ballon.transform.parent.gameObject.SetActive(true);
				ballon.transform.localScale += new Vector3(0f, 0.1f, 0f);
			}
			else
			{
				if (dialogueIndex == 8)		//대사가 끝나면
				{
					if(lightingState == (int)lightState.notLight)	//빛 뿜기 시작
					{
						StartCoroutine(LightSpreadAndOut());
						lightingState = (int)lightState.isLighting;
					}
					else if(lightingState == (int)lightState.lightingEnd)
					{
						endingCredit.SetActive(true);
					}
				}
				else
				{
					if(nowChatting == (int)chatState.next)
					{
						StartCoroutine(Speeking(dialogue[dialogueIndex]));
						nowChatting = (int)chatState.chatting;
					}
				}
			}
		}
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

	private IEnumerator LightSpreadAndOut()	//빛이 퍼졌다가 점점 사라지는 효과
	{
		ballon.transform.parent.gameObject.SetActive(false);
		lightObject.SetActive(true);
		while (lightObject.transform.localScale.x < 10)		//빛이 커짐
		{
			lightObject.transform.localScale += new Vector3(0.1f, 0.1f, 0f);
			yield return null;
		}
		Color color = lightObject.GetComponent<Image>().color;		//투명도 조절용 변수

		Tree.transform.GetChild(0).gameObject.SetActive(false);     //나무 엔딩나무로 변경
		Tree.transform.GetChild(1).gameObject.SetActive(true);
		fairy.SetActive(false);                 //요정 사라짐
		bgm.clip = this.endingMusic;			//엔딩 bgm으로 변경

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

		yield return new WaitForSeconds(3f);
		lightingState = (int)lightState.lightingEnd;
	}

	public void OnTouchScreenClick()
	{
		if (nowChatting == (int)chatState.wait)
		{
			dialogueIndex++;
			if (dialogueIndex != 8)
				nowChatting = (int)chatState.next;
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
