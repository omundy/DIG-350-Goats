using UnityEngine;

public class MapAudioManager : MonoBehaviour
{
    public static AudioSource playerMovementAudio;
    public static AudioSource collectItemAudio;

    void Awake(){
        AudioSource[] audioSources = GetComponents<AudioSource>();
        foreach (AudioSource source in audioSources)
        {
            if (source.clip != null && source.clip.name == "Player_Movement")
            {
                playerMovementAudio = source;
            }
            if (source.clip != null && source.clip.name == "Collect_Item")
            {
                collectItemAudio = source;
            }
        }
    }
    

}