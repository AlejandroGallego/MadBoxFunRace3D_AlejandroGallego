using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public float characterPosition = 0;
    public float nextCharacterPositionRoadPieceDistance = 0;
    public float characterSpeed = 1;

    public CarreteraSpawnBehaviour initialRoadStage;
    public CarreteraSpawnBehaviour currentRoadStage;

    public Transform mesh;
    public Transform transformer;

    public bool win = false;

    // Start is called before the first frame update
    public void Initialize()
    {
        characterPosition = 0;
        currentRoadStage = initialRoadStage;
        nextCharacterPositionRoadPieceDistance = 1;

        Reinitialize();

}

void Update()
    {
        if(!win) DefaultUpdate();
    }

    void DefaultUpdate()
    {
    
        if(Input.GetKey(KeyCode.Space))
        {
            characterPosition += characterSpeed * Time.deltaTime;

            if(currentRoadStage.displacementType == CarreteraSpawnBehaviour.DisplacementType.straight) transform.position += ((transformer.forward * Time.deltaTime * -1 * characterSpeed));
            else if(currentRoadStage.displacementType == CarreteraSpawnBehaviour.DisplacementType.curveLeft) transformer.Rotate(0, -90*Time.deltaTime * characterSpeed / currentRoadStage.roadLength, 0);
            else transformer.Rotate(0, 90*Time.deltaTime * characterSpeed / currentRoadStage.roadLength, 0);
        }

        if(characterPosition >= nextCharacterPositionRoadPieceDistance)
        {
            CarreteraSpawnBehaviour pieza = currentRoadStage.nextPiece.GetComponent<CarreteraSpawnBehaviour>();
            nextCharacterPositionRoadPieceDistance += pieza.roadLength;
            currentRoadStage = pieza;

            if(currentRoadStage.displacementType == CarreteraSpawnBehaviour.DisplacementType.final)
            {
                win = true;
                FindObjectOfType<SceneLogic>().Win();
            }

            transformer.position = pieza.displacementPoint.transform.position;
            transformer.rotation = pieza.displacementPoint.transform.rotation;

            mesh.position = pieza.transform.position;
            mesh.transform.rotation = pieza.transform.rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collider!!");

        if(other.tag == "Enemy")
        {
            Debug.Log("DIE!!!!");
            Reinitialize();

        }
    }

    private void Reinitialize()
    {
        characterPosition = 0;
        currentRoadStage = initialRoadStage;
        nextCharacterPositionRoadPieceDistance = 0;

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        mesh.position = Vector3.zero;
        mesh.rotation = Quaternion.Euler(Vector3.zero);
        transformer.position = Vector3.zero;
        transformer.rotation = Quaternion.Euler(Vector3.zero);
    }
}
