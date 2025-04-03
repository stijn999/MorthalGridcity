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

					
					int randomInt = Random.Range(1, 15);
					Vector3 position = new Vector3(col * columnWidth + randomInt, 0, row * rowWidth + randomInt);
					float randomFloat = Random.Range(0.0f, 90.0f);


					Collider[] overlaps = Physics.OverlapBox(position + new Vector3(10,-5,10), new Vector3(50, -1, 50));
					if (overlaps.Length > 0) continue;

					////terrain raycast
					RaycastHit hitinfo;
					if (Physics.Raycast(position + new Vector3(0, 50, 0), Vector3.down, out hitinfo, 51))
					{
						position.y = hitinfo.point.y;
					}
					if (position.y < 2) continue;

					// Create a new building, chosen randomly from the prefabs:
					int buildingIndex = Random.Range(0, buildingPrefabs.Length);
					GameObject newBuilding = Instantiate(buildingPrefabs[buildingIndex], transform);

					// Place it in the grid:
					newBuilding.transform.localPosition = position;
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