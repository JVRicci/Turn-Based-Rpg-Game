// using System.Collections;
// using TMPro;
// using UnityEngine;
// using UnityEngine.UI;

// public class PlayerBattleScript : MonoBehaviour{

//     public BattleState state;
//     public Transform playerObject;  // O transform do jogador
//     public Transform enemyObject;   // O transform do inimigo
//     private Vector2 playerStartPosition;     // Guarda a posição inicial do jogador
//     private Vector2 enemyStartPosition;     // Guarda a posição inicial do inimigo
//     public float moveSpeed = 25f;       // Velocidade do movimento
//     public float attackWaitTime = 0.5f; // Tempo de espera antes de voltar

//     public TMP_Text BattleInput; // Recebe pela interface da unity o input text da tela
//     IEnumerator PlayerAttack()
//     {
//         // Aplica dano ao inimigo
//         BattleInput.text += "\nO jogador atacou!";

//          // Move o jogador até o inimigo
//         //  O vector2 eu utilizei para que o player não chhegasse totalmente até o inimigo
//         yield return StartCoroutine(MoveToPosition(playerObject, new Vector2(enemyObject.position.x - 2, enemyObject.position.y)));

//         // Simula o ataque (adiciona um delay antes de voltar)
//         yield return new WaitForSeconds(attackWaitTime);

//         // Move o jogador de volta para a posição original
//         yield return StartCoroutine(MoveToPosition(playerObject, playerStartPosition));
    
//         yield return new WaitForSeconds(1f);

//         // Verifica se o inimigo foi derrotado
//         EnemyTurn();
//     }
// }