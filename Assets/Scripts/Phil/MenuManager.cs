using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
	[SerializeField] BookScoreManager scoreManager;
	[SerializeField] List<GameObject> cameras = new List<GameObject>();
	[SerializeField] float bookFadeInSpeed;
	[SerializeField] float bookFadeOutSpeed;
	[SerializeField] float bookRotDuration;
	[SerializeField] Transform trainMenuFront;
	[SerializeField] Transform trainMenuBack;
	[SerializeField] TextMeshProUGUI bookScore;
	[SerializeField] TextMeshProUGUI bookTitle;
	[SerializeField] TextMeshProUGUI bookDescription;
	[SerializeField] TextMeshProUGUI chapterScore;
	[SerializeField] TextMeshProUGUI chapterTitle;
	[SerializeField] TextMeshProUGUI pageL;
	[SerializeField] TextMeshProUGUI pageR;
	[TextArea]
	[SerializeField] string pageContent;
	[SerializeField] float delaisLetter;
	[SerializeField] float delaisPage;
	[SerializeField] float speedFadeOutText;
	[SerializeField] Color textNormalColor;
	[SerializeField] Color textClicheColor;
	[SerializeField] GameObject[] buttonsChapterEnd;
	[SerializeField] int minChapter;
	[SerializeField] int maxChapter;
	[SerializeField] GameObject arm;
	[SerializeField] Transform armHiding;
	[SerializeField] Transform armStart;
	[SerializeField] Transform armLimitL;
	[SerializeField] Transform armLimitR;
	[SerializeField] Transform armLimitL2;
	[SerializeField] Transform armLimitR2;
	int nbWordChap;

	int camToClose;
	int newCam;
	public int CamToClose
	{
		set
		{
			camToClose = value;
		}
	}
	public int NewCam
	{
		set
		{
			newCam = value;
		}
	}

	GameObject bookFadeIn;
	GameObject bookFadeOut;
	public GameObject BookFadeIn
	{
		set
		{
			bookFadeIn = value;
		}
	}
	public GameObject BookFadeOut
	{
		set
		{
			bookFadeOut = value;
		}
	}

	GameObject menuToClosed;
	GameObject menuToOpen;
	public GameObject MenuToClosed
	{
		set
		{
			menuToClosed = value;
		}
	}
	public GameObject MenuToOpen
	{
		set
		{
			menuToOpen = value;
		}
	}

	GameObject bookToRotate;
	public GameObject BookToRotate
	{
		set
		{
			bookToRotate = value;
		}
	}

	int numberOfRot;
	public int NumberofRot
	{
		set
		{
			numberOfRot = value;
		}
	}

	float menuDelais;
	public float MenuDelais
	{
		set
		{
			menuDelais = value;
		}
	}
	private void Awake()
	{
		//ComputeNbWordPerChap();
	}
	void ComputeNbWordPerChap()
	{
		int nbSpace = 0;
		foreach (char c in pageContent)
		{
			if (c == ' ')
			{
				nbSpace++;
			}
		}
		nbSpace *= 6;

		nbWordChap = (pageContent.Length * 6) - nbSpace;
	}
	public void DebugChapterScore()
	{
		float score = UnityEngine.Random.Range(0, 101);
		scoreManager.AddChapterScore(score);
		//DisplayChapterBook();
	}

	public void SwitchCamera()
	{
		cameras[newCam].SetActive(true);
		cameras[camToClose].SetActive(false);
	}

	public void OpenMenuDelais()
	{
		StartCoroutine(OpenMenuRoutine(menuDelais));
	}
	IEnumerator OpenMenuRoutine(float delais)
	{
		float timer = 0;
		while (timer < delais)
		{
			timer += Time.deltaTime;
			yield return null;
		}
		menuToOpen.SetActive(true);
	}
	public void CloseMenuDelais()
	{
		StartCoroutine(CloseMenuRoutine(menuDelais));
	}
	IEnumerator CloseMenuRoutine(float delais)
	{
		float timer = 0;
		while (timer < delais)
		{
			timer += Time.deltaTime;
			yield return null;
		}
		menuToClosed.SetActive(false);
	}
	public void FadeBook()
	{
		StartCoroutine(FadeInOut(bookFadeIn, bookFadeOut, 1f));

		//StartCoroutine(Fade(bookFadeOut, bookFadeOutTime, false));
		//StartCoroutine(Fade(bookFadeIn, bookFadeInTime, true));
	}

	IEnumerator FadeInOut(GameObject bookIn, GameObject bookOut, float delais)
	{
		var material = bookIn.GetComponent<Renderer>().material;
		var material2 = bookOut.GetComponent<Renderer>().material;

		material.color = new Color(material.color.r, material.color.g, material.color.b, 0f);
		material2.color = new Color(material2.color.r, material2.color.g, material2.color.b, 1f);

		yield return new WaitForSeconds(delais);

		StartCoroutine(Fade(material, 1f, bookFadeInSpeed, true, bookIn));
		StartCoroutine(Fade(material2, 0f, bookFadeOutSpeed, false, bookOut));
	}

	IEnumerator Fade(Material mat, float targetAlpha, float fadeSpeed, bool fadeIn, GameObject book)
	{
		while (mat.color.a != targetAlpha)
		{
			var newAlpha = Mathf.MoveTowards(mat.color.a, targetAlpha, fadeSpeed * Time.deltaTime);
			mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, newAlpha);
			yield return null;
		}
		if (!fadeIn)
		{
			book.SetActive(false);
		}

	}

	public void RotateBook360()
	{
		StartCoroutine(Rotate(bookRotDuration, numberOfRot, bookToRotate.transform));
	}
	public void RotateBook()
	{
		StartCoroutine(Rotate(1f, 18.5f, bookToRotate.transform, true));
	}
	IEnumerator Rotate(float duration, int numberOfTurn, Transform book)
	{
		for (int i = 0; i < numberOfTurn; i++)
		{
			float startRotation = book.eulerAngles.z;
			float endRotation = startRotation - 360.0f;
			float t = 0.0f;
			while (t < duration)
			{
				t += Time.deltaTime;
				float zRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
				book.eulerAngles = new Vector3(book.eulerAngles.x, book.eulerAngles.y,
				zRotation);
				yield return null;
			}
		}
	}
	IEnumerator Rotate(float duration, float angle, Transform book, bool clockWise)
	{
		float startRotation = book.eulerAngles.z;
		float endRotation = startRotation - angle;
		float t = 0.0f;
		while (t < duration)
		{
			t += Time.deltaTime;
			float zRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % (clockWise ? 18.5f : -18.5f);
			book.eulerAngles = new Vector3(book.eulerAngles.x, book.eulerAngles.y,
				zRotation);
			yield return null;
		}
		book.eulerAngles = new Vector3(book.eulerAngles.x, book.eulerAngles.y, endRotation);
	}

	public void PublishBook()
	{
		scoreManager.PublishBook();
		StartCoroutine(PublishBookRoutine());
	}

	IEnumerator PublishBookRoutine()
	{
		yield return new WaitForSeconds(2f);

		StartCoroutine(CameraWaWa());

		yield return Rotate(1f, 2, bookToRotate.transform);
		//yield return new WaitForSeconds(1f);

		newCam = 1;
		camToClose = 0;
		SwitchCamera();

		yield return Rotate(0.5f, -18.5f, bookToRotate.transform, false);

		DisplayScoreBook();
	}

	IEnumerator CameraWaWa()
	{
		//int counter = 0;
		//while (counter < 2)
		//{
		//counter++;

		newCam = 3;
		camToClose = 1;
		SwitchCamera();
		yield return new WaitForSeconds(0.8f);
		newCam = 1;
		camToClose = 3;
		SwitchCamera();
		//yield return new WaitForSeconds(0.5f);

		//yield return null;
		//}
	}

	public void MoveTrainMenu()
	{
		StartCoroutine(MoveTrainMenuRoutine(-trainMenuFront.right, 4f, trainMenuFront));
		StartCoroutine(MoveTrainMenuRoutine(-trainMenuBack.right, 4f, trainMenuBack));
	}

	IEnumerator MoveTrainMenuRoutine(Vector3 dir, float timer, Transform train)
	{
		float time = 0;
		while (time < timer)
		{
			time += Time.deltaTime;
			train.position += dir * Time.deltaTime * 3f;
			yield return null;
		}
	}

	public void DisplayChapterBook()
	{
		chapterTitle.text = "Chapter " + scoreManager.chapterScores.Count.ToString();
		chapterScore.text = "Cliché : " + scoreManager.GetLastChapterPercent().ToString() + "%";
	}
	public void DisplayScoreBook()
	{
		bookScore.text = "Cliché : " + scoreManager.GetBookPercent().ToString() + "%";
		string[] strs = GetTitle();
		bookTitle.text = strs[0];
		bookDescription.text = strs[1];
	}

	string[] GetTitle()
	{
		string title = "";
		string description = "";

		switch (scoreManager.GetBookGrade())
		{
			case 1:
				title = "Plagiarism Lawsuit";
				break;
			case 2:
				title = "Erotic Fan Fiction";
				break;
			case 3:
				title = "Uninspired";
				break;
			case 4:
				title = "Unremarkable";
				break;
			case 5:
				title = "A Missed Opportunity";
				break;
			case 6:
				title = "Actually Not That Bad";
				break;
			case 7:
				title = "Store Favorites";
				break;
			case 8:
				title = "Best Seller";
				break;
			case 9:
				title = "Instant Classic";
				break;
			case 10:
				title = "Masterpiece";
				break;
			case 11:
				title = "The Goat";
				break;
		}

		return new string[] { title, description };
	}

	public void GoToMain()
	{ }
	public void GoToRules()
	{

	}
	public void StartMiniGame()
	{ }
	public void PauseGame()
	{ }

	public void ChapterEnd()
	{ }
	public void BookEnd()
	{ }

	public void WriteChapter()
	{
		StartCoroutine(WriteChapterRoutine());
	}

	public IEnumerator WriteChapterRoutine()
	{
		chapterScore.text = "";
		chapterTitle.text = "";
		buttonsChapterEnd[0].gameObject.SetActive(false);
		buttonsChapterEnd[1].gameObject.SetActive(false);
		pageL.color = textNormalColor;
		pageR.color = textNormalColor;
		int clichePercent = scoreManager.GetLastChapterPercent();
		int redPage = Mathf.CeilToInt((clichePercent * 6) / 100);
		if (clichePercent == 0) { redPage = 0; }
		int pageCount = 0;
		int firstRed = 6 - redPage;
		bool lIsRed = false;
		bool rIsRed = false;
		yield return new WaitForSeconds(1f);
		StartCoroutine(MoveArm());
		yield return new WaitForSeconds(1f);

		for (int i = 0; i < 2; i++)
		{
			pageCount++;
			if (!lIsRed && pageCount >= firstRed)
			{
				pageL.color = textClicheColor;
				pageR.color = textClicheColor;
				lIsRed = true;
			}
			yield return WritePage(pageContent, delaisLetter, pageL);

			yield return new WaitForSeconds(delaisPage);
			pageCount++;
			if (!rIsRed && pageCount >= firstRed)
			{
				pageR.color = textClicheColor;
				rIsRed = true;
			}
			yield return WritePage(pageContent, delaisLetter, pageR);

			yield return ErasePage(pageR);
			yield return ErasePage(pageL);
		}

		pageCount++;
		if (!lIsRed && pageCount >= firstRed)
		{
			pageL.color = textClicheColor;
			pageR.color = textClicheColor;
			lIsRed = true;
		}
		yield return WritePage(pageContent, delaisLetter, pageL);

		yield return new WaitForSeconds(delaisPage);
		pageCount++;
		if (!rIsRed)
		{
			if (clichePercent != 0)
			{
				pageR.color = textClicheColor;
				rIsRed = true;
			}
		}
		yield return WritePage(pageContent, delaisLetter, pageR);
		
		DisplayChapterBook();
		int ch = scoreManager.chapterScores.Count;
		if (ch >= minChapter)
		{
			buttonsChapterEnd[1].gameObject.SetActive(true);
		}
		if (ch < maxChapter)
		{
			buttonsChapterEnd[0].gameObject.SetActive(true);
		}
	}

	int IncrementPageCounter(int pageCounter, TextMeshProUGUI curPage, int redPage)
	{
		pageCounter++;

		if (pageCounter == (6 - redPage))
		{
			curPage.color = textClicheColor;
		}

		return pageCounter;
	}

	IEnumerator WritePage(string content, float delaisLetter, TextMeshProUGUI page)
	{
		//int nbLetter = content.Length;

		//foreach (char letter in content)
		//{
		//	//if (letter != ' ')
		//	//{
		//	//	yield return new WaitForSeconds(delaisLetter);
		//	//}

		//	page.text += letter;

		//    yield return null;
		//}

		for (int i = 0; i < content.Length - 10; i += 10)
		{
			for (int j = i; j < i + 10; j++)
			{
				page.text += content[j];
			}

			yield return new WaitForSeconds(delaisLetter);
		}
	}

	public IEnumerator ErasePage(TextMeshProUGUI page)
	{
		float alpha = 1f;
		while (alpha >= 0)
		{
			alpha -= Time.deltaTime * speedFadeOutText;
			page.alpha = alpha;
			yield return null;
		}
		page.text = "";
		page.alpha = 1f;
	}
	public void ErasePages()
	{
		StartCoroutine(ErasePagesRoutine());
	}
	IEnumerator ErasePagesRoutine()
	{
		yield return ErasePage(pageR);
		yield return ErasePage(pageL);
	}
	public void PlaceArm()
	{
		StartCoroutine(PlaceArmRoutine());
	}
	IEnumerator PlaceArmRoutine()
	{
		Vector3 start = arm.transform.position;
		Vector3 end = armStart.position;
		float timer = 0f;
		float duration = 1f;
		while (timer <= duration)
		{
			timer += Time.deltaTime;
			arm.transform.position = Vector3.Lerp(start, end, timer / duration);
			yield return null;
		}
	}
	public void HideArm()
	{
		StartCoroutine(HideArmRoutine());
	}
	IEnumerator HideArmRoutine()
	{
		Vector3 end = armHiding.position;
		Vector3 start = arm.transform.position;
		float timer = 0f;
		float duration = 1f;
		while (timer <= duration)
		{
			timer += Time.deltaTime;
			arm.transform.position = Vector3.Lerp(start, end, timer / duration);
			yield return null;
		}
	}
	IEnumerator MoveArm()
	{
		yield return PlaceArmRoutine();

		for (int j =0; j < 3; j++)
		{
			for (int i = 0; i < 4; i++)
			{
				Vector3 end = armLimitL.position + (new Vector3(0, -2f, 0) * i);
				Vector3 start = armLimitR.position + (new Vector3(0, -2f, 0) * i);
				float timer = 0f;
				float duration = 0.18f;
				while (timer <= duration)
				{
					timer += Time.deltaTime;
					arm.transform.position = Vector3.Lerp(start, end, timer / duration);
					yield return null;
				}
			}
			Vector3 endd = armLimitL2.position;
			Vector3 startt = armLimitR2.position;
			float timerr = 0f;
			float durationn = 0.35f;
			while (timerr <= durationn)
			{
				timerr += Time.deltaTime;
				arm.transform.position = Vector3.Lerp(startt, endd, timerr / durationn);
				yield return null;
			}
			for (int i = 0; i < 4; i++)
			{
				Vector3 end = armLimitL2.position + (new Vector3(0, -2f, 0) * i);
				Vector3 start = armLimitR2.position + (new Vector3(0, -2f, 0) * i);
				float timer = 0f;
				float duration = 0.18f;
				while (timer <= duration)
				{
					timer += Time.deltaTime;
					arm.transform.position = Vector3.Lerp(start, end, timer / duration);
					yield return null;
				}
			}

			if (j != 2)
			{
				Vector3 end = armLimitL.position;
				Vector3 start = arm.transform.position;
				float timer = 0f;
				float duration = 0.3f;
				while (timer <= duration)
				{
					timer += Time.deltaTime;
					arm.transform.position = Vector3.Lerp(start, end, timer / duration);
					yield return null;
				}
			}

			yield return null;
		}

		StartCoroutine(HideArmRoutine());
	}

}
