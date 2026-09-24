using UnityEngine;

public class SetBoolInEvents : MonoBehaviour
{
    public Animator animator;
    

    public void ChangeBoolValueAxe(bool state)
    {
        if (animator != null)
        {
            animator.SetBool("AxeCollect", state);
        }
    }

    public void ChangeBoolValueMusicBox(bool state)
    {
        if (animator != null)
        {
            animator.SetBool("MusicBoxCollect", state);
        }
    }
}
