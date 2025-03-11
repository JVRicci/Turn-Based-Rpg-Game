using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBattleScript : MonoBehaviour {

    public GameObject BattleController;
    public float moveSpeed = 25f;       // Velocidade do movimento
    public float attackWaitTime = 0.5f; // Tempo de espera antes de voltar
    public Transform playerObject;  // O transform do jogador
    private Vector2 playerStartPosition;     // Guarda a posição inicial do jogador


    // Modificar isso para receber mais de um inimigo
    private void Start() {
        playerStartPosition = playerObject.position; // Guarda a posição inicial do player
    }

    
    public void OnAttackButton()
    {
        if (GameBattleControllerScript.instance.state != BattleState.PLAYER_TURN)
            return;

        StartCoroutine(PlayerMeleeAttack());
        
        GameBattleControllerScript.instance.playerOptions.SetActive(false);

    }


    IEnumerator PlayerMeleeAttack()
    {
        
        // Aplica dano ao inimigo
        GameBattleControllerScript.instance.BattleInput.text += "\nO jogador atacou!";

        Transform enemyObject = GameBattleControllerScript.instance.enemyObject;

         // Move o jogador até o inimigo
        //  O vector2 eu utilizei para que o player não chhegasse totalmente até o inimigo
        yield return StartCoroutine(MoveToPosition(playerObject, new Vector2(enemyObject.position.x - 2, enemyObject.position.y)));

        // Simula o ataque (adiciona um delay antes de voltar)
        yield return new WaitForSeconds(attackWaitTime);

        // Move o jogador de volta para a posição original
        yield return StartCoroutine(MoveToPosition(playerObject, playerStartPosition));

        // Inicia Turno do inimigo
        GameBattleControllerScript.instance.EnemyTurn();
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