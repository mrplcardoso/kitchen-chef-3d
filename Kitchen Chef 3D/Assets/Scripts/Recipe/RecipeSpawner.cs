using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.EasingEquations;
using Utility.Random;

public class RecipeSpawner : MonoBehaviour
{
  [SerializeField] RecipeBox prefab;
	public List<RecipeBox> boxList { get; private set; }
  public float witdhOffset;

  [SerializeField] RecipeDatabase database;
  public Recipe[] recipeList { get; private set; }

  public Queue<Recipe> recipeQueue { get; private set; }
  public Recipe currentRecipe { get { return recipeQueue.Peek(); } }
  public readonly int maxQueue = 5;

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

      //TODO: fazer intervalo de tempo diminuir em função do Time.deltaTime (escalado)
      yield return new WaitForSeconds(spawnInterval);

      for(int i = 0; i < boxList.Count; ++i)
      {
        float t = 0;
        Vector3 start = boxList[i].rectTransform.position;
        Vector3 end = boxList[i].rectTransform.position + Vector3.right * witdhOffset;
        while(t < 1.01f)
        {
          boxList[i].rectTransform.position = EasingVector3Equations.Linear(start, end, t);
          t += 5 * Time.deltaTime;
          yield return null;
        }
        boxList[i].rectTransform.position = end;
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
    }
  }
}