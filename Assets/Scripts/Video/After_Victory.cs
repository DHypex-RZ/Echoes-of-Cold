using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Video;


public class After_Victory : MonoBehaviour
{
    public VideoClip video;
    public float cinematic_length;
    public float t;

    public UnityEvent Activate;
    public UnityEvent Deactivate;


    void Start()
    {
        cinematic_length = (float)video.length;
    }

    void Update()
    {
        t += Time.deltaTime;

        if (t >= cinematic_length)
        {
            Activate.Invoke();
            Deactivate.Invoke();
            Destroy(this);
        }
    }
}