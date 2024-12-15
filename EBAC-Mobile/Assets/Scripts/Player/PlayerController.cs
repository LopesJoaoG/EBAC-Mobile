using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;
using TMPro;
using DG.Tweening;

public class PlayerController : Singleton<PlayerController>
{
    // publics
    [Header("Controller")]
    public float speed = 1f;
    public string compareTagEnemy = "Enemy";
    public string compareTagEndLine = "EndLine";
    public GameObject startScreen;
    public GameObject endScreen;
    public bool invencible = false;
    public TextMeshPro uiTextPowerUp;


    [Header("Coin Setup")]
    public GameObject coinCollector;

    [Header("Animation")]
    public AnimatorManager animatorManager;

    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 1f;

    // privates
    private bool _canRun;
    private Vector3 _pos;
    private Vector3 _startPosition;
    private float _currentSpeed;
    private float _baseSpeedAnimation = 0f;
    private void Start()
    {
        _startPosition = transform.position;
        ResetSpeed();
        ResetHeight();
    }

    void Update()
    {
        if (!_canRun) return;

        _pos = target.position;

        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        transform.Translate(transform.forward * _currentSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == compareTagEnemy)
        {
            if (!invencible)
            {
                MoveBack();
                EndGame(AnimatorManager.AnimationType.DEATH);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == compareTagEndLine)
        {
            EndGame();
        }
    }

    private void EndGame(AnimatorManager.AnimationType type = AnimatorManager.AnimationType.IDLE)
    {

        endScreen.SetActive(true);
        _canRun = false;
        animatorManager.Play(type);
    }

    public void StartGame()
    {

        startScreen.SetActive(false);
        _canRun = true;
        animatorManager.Play(AnimatorManager.AnimationType.RUN, _currentSpeed / _baseSpeedAnimation);
        
    }

    public void MoveBack()
    {
        transform.DOMoveZ(-1f, .3f).SetRelative();
    }

    #region POWERUPS

    public void SetPowerUpText(string s)
    {
        uiTextPowerUp.text = s;
    }
    public void PowerUpSpeedUp(float f)
    {
        _currentSpeed = f;
    }
    public void ResetSpeed()
    {
        _currentSpeed = speed;
    }

    public void SetInvencible(bool b)
    {
        invencible = b;
    }

    public void ChangeHeight(float amount, float duration, float animationDuration, Ease ease)
    {
        transform.DOMoveY(_startPosition.y + amount, animationDuration).SetEase(ease);//.OnComplete(ResetHeight);a
        Invoke(nameof(ResetHeight), duration);

    }
    public void ResetHeight()
    {
        transform.DOMoveY(_startPosition.y, .1f);
        SetPowerUpText("");
    }

    public void ChangeCoinCollectorSize(float amount)
    {
        coinCollector.transform.localScale = Vector3.one * amount;
    }
    #endregion
}
