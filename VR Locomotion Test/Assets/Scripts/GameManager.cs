using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    // The Player
    [SerializeField] private GameObject _player;

    // Target-related variables
    [SerializeField] private GameObject targetPrefab;
    private GameObject[] _targetPool;
    public int numberOfTargets;
    public int numberOfHitTargets = 0;

    // Quantitative data
    private float _totalTime;
    private float[] _timeBetweenHits;
    //private float _averageMovementSpeed;
    private bool _isStarted;
    // Used for measuring time between target hits
    private float _previousHitTime, _currentHitTime;

    private void Awake()
    {
        // Make sure there's only 1 game manager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Set up
        _totalTime = 0;
        _isStarted = false;

        _targetPool = new GameObject[numberOfTargets];
        _timeBetweenHits = new float[numberOfTargets];
        for (var i = 0; i < numberOfTargets; i++)
        {
            _targetPool[i] = Instantiate(targetPrefab);
            _targetPool[i].SetActive(false);

            _timeBetweenHits[i] = 0.0f;
        }
    }
    
    private void Start()
    {
        
    }
    
    private void Update()
    {
        // Don't do anything until the test has started
        if (!_isStarted)
        {
            return;
        }
        
        _totalTime += Time.deltaTime;
        
        
        
        
        
        if (numberOfHitTargets == numberOfTargets)
        {
            Debug.Log("Hit all of the targets!");
            numberOfHitTargets = 0;
        }
        
    }

    public void StartTest()
    {
        _isStarted = true;
    }

    public void RecordTimeBetweenHits()
    {
        numberOfHitTargets++;

        // For the first time through, only set the 'previous' time
        if (numberOfHitTargets <= 1)
        {
            _previousHitTime = _totalTime;
            return;
        }

        _currentHitTime = _totalTime;
        _timeBetweenHits[numberOfHitTargets--] = _currentHitTime - _previousHitTime;
    }

    public void EndTest()
    {
        _isStarted = false;
        
        var averageTime = _timeBetweenHits.Sum() / _timeBetweenHits.Length;
        Debug.Log(averageTime);
        Debug.Log(_totalTime);
    }

}
