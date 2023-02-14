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
    //TODO change _timeToComplete to instead represent the total time elapsed.
    private float _timeToComplete;
    private float[] _timeBetweenHits;
    //private float _averageMovementSpeed;
    private bool _isStarted;

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
        
        _timeToComplete += Time.deltaTime;
        
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

    public void RecordTimeBetweenHits()
    {
        
    }

    private void EndTest()
    {
        _isStarted = false;
        
        Debug.Log(_timeToComplete);
    }

}
