using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Random;

public class RecipeSpawner : MonoBehaviour
{
  [SerializeField] RecipeBox prefab;
	public List<RecipeBox> boxList { get; private set; }
  public float witdhOffset;

  [SerializeField] RecipeDatabase database;
  public Recipe[] recipeList { get; private set; }

  public Queue<Recipe> recipeQueue { get; private set; }
  public Recipe currentRecipe {get { return recipeQueue.Peek(); } }
  public readonly int maxQueue = 7;

  Transform spawnSpot;
  public float spawnInterval;
  bool spawn, pause;

	private void Awake()
	{
		boxList = new List<RecipeBox>();
		recipeList = database.LoadRecipes();
    recipeQueue = new Queue<Recipe>();

    spawnSpot = transform.GetChild(0);
    spawn = pause = false;
	}

	private void Start()
	{
    spawn = true;
    StartCoroutine(Spawn());
	}

	IEnumerator Spawn()
  {
    while(spawn)
    {
      if (pause || recipeQueue.Count >= maxQueue) { yield return null; continue; }

      for(int i = 0; i < boxList.Count; ++i)
      {
        boxList[i].rectTransform.position += Vector3.right * witdhOffset;
        yield return null;
      }

      int r = RandomStream.NextInt(0, recipeList.Length);
      Recipe recipe = recipeList[r];
      string description = "";
			for (int i = 0; i < recipe.ingredients.Count; ++i)
			{
				description += recipe.ingredients[i].amount + " + " + recipe.ingredients[i].ingredient.ToString()
					+ "(" + recipe.ingredients[i].preparation + ")";

        if(i < recipe.ingredients.Count - 1) { description += " + "; }
			}
			
      RecipeBox next = Instantiate(prefab, spawnSpot.position, Quaternion.identity, transform);
      next.recipe = recipe.name;
      next.description = description;

      recipeQueue.Enqueue(recipe);
      boxList.Add(next);
      yield return new WaitForSeconds(spawnInterval);
    }
  }
}