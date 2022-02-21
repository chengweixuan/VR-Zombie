using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcAI : MonoBehaviour
{

    public Transform playerPoint;
    private NavMeshAgent theAgent;
    private Animator anim;
    private Rigidbody rb;
    private float velocity;
    private Vector3 previous;

    public Dialogue dialogueStart;
    public Dialogue dialogueTalk;
    public Dialogue dialoguePrompt;
    public Dialogue dialogueEnd;
    public Dialogue dialogueFinish;

    private int dialogindex;

    public Animator GetAnim
    {
        get
        {
            return anim;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        theAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        anim.SetBool("Grounded", true);

        theAgent.isStopped = false;
        theAgent.velocity = Vector3.zero;

        dialogindex = 1;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = ((transform.position - previous).magnitude) / Time.deltaTime;
        previous = transform.position;
        anim.SetFloat("Speed", velocity);

        theAgent.SetDestination(playerPoint.position);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (GameManager.Instance.isPressToTalk && !GameManager.Instance.isPressToPickUp)
            {
                if (dialogindex == 1)
                {
                    GameManager.Instance.StartDialog(dialogueStart);
                }
                else if (dialogindex >= 2 && dialogindex < 5)
                {
                    GameManager.Instance.StartDialog(dialogueTalk);
                }
                else if (dialogindex < 1)
                {
                    GameManager.Instance.StartDialog(dialogueFinish);
                }
                else
                {
                    GameManager.Instance.StartDialog(dialoguePrompt);
                }

                dialogindex++;
            }
            else if (GameManager.Instance.isTalking)
            {
                GameManager.Instance.DisplayNextSentence();
            }
        }

        if (GameManager.Instance.hasCalledHelp)
        {
            GameManager.Instance.isTalking = true;
            GameManager.Instance.isPressToTalk = false;
            GameManager.Instance.StartDialog(dialogueEnd);
            GameManager.Instance.hasCalledHelp = false;
            dialogindex = -100;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "FPSController")
        {

            GameManager.Instance.ShowPrompt("Press E to talk");
            GameManager.Instance.isPressToTalk = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "FPSController")
        {

            GameManager.Instance.StopPrompt();
        }
    }


}
