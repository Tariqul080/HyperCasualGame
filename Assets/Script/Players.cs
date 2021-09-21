using UnityEngine;

public class Players : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float CubePushSpeed;
    [SerializeField] private float CubeMaxPosX;
    [Space]
    [SerializeField] private TouchSllider touchSlider;
    private Cube MainCube;

    private bool isPointerDown;
    private Vector3 CubePositon;
    private bool canMove;

    private void Start()
    {
        SpawnCube();
        canMove = true;
        touchSlider.OnpointerDownEvent += OnPointerDown;
        touchSlider.OnpointerDragEvent += OnPointerDrag;
        touchSlider.OnpointerUpEvent += OnPointerUp;
    }
    private void Update()
    {
        if (isPointerDown)
        {
            MainCube.transform.position = Vector3.Lerp(MainCube.transform.position, CubePositon, MoveSpeed*Time.deltaTime);
        }
        
    }

    private void OnPointerDown()
    {
        isPointerDown = true;
    }
    private void OnPointerDrag(float value)
    {
        if (isPointerDown)
        {
            CubePositon = MainCube.transform.position;
            CubePositon.x = value * CubeMaxPosX;
        }
    }
    private void OnPointerUp()
    {
        if (isPointerDown && canMove)
        {
            isPointerDown = false;
            canMove = false;
            MainCube.CubeRigidbody.AddForce(Vector3.forward * CubePushSpeed, ForceMode.Impulse);
            Invoke("SpawnNewCube",0.3f);
        }
    }

    private void SpawnNewCube()
    {
        MainCube.IsMainCube = false;
        canMove = true;
        SpawnCube();
    }

    private void SpawnCube()
    {
        
        MainCube = CubeSpawner.Instance.SpawnRandom();
        MainCube.IsMainCube = true;
        CubePositon = MainCube.transform.position;


    }
    private void OnDisable()
    {
        touchSlider.OnpointerDownEvent -= OnPointerDown;
        touchSlider.OnpointerDragEvent -= OnPointerDrag;
        touchSlider.OnpointerUpEvent -= OnPointerUp;
    }






}
