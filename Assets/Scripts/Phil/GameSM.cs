using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSM : MonoBehaviour
{
	MenuState menuState;
	GameState gameState;
	PauseState pauseState;
	ChapterEndState chapterEndState;
	BookEndState bookEndState;

	BaseState currentState;

	private void Awake()
	{
		menuState = new MenuState();
		gameState = new GameState();
		pauseState = new PauseState();
		chapterEndState = new ChapterEndState();
		bookEndState = new BookEndState();

		currentState = menuState;
	}

	public void ChangeState(BaseState newState)
	{
		currentState.Exit();
		currentState = newState;
		currentState.Enter();
	}

	private void Update()
	{
		currentState.Update();
	}
}
