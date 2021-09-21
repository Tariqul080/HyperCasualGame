using UnityEngine;
using TMPro;

public class Cube : MonoBehaviour
{
    static int StaticID = 0;
    [SerializeField] private TMP_Text[] NumbersText;

    [HideInInspector] public int cubeID;
    [HideInInspector] public Color CubeColor;
    [HideInInspector] public int CubeNumber;
    [HideInInspector] public Rigidbody CubeRigidbody;
    [HideInInspector] public bool IsMainCube;

    private MeshRenderer CubeMeshRenderer;
    private void Awake()
    {
        cubeID = StaticID++;
        CubeMeshRenderer = GetComponent<MeshRenderer>();
        CubeRigidbody = GetComponent<Rigidbody>();
    }
    public void SetColor(Color color)
    {
        CubeColor = color;
        CubeMeshRenderer.material.color = color;

    }
    public void SetNumbe(int number)
    {
        CubeNumber = number;
        for (int i = 0; i < 6; i++)
        {
            NumbersText[i].text = number.ToString();
        }
    }
}
