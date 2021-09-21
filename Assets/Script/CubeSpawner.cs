using UnityEngine;
using System.Collections.Generic;


public class CubeSpawner : MonoBehaviour
{
    public static CubeSpawner Instance;
    Queue<Cube> cubesQuene = new Queue<Cube>();
    [SerializeField] private int cubesQueueCapacity = 20;
    [SerializeField] private bool autoQueueGrow = true;

    [SerializeField] GameObject cubePrefed;
    [SerializeField] private Color[] cubeColor;

    [HideInInspector] public int maxCubeNumber;

    private int maxPower = 12;

    private Vector3 defaultSpawnPosition;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        defaultSpawnPosition = transform.position;
        maxCubeNumber = (int)Mathf.Pow(2, maxPower);
        InitializeCubesQuene();
    }

    private void InitializeCubesQuene()
    {
        for (int i = 0; i < cubesQueueCapacity; i++)
        {
            AddCubeToQueue();
        }
    }
    private void AddCubeToQueue()
    {
        Cube cube = Instantiate(cubePrefed, defaultSpawnPosition, Quaternion.identity, transform).GetComponent<Cube>();
        cube.gameObject.SetActive(false);
        cube.IsMainCube = false;
        cubesQuene.Enqueue(cube);
    }
    public Cube Spawn(int number, Vector3 Position)
    {
        if (cubesQuene.Count == 0)
        {
            if (autoQueueGrow)
            {
                cubesQueueCapacity++;
                AddCubeToQueue();
            }
            else
            {
                Debug.LogError("No more cube availab fo this event");
                return null;
            }
        }
        Cube cube = cubesQuene.Dequeue();
        cube.transform.position = Position;
        cube.SetNumbe(number);
        cube.SetColor(GetColor(number));
        cube.gameObject.SetActive(true);
        return cube;
    }
    public Cube SpawnRandom()
    {
        return Spawn(GenerateRandomNumber(), defaultSpawnPosition);
    }

    public void DestroyCube(Cube cube)
    {
        cube.CubeRigidbody.velocity = Vector3.zero;
        cube.CubeRigidbody.angularVelocity = Vector3.zero;
        cube.transform.rotation = Quaternion.identity;
        cube.gameObject.SetActive(false);
        cubesQuene.Enqueue(cube);
    }


    public int GenerateRandomNumber()
    {
        return (int)Mathf.Pow(2, Random.Range(1, 6));

    }
    private Color GetColor(int number)
    {
        return cubeColor[(int)(Mathf.Log(number) / Mathf.Log(2)) - 1];
    }


}


