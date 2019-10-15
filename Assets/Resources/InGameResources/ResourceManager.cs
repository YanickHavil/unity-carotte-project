using UnityEngine;
using System.Collections;

public class ResourceManager : MonoBehaviour {



	public int nbResource = 0; // nombre de resources actuels 

	public int nbResourceMax = 10; // récolte max
	//Pour le nombre de stack maximum sur stockpile
	public int maxStockPile = 50;
	public TypeResource type;
	public int secondTogrow = 60;

    public float ratioHungerFood = 1.0f;

	public Sprite spriteGrow; 
	public Sprite spriteHalf;
	public Sprite spriteEmpty; 

	public bool autoTaskTake = false;
    GameObject gbjTaskEat = null;

	//Possibilité de repousser ?
	//Permet de savoir si le végétal ou autre peut repousser ses fruits ou autres.
	//Cela permet de savoir si on peut collecter au lieu de couper (détruire la ressource)
	public bool regrow;
	public int difficultyCollect = 1;
	public GameObject regrowableResource;

	//Possibilité de détérioration ?
	//Attribut qui servirai a savoir si un objet est sur le sol également car (quasi) tout objet au sol se détériore
	//Si true = objet au sol
	//Si false = arbre ou autre ressources non pourrissable.
	public bool spoilage;

	int compteur = 0;
	// Use this for initialization
	void Start () {
		if (regrow) {
			InvokeRepeating ("resourceManager", 1, 1);
		}
		if (autoTaskTake) {
			Debug.Log ("auto task");
			GameObject.Find ("Player").GetComponent<Player>().GetComponent<TaskManager>().addTask(new Task (gameObject,TaskEnum.TAKE));

		}
        else if(gbjTaskEat != null)
        {
            Debug.Log("tache mangé");
            gbjTaskEat.GetComponent<WorkManager>().attributeTask(new Task(gameObject, TaskEnum.EAT));

        }

	}
	
	// Update is called once per frame
	void Update () {
		

	}
	void resourceManager(){
		if (nbResource < nbResourceMax) {
			compteur++;
		}
		if (compteur == secondTogrow / 2 ) {
			if (spriteHalf != null) {
				changeSprite (spriteHalf);

			}
		}
		if (compteur == secondTogrow && nbResource == 0) {
			nbResource = nbResourceMax;
			compteur = 0;
			if (spriteGrow != null) {
				changeSprite (spriteGrow);
			}
		}
	}
    //pas utilisé encore
	public int collectResource(int n){
		int food;
		if (n > nbResource) {
			food = nbResource;
			nbResource = 0;

		} else {
			food = n;
			nbResource -= n;
		}


		return food;

	}
    // Le gameobject est celui qui va collecter les resource
	public void collectResource(GameObject gbj){

		int food = nbResource;
		nbResource = 0;
		if (spriteEmpty != null) {
			changeSprite (spriteEmpty);
		}


		Tile t = gameObject.GetComponent<Attributes> ().getNearEmptyTile ();
		GameObject newGbj;

		Vector3 v3 = new Vector3 (t.x + 0.5f, 1.2f, t.y + 0.5f);
		newGbj = Instantiate (regrowableResource as GameObject, v3 , Quaternion.Euler (new Vector3 (90, 0, 0))) as GameObject;
		newGbj.transform.position = v3;
		t.staticEntity = newGbj;

        if(gbj == null)
        {
            newGbj.GetComponent<ResourceManager>().autoTaskTake = true;
        }
        else
        {
            newGbj.GetComponent<ResourceManager>().gbjTaskEat = gbj;
            
        }


	}


	//retourne false si tout n'a pas été posé
	public bool addStackToStock(GameObject gbj){

		ResourceManager resourceM = gbj.GetComponent<ResourceManager> ();
		if (resourceM.type == this.type) {
			

			if (resourceM.nbResource + this.nbResource > maxStockPile) {
				this.nbResource += resourceM.nbResource;
				return false;

			} else {
				this.nbResource += resourceM.nbResource;
				return true;
			}
		}
		return false;
	}

	public TypeResource getTypeResource()
	{
		return type;
	}

	public bool isEmpty(){
		if (nbResource == 0)
			return true;
		return false;
	}

	void changeSprite(Sprite s){
		gameObject.GetComponent<SpriteRenderer> ().sprite = s;
	}
}
