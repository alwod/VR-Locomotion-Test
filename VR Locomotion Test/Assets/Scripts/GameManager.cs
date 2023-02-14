using System.Linq;
using UnityEngine;
using System.IO;

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
    
    // File path for storing quantitative data as a csv file
    private string _fileName = "";

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


        // Set the file path to store data
        _fileName = Application.dataPath + "/Data/data.csv";
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

    private void StoreData(float averageTime)
    {
        TextWriter textWriter = new StreamWriter(_fileName, false);
        // Headings
        textWriter.WriteLine("Average time between target hits, Total time to complete");
        textWriter.Close();
        
        // Actual data
        textWriter = new StreamWriter(_fileName, true);
        textWriter.WriteLine(averageTime + ", " + _totalTime);
        textWriter.Close();
    }
    
    public void EndTest()
    {
        _isStarted = false;
        
        var averageTime = _timeBetweenHits.Sum() / _timeBetweenHits.Length;
        StoreData(averageTime);
        Debug.Log(averageTime);
        Debug.Log(_totalTime);
    }
}
