using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    // The Player
    [SerializeField] private GameObject player;
    // Previous position of the player
    private Vector3 _previousPlayerPosition;

    // Target-related variables
    [SerializeField] private GameObject targetPrefab;
    private GameObject[] _targetPool;
    public int numberOfTargets;
    private int _numberOfHitTargets;

    private bool _isStarted;
    
    // Quantitative data
    private float _totalTime;
    private float[] _timeBetweenHits;
    private Stack<float> _movementSpeeds;
    // Used for measuring time between target hits
    private float _previousHitTime, _currentHitTime;
    private int _numberOfAccurateHits;

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
            //_targetPool[i] = Instantiate(targetPrefab);
            //_targetPool[i].SetActive(false);

            _timeBetweenHits[i] = 0.0f;
        }

        _numberOfHitTargets = 0;

        _previousPlayerPosition = player.transform.position;

        // Set the file path to store data
        _fileName = Application.dataPath + "/Data/data.csv";
    }

    private void Update()
    {
        // Don't do anything until the test has started
        if (!_isStarted)
        {
            // Start test is space is pressed
            if (Input.GetKey(KeyCode.Space))
            {
                StartTest();
            }
            return;
        }

        _totalTime += Time.deltaTime;
        
        //CalculatePlayerSpeed();

        if (_numberOfHitTargets == numberOfTargets)
        {
            Debug.Log("Hit all of the targets!");
            EndTest();
            _numberOfHitTargets = 0;
        }
        
        // If the test is started and space is pressed, end the test
        if (Input.GetKey(KeyCode.Space))
        {
            EndTest();
        }
    }

    public void StartTest()
    {
        Debug.Log("Test Started!");
        _isStarted = true;
    }

    public void RecordTimeBetweenHits(bool wasAccurate)
    {
        _numberOfHitTargets++;
        if (wasAccurate)
        {
            _numberOfAccurateHits++;
        }

        // For the first time through, only set the 'previous' time
        if (_numberOfHitTargets <= 1)
        {
            _previousHitTime = _totalTime;
            return;
        }

        _currentHitTime = _totalTime;
        _timeBetweenHits[_numberOfHitTargets--] = _currentHitTime - _previousHitTime;
    }

    public void CalculatePlayerSpeed()
    {
        // Calculate speed of the player
        var position = player.transform.position;
        var speed = (position - _previousPlayerPosition).magnitude / Time.deltaTime;
        _previousPlayerPosition = position;
        // Dont bother storing minuscule speeds
        if (speed > 1.0f)
        {
            _movementSpeeds.Push(speed);
        }
    }

    private void StoreData(float averageTime, float averageSpeed)
    {
        TextWriter textWriter = new StreamWriter(_fileName, false);
        // Headings
        textWriter.WriteLine("Average time between target hits, " +
                             "Total time to complete, " +
                             "Average movement speed, " +
                             "Accuracy");
        textWriter.Close();
        
        // Actual data
        textWriter = new StreamWriter(_fileName, true);
        textWriter.WriteLine(averageTime + ", " + 
                             _totalTime + ", " +
                             averageSpeed + ", " +
                             ((_numberOfAccurateHits / _numberOfHitTargets) * 100));
        textWriter.Close();
    }
    
    public void EndTest()
    {
        Debug.Log("Test Ended!");
        _isStarted = false;
        
        var averageTime = _timeBetweenHits.Sum() / _timeBetweenHits.Length;
        var averageSpeed = _movementSpeeds.Sum() / _movementSpeeds.Count;
        
        StoreData(averageTime, averageSpeed);
        Debug.Log(averageTime);
        Debug.Log(_totalTime);

        SceneManager.LoadScene("StartScreen");
    }
}
