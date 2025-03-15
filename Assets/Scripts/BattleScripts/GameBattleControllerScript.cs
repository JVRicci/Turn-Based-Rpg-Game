using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYER_TURN, ENEMY_TURN, WIN, LOSE }
public class GameBattleControllerScript : MonoBehaviour 
{
    public BattleState state;
    public GameObject playerOptions;
    public Transform playerObject;  // O transform do jogador
    public Transform enemyObject;   // O transform do inimigo
    private Vector2 playerStartPosition;     // Guarda a posição inicial do jogador
    public Vector2 enemyStartPosition;     // Guarda a posição inicial do inimigo
    public float moveSpeed = 25f;       // Velocidade do movimento
    public float attackWaitTime = 0.5f; // Tempo de espera antes de voltar

    public TMP_Text BattleInput; // Recebe pela interface da unity o input text da tela

    public static GameBattleControllerScript instance;
    void Start()
    {
        instance = this;
        state = BattleState.START;
        StartCoroutine(SetupBattle()); 
        playerStartPosition = playerObject.position; // Guarda a posição inicial do player
        
        enemyStartPosition = enemyObject.position; // Guarda a posição inicial do player
    }

    IEnumerator SetupBattle()
    {
        // Configuração inicial do combate (spawn de personagens, UI, etc.)
        yield return new WaitForSeconds(1f);
        PlayerTurn();
    }

    void PlayerTurn()
    {
        playerOptions.SetActive(true);
        state = BattleState.PLAYER_TURN;
        
        BattleInput.text = "Escolha uma ação";
    }


    public void EnemyTurn()
    {
        state = BattleState.ENEMY_TURN;
        
        BattleInput.text += "\nTurno do inimigo";

        StartCoroutine(EnemyAttack());
    }

    IEnumerator EnemyAttack()
    {
        yield return new WaitForSeconds(attackWaitTime + .5f);
        BattleInput.text += "\nO inimigo atacou!";
        // Simula o ataque (adiciona um delay antes de voltar)

        yield return StartCoroutine(MoveToPosition(enemyObject, new Vector2(playerObject.position.x + 2, playerObject.position.y)));

        yield return new WaitForSeconds(attackWaitTime);

        yield return StartCoroutine(MoveToPosition(enemyObject, enemyStartPosition));
        // Espera um frame para garantir que a interface seja atualizada antes de continuar
        yield return new WaitForSeconds(1f);
        
        PlayerTurn();
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
