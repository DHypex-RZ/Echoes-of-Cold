using UnityEngine;

public class FootSteps_Sounds : MonoBehaviour
{
    public AudioSource Walk, Sprint;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Sprint.enabled = true;
                Walk.enabled = false;
            }
            else
            {
                Sprint.enabled = false;
                Walk.enabled = true;
            }
        }
        else
        {
            Sprint.enabled = false;
            Walk.enabled = false;
        }
    }
}
