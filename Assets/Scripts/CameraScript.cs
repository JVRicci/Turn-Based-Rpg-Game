using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public Transform player;
    // Necessário ajustar manualmente a posição da camera no inspector
    public Vector3 offset;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Ajusta automaticamente o offset com base na posição inicial
        offset = transform.position - player.position;

        transform.position = player.position + offset;
    }

    // LateUpdate é executado sempre após o evento update
    void LateUpdate()
    {
        // Camera segue o player mantendo deslocamneto fixo
        transform.position = player.position + offset;
    }
}
