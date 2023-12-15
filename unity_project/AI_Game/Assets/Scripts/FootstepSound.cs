using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public float stepDistance = 1.0f;  // �߼Ҹ� ��� ����
    public AudioClip footstepSound;    // �߼Ҹ� ����� Ŭ��

    private AudioSource audioSource;
    private Vector3 lastPosition;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            // AudioSource�� ������ ���� ���
            Debug.LogError("AudioSource component not found on the camera.");
        }

        lastPosition = transform.position;
    }

    void Update()
    {
        // �̵� �Ÿ� ����
        float distanceMoved = Vector3.Distance(transform.position, lastPosition);

        // �̵� �Ÿ��� ������ �Ӱ谪�� ������ �߼Ҹ� ���
        if (distanceMoved >= stepDistance)
        {
            PlayFootstepSound();
            lastPosition = transform.position; // ������ ��ġ ������Ʈ
        }
    }

    void PlayFootstepSound()
    {
        // AudioSource�� �����Ǿ� ������ �߼Ҹ� ���
        if (audioSource != null && footstepSound != null)
        {
            audioSource.PlayOneShot(footstepSound);
        }
    }
}
