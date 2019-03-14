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

    
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        audioSource.volume = 0f;
        audioSource.loop = true;
        audioSource.panStereo = -1;
    }

    void Update()
    {
        // Calculate the distance between the Game Object attached to script
        // and the player
        distance = Mathf.Abs((gameObject.transform.position.x - player.transform.position.x));
    }

    private void FixedUpdate()
    {
              
        if (( distance < TrackDist) && (distance != 0))
        {
            distanceNorm = (distance - player.transform.position.x) / (gameObject.transform.position.x - player.transform.position.x);

            // As the player gets closer to the object associated to the script
            // increase the volume of the audio
            if ((distanceNorm < 0) && (Mathf.Abs(distanceNorm) < MaxVolume))
            {
                audioSource.volume = Mathf.Abs(distanceNorm);
            }else {
                audioSource.volume = MaxVolume;
            }
            
            audioSource.panStereo = 0;
        }
    }

}
