using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enamy : MonoBehaviour
{
    [SerializeField] private float walk = 2.9f;
    [SerializeField] private float walkSpeed = 1f;
    [SerializeField] private float timeToWait = 1f;
    [SerializeField] private float timeToChase = 1f;
    [SerializeField] private float chaseSpeed = 1.5f;
    [SerializeField] private float minDistaceToPlayer = 4f;

    private Rigidbody2D rb;
    private Transform _playerTransofrm;

    private Vector2 leftPos;
    private Vector2 rightPos;
    private Vector2 nextPoint;

    private bool _isFacingRight = true;
    private bool _isWait = false;
    private bool _isChasingPlayer;
    private float _waitTime;
    private float _chaseTime;
    private float _walkSpeedS;

    public bool isFacingRight
    {
        get => _isFacingRight;
    }

    void Start()
    {
        _waitTime = timeToWait;
        rb = GetComponent<Rigidbody2D>();
        _playerTransofrm = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        leftPos = transform.position;
        rightPos = leftPos + Vector2.right * walk;
        _chaseTime = timeToChase;
        _walkSpeedS = walkSpeed;
    }

    public void StartChasingPlayer()
    {
        _isChasingPlayer = true;
        _chaseTime = timeToChase;
        _walkSpeedS = chaseSpeed;
    }

    private void Update()
    {
        if (_isChasingPlayer)
        {
            StartChasingTimer();
        }

        if (_isWait && !_isChasingPlayer) { 
            Wait();
        }

        EnemyWait();
    }

    private void StartChasingTimer()
    {
        _chaseTime -= Time.deltaTime;
        if (_chaseTime < 0f)
        {
            _isChasingPlayer = false;
            _chaseTime = timeToChase;
            _walkSpeedS = walkSpeed;
        }
    }

    private void Wait()
    {
       
        _waitTime -= Time.deltaTime;
        if (_waitTime < 0f)
        {
            _waitTime -= timeToWait;
            _isWait = false;
            Flip();
        }
    }

    private void EnemyWait()
    {
        bool isOutOfRight = _isFacingRight && transform.position.x >= rightPos.x;
        bool isOutOfLeft = !_isFacingRight && transform.position.x <= leftPos.x;
        if (isOutOfRight || isOutOfLeft)
        {
            _isWait = true;
        }
    }

    void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }

    private void FixedUpdate()
    {
        nextPoint = Vector2.right * walkSpeed * Time.fixedDeltaTime;
        if (_isChasingPlayer && Mathf.Abs(_playerTransofrm.position.x - transform.position.x) < minDistaceToPlayer)
        {
            return;
        }
       
        if(_isChasingPlayer)
        {
            ChasingPlayer();
        }

        if (!_isWait && !_isChasingPlayer)
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if (!_isFacingRight)
        {
            nextPoint.x *= -1;
        }

        rb.MovePosition((Vector2)transform.position + nextPoint);
    }

    private void ChasingPlayer()
    {
        float distance = _playerTransofrm.position.x - transform.position.x;
       
        if (distance < 0)
        {
            nextPoint.x *= -1;
        }

        if(distance > 0.2f && !_isFacingRight)
        {
            Flip();
        } 
        else if(distance < 0.2f && _isFacingRight)
        {
            Flip();
        }
             
        rb.MovePosition((Vector2)transform.position + nextPoint);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(leftPos, rightPos);
    }
}
