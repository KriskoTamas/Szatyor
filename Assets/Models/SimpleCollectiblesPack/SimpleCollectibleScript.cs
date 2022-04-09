using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SimpleCollectibleScript : MonoBehaviour {

	public enum CollectibleType {Coin, Gold, Diamond};
	public CollectibleType collectibleType;

	public bool rotate;
	public float rotationSpeed;
	public AudioClip collectSound;
	public GameObject collectEffect;

	private int GetAmountByType(CollectibleType type)
    {
		if (type == CollectibleType.Coin) return 5;
		else if (type == CollectibleType.Gold) return 10;
		else if (type == CollectibleType.Diamond) return 20;
		else return 0;
	}

	void Start()
	{
		
	}
	
	void Update()
	{
		if (rotate)
			transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
            Collect();
			Game.AddToScore(GetAmountByType(collectibleType));
        }
	}

	public void Collect()
	{
        /*if(collectSound)
			AudioSource.PlayClipAtPoint(collectSound, transform.position);
		if(collectEffect)
			Instantiate(collectEffect, transform.position, Quaternion.identity);*/

        //print(collectibleType);

        if (collectibleType == CollectibleType.Coin) {
			//Add in code here;
		}
		if (collectibleType == CollectibleType.Gold) {
			//Add in code here;
		}
		if (collectibleType == CollectibleType.Diamond) {
			//Add in code here;
		}

		Destroy(gameObject);
	}
}
