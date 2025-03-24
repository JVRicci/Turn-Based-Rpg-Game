using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class EnemyBattleScript : MonoBehaviour
{
    public float attackWaitTime = .5f;
    public float moveSpeed = 25f;
    public TMP_Text BattleInput; 
    public Transform playerPosition;
    public Transform enemyTransform ;
    public Vector2 enemyStartPosition;     // Guarda a posição inicial do inimigo



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyTransform = GetComponent<Transform>();
        enemyStartPosition = enemyTransform.position; // Guarda a posição inicial do player
    }

    
    public IEnumerator EnemyAttack()
    {
        yield return new WaitForSeconds(attackWaitTime + .5f);

        GameBattleControllerScript.instance.BattleInput.text += "\nO inimigo atacou!";

        // Simula o ataque (adiciona um delay antes de voltar)
        yield return StartCoroutine(MoveToPosition(enemyTransform, new Vector2(playerPosition.position.x + 2, playerPosition.position.y)));

        yield return new WaitForSeconds(attackWaitTime);

        yield return StartCoroutine(MoveToPosition(enemyTransform, enemyStartPosition));
        // Espera um frame para garantir que a interface seja atualizada antes de continuar
        yield return new WaitForSeconds(1f);
        
        GameBattleControllerScript.instance.PlayerTurn();
    }

    IEnumerator MoveToPosition(Transform obj, Vector2 target)
    {
        while ((Vector2)obj.position != target)
        {
            obj.position = Vector2.MoveTowards(obj.position, target, moveSpeed * Time.deltaTime);

            yield return null; // Espera o próximo frame

        }
    }
}
