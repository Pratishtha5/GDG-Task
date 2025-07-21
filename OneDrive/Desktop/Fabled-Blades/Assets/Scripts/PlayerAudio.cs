using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip jumpClip;
    public AudioClip swordClip;
    public AudioClip collect;

    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpClip);
    }

    public void PlaySwordSound()
    {
        audioSource.PlayOneShot(swordClip);
    }
    public void PlayCollectSound()
    {
        audioSource.PlayOneShot(collect);
    }

}
