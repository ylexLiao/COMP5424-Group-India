using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMgr : MonoBehaviour
{

    // Start is called before the first frame update
    public InputActionAsset inputActions;
    private InputActionMap playerActions;
    private InputAction qKeyAction;
    public static GameMgr Instance;
    public List<Player2> players = new List<Player2>();
    public GameObject npcPrefab;
    public GameMgr() { 
        Instance = this;
    }
    private void Awake()
    {
        playerActions = inputActions.FindActionMap("Player"); // 根据你创建的动作映射名称来修改这里

        qKeyAction = playerActions.FindAction("q"); // 根据你在Input Actions中创建的动作名称来修改这里

        // 监听Q键的按下和释放事件
        qKeyAction.Enable();
        qKeyAction.performed += v => { 
            Instantiate(npcPrefab);
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (qKeyAction.ReadValue<float>() > 0)
        {
            
        }
    }
}
