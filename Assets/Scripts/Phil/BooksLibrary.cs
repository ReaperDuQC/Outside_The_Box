using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Book
{
	public int clichePercent;
	public int grade;
	public int nbChapter;
	public string name;
}

public class BooksLibrary : MonoBehaviour
{
	List<Book> books = new List<Book>();

	public List<Book> Books
	{
		get { return books; }
	}

	private void Awake()
	{
		LoadLibrary();
	}
	void LoadLibrary()
	{
		int nbTotalBook = 0;
		if (PlayerPrefs.HasKey("BookCount"))
		{
			nbTotalBook = PlayerPrefs.GetInt("BookCount");
		}
		else
		{
			PlayerPrefs.SetInt("BookCount", 0);
		}

		if (nbTotalBook > 0)
		{
			for (int i = 0; i < nbTotalBook; i++)
			{
				Book savedBook = new Book();
				string savedKey = i.ToString();

				savedBook.clichePercent = PlayerPrefs.GetInt(savedKey + "_Percent");
				savedBook.grade = PlayerPrefs.GetInt(savedKey + "_Grade");
				savedBook.nbChapter = PlayerPrefs.GetInt(savedKey + "_NbChapter");
				savedBook.name = PlayerPrefs.GetString(savedKey + "_Name");

				books.Add(savedBook);
			}
		}
	}
	public void SaveBook(Book newBook)
	{
	    int nbBook = PlayerPrefs.GetInt("BookCount");
		string newKey = nbBook.ToString();

		PlayerPrefs.SetInt(newKey + "_Percent", newBook.clichePercent);
		PlayerPrefs.SetInt(newKey + "_Grade", newBook.grade);
		PlayerPrefs.SetInt(newKey + "_NbChapter", newBook.nbChapter);
		PlayerPrefs.SetString(newKey + "_Name", newBook.name);

		nbBook++;
		PlayerPrefs.SetInt("BookCount", nbBook);
	}
}
