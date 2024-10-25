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
        playerActions = inputActions.FindActionMap("Player"); // �����㴴���Ķ���ӳ���������޸�����

        qKeyAction = playerActions.FindAction("q"); // ��������Input Actions�д����Ķ����������޸�����

        // ����Q���İ��º��ͷ��¼�
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
