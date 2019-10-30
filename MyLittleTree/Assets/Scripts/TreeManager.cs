using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public GameObject treeMenu;
    private bool menuOpen = false;
	private bool isOn = false;

    void Start()
    {
        
    }

    void Update()
    {
        MenuAnimation();
    }

    private void MenuAnimation()        //메뉴가 커졌다 작아졌다 하면서 나오는 연출을 위한 함수
    {
        if (menuOpen && treeMenu.transform.localScale.x < 1)
            treeMenu.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        if (!(menuOpen) && treeMenu.transform.localScale.x > 0.0f)
            treeMenu.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
        if (treeMenu.transform.localScale.x <= 0 && isOn)
		{
			treeMenu.SetActive(false);
			isOn = false;
		}
            
    }

    public void MenuTouched()       //나무를 터치 했을 때 메뉴를 여는 함수
    {
		treeMenu.transform.localScale = new Vector3(0, 0, 0);
		treeMenu.SetActive(true);
        menuOpen = true;
		isOn = true;
    }
    public void MenuClosed()        //메뉴를 닫기 했을 때 실행되는 함수
    {
        menuOpen = false;
    }
}
