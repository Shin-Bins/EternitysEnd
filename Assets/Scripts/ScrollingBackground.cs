using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
	public GameObject spawnSection;

 void Update()
 {
	 transform.position += new Vector3(-3, 0, 0) * Time.deltaTime;
 }

 public void SpawnNext()
 {
	 Instantiate(spawnSection, new Vector3(66, 0,0), Quaternion.identity);
 }
}
