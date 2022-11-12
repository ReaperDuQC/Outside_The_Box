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
	[SerializeField] TextMeshProUGUI chapterScore;

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

	public void DebugChapterScore()
	{
		float score = UnityEngine.Random.Range(0, 101);
		scoreManager.AddChapterScore(score);
		DisplayChapterBook();
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
		while(timer < delais)
		{
			timer += Time.deltaTime;
			yield return null;
		}
		menuToOpen.SetActive(true);
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
				float zRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % (clockWise? 18.5f : -18.5f);
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
		chapterScore.text = scoreManager.GetLastChapterPercent().ToString();
	}
	public void DisplayScoreBook()
	{
		bookScore.text = scoreManager.GetBookPercent().ToString();
	}

	//IEnumerator RotateBook(GameObject book,int numberOfTurn, float angle, float speed)
	//{
	//    for (int i =0; i < numberOfTurn; i++)
	//    {
	//        while (bo)
	//        {

	//            yield return null;
	//        }
	//    }
	//}

	//private IEnumerator Fade(GameObject book, float fadeDuration, bool fadeIn)
	//{
	//    MeshRenderer rend = book.GetComponent<MeshRenderer>();
	//    Color initialColor = rend.material.color;
	//    Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);

	//    if (fadeIn)
	//    {
	//        rend.material.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);
	//        initialColor = rend.material.color;
	//        book.SetActive(true);
	//        targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 255f);
	//    }
	//    else
	//    {
	//        rend.material.color = new Color(rend.material.color.r, rend.material.color.g, rend.material.color.b, 255f);
	//        initialColor = rend.material.color;
	//        targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);
	//    }

	//    float elapsedTime = 0f;

	//    if (!fadeIn)
	//    {
	//        while (elapsedTime < fadeDuration)
	//        {
	//            elapsedTime += Time.deltaTime;
	//            rend.material.color = Color.Lerp(initialColor, targetColor, elapsedTime / fadeDuration);
	//            yield return null;
	//        }
	//    }

	//    if (!fadeIn)
	//    {
	//        book.SetActive(false);
	//    }
	//    else
	//    {
	//        menuToOpen.SetActive(true);
	//    }
	//}

}
