using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private Detector _enemyDetector;
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

    private void OnEnable()
    {
        _enemyDetector.EnemyEntered += IncreaseVolume;
        _enemyDetector.EnemyExited += DecreaseVolume;
    }

    private void OnDisable()
    {
        _enemyDetector.EnemyEntered -= IncreaseVolume;
        _enemyDetector.EnemyExited -= DecreaseVolume;
    }

    private void IncreaseVolume() 
    {
        _audioSource.Play();
        StartNewCorutine(ChangeVolum(_maximumVolum));
    }

    private void DecreaseVolume() 
    {
        StartNewCorutine(ChangeVolum(_minimumVolum));
    }

    private IEnumerator ChangeVolum(float targetVolum)
    {
        while (!Mathf.Approximately(_audioSource.volume, targetVolum))
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolum, _speed * Time.deltaTime); ;
            yield return null;
        }

        if(Mathf.Approximately(_audioSource.volume, _minimumVolum))
            _audioSource.Stop();
    }

    private void StartNewCorutine(IEnumerator enumerator)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(enumerator);
    }
}
