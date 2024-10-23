using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    [SerializeField]
    private GameObject yellowBallProfabObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") == true)
        {
            Destroy(other);
            Debug.Log("the ball has been destroyed.");

            if (yellowBallProfabObj != null)
            {
                GameObject newball = Instantiate(yellowBallProfabObj);
                newball.transform.position = new Vector3(0f,1f,0f);
            }

        }

    }
}
