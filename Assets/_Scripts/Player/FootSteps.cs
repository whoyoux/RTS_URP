using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FootSteps : MonoBehaviour
{
    private AudioSource audioSource;

    public LayerMask terrainLayerMask;

    [Header("SFX")]
    public AudioClip[] groundFootsteps;
    public AudioClip[] grassFootsteps;

    private NavMeshAgent agent;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
    }
    private AudioClip[] DetermineAudioClips()
    {
        if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0f), Vector3.down, out RaycastHit hit, 1.5f, terrainLayerMask))
        {
            string tag = hit.collider.tag;

            switch (tag)
            {
                case "Ground":
                    return groundFootsteps;
                case "Grass":
                    return grassFootsteps;
                default:
                    return groundFootsteps;
            }
        }
        else
        {
            return groundFootsteps;
        }
    }


    public void Step(AnimationEvent animationEvent)
    {
        if(animationEvent.animatorClipInfo.weight <= 0.5f) return;

        if (!audioSource || agent.velocity.magnitude < 0.1f) return;

        AudioClip[] clips = DetermineAudioClips();
        if (clips.Length > 0)
        {
            AudioClip clip = clips[UnityEngine.Random.Range(0, clips.Length)];
            audioSource.PlayOneShot(clip);
        }
    }
}
