using UnityEngine;

public class FXScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem cubeExplosinFX;

    ParticleSystem.MainModule cubeExplosionFXMainModule;

    public static FXScript Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        cubeExplosionFXMainModule = cubeExplosinFX.main;
    }
    
    public void PlayCubeExlosionFX(Vector3 position, Color color)
    {
        cubeExplosionFXMainModule.startColor = new ParticleSystem.MinMaxGradient(color);
        cubeExplosinFX.transform.position = position;
        cubeExplosinFX.Play();
    }
      

}
