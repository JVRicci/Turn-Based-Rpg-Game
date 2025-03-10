using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYER_TURN, ENEMY_TURN, WIN, LOSE }
public interface BattleText{
    void Text();
}
public class GameBattleControllerScript : MonoBehaviour 
{

    public BattleState state;
    public Transform playerTransform;  // O transform do jogador
    public Transform enemyTransform;   // O transform do inimigo
    private Vector2 playerStartPosition;     // Guarda a posição inicial do jogador
    private Vector2 enemyStartPosition;     // Guarda a posição inicial do inimigo
    public float moveSpeed = 25f;       // Velocidade do movimento
    public float attackWaitTime = 0.5f; // Tempo de espera antes de voltar

    public TMP_Text BattleInput; // Recebe pela interface da unity o input text da tela
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle()); 
        playerStartPosition = playerTransform.position; // Guarda a posição inicial do player
        enemyStartPosition = enemyTransform.position; // Guarda a posição inicial do player
    }

    IEnumerator SetupBattle()
    {
        // Configuração inicial do combate (spawn de personagens, UI, etc.)
        yield return new WaitForSeconds(1f);
        PlayerTurn();
    }

    void PlayerTurn()
    {
        state = BattleState.PLAYER_TURN;
        
        BattleInput.text = "Escolha uma ação";
        // Espera um frame para garantir que a interface seja atualizada antes de rolar
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYER_TURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        // Aplica dano ao inimigo
        BattleInput.text += "\nO jogador atacou!";

         // Move o jogador até o inimigo
        yield return StartCoroutine(MoveToPosition(playerTransform, enemyTransform.position));

        // Simula o ataque (adiciona um delay antes de voltar)
        yield return new WaitForSeconds(attackWaitTime);

        // Move o jogador de volta para a posição original
        yield return StartCoroutine(MoveToPosition(playerTransform, playerStartPosition));
    
        yield return new WaitForSeconds(1f);

        // Verifica se o inimigo foi derrotado
        EnemyTurn();
    }

    void EnemyTurn()
    {
        state = BattleState.ENEMY_TURN;
        
        BattleInput.text += "\nTurno do inimigo";

        StartCoroutine(EnemyAttack());
    }

    IEnumerator EnemyAttack()
    {
        BattleInput.text += "\nO inimigo atacou!";

        yield return StartCoroutine(MoveToPosition(enemyTransform, playerTransform.position));

        yield return new WaitForSeconds(attackWaitTime);

        yield return StartCoroutine(MoveToPosition(enemyTransform, enemyStartPosition));
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
