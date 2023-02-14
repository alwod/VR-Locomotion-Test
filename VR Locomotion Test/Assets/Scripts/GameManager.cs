using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Target-related variables
    [SerializeField] private GameObject targetPrefab;
    private GameObject[] _targetPool;
    public int numberOfTargets;
    public int numberOfHitTargets = 0;

    // Quantitative data
    private float _timeToComplete = 0;
    private float _timeBetweenHits = 0;
    private float _averageMovementSpeed = 0;

    private void Awake()
    {
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
        
    }
}
