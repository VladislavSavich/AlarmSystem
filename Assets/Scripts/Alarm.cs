using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;

    private AudioSource _audioSource;
    private Coroutine _coroutine;
    private float _maximumVolum = 1.0f;
    private float _minimumVolum = 0f;
    private float _speed = 0.1f;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _audioClip;
        _audioSource.volume = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        _audioSource.Play();
        StartNewCorutine(ChangeVolum(_maximumVolum));
    }

    private void OnTriggerExit(Collider other)
    {
        StartNewCorutine(ChangeVolum(_minimumVolum));
    }

    private IEnumerator ChangeVolum(float targetVolum)
    {
        while (_audioSource.volume != targetVolum)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolum, _speed * Time.deltaTime); ;
            yield return null;
        }

        if(_audioSource.volume == _minimumVolum)
            _audioSource.Stop();
    }

    private void StartNewCorutine(IEnumerator enumerator)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(enumerator);
    }
}
