using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missions : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 미션 버튼을 누르면 호출
    public void OnMissionButtonClick() {
        if (gameObject.activeSelf == false) {
            UIManager.instance.PanelDeactivation();
            gameObject.SetActive(true);
        }
    }

    // X 버튼을 누르면 호출
    public void OnCloseButtonClick() {
        gameObject.SetActive(false);
    }
}
