using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarreteraSpawnBehaviour : MonoBehaviour
{
    public GameObject[] roadInstanceType;
    public int[] instanceChanceMultipler;

    public enum DisplacementType { straight, curveRight, curveLeft, final };
    public DisplacementType displacementType;

    [Range(1,3)]
    public int roadLength = 1;

    public GameObject roadFinish;

    public Transform instancePoint;
    public Transform displacementPoint;

    public GameObject nextPiece;

    public void Initialize()
    {
        if(displacementType != DisplacementType.final)
        {
            //Busca la lógica de la escena
            SceneLogic logic = FindObjectOfType<SceneLogic>();

            //Si según la lógica, aún quedan piezas por poner, ejecuta la lógica para ponerlas
            if(logic.roadLongitudeRemaining > 0)
            {
                //Le reduce una pieza al indicador del logic
                logic.roadLongitudeRemaining -= roadLength;

                ///
                ///A continuación se inicia un proceso mediante el cual se instancia una pieza aleatoria, calculandola a través de un valor complejo
                ///

                //Mediante este indice se generará una instancia posteriormente, cambiando este valor
                int indiceInstancia = 0;

                //Antes de crear un valor aleatorio para elegir la pieza, hay que establecer quan alto puede ser ese valor
                int maxRandomValue = 0;
                for(int i = 0; i < instanceChanceMultipler.Length; i++)
                {
                    maxRandomValue += instanceChanceMultipler[i];
                }

                //Se crea un valor aleatorio entre 0 y el porcentaje máximo designado a cada pieza
                int randomValue = Random.Range(0, maxRandomValue);

                //Este valor simplemente almacena el recorrido del array por los porcentajes
                int lastValueChecked = 0;

                //Comparamos si el valor aleatorio coincide con el porcentaje asignado a alguna pieza
                for(int i = 0; i < instanceChanceMultipler.Length; i++)
                {
                    if((randomValue < instanceChanceMultipler[i] + lastValueChecked) && (randomValue >= lastValueChecked)) indiceInstancia = i;

                    lastValueChecked += instanceChanceMultipler[i];
                }

                //Se procede a instanciar la pieza
                GameObject instance = Instantiate(roadInstanceType[indiceInstancia], instancePoint.position, instancePoint.rotation);
                nextPiece = instance;
                instance.GetComponent<CarreteraSpawnBehaviour>().Initialize();

            }

            //Por el contrario, si no quedan piezas por poner en la carretera, esta se cierra
            else
            {
                GameObject instance = Instantiate(roadFinish, instancePoint.position, instancePoint.rotation);
                nextPiece = instance;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collider!!");

        if(other.tag == "Road")
        {
            FindObjectOfType<SceneLogic>().Reinitialize();
            Debug.Log("REINITIALIZED!!!!");
        }
    }
}
