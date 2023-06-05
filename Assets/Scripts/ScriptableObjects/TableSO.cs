using UnityEngine;

namespace ScriptableObjects
{
	[CreateAssetMenu(fileName = "Table", menuName = "Pişti/Table", order = 2)]
	public class TableSO : ScriptableObject
	{
		public int MinBet;
		public int MaxBet;
		public string TableName;
	}
}