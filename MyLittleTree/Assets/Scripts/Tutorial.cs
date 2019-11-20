using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
	private AudioSource bgm;                //브금깔기용
	public AudioClip backgroundSound;

	public GameObject touchText;		//터치하세요 문구 끄기용

	public GameObject dog;				//개
	public GameObject tree;				//나무
	public GameObject background;		//배경
	public Text talk;					//대사
	public GameObject ballon;			//말풍선
	public GameObject tutorialCanvus;   //메뉴창들 있는 캔버스
	public GameObject lightObject;			//화면에 빛을 뿌리기위한 빛

	bool treeTouch;                     //나무 터치했는지
	bool lighting;                      //빛 내는 중인지
	enum chatState { chatting = 0, wait, next };//대사의 상태 chatting은 대사나오는중, wait는 터치 대기, next는 다음대사로
	int nowChatting = (int)chatState.next;      //대사의 상태
	int chatNum = 0;            //현재 대사가 무엇인지 결정
	private string[] chatList;
	void Awake()
	{
		treeTouch = false;
		lighting = false;
		chatNum = 0;
		chatList = GameObject.Find("TutorialManager").GetComponent<TalkList>().getTutoChat();
		bgm = this.gameObject.AddComponent<AudioSource>();
		bgm.clip = this.backgroundSound;
		bgm.loop = true;
	}

    void Start()
    {
        
    }

 
    void Update()
    {
        if(treeTouch && !(lighting))
		{
			if (!(bgm.isPlaying)) bgm.Play();
			if (dog.transform.localPosition.x > 600f)
				dog.transform.localPosition -= new Vector3(10f, 0f, 0f);	//개 움직임
			else
			{
				ballon.transform.parent.gameObject.SetActive(true);
				if (ballon.transform.localScale.y < 0.5)  //말풍선 나타내기
					ballon.transform.localScale += new Vector3(0f, 0.1f, 0f);
				else
				{
					if (nowChatting == (int)chatState.next)
					{
						if (chatNum >= 21)
							SceneManager.LoadScene("MainScene");
						else
						{
							StartCoroutine(Explaining(chatList[chatNum]));
							nowChatting = (int)chatState.chatting;
						}
						if(chatNum == 6 )		//열매 강조
							tree.transform.GetChild(3).gameObject.SetActive(true);
						else if(chatNum == 7)		//열매강조끄기, 열매 창 오픈
						{
							tree.transform.GetChild(3).gameObject.SetActive(false);
							tutorialCanvus.transform.GetChild(8).gameObject.SetActive(true);
						}
						else if(chatNum == 8)		//열매 창 끄기, 축복 강조
						{
							tutorialCanvus.transform.GetChild(8).gameObject.SetActive(false);
							tutorialCanvus.transform.GetChild(7).gameObject.SetActive(true);
						}
						else if(chatNum == 10)		//축복강조 끄기, 나무 강조
						{
							tutorialCanvus.transform.GetChild(7).gameObject.SetActive(false);
							tree.transform.GetChild(2).gameObject.SetActive(true);
						}
						else if(chatNum == 11)		//나무 강조 끄기, 나무 창 오픈
						{
							tree.transform.GetChild(2).gameObject.SetActive(false);
							tutorialCanvus.transform.GetChild(9).gameObject.SetActive(true);
						}
						else if(chatNum == 14)		//나무 창 끄기, 도전과제 강조
						{
							tutorialCanvus.transform.GetChild(9).gameObject.SetActive(false);
							tutorialCanvus.transform.GetChild(1).gameObject.SetActive(true);
						}
						else if(chatNum == 15)		//도전과제 강조 끄기, 도전과제 창 오픈
						{
							tutorialCanvus.transform.GetChild(1).gameObject.SetActive(false);
							tutorialCanvus.transform.GetChild(10).gameObject.SetActive(true);
						}
						else if(chatNum == 17)		//도전과제 창 끄기, 동물도감 강조
						{
							tutorialCanvus.transform.GetChild(10).gameObject.SetActive(false);
							tutorialCanvus.transform.GetChild(3).gameObject.SetActive(true);
						}
						else if(chatNum == 18)		//동물도감 강조 끄기, 동물도감 창 오픈
						{
							tutorialCanvus.transform.GetChild(3).gameObject.SetActive(false);
							tutorialCanvus.transform.GetChild(11).gameObject.SetActive(true);
						}
						else if(chatNum == 19)		//동물도감 창 끄기
						{
							tutorialCanvus.transform.GetChild(11).gameObject.SetActive(false);
						}

					}
				}
			}
		}
    }

	public void OnTreeClick()		//나무 터치하면 튜토리얼시작
	{
		treeTouch = true;
		lightObject.SetActive(true);
		lighting = true;
		touchText.SetActive(false);
		StartCoroutine(LightingAction());
	}
	private IEnumerator LightingAction()		//빛이 퍼지는 효과를 위한 함수
	{
		Color color = lightObject.GetComponent<Image>().color;
		while (lightObject.transform.localScale.x < 10)
		{
			lightObject.transform.localScale += new Vector3(0.1f, 0.1f, 0f);
			yield return null;
		}
		tree.transform.GetChild(0).gameObject.SetActive(false);	//나무성장시키기
		tree.transform.GetChild(1).gameObject.SetActive(true);
		background.transform.GetChild(0).gameObject.SetActive(false);
		background.transform.GetChild(1).gameObject.SetActive(true);
		yield return new WaitForSeconds(1f);
		while (color.a>0)				//빛이 희미해짐
		{
			color.a -= 0.02f;
			lightObject.GetComponent<Image>().color = color;
			yield return null;
		}
		tutorialCanvus.SetActive(true);     //UI 키기
		lightObject.SetActive(false);               //원상복귀
		lightObject.transform.localScale = new Vector3(0f, 0f, 0f);
		color.a = 1;
		lightObject.GetComponent<Image>().color = color;
		lighting = false;
		yield return new WaitForSeconds(1f);
	}

	public void OnBallonClick()		//대사 진행하려 터치할 때
	{
		if(nowChatting == (int)chatState.wait)
		{
			nowChatting = (int)chatState.next;
			chatNum++;
		}
	}

	private IEnumerator Explaining(string narration)	//대사가 하나씩 나오기 위함
	{
		string writeText = "";

		for(int a = 0; a < narration.Length; a++)
		{
			writeText += narration[a];
			talk.text = writeText;
			
			yield return new WaitForSeconds(0.02f);
		}
		nowChatting = (int)chatState.wait;
	}
	public void OnSkipClick()		//스킵 눌렀을 때
	{
		SceneManager.LoadScene("MainScene");
	}
}
