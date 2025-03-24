using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYER_TURN, ENEMY_TURN, WIN, LOSE }
public class GameBattleControllerScript : MonoBehaviour 
{
    public BattleState state;
    public GameObject playerOptions;
    public GameObject enemyOptions;
    public Transform playerObject;  // O transform do jogador
    public Transform enemyObject;   // O transform do inimigo
    private Vector2 playerStartPosition;     // Guarda a posição inicial do jogador
    public Vector2 enemyStartPosition;     // Guarda a posição inicial do inimigo
    public float moveSpeed = 25f;       // Velocidade do movimento
    public float attackWaitTime = 0.5f; // Tempo de espera antes de voltar

    public TMP_Text BattleInput; // Recebe pela interface da unity o input text da tela

    public static GameBattleControllerScript instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
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

    public void PlayerTurn()
    {
        playerOptions.SetActive(true);
        state = BattleState.PLAYER_TURN;
        
        BattleInput.text = "Escolha uma ação";
    }


    public void EnemyTurn()
    {
        state = BattleState.ENEMY_TURN;
        
        BattleInput.text += "\nTurno do inimigo";

        StartCoroutine(enemyObject.GetComponent<EnemyBattleScript>().EnemyAttack());
    }
}
