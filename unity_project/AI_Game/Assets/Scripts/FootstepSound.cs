using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public float stepDistance = 1.0f;  // 발소리 재생 간격
    public AudioClip footstepSound;    // 발소리 오디오 클립

    private AudioSource audioSource;
    private Vector3 lastPosition;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            // AudioSource가 없으면 에러 출력
            Debug.LogError("AudioSource component not found on the camera.");
        }

        lastPosition = transform.position;
    }

    void Update()
    {
        // 이동 거리 측정
        float distanceMoved = Vector3.Distance(transform.position, lastPosition);

        // 이동 거리가 설정한 임계값을 넘으면 발소리 재생
        if (distanceMoved >= stepDistance)
        {
            PlayFootstepSound();
            lastPosition = transform.position; // 마지막 위치 업데이트
        }
    }

    void PlayFootstepSound()
    {
        // AudioSource가 설정되어 있으면 발소리 재생
        if (audioSource != null && footstepSound != null)
        {
            audioSource.PlayOneShot(footstepSound);
        }
    }
}
