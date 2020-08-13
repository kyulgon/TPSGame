using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string moveHorizontalAxisName = "Horizontal";
    public string moveVerticalAxisName = "Vertical";

    public string fireButtonName = "Fire1";
    public string jumpButtonName = "Jump";
    public string reloadButtonName = "Reload";


    // 읽기만 가능하고 다른 곳에서 수정 불가능
    public Vector2 moveInput { get; private set; }
    public bool fire { get; private set; }
    public bool reload { get; private set; }
    public bool jump { get; private set; }

        
    void Update()
    {
        if(GameManager.Instance != null && GameManager.Instance.isGameover) // 게임매니저가 존재하거나 게임오버일 때
        {
            // 유저 입력을 무시
            moveInput = Vector2.zero;
            fire = false;
            reload = false;
            jump = false;
            return;
        }

        // 
        moveInput = new Vector2(Input.GetAxis(moveHorizontalAxisName), Input.GetAxis(moveVerticalAxisName));

        if (moveInput.sqrMagnitude > 1) // 1보다 큰 moveInput를 잘라줘 크기를 정규화줌
            moveInput = moveInput.normalized;

        // 각 버튼을 누를때 bool값을 준다
        jump = Input.GetButtonDown(jumpButtonName);
        fire = Input.GetButton(fireButtonName);
        reload = Input.GetButtonDown(reloadButtonName);
    }
}
