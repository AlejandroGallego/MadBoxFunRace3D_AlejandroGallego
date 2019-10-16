using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLogic : MonoBehaviour
{
    public int roadLongitude = 12;
    [HideInInspector]
    public int roadLongitudeRemaining;

    public CarreteraSpawnBehaviour firstRoad;

    public Camera mainCamera;
    public Transform cameraTargetPosition;
    public Transform cameraTargetLookAt;
    public float cameraSmooth;

    public GameObject winCanvas;

    // Start is called before the first frame update
    void Start()
    {

        winCanvas.SetActive(false);
        mainCamera = Camera.main;

        roadLongitudeRemaining = roadLongitude;
        firstRoad = FindObjectOfType<CarreteraSpawnBehaviour>();
        firstRoad.Initialize();

        FindObjectOfType<CharacterBehaviour>().Initialize();
    }

    public void Reinitialize()
    {
        roadLongitudeRemaining = roadLongitude;

        CarreteraSpawnBehaviour[] carreteras = FindObjectsOfType<CarreteraSpawnBehaviour>();
        foreach(CarreteraSpawnBehaviour carretera in carreteras)
        {
            if(carretera != firstRoad) Destroy(carretera.gameObject);
        }

        firstRoad.Initialize();

        FindObjectOfType<CharacterBehaviour>().Initialize();
    }

    void Update()
    {
        mainCamera.transform.LookAt(cameraTargetLookAt);
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cameraTargetPosition.position, cameraSmooth * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(0);
    }

    public void Win()
    {
        winCanvas.SetActive(true);
    }
}
