using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2 : MonoBehaviour
{
    bool wudiTime = false;
    // Start is called before the first frame update
    public InputActionAsset inputActions;
    private InputActionMap playerActions;
    private InputAction qKeyAction;
    private void Awake()
    {
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hurt()
    {
        if (!wudiTime)
        {
            wudiTime = true;
            GetComponent<ThirdPersonController>().enabled=false;
            GetComponent<Animator>().SetTrigger("behurt");
            StartCoroutine(zhanli());
        }
    }

    IEnumerator zhanli()
    {
        yield return new WaitForSeconds(2);
        GetComponent<ThirdPersonController>().enabled = true;
        wudiTime=false;
    }
}
