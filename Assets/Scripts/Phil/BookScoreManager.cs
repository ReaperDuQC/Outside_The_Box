using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Score
{
	public int clichePercent;
	public int grade;
}

public class BookScoreManager : MonoBehaviour
{
	[SerializeField] BooksLibrary library;

	List<Score> chapterScores = new List<Score>();
	Book book;

	bool published = false;
	int totalPercent = 0;
	int finalGrade = 0;
	string bookName = "";
	
	public string BookName
	{
		set { bookName = value; }
		get { return bookName; }
	}

	

	public void AddChapterScore(float clichePercent) // score = cliche percentage
	{
		if (clichePercent <= 0.5f && clichePercent > 0 )
		{
			clichePercent = 0.6f;
		}

		int clichePrecentInt = (int)Mathf.Round(clichePercent);
		int grade = 0;

		switch (clichePrecentInt)
		{
			case > 90:
				grade = 1;
				break;
			case > 75:
				grade = 2;
				break;
			case > 50:
				grade = 3;
				break;
			case > 25:
				grade = 4;
				break;
			case > 5:
				grade = 5;
				break;
			case > 0:
				grade = 6;
				break;
			case  0:
				grade = 7;
				break;
		}

		Score newScore = new Score();
		newScore.clichePercent = clichePrecentInt;
		newScore.grade = grade;
		chapterScores.Add(newScore);
	}

	public int GetLastChapterGrade()
	{
		if (chapterScores.Count == 0)
		{
			return 0;
			Debug.LogError("chapterScore is Empty");
		}
		return chapterScores[chapterScores.Count - 1].grade;
	}
	public int GetLastChapterPercent()
	{
		if (chapterScores.Count == 0)
		{
			return 101;
			Debug.LogError("chapterScore is Empty");
		}
		return chapterScores[chapterScores.Count - 1].clichePercent;
	}
	public int GetBookGrade()
	{
		if (!published)
		{
			return 0;
			Debug.LogError("book not published");
		}
		return book.grade;
	}
	public int GetBookPercent()
	{
		if (!published)
		{
			return 101;
			Debug.LogError("book not published");
		}
		return book.clichePercent;
	}

	int ComputeFinalScore()
	{
		float total = 0;
		foreach (Score sc in chapterScores)
		{
			total += sc.clichePercent;
		}
		float finalScoreF = total / chapterScores.Count;

		int finalScore = (int)Mathf.Round(finalScoreF);
		return finalScore;
	}

	int ComputeFinalGrade(int finalScore)
	{
		int grade = 0;

		switch (finalScore)
		{
			case > 85:
				grade = 1;
				break;
			case > 70:
				grade = 2;
				break;
			case > 50:
				grade = 3;
				break;
			case > 40:
				grade = 4;
				break;
			case > 30:
				grade = 5;
				break;
			case > 20:
				grade = 6;
				break;
			case > 15:
				grade = 7;
				break;
			case > 10:
				grade = 8;
				break;
			case > 5:
				grade = 9;
				break;
			case > 0:
				grade = 10;
				break;
			case 0:
				grade = 11;
				break;
		}

		return grade;
	}

	public void PublishBook()
	{
		totalPercent = ComputeFinalScore();
		finalGrade = ComputeFinalGrade(totalPercent);

		book = new Book();
		book.clichePercent = totalPercent;
		book.grade = finalGrade;
		book.nbChapter = chapterScores.Count;
		book.name = bookName;

		published = true;
	}

	public void SaveBook()
	{
		if (published)
		{
			library.SaveBook(book);
		}
		else { Debug.LogError("Trying to save a unpublished book"); }
	}
}
