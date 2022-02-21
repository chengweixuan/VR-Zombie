using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkieTalkie : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (GameManager.Instance.isPressToPickUp)
            {
                GameManager.Instance.hasTalkie = true;
                GameManager.Instance.ShowPrompt("Press Q to call for help");
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "FPSController")
        {

            GameManager.Instance.ShowPrompt("Press E to pick up");
            GameManager.Instance.isPressToPickUp = true;
        }
    }
}
