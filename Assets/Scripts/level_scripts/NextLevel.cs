using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

	[SerializeField]private string nextScene;//drop the scene name in here

	public void NextArea()//shorter load, for levels in the same region
	{
		GameManager.Instance.LoadLevelWithFade(nextScene);
	}

	public void NextRegion()//used for da big loading. Moving between regions
	{
		GameManager.Instance.LoadLevelWithFade(nextScene);
	}
}
