using ScriptableObjects;
using UI.Lobby;
using UnityEngine;

namespace Managers
{
	public class TablesManager : MonoBehaviour
	{
		[SerializeField] private TableSO[] tableSOs;
		[SerializeField] private TableUI tablePrefab;

		[Space]
		[SerializeField] private RectTransform content;

		public CreateTable CreateTablePanel;

		private void Awake()
		{
			CreateTablePanel = GetComponentInChildren<CreateTable>();
		}

		private void Start()
		{
			Setup();
		}

		private void Setup()
		{
			foreach (var tableSO in tableSOs)
			{
				var table = Instantiate(tablePrefab, content);
				table.Setup(tableSO);
				table.OnCreateTable += OnCreateTable;
			}
		}

		private void OnCreateTable(TableSO tableSO)
		{
			CreateTablePanel.Setup(tableSO);
			CreateTablePanel.Show();
		}
	}
}