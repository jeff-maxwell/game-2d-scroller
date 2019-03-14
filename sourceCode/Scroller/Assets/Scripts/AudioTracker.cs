using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTracker : MonoBehaviour
{
    public GameObject player;
    public float TrackDist;
    public float MaxVolume;

    private AudioSource audioSource;
    private float distance;
    private float distanceNorm;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        audioSource.volume = 0f;
        audioSource.loop = true;
        audioSource.panStereo = -1;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Mathf.Abs((gameObject.transform.position.x - player.transform.position.x));
    }

    private void FixedUpdate()
    {
              
        if (( distance < TrackDist) && (distance != 0))
        {
            distanceNorm = (distance - player.transform.position.x) / (gameObject.transform.position.x - player.transform.position.x);

            if ((distanceNorm < 0) && (Mathf.Abs(distanceNorm) < MaxVolume))
            {
                audioSource.volume = Mathf.Abs(distanceNorm);
            }else {
                audioSource.volume = MaxVolume;
            }
            /*
            if (Mathf.Abs(1 - distanceNorm) > MaxVolume)
            {
                audioSource.volume = MaxVolume;
            }
            else
            {
                audioSource.volume = 1 - distanceNorm;
            }
             */
            
            audioSource.panStereo = 0;
        }
    }

}
