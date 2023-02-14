using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Target-related variables
    [SerializeField] private GameObject targetPrefab;
    private GameObject[] _targetPool;
    public int numberOfTargets;
    public int numberOfHitTargets = 0;

    // Quantitative data
    private float _timeToComplete;
    private float _timeBetweenHits;
    private float _averageMovementSpeed;
    private bool _isStarted = false;

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
        _timeToComplete = 0;
        _timeBetweenHits = 0;
        _averageMovementSpeed = 0;
        
        _targetPool = new GameObject[numberOfTargets];
        for (var i = 0; i < _targetPool.Length; i++)
        {
            _targetPool[i] = Instantiate(targetPrefab);
            _targetPool[i].SetActive(false);
        }
    }
    
    private void Start()
    {
        
    }
    
    private void Update()
    {
        // If the test has started, increase the _timeToComplete variable until test is finished.
        if (_isStarted)
        {
            _timeToComplete += Time.deltaTime;
        }
        
        if (numberOfHitTargets == numberOfTargets)
        {
            Debug.Log("Hit all of the targets!");
            numberOfHitTargets = 0;
        }
        
    }

    private void StartTest()
    {
        _isStarted = true;
    }

    private void RecordTimeBetweenHits()
    {
        
    }

    private void EndTest()
    {
        _isStarted = false;
    }

}
