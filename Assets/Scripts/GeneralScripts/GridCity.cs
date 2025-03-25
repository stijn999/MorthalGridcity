// Version 2023
//  (Updates: supports different root positions)
using UnityEngine;

namespace Demo {
	public class GridCity : MonoBehaviour {
		public int rows = 10;
		public int columns = 10;
		public int rowWidth = 10;
		public int columnWidth = 10;
		public GameObject[] buildingPrefabs;

		public float buildDelaySeconds = 0.1f;


		void Start() {
			Generate();
		}

		void Update() {
			if (Input.GetKeyDown(KeyCode.G)) {
				DestroyChildren();
				Generate();
			}
		}

		void DestroyChildren() {
			for (int i = 0; i<transform.childCount; i++) {
				Destroy(transform.GetChild(i).gameObject);
			}
		}

		void Generate() {

			for (int row = 0; row<rows; row++) {
				for (int col = 0; col<columns; col++) {

					int randomInt = Random.Range(1, 10);
					float randomFloat = Random.Range(0.0f, 45.0f); 

					// Create a new building, chosen randomly from the prefabs:
					int buildingIndex = Random.Range(0, buildingPrefabs.Length);
					GameObject newBuilding = Instantiate(buildingPrefabs[buildingIndex], transform);

					// Place it in the grid:
					newBuilding.transform.localPosition = new Vector3(col * columnWidth+randomInt, 0, row* rowWidth+randomInt);
					// Rotate building randomly
					newBuilding.transform.Rotate(0.0f, randomFloat, 0.0f);

					// If the building has a Shape (grammar) component, launch the grammar:
					Shape shape = newBuilding.GetComponent<Shape>();
					if (shape!=null) {
						shape.Generate(buildDelaySeconds);
					}
				}
			}
		}
	}
}